//////////////////////////////////////////////
//////////////////////////////////////////////
// Coded by Mehedi Shams Rony ////////////////
// September, October 2016 ///////////////////
//////////////////////////////////////////////
//////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WordPuzzle.Classes;
using System.Configuration;

namespace WordPuzzle
{
    public partial class LoadWords : Form
    {
        Responsive ResponsiveObj;
        private List<string> Categories = new List<string>();
        private List<WordEntity> WordsList = new List<WordEntity>();
        private int MAX_WORDS = Convert.ToInt16(ConfigurationManager.AppSettings["MAX_WORDS"]);
        Words WordsObj = new Words();

        public LoadWords()
        {
            try
            {
                InitializeComponent();
                StartPosition = FormStartPosition.CenterScreen;

                ResponsiveObj = new Responsive(Screen.PrimaryScreen.Bounds);
                ResponsiveObj.SetMultiplicationFactor();

                SetupPlayersWordGrid();

                Categories = WordsObj.GetCategories();
                WordsList = WordsObj.GetWords();
                CategoriesComboBox.DataSource = WordsObj.GetCategories();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'LoadWords' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void SetupPlayersWordGrid()
        {
            try
            {
                // http://stackoverflow.com/questions/64041/winforms-datagridview-font-size
                WordsDataGridView.DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics(15), FontStyle.Regular);
                WordsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                WordsDataGridView.RowTemplate.Height = ResponsiveObj.GetMetrics(30);
                WordsDataGridView.Columns.Add("", "Word");
                WordsDataGridView.Columns[0].HeaderCell.Style.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics(15), FontStyle.Regular);

                // https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.font%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                // http://stackoverflow.com/questions/5206203/can-i-set-the-max-number-of-rows-in-unbound-datagridview
                WordsDataGridView.UserAddedRow += WordsDataGridView_RowCountChanged;
                WordsDataGridView.UserDeletedRow += WordsDataGridView_RowCountChanged;
                WordsDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                WordsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                // http://stackoverflow.com/questions/2154154/datagridview-how-to-set-column-width
                WordsDataGridView.Columns[0].FillWeight = ResponsiveObj.GetMetrics(200);
                DataGridViewColumn column = WordsDataGridView.Columns[0];
                column.Width = ResponsiveObj.GetMetrics(200);
                column.MinimumWidth = ResponsiveObj.GetMetrics(200);
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.BackColor = Color.Red;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'SetupPlayersWordGrid' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void SaveAndPlayButton_Click(object sender, EventArgs e)
        {
            string CategoryNameFromUser;
            try
            {
                List<string> WordsByThePlayer = new List<string>();
                if (WordsObj.CheckUserInputValidity(WordsDataGridView, WordsByThePlayer))
                {
                    if (WordsObj.SavedWordsAndCategoriesSuccessfully(WordsByThePlayer, out CategoryNameFromUser))
                        LoadGameBoard(WordsByThePlayer, CategoryNameFromUser);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'SaveAndPlayButton_Click' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void CategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                WordsListView.Clear();
                List<WordEntity> WordsInCategory = WordsList.FindAll(p => p.Category.Equals(CategoriesComboBox.Text));
                foreach (WordEntity WordStr in WordsInCategory)
                    WordsListView.Items.Add(new ListViewItem(WordStr.Word));
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'CategoriesComboBox_SelectedIndexChanged' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }        
        
        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            WordsObj.KeyPressValidity(sender, e);
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<WordEntity> WordsInCategory = WordsList.FindAll(p => p.Category.Equals(CategoriesComboBox.Text));
                List<string> Words = new List<string>();
                foreach (WordEntity WordStr in WordsInCategory)
                    Words.Add(WordStr.Word);
                LoadGameBoard(Words, CategoriesComboBox.Text);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'PlayBtn_Click' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void LoadGameBoard(List<string> Words, string Category)
        {
            try
            {
                ChooseDirections ChooseDirectionsForm = new ChooseDirections(Words.ToArray(), Category);
                ChooseDirectionsForm.MdiParent = Parent.FindForm();
                ChooseDirectionsForm.Show();
                Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'LoadGameBoard' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }

        private void LoadWords_Load(object sender, EventArgs e)
        {
            Width = ResponsiveObj.GetMetrics(Width, "Width");    // Form width and height set up.
            Height = ResponsiveObj.GetMetrics(Height, "Height");
            Left = Screen.GetBounds(this).Width / 2 - Width / 2;  // Form centering.
            Top = Screen.GetBounds(this).Height / 2 - Height / 2 - 30;  // 30 is a calibration factor.

            foreach (Control Ctl in Controls)
            {
                Ctl.Font = new Font(FontFamily.GenericSansSerif, ResponsiveObj.GetMetrics((int)Ctl.Font.Size), FontStyle.Regular);
                Ctl.Width = ResponsiveObj.GetMetrics(Ctl.Width, "Width");
                Ctl.Height = ResponsiveObj.GetMetrics(Ctl.Height, "Height");
                Ctl.Top = ResponsiveObj.GetMetrics(Ctl.Top, "Top");
                Ctl.Left = ResponsiveObj.GetMetrics(Ctl.Left, "Left");
            }
        }

        private void WordsDataGridView_RowCountChanged(object sender, EventArgs e)
        {
            CheckRowCount();
        }

        private void CheckRowCount()
        {
            if (WordsDataGridView.Rows != null && WordsDataGridView.Rows.Count > MAX_WORDS)
            {
                WordsDataGridView.AllowUserToAddRows = false;
            }
            else if (!WordsDataGridView.AllowUserToAddRows)
            {
                WordsDataGridView.AllowUserToAddRows = true;
            }
        }

        private void WordsDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                int i = WordsDataGridView.CurrentRow.Index;
                MessageBox.Show(i.ToString());
            }
        }

        // https://social.msdn.microsoft.com/Forums/en-US/ecf3dec5-9cba-410b-bef5-8b950d04e6bf/change-the-character-to-upper-case-when-i-keying?forum=csharpgeneral
        private void WordsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("An error occurred in 'WordsDataGridView_EditingControlShowing' method of 'LoadWords' form. Error msg: " + Ex.Message);
            }
        }
    }
}