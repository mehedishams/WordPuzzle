namespace WordPuzzle
{
    partial class GameBoard
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
            this.components = new System.ComponentModel.Container();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.TimerLabel = new System.Windows.Forms.Label();
            this.StatusTimer = new System.Windows.Forms.Timer(this.components);
            this.WordsListView = new System.Windows.Forms.ListView();
            this.WordsListLabel = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.CheatTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.StatusLabel.Location = new System.Drawing.Point(797, 72);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(379, 111);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "StatusLabel";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 1000;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.TimerLabel.Location = new System.Drawing.Point(912, 30);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(149, 31);
            this.TimerLabel.TabIndex = 1;
            this.TimerLabel.Text = "TimerLabel";
            // 
            // StatusTimer
            // 
            this.StatusTimer.Interval = 2000;
            this.StatusTimer.Tick += new System.EventHandler(this.StatusTimer_Tick);
            // 
            // WordsListView
            // 
            this.WordsListView.BackColor = System.Drawing.SystemColors.Control;
            this.WordsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WordsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordsListView.ForeColor = System.Drawing.Color.DarkGreen;
            this.WordsListView.Location = new System.Drawing.Point(844, 217);
            this.WordsListView.Name = "WordsListView";
            this.WordsListView.Size = new System.Drawing.Size(303, 498);
            this.WordsListView.TabIndex = 2;
            this.WordsListView.UseCompatibleStateImageBehavior = false;
            this.WordsListView.View = System.Windows.Forms.View.List;
            this.WordsListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WordsListView_KeyUp);
            // 
            // WordsListLabel
            // 
            this.WordsListLabel.AutoSize = true;
            this.WordsListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordsListLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.WordsListLabel.Location = new System.Drawing.Point(935, 183);
            this.WordsListLabel.Name = "WordsListLabel";
            this.WordsListLabel.Size = new System.Drawing.Size(150, 31);
            this.WordsListLabel.TabIndex = 3;
            this.WordsListLabel.Text = "Words List:";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.ScoreLabel.Location = new System.Drawing.Point(911, 720);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(93, 31);
            this.ScoreLabel.TabIndex = 4;
            this.ScoreLabel.Text = "Score:";
            // 
            // CheatTimer
            // 
            this.CheatTimer.Interval = 1000;
            this.CheatTimer.Tick += new System.EventHandler(this.CheatTimer_Tick);
            // 
            // GameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 781);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.WordsListLabel);
            this.Controls.Add(this.WordsListView);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.StatusLabel);
            this.MaximizeBox = false;
            this.Name = "GameBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Board";
            this.Load += new System.EventHandler(this.GameBoard_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameBoard_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameBoard_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameBoard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameBoard_MouseUp);
            this.Move += new System.EventHandler(this.GameBoard_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Timer StatusTimer;
        private System.Windows.Forms.ListView WordsListView;
        private System.Windows.Forms.Label WordsListLabel;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Timer CheatTimer;
    }
}

