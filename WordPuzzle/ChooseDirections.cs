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

namespace WordPuzzle
{
    public partial class ChooseDirections : Form
    {
        Responsive ResponsiveObj;
        private string CurrentCategory;
        private string[] CurrentWords;

        public ChooseDirections(string[] Words, string SelectedCategory)
        {
            // Arrows collected from: https://openclipart.org/user-cliparts/mondspeer?page=2
            InitializeComponent();

            ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
            ResponsiveObj.SetMultiplicationFactor();

            CurrentCategory = SelectedCategory;
            CurrentWords = Words;
        }

        private void ChooseAllButton_Click(object sender, EventArgs e)
        {
            foreach (Control Ctl in Controls)
                if (Ctl is CheckBox)
                    (Ctl as CheckBox).Checked = true;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<GameEngine.Direction> ChosenDirections = new List<GameEngine.Direction>();
                if (!ListedDirectionsSuccessfully(ref ChosenDirections))
                {
                    MessageBox.Show("Please choose at least two directions.");
                    return;
                }

                GameBoard Board = new GameBoard(CurrentWords, CurrentCategory, ChosenDirections);
                Board.MdiParent = Parent.FindForm();
                Board.Show();
                Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'PlayButton_Click' method of 'ChooseDirections' form. Error msg: " + Ex.Message);
            }
        }

        private bool ListedDirectionsSuccessfully(ref List<GameEngine.Direction> Directions)
        {
            foreach (Control Ctl in Controls)
                if (Ctl is CheckBox)
                    if ((Ctl as CheckBox).Checked)
                        Directions.Add((GameEngine.Direction)Enum.Parse(typeof(GameEngine.Direction), Ctl.Tag.ToString()));
            return Directions.Count >= 2;
        }

        private void ChooseDirections_Load(object sender, EventArgs e)
        {
            Width = ResponsiveObj.GetMetrics(Width, "Width");    // Form width and height set up.
            Height = ResponsiveObj.GetMetrics(Height, "Height");
            Left = Screen.GetBounds(this).Width / 2 - Width / 2;  // Form centering.
            Top = Screen.GetBounds(this).Height / 2 - Height / 2 - 30;  // 30 is a calibration factor.

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