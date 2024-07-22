using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordPuzzle.Classes;
using System.IO;

namespace WordPuzzle
{
    public partial class ScoreDetails : Form
    {
        private Responsive ResponsiveObj;
        private List<WordPosition> WordPositions = new List<WordPosition>();
        private List<string> WORDS_FOUND = new List<string>();
        private int REMAINING_TIME_MULTIPLIER;
        public string[] WORD_ARRAY;                    // These arrays are needed to store all the available words.
        private int RemainingTime;
        private int TotalScore;
        private MemoryStream MS;
        
        public ScoreDetails(List<WordPosition> WordPositionsParam, int REMAINING_TIME_MULTIPLIER_PARAM, List<string> WORDS_FOUND_PARAM, string[] WORD_ARRAY_PARAM, int RemainingTimeParam, int TotalScoreParam, ref MemoryStream MSParam)
        {
            InitializeComponent();
            MS = MSParam;

            WordPositions = WordPositionsParam;
            REMAINING_TIME_MULTIPLIER = REMAINING_TIME_MULTIPLIER_PARAM;
            WORDS_FOUND = WORDS_FOUND_PARAM;
            WORD_ARRAY = WORD_ARRAY_PARAM;
            RemainingTime = RemainingTimeParam;
            TotalScore = TotalScoreParam;            
            
            ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
            ResponsiveObj.SetMultiplicationFactor();

            LoadScoreDetails();
        }

        private void LoadScoreDetails()
        {
            StringBuilder SBuilder = new StringBuilder();
            SBuilder.Append("Score for found words:\n");
            SBuilder.Append("======================\n");
            int Augmenter, Len;
            foreach(string Wrd in WORDS_FOUND)
            {
                Augmenter = WordPositions.Find(p => p.Word.Equals(Wrd)).ScoreAugmenter;
                Len = Wrd.Length;
                SBuilder.Append(Wrd + ", Score:\t\t" + Len.ToString() + " x " + WordPositions.Find(p => p.Word.Equals(Wrd)).ScoreAugmenter.ToString() + " = " + (Len * Augmenter).ToString() + "\n");
            }

            SBuilder.Append("\nFailed Words:\n");
            SBuilder.Append("======================\n");

            // http://stackoverflow.com/questions/3944803/use-linq-to-get-items-in-one-list-that-are-not-in-another-list
            string[] FailedWords = WORD_ARRAY.Where(p => !WORDS_FOUND.Any(p2 => p2.Equals(p))).ToArray();
            if (FailedWords.GetUpperBound(0) < 0)
                SBuilder.Append("None\n");
            else
                foreach(string Word in FailedWords)
                    SBuilder.Append(Word + "\n");
            SBuilder.Append("\nTimer bonus:\t\t");
            SBuilder.Append("======================\n");
            if (RemainingTime == 0)
                SBuilder.Append("None\n");
            else SBuilder.Append(RemainingTime.ToString() + " x " + REMAINING_TIME_MULTIPLIER.ToString() + " = " + (RemainingTime * REMAINING_TIME_MULTIPLIER).ToString() + "\n");

            SBuilder.Append("======================\n");
            SBuilder.Append("Total score:\t\t" + TotalScore.ToString());

            ScoreDetailslabel.Text = SBuilder.ToString();
        }

        private void ScoreDetails_Load(object sender, EventArgs e)
        {
            Width = ResponsiveObj.GetMetrics(Width, "Width");    // Form width and height set up.
            Height = ResponsiveObj.GetMetrics(Height, "Height");
            Left = Screen.GetBounds(this).Width / 2 - Width / 2;  // Form centering.
            Top = Screen.GetBounds(this).Height / 2 - Height / 2 - 30;

            foreach (Control Ctl in Controls)
            {
                if (Ctl is PictureBox)
                {
                    Ctl.Width = ResponsiveObj.GetMetrics(Ctl.Width, "Width");
                    Ctl.Height = ResponsiveObj.GetMetrics(Ctl.Height, "Height");
                    Ctl.Top = ResponsiveObj.GetMetrics(Ctl.Top, "Top");
                    Ctl.Left = ResponsiveObj.GetMetrics(Ctl.Left, "Left");
                }
                else
                {
                    Ctl.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics((int)Ctl.Font.Size), FontStyle.Regular);
                    Ctl.Width = ResponsiveObj.GetMetrics(Ctl.Width, "Width");
                    Ctl.Height = ResponsiveObj.GetMetrics(Ctl.Height, "Height");
                    Ctl.Top = ResponsiveObj.GetMetrics(Ctl.Top, "Top");
                    Ctl.Left = ResponsiveObj.GetMetrics(Ctl.Left, "Left");
                }
            }

            GameBoardPictureBox.Image = Image.FromStream(MS);
        }
    }
}