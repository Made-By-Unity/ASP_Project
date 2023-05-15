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
            this.lbGameList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // tbPlayer1
            // 
            this.tbPlayer1.Location = new System.Drawing.Point(60, 35);
            this.tbPlayer1.Name = "tbPlayer1";
            this.tbPlayer1.ReadOnly = true;
            this.tbPlayer1.Size = new System.Drawing.Size(113, 21);
            this.tbPlayer1.TabIndex = 0;
            // 
            // tbPlayer2
            // 
            this.tbPlayer2.Location = new System.Drawing.Point(60, 74);
            this.tbPlayer2.Name = "tbPlayer2";
            this.tbPlayer2.ReadOnly = true;
            this.tbPlayer2.Size = new System.Drawing.Size(113, 21);
            this.tbPlayer2.TabIndex = 1;
            // 
            // tbPlayer3
            // 
            this.tbPlayer3.Location = new System.Drawing.Point(60, 114);
            this.tbPlayer3.Name = "tbPlayer3";
            this.tbPlayer3.ReadOnly = true;
            this.tbPlayer3.Size = new System.Drawing.Size(113, 21);
            this.tbPlayer3.TabIndex = 2;
            // 
            // tbPlayer4
            // 
            this.tbPlayer4.Location = new System.Drawing.Point(60, 152);
            this.tbPlayer4.Name = "tbPlayer4";
            this.tbPlayer4.ReadOnly = true;
            this.tbPlayer4.Size = new System.Drawing.Size(113, 21);
            this.tbPlayer4.TabIndex = 3;
            // 
            // btnGameStart
            // 
            this.btnGameStart.Enabled = false;
            this.btnGameStart.Location = new System.Drawing.Point(190, 150);
            this.btnGameStart.Name = "btnGameStart";
            this.btnGameStart.Size = new System.Drawing.Size(82, 23);
            this.btnGameStart.TabIndex = 4;
            this.btnGameStart.Text = "게임 시작";
            this.btnGameStart.UseVisualStyleBackColor = true;
            this.btnGameStart.Click += new System.EventHandler(this.btnGameStart_Click);
            // 
            // lbGameList
            // 
            this.lbGameList.FormattingEnabled = true;
            this.lbGameList.ItemHeight = 12;
            this.lbGameList.Items.AddRange(new object[] {
            "Yacht Dice"});
            this.lbGameList.Location = new System.Drawing.Point(190, 35);
            this.lbGameList.Name = "lbGameList";
            this.lbGameList.Size = new System.Drawing.Size(82, 52);
            this.lbGameList.TabIndex = 5;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 209);
            this.Controls.Add(this.lbGameList);
            this.Controls.Add(this.btnGameStart);
            this.Controls.Add(this.tbPlayer4);
            this.Controls.Add(this.tbPlayer3);
            this.Controls.Add(this.tbPlayer2);
            this.Controls.Add(this.tbPlayer1);
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
        private System.Windows.Forms.ListBox lbGameList;
    }
}