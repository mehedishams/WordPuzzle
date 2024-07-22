namespace WordPuzzle
{
    partial class ScoreDetails
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
            this.ScoreDetailslabel = new System.Windows.Forms.Label();
            this.GameBoardPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameBoardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ScoreDetailslabel
            // 
            this.ScoreDetailslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreDetailslabel.Location = new System.Drawing.Point(776, 12);
            this.ScoreDetailslabel.Name = "ScoreDetailslabel";
            this.ScoreDetailslabel.Size = new System.Drawing.Size(334, 670);
            this.ScoreDetailslabel.TabIndex = 0;
            this.ScoreDetailslabel.Text = "ScoreDetailsLabel";
            // 
            // GameBoardPictureBox
            // 
            this.GameBoardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameBoardPictureBox.Location = new System.Drawing.Point(78, 12);
            this.GameBoardPictureBox.Name = "GameBoardPictureBox";
            this.GameBoardPictureBox.Size = new System.Drawing.Size(670, 670);
            this.GameBoardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameBoardPictureBox.TabIndex = 1;
            this.GameBoardPictureBox.TabStop = false;
            // 
            // ScoreDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 781);
            this.Controls.Add(this.GameBoardPictureBox);
            this.Controls.Add(this.ScoreDetailslabel);
            this.MaximizeBox = false;
            this.Name = "ScoreDetails";
            this.Text = "Score Details";
            this.Load += new System.EventHandler(this.ScoreDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GameBoardPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ScoreDetailslabel;
        private System.Windows.Forms.PictureBox GameBoardPictureBox;
    }
}