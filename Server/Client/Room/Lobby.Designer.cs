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
            this.tbPlayer1.Location = new System.Drawing.Point(110, 69);
            this.tbPlayer1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbPlayer1.Name = "tbPlayer1";
            this.tbPlayer1.ReadOnly = true;
            this.tbPlayer1.Size = new System.Drawing.Size(206, 35);
            this.tbPlayer1.TabIndex = 0;
            // 
            // tbPlayer2
            // 
            this.tbPlayer2.Location = new System.Drawing.Point(110, 148);
            this.tbPlayer2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbPlayer2.Name = "tbPlayer2";
            this.tbPlayer2.ReadOnly = true;
            this.tbPlayer2.Size = new System.Drawing.Size(206, 35);
            this.tbPlayer2.TabIndex = 1;
            // 
            // tbPlayer3
            // 
            this.tbPlayer3.Location = new System.Drawing.Point(110, 228);
            this.tbPlayer3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbPlayer3.Name = "tbPlayer3";
            this.tbPlayer3.ReadOnly = true;
            this.tbPlayer3.Size = new System.Drawing.Size(206, 35);
            this.tbPlayer3.TabIndex = 2;
            // 
            // tbPlayer4
            // 
            this.tbPlayer4.Location = new System.Drawing.Point(110, 304);
            this.tbPlayer4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbPlayer4.Name = "tbPlayer4";
            this.tbPlayer4.ReadOnly = true;
            this.tbPlayer4.Size = new System.Drawing.Size(206, 35);
            this.tbPlayer4.TabIndex = 3;
            // 
            // btnGameStart
            // 
            this.btnGameStart.Enabled = false;
            this.btnGameStart.Location = new System.Drawing.Point(354, 300);
            this.btnGameStart.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnGameStart.Name = "btnGameStart";
            this.btnGameStart.Size = new System.Drawing.Size(181, 45);
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
            this.cbGame.Location = new System.Drawing.Point(354, 69);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(181, 32);
            this.cbGame.TabIndex = 6;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 419);
            this.Controls.Add(this.cbGame);
            this.Controls.Add(this.btnGameStart);
            this.Controls.Add(this.tbPlayer4);
            this.Controls.Add(this.tbPlayer3);
            this.Controls.Add(this.tbPlayer2);
            this.Controls.Add(this.tbPlayer1);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Lobby";
            this.Text = "Room";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Lobby_FormClosing);
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