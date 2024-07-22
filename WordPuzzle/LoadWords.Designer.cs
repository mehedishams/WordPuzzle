namespace WordPuzzle
{
    partial class LoadWords
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadPresetsLabel = new System.Windows.Forms.Label();
            this.CategoriesComboBox = new System.Windows.Forms.ComboBox();
            this.CreateOwnSetLabel = new System.Windows.Forms.Label();
            this.WordsDataGridView = new System.Windows.Forms.DataGridView();
            this.SaveAndPlayButton = new System.Windows.Forms.Button();
            this.WordsListView = new System.Windows.Forms.ListView();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.InstructionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WordsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadPresetsLabel
            // 
            this.LoadPresetsLabel.AutoSize = true;
            this.LoadPresetsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadPresetsLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.LoadPresetsLabel.Location = new System.Drawing.Point(59, 42);
            this.LoadPresetsLabel.Name = "LoadPresetsLabel";
            this.LoadPresetsLabel.Size = new System.Drawing.Size(179, 31);
            this.LoadPresetsLabel.TabIndex = 2;
            this.LoadPresetsLabel.Text = "Load presets:";
            // 
            // CategoriesComboBox
            // 
            this.CategoriesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoriesComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoriesComboBox.ForeColor = System.Drawing.Color.DarkGreen;
            this.CategoriesComboBox.FormattingEnabled = true;
            this.CategoriesComboBox.Location = new System.Drawing.Point(65, 76);
            this.CategoriesComboBox.Name = "CategoriesComboBox";
            this.CategoriesComboBox.Size = new System.Drawing.Size(274, 39);
            this.CategoriesComboBox.TabIndex = 3;
            this.CategoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.CategoriesComboBox_SelectedIndexChanged);
            // 
            // CreateOwnSetLabel
            // 
            this.CreateOwnSetLabel.AutoSize = true;
            this.CreateOwnSetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateOwnSetLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.CreateOwnSetLabel.Location = new System.Drawing.Point(567, 9);
            this.CreateOwnSetLabel.Name = "CreateOwnSetLabel";
            this.CreateOwnSetLabel.Size = new System.Drawing.Size(265, 31);
            this.CreateOwnSetLabel.TabIndex = 4;
            this.CreateOwnSetLabel.Text = "Create your own set:";
            // 
            // WordsDataGridView
            // 
            this.WordsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WordsDataGridView.Location = new System.Drawing.Point(573, 121);
            this.WordsDataGridView.Name = "WordsDataGridView";
            this.WordsDataGridView.RowTemplate.Height = 30;
            this.WordsDataGridView.Size = new System.Drawing.Size(390, 512);
            this.WordsDataGridView.TabIndex = 5;
            this.WordsDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.WordsDataGridView_EditingControlShowing);
            // 
            // SaveAndPlayButton
            // 
            this.SaveAndPlayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveAndPlayButton.ForeColor = System.Drawing.Color.Firebrick;
            this.SaveAndPlayButton.Location = new System.Drawing.Point(573, 639);
            this.SaveAndPlayButton.Name = "SaveAndPlayButton";
            this.SaveAndPlayButton.Size = new System.Drawing.Size(295, 58);
            this.SaveAndPlayButton.TabIndex = 6;
            this.SaveAndPlayButton.Text = "Save and Play";
            this.SaveAndPlayButton.UseVisualStyleBackColor = true;
            this.SaveAndPlayButton.Click += new System.EventHandler(this.SaveAndPlayButton_Click);
            // 
            // WordsListView
            // 
            this.WordsListView.BackColor = System.Drawing.SystemColors.Control;
            this.WordsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WordsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordsListView.ForeColor = System.Drawing.Color.DarkGreen;
            this.WordsListView.Location = new System.Drawing.Point(65, 121);
            this.WordsListView.Name = "WordsListView";
            this.WordsListView.Size = new System.Drawing.Size(274, 470);
            this.WordsListView.TabIndex = 7;
            this.WordsListView.UseCompatibleStateImageBehavior = false;
            this.WordsListView.View = System.Windows.Forms.View.List;
            // 
            // PlayBtn
            // 
            this.PlayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayBtn.ForeColor = System.Drawing.Color.Firebrick;
            this.PlayBtn.Location = new System.Drawing.Point(65, 639);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(274, 58);
            this.PlayBtn.TabIndex = 8;
            this.PlayBtn.Text = "Play";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.InstructionLabel.Location = new System.Drawing.Point(573, 42);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(390, 73);
            this.InstructionLabel.TabIndex = 9;
            this.InstructionLabel.Text = "14 words, Max length 10 each, Min length 3 each, no duplicate";
            // 
            // LoadWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 760);
            this.Controls.Add(this.InstructionLabel);
            this.Controls.Add(this.PlayBtn);
            this.Controls.Add(this.WordsListView);
            this.Controls.Add(this.SaveAndPlayButton);
            this.Controls.Add(this.WordsDataGridView);
            this.Controls.Add(this.CreateOwnSetLabel);
            this.Controls.Add(this.CategoriesComboBox);
            this.Controls.Add(this.LoadPresetsLabel);
            this.MaximizeBox = false;
            this.Name = "LoadWords";
            this.Text = "Load Words";
            this.Load += new System.EventHandler(this.LoadWords_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WordsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoadPresetsLabel;
        private System.Windows.Forms.ComboBox CategoriesComboBox;
        private System.Windows.Forms.Label CreateOwnSetLabel;
        private System.Windows.Forms.DataGridView WordsDataGridView;
        private System.Windows.Forms.Button SaveAndPlayButton;
        private System.Windows.Forms.ListView WordsListView;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Label InstructionLabel;
    }
}