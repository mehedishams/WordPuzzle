//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WordPuzzle.Classes;

namespace WordPuzzle
{
    public partial class About : Form
    {        
        Responsive ResponsiveObj;
        public About()
        {
            InitializeComponent();
            StringBuilder Instructions = new StringBuilder();
            ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
            ResponsiveObj.SetMultiplicationFactor();
            LabelInstructions(Instructions);
        }

        private void LabelInstructions(StringBuilder Instructions)
        {
            Instructions.Append("Find words that are randomly placed on the board either in up to 8 directions. You can create your custom word list in the loading dock.");
            Instructions.Append("\n\nUse the mouse to click, drag and release on the word that you found.");
            Instructions.Append("E.g.: If you found BANGLADESH, click on the first letter (B), drag the mouse up to the last letter (H), and release there.");
            Instructions.Append("\n\nIf a word is found, the game will colour the cells to light blue. " +
                                "If you run out of time, the game will display the words hidden in the board with green colour.");
            Instructions.Append("\n\nYou can be a top scorer if you are able to score enough to be listed.");
            Instructions.Append("\n\nThere are two cheat codes. Type \"mambazamba\" or \"flash\" without the quote on the game board " +
                                "to activate. The former increases the remaining time to 100 more seconds, the later one flashes the words for a second. ");
            Instructions.Append("Mind that cheating will penalise your score by deducting 50.");
            Instructions.Append("\n\nHAPPY PLAYING! TEASE YOUR EYES!!!");
            InstructionsLabel.Text = Instructions.ToString();
        }

        private void About_Load(object sender, EventArgs e)
        {
            Width = ResponsiveObj.GetMetrics(Width, "Width");    // Form width and height set up.
            Height = ResponsiveObj.GetMetrics(Height, "Height");
            Left = Screen.GetBounds(this).Width / 2 - Width / 2;  // Form centering.
            Top = Screen.GetBounds(this).Height / 2 - Height / 2 - 30;

            foreach (Control Ctl in this.Controls)
            {
                Ctl.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics((int)Ctl.Font.Size), FontStyle.Regular);
                Ctl.Width = ResponsiveObj.GetMetrics(Ctl.Width, "Width");
                Ctl.Height = ResponsiveObj.GetMetrics(Ctl.Height, "Height");
                Ctl.Top = ResponsiveObj.GetMetrics(Ctl.Top, "Top");
                Ctl.Left = ResponsiveObj.GetMetrics(Ctl.Left, "Left");
            }
        }
    }
}