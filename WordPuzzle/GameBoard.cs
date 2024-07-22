//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using WordPuzzle.Classes;
using System.IO;
using System.Threading;

namespace WordPuzzle
{
    public partial class GameBoard : Form
    {
        Responsive ResponsiveObj;
        GameEngine TheGameEngine;

        int SizeFactor;
        int CalibrationFactor;
        int MouseDrawYCalibrationFactor;

        private int MAX_WORDS = Convert.ToInt16(ConfigurationManager.AppSettings["MAX_WORDS"]);
        private string CurrentCategory;
        private List<string> Categories = new List<string>();
        CountDownClock Clock;        
        private Stack<Point> Points = new Stack<Point>();
        private const int MAX_SECONDS = 720;
        List<Point> FailedRectangles = new List<Point>();
        private int GridSize = Convert.ToInt16(ConfigurationManager.AppSettings["GRID_SIZE"]);
        string[] Words;

        public GameBoard(string[] WordsParam, string SelectedCategory, List<GameEngine.Direction> ChosenDirections)
        {
            try
            {
                InitializeComponent();
                InitializeGlobals(WordsParam);

                TheGameEngine = new GameEngine(WordsParam, SelectedCategory, GridSize, MAX_SECONDS, ChosenDirections);
                TheGameEngine.InitializeBoard(GridSize);

                ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
                ResponsiveObj.SetMultiplicationFactor();

                SizeFactor = ResponsiveObj.GetMetrics(40);
                CalibrationFactor = ResponsiveObj.GetMetrics(10);   // Calibration adjustment for the X-coordinate of the mouse, and also for placement of letters in the middle of boxes.
                MouseDrawYCalibrationFactor = 80;   // Calibration adjustment for the Y-coordinate of the mouse.

                StartPosition = FormStartPosition.CenterScreen;
                CurrentCategory = SelectedCategory;
                Focus();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'GameBoard' method of 'GameBoard' form. Error msg: " + Ex.Message);
            }
        }

        private void InitializeGlobals(string[] WordsParam)
        {
            try
            {                
                foreach (string Str in WordsParam)
                    WordsListView.Items.Add(new ListViewItem(Str));
                Words = WordsParam;
                Clock = new CountDownClock(MAX_SECONDS);
                TimerLabel.Text = Clock.TimeLeft + " seconds left.";
                GameTimer.Start();
                StatusLabel.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'InitializeGlobals' method of 'GameBoard' form. Error msg: " + Ex.Message);
            }
        }

