//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace WordPuzzle
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void LoadMenuItem_Click(object sender, EventArgs e)
        {
            LoadWords LoadWordsObj = new LoadWords();
            LoadWordsObj.MdiParent = this;
            LoadWordsObj.Show();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ViewScores_Click(object sender, EventArgs e)
        {
            ScoreBoard ScoreBoardObj = new ScoreBoard();
            ScoreBoardObj.MdiParent = this;
            ScoreBoardObj.Show();
        }

        // http://stackoverflow.com/questions/10706963/call-method-of-specific-mdi-child-from-parent
        private void GameWindow_Resize(object sender, EventArgs e)
        {
            Redraw();
        }

        private void Redraw()
        {
            GameBoard Board = ActiveMdiChild as GameBoard;
            if (Board != null)  // If game board is available, then refresh it.
                Board.Invalidate();
        }

        private void GameWindow_Move(object sender, EventArgs e)
        {
            Redraw();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            About AboutObj = new About();
            AboutObj.MdiParent = this;
            AboutObj.Show();
        }
    }
}