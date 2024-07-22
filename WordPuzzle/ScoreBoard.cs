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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordPuzzle.Classes;

namespace WordPuzzle
{
    public partial class ScoreBoard : Form
    {
        Responsive ResponsiveObj;
        Words WordsObj = new Words();
        TopScore TopScoreObj = new TopScore();
        public ScoreBoard()
        {
            InitializeComponent();

            ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
            ResponsiveObj.SetMultiplicationFactor();
            SetupScoresGrid();

            CategoryLabel.Visible = CategoryComboBox.Visible = true;
            CategoryComboBox.DataSource = WordsObj.GetCategories();
        }

        public ScoreBoard(string Category, int Score)
        {
            InitializeComponent();

            ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
            ResponsiveObj.SetMultiplicationFactor();
            SetupScoresGrid();

            CategoryLabel.Text = Category;
            ScoreLabel.Text = "Your score: " + Score;
            ScoreLabel.Visible = true;

            TopScore TopScoreObj = new TopScore();
            ScoresDataGridView.DataSource = TopScoreObj.GetScores(Category);
            ScoresDataGridView.ClearSelection();
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TopScore TopScoreObj = new TopScore();
                List<ScoreEntity> Scores = TopScoreObj.GetScores(CategoryComboBox.Text);
                CategoryLabel.Text = CategoryComboBox.Text;
                ScoresDataGridView.DataSource = TopScoreObj.GetScores(CategoryComboBox.Text);
                ScoresDataGridView.ClearSelection();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'CategoryComboBox_SelectedIndexChanged' method of 'ScoreBoard.cs' class. Error msg: " + Ex.Message);
            }
        }

        private void SetupScoresGrid()
        {
            try
            {
                // http://stackoverflow.com/questions/64041/winforms-datagridview-font-size
                ScoresDataGridView.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics(20), FontStyle.Regular);
                ScoresDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                ScoresDataGridView.Columns.Add("Name", "Name");
                ScoresDataGridView.Columns.Add("Score", "Score");
                ScoresDataGridView.Columns.Add("ScoreTime", "Date/Time");

                // http://stackoverflow.com/questions/2154154/datagridview-how-to-set-column-width

                for (int i = 0; i < 3; i++)
                {
                    ScoresDataGridView.Columns[i].FillWeight = ResponsiveObj.GetMetrics(200);
                    DataGridViewColumn column = ScoresDataGridView.Columns[i];
                    ScoresDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.Width = ResponsiveObj.GetMetrics(300);
                    column.MinimumWidth = ResponsiveObj.GetMetrics(300);
                    column.HeaderCell.Style.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics(20), FontStyle.Regular);
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.BackColor = Color.Red;
                }

                // https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.font%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                ScoresDataGridView.RowCount = 14;
                ScoresDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                ScoresDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

                // http://stackoverflow.com/questions/1741299/how-to-bind-datagridview-predefined-columns-with-columns-from-sql-statement-wit
                ScoresDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
                ScoresDataGridView.AutoGenerateColumns = false;
                ScoresDataGridView.Columns["Name"].DataPropertyName = "Scorer";
                ScoresDataGridView.Columns["Score"].DataPropertyName = "Score";
                ScoresDataGridView.Columns["ScoreTime"].DataPropertyName = "ScoreTime";
                ScoresDataGridView.Columns["ScoreTime"].ValueType = typeof(string);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'SetupPlayersWordGrid' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void ScoreBoard_Load(object sender, EventArgs e)
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