        private void GameBoard_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));

                ColourCells(TheGameEngine.ColouredRectangles, Color.LightBlue);
                if (FailedRectangles.Count > 0) ColourCells(FailedRectangles, Color.ForestGreen);

                // Draw horizontal lines.
                for (int i = 0; i <= GridSize; i++)
                    e.Graphics.DrawLine(pen, SizeFactor, (i + 1) * SizeFactor, GridSize * SizeFactor + SizeFactor, (i + 1) * SizeFactor);

                // Draw vertical lines.
                for (int i = 0; i <= GridSize; i++)
                    e.Graphics.DrawLine(pen, (i + 1) * SizeFactor, SizeFactor, (i + 1) * SizeFactor, GridSize * SizeFactor + SizeFactor);

                MapArrayToGameBoard();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'GameBoard_Paint' method of 'GameBoard' form. Error msg: " + Ex.Message);
            }
        }

        private void MapArrayToGameBoard()
        {
            // https://msdn.microsoft.com/en-us/library/9why95hd%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            Graphics formGraphics = CreateGraphics();
            Font drawFont = new Font("Arial", ResponsiveObj.GetMetrics(16));
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            string CharacterToMap;

            try
            {
                for (int i = 0; i < GridSize; i++)
                    for (int j = 0; j < GridSize; j++)
                    {
                        if (TheGameEngine.WORDS_IN_BOARD[i, j] != '\0')
                        {
                            CharacterToMap = "" + TheGameEngine.WORDS_IN_BOARD[i, j]; // "" is needed as a means for conversion of character to string.
                            formGraphics.DrawString(CharacterToMap, drawFont, drawBrush, (i + 1) * SizeFactor + CalibrationFactor, (j + 1) * SizeFactor + CalibrationFactor);
                        }
                    }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'MapArrayToGameBoard' method of 'GameBoard' form. Error msg: " + Ex.Message);
            }
            finally
            {
                drawFont.Dispose();
                drawBrush.Dispose();
                formGraphics.Dispose();
            }
        }

        // http://stackoverflow.com/questions/18424586/drawing-a-line-while-moving-the-mouse-and-holding-the-right-click-button
        // http://stackoverflow.com/questions/23424558/how-to-draw-straight-line-to-the-mouse-coordinates
        private void GameBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Points.Clear();
                Points.Push(e.Location);
            }
        }

        private void GameBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (Points.Count == 1) return; // This was a doble click, no dragging, hence return.

            string TheWordIntended = "";
            TheGameEngine.CheckValidityAndUpdateScore(Points, SizeFactor, ref TheWordIntended, Clock.TimeLeft);
            
            if (TheGameEngine.StatusForDisplay.Equals("Bingo!!!"))
            {
                WordsListView.Items[WordsListView.FindItemWithText(TheWordIntended).Index].BackColor = Color.Gray;
                ScoreLabel.Text = "Score: " + TheGameEngine.CurrentScore;
            }

            StatusLabel.Text = TheGameEngine.StatusForDisplay;
            StatusTimer.Start();

            Invalidate();

            if (TheGameEngine.FoundAllWords())
            {
                GameTimer.Stop();
                if (TheGameEngine.SavedTopScoreSuccessfully())
                {
                    ScoreBoard ScoreBoardObj = new ScoreBoard(CurrentCategory, TheGameEngine.CurrentScore);
                    ScoreBoardObj.MdiParent = Parent.FindForm();
                    ScoreBoardObj.Show();

                    DisplayScoreDetails();
                }
            }
        }

        private void DisplayScoreDetails()
        {
            MemoryStream MS = new MemoryStream();
            CaptureGameScreen(ref MS);

            ScoreDetails ScoreDetailsObj = new ScoreDetails(TheGameEngine.WordPositions, GameEngine.REMAINING_TIME_BONUS_FACTOR, TheGameEngine.WORDS_FOUND, Words, Clock.TimeLeft, TheGameEngine.CurrentScore, ref MS);
            ScoreDetailsObj.MdiParent = Parent.FindForm();
            ScoreDetailsObj.Show();
        }

        // http://stackoverflow.com/questions/5049122/capture-the-screen-shot-using-net
        // http://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window
        private void CaptureGameScreen(ref MemoryStream MS)
        {
            using (Bitmap bitmap = new Bitmap(GridSize * SizeFactor + 2, GridSize * SizeFactor + 2))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (Screen.PrimaryScreen.Bounds.Width >= 1600)
                        g.CopyFromScreen(new Point(Bounds.Left + SizeFactor + ResponsiveObj.GetMetrics(10), Convert.ToInt16(Bounds.Top + (SizeFactor * 3.25))), Point.Empty, Bounds.Size);
                    else if (Screen.PrimaryScreen.Bounds.Width > 1200)
                        g.CopyFromScreen(new Point(Bounds.Left + SizeFactor + ResponsiveObj.GetMetrics(10), Convert.ToInt16(Bounds.Top + (SizeFactor * 3.85))), Point.Empty, Bounds.Size);
                    else if (Screen.PrimaryScreen.Bounds.Width > 1100)
                        g.CopyFromScreen(new Point(Bounds.Left + SizeFactor + ResponsiveObj.GetMetrics(10), Convert.ToInt16(Bounds.Top + (SizeFactor * 4.2))), Point.Empty, Bounds.Size);
                    else
                        g.CopyFromScreen(new Point(Bounds.Left + SizeFactor + ResponsiveObj.GetMetrics(10), Convert.ToInt16(Bounds.Top + (SizeFactor * 4.65))), Point.Empty, Bounds.Size);
                }
                bitmap.Save(MS, ImageFormat.Bmp);
            }
        }

        // https://msdn.microsoft.com/en-us/library/ztxk24yx(v=vs.110).aspx
        private void ColourCells(List<Point> Rect, Color RectColor)
        {
            try
            {
                SolidBrush myBrush = new SolidBrush(RectColor);
                Graphics formGraphics;
                formGraphics = CreateGraphics();

                for (int i = 0; i < Rect.Count; i++)
                    formGraphics.FillRectangle(myBrush, new Rectangle(Rect[i], new Size(SizeFactor, SizeFactor)));

                myBrush.Dispose();
                formGraphics.Dispose();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'ColourCells' method of 'GameBoard' form. Error msg: " + Ex.Message);
            }
        }

        private void GameBoard_Move(object sender, EventArgs e)
        {
            Points.Clear();
        }

        private void GameBoard_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (Points.Count > 1)
                        Points.Pop();
                    if (Points.Count > 0)
                        Points.Push(e.Location);

                    // Form top = X = Distance from top, left = Y = Distance from left.
                    // However mouse location X = Distance from left, Y = Distance from top.

                    // Need an adjustment to exact the location.
                    Point TopLeft = new Point(Top, Left);
                    Point DrawFrom = new Point(TopLeft.Y + Points.ToArray()[0].X + CalibrationFactor, TopLeft.X + Points.ToArray()[0].Y + MouseDrawYCalibrationFactor);
                    Point DrawTo = new Point(TopLeft.Y + Points.ToArray()[1].X + CalibrationFactor, TopLeft.X + Points.ToArray()[1].Y + MouseDrawYCalibrationFactor);

                    ControlPaint.DrawReversibleLine(DrawFrom, DrawTo, Color.Black); // draw new line
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'GameBoard_MouseMove' method of 'GameBoard' form. Error msg: " + Ex.Message);
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Clock.UpdateTimer();    // Display the new time left by updating the Time Left label.
            if (Clock.TimeOut())
            {
                GameTimer.Stop();
                TimerLabel.Text = "Time out!!!";
                MessageBox.Show("Time out! Better eye next time ;)");
                DisplayFailedWords();
                DisplayScoreDetails();
                if (TheGameEngine.SavedTopScoreSuccessfully())
                {
                    ScoreBoard ScoreBoardObj = new ScoreBoard(CurrentCategory, TheGameEngine.CurrentScore);
                    ScoreBoardObj.MdiParent = Parent.FindForm();
                    ScoreBoardObj.Show();
                }
            }
            else TimerLabel.Text = Clock.TimeLeft + " seconds left.";
        }

        private void DisplayFailedWords()
        {
            List<string> FailedWords = new List<string>();
            TheGameEngine.ObtainFailedWords(ref FailedWords);

            foreach (string Word in FailedWords)
            {
                WordPosition Pos = TheGameEngine.ObtainFailedWordPosition(Word);

                if (Pos.Direction == GameEngine.Direction.Right) // Right.
                    for (int i = Pos.PlacementIndex_X + 1, j = Pos.PlacementIndex_Y + 1, k = 0; k < Pos.Word.Length; i++, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.Left) // Left.
                    for (int i = Pos.PlacementIndex_X + 1, j = Pos.PlacementIndex_Y + 1, k = 0; k < Pos.Word.Length; i--, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.Down) // Down.
                    for (int i = Pos.PlacementIndex_X + 1, j = Pos.PlacementIndex_Y + 1, k = 0; k < Pos.Word.Length; j++, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.Up) // Up.
                    for (int i = Pos.PlacementIndex_X + 1, j = Pos.PlacementIndex_Y + 1, k = 0; k < Pos.Word.Length; j--, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.DownLeft) // Down left word.
                    for (int i = Pos.PlacementIndex_Y + 1, j = Pos.PlacementIndex_X + 1, k = 0; k < Pos.Word.Length; i--, j++, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.UpLeft) // Up left word.
                    for (int i = Pos.PlacementIndex_Y + 1, j = Pos.PlacementIndex_X + 1, k = 0; k < Pos.Word.Length; i--, j--, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.DownRight) // Down right word.
                    for (int i = Pos.PlacementIndex_X + 1, j = Pos.PlacementIndex_Y + 1, k = 0; k < Pos.Word.Length; i++, j++, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
                else if (Pos.Direction == GameEngine.Direction.UpRight) // Up Right word.
                    for (int i = Pos.PlacementIndex_X + 1, j = Pos.PlacementIndex_Y + 1, k = 0; k < Pos.Word.Length; i++, j--, k++)
                        FailedRectangles.Add(new Point(i * SizeFactor, j * SizeFactor));
            }
            Invalidate();
            Application.DoEvents();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            StatusLabel.Text = "";
            StatusTimer.Stop();
        }

        private void WordsListView_KeyUp(object sender, KeyEventArgs e)
        {
            CheatCode.CHEAT_TYPE CheatType;
            if (TheGameEngine.CheckCheat(e.KeyCode.ToString().ToUpper(), out CheatType))            
                ApplyCheat(CheatType);
        }

        private void ApplyCheat(CheatCode.CHEAT_TYPE CheatType)
        {
            if (CheatType == CheatCode.CHEAT_TYPE.INCREASE_TIME)
                Clock.TimeLeft += 100;                 // Cheat code applied, boost up time to 100 more seconds.
            else if (CheatType == CheatCode.CHEAT_TYPE.FLASH_WORDS)
            {
                DisplayFailedWords();
                CheatTimer.Start();
            }

            StatusLabel.Text = TheGameEngine.StatusForDisplay;
            ScoreLabel.Text = "Score: " + TheGameEngine.CurrentScore;
            Invalidate();
            StatusTimer.Start();
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {
            Width = ResponsiveObj.GetMetrics(Width, "Width");    // Form width and height set up.
            Height = ResponsiveObj.GetMetrics(Height, "Height");
            Left = Screen.GetBounds(this).Width / 2 - Width / 2;  // Form centering.
            Top = Screen.GetBounds(this).Height / 2 - Height / 2 - 30;

            foreach(Control Ctl in this.Controls)
            {
                Ctl.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics((int)Ctl.Font.Size), FontStyle.Regular);
                Ctl.Width = ResponsiveObj.GetMetrics(Ctl.Width, "Width");
                Ctl.Height = ResponsiveObj.GetMetrics(Ctl.Height, "Height");
                Ctl.Top = ResponsiveObj.GetMetrics(Ctl.Top, "Top");
                Ctl.Left = ResponsiveObj.GetMetrics(Ctl.Left, "Left");
            }            
        }

        private void CheatTimer_Tick(object sender, EventArgs e)
        {
            CheatTimer.Stop();
            FailedRectangles.Clear();
            Invalidate();
        }
    }
}