namespace Client.Room
{
    partial class Lobby
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
            this.tbPlayer1 = new System.Windows.Forms.TextBox();
            this.tbPlayer2 = new System.Windows.Forms.TextBox();
            this.tbPlayer3 = new System.Windows.Forms.TextBox();
            this.tbPlayer4 = new System.Windows.Forms.TextBox();
            this.btnGameStart = new System.Windows.Forms.Button();
            this.cbGame = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbPlayer1
            // 
            this.tbPlayer1.Location = new System.Drawing.Point(85, 52);
            this.tbPlayer1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbPlayer1.Name = "tbPlayer1";
            this.tbPlayer1.ReadOnly = true;
            this.tbPlayer1.Size = new System.Drawing.Size(159, 28);
            this.tbPlayer1.TabIndex = 0;
            // 
            // tbPlayer2
            // 
            this.tbPlayer2.Location = new System.Drawing.Point(85, 111);
            this.tbPlayer2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbPlayer2.Name = "tbPlayer2";
            this.tbPlayer2.ReadOnly = true;
            this.tbPlayer2.Size = new System.Drawing.Size(159, 28);
            this.tbPlayer2.TabIndex = 1;
            // 
            // tbPlayer3
            // 
            this.tbPlayer3.Location = new System.Drawing.Point(85, 171);
            this.tbPlayer3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbPlayer3.Name = "tbPlayer3";
            this.tbPlayer3.ReadOnly = true;
            this.tbPlayer3.Size = new System.Drawing.Size(159, 28);
            this.tbPlayer3.TabIndex = 2;
            // 
            // tbPlayer4
            // 
            this.tbPlayer4.Location = new System.Drawing.Point(85, 228);
            this.tbPlayer4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbPlayer4.Name = "tbPlayer4";
            this.tbPlayer4.ReadOnly = true;
            this.tbPlayer4.Size = new System.Drawing.Size(159, 28);
            this.tbPlayer4.TabIndex = 3;
            // 
            // btnGameStart
            // 
            this.btnGameStart.Enabled = false;
            this.btnGameStart.Location = new System.Drawing.Point(272, 225);
            this.btnGameStart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGameStart.Name = "btnGameStart";
            this.btnGameStart.Size = new System.Drawing.Size(139, 34);
            this.btnGameStart.TabIndex = 4;
            this.btnGameStart.Text = "게임 시작";
            this.btnGameStart.UseVisualStyleBackColor = true;
            this.btnGameStart.Click += new System.EventHandler(this.btnGameStart_Click);
            // 
            // cbGame
            // 
            this.cbGame.FormattingEnabled = true;
            this.cbGame.Items.AddRange(new object[] {
            "Yacht Dice"});
            this.cbGame.Location = new System.Drawing.Point(272, 52);
            this.cbGame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(140, 26);
            this.cbGame.TabIndex = 6;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 314);
            this.Controls.Add(this.cbGame);
            this.Controls.Add(this.btnGameStart);
            this.Controls.Add(this.tbPlayer4);
            this.Controls.Add(this.tbPlayer3);
            this.Controls.Add(this.tbPlayer2);
            this.Controls.Add(this.tbPlayer1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Lobby";
            this.Text = "Room";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPlayer1;
        private System.Windows.Forms.TextBox tbPlayer2;
        private System.Windows.Forms.TextBox tbPlayer3;
        private System.Windows.Forms.TextBox tbPlayer4;
        private System.Windows.Forms.Button btnGameStart;
        private System.Windows.Forms.ComboBox cbGame;
    }
}