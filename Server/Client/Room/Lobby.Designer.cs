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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPlayer1
            // 
            this.tbPlayer1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbPlayer1.Location = new System.Drawing.Point(59, 34);
            this.tbPlayer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPlayer1.Name = "tbPlayer1";
            this.tbPlayer1.ReadOnly = true;
            this.tbPlayer1.Size = new System.Drawing.Size(113, 26);
            this.tbPlayer1.TabIndex = 0;
            // 
            // tbPlayer2
            // 
            this.tbPlayer2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbPlayer2.Location = new System.Drawing.Point(59, 74);
            this.tbPlayer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPlayer2.Name = "tbPlayer2";
            this.tbPlayer2.ReadOnly = true;
            this.tbPlayer2.Size = new System.Drawing.Size(113, 26);
            this.tbPlayer2.TabIndex = 1;
            // 
            // tbPlayer3
            // 
            this.tbPlayer3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPlayer3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbPlayer3.Location = new System.Drawing.Point(59, 114);
            this.tbPlayer3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPlayer3.Name = "tbPlayer3";
            this.tbPlayer3.ReadOnly = true;
            this.tbPlayer3.Size = new System.Drawing.Size(113, 26);
            this.tbPlayer3.TabIndex = 2;
            // 
            // tbPlayer4
            // 
            this.tbPlayer4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbPlayer4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbPlayer4.Location = new System.Drawing.Point(59, 152);
            this.tbPlayer4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPlayer4.Name = "tbPlayer4";
            this.tbPlayer4.ReadOnly = true;
            this.tbPlayer4.Size = new System.Drawing.Size(113, 26);
            this.tbPlayer4.TabIndex = 3;
            // 
            // btnGameStart
            // 
            this.btnGameStart.BackColor = System.Drawing.Color.Moccasin;
            this.btnGameStart.Enabled = false;
            this.btnGameStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGameStart.Location = new System.Drawing.Point(191, 150);
            this.btnGameStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGameStart.Name = "btnGameStart";
            this.btnGameStart.Size = new System.Drawing.Size(99, 29);
            this.btnGameStart.TabIndex = 4;
            this.btnGameStart.Text = "게임 시작";
            this.btnGameStart.UseVisualStyleBackColor = false;
            this.btnGameStart.Click += new System.EventHandler(this.btnGameStart_Click);
            // 
            // cbGame
            // 
            this.cbGame.Font = new System.Drawing.Font("SUIT", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGame.FormattingEnabled = true;
            this.cbGame.Items.AddRange(new object[] {
            "Yacht Dice",
            "Knucklebone"});
            this.cbGame.Location = new System.Drawing.Point(191, 34);
            this.cbGame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(99, 24);
            this.cbGame.TabIndex = 6;
            this.cbGame.SelectedIndexChanged += new System.EventHandler(this.CbGame_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources.crown_tr;
            this.pictureBox1.Location = new System.Drawing.Point(22, 34);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 210);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbGame);
            this.Controls.Add(this.btnGameStart);
            this.Controls.Add(this.tbPlayer4);
            this.Controls.Add(this.tbPlayer3);
            this.Controls.Add(this.tbPlayer2);
            this.Controls.Add(this.tbPlayer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Lobby";
            this.Text = "Room";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Lobby_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}