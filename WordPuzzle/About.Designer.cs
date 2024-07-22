namespace WordPuzzle
{
    partial class About
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
            this.GameNameLabel = new System.Windows.Forms.Label();
            this.GameVersionLabel = new System.Windows.Forms.Label();
            this.WordPuzzlePictureBox = new System.Windows.Forms.PictureBox();
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.GameCreditLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WordPuzzlePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameNameLabel
            // 
            this.GameNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameNameLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.GameNameLabel.Location = new System.Drawing.Point(0, 9);
            this.GameNameLabel.Name = "GameNameLabel";
            this.GameNameLabel.Size = new System.Drawing.Size(999, 31);
            this.GameNameLabel.TabIndex = 2;
            this.GameNameLabel.Text = "Word Puzzle - the Eye-Teaser";
            this.GameNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameVersionLabel
            // 
            this.GameVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameVersionLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.GameVersionLabel.Location = new System.Drawing.Point(6, 40);
            this.GameVersionLabel.Name = "GameVersionLabel";
            this.GameVersionLabel.Size = new System.Drawing.Size(993, 31);
            this.GameVersionLabel.TabIndex = 3;
            this.GameVersionLabel.Text = "version 3.0 November 2016";
            this.GameVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WordPuzzlePictureBox
            // 
            this.WordPuzzlePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WordPuzzlePictureBox.Image = global::WordPuzzle.Properties.Resources.Game_Board_v3;
            this.WordPuzzlePictureBox.Location = new System.Drawing.Point(190, 74);
            this.WordPuzzlePictureBox.Name = "WordPuzzlePictureBox";
            this.WordPuzzlePictureBox.Size = new System.Drawing.Size(603, 394);
            this.WordPuzzlePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WordPuzzlePictureBox.TabIndex = 4;
            this.WordPuzzlePictureBox.TabStop = false;
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InstructionsLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.InstructionsLabel.Location = new System.Drawing.Point(31, 475);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(924, 287);
            this.InstructionsLabel.TabIndex = 5;
            this.InstructionsLabel.Text = "Instructions:";
            // 
            // GameCreditLabel
            // 
            this.GameCreditLabel.AutoSize = true;
            this.GameCreditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.GameCreditLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.GameCreditLabel.Location = new System.Drawing.Point(796, 762);
            this.GameCreditLabel.Name = "GameCreditLabel";
            this.GameCreditLabel.Size = new System.Drawing.Size(159, 13);
            this.GameCreditLabel.TabIndex = 6;
            this.GameCreditLabel.Text = "Created by Mehedi Shams Rony";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 819);
            this.Controls.Add(this.GameCreditLabel);
            this.Controls.Add(this.InstructionsLabel);
            this.Controls.Add(this.WordPuzzlePictureBox);
            this.Controls.Add(this.GameVersionLabel);
            this.Controls.Add(this.GameNameLabel);
            this.MaximizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WordPuzzlePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameNameLabel;
        private System.Windows.Forms.Label GameVersionLabel;
        private System.Windows.Forms.PictureBox WordPuzzlePictureBox;
        private System.Windows.Forms.Label InstructionsLabel;
        private System.Windows.Forms.Label GameCreditLabel;
    }
}