namespace Client.Title
{
    partial class Title
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbNickName = new System.Windows.Forms.TextBox();
            this.lbIP = new System.Windows.Forms.Label();
            this.lbNickName = new System.Windows.Forms.Label();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(97, 140);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbNickName
            // 
            this.tbNickName.Location = new System.Drawing.Point(82, 98);
            this.tbNickName.Name = "tbNickName";
            this.tbNickName.Size = new System.Drawing.Size(168, 21);
            this.tbNickName.TabIndex = 1;
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.Location = new System.Drawing.Point(23, 49);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(52, 12);
            this.lbIP.TabIndex = 3;
            this.lbIP.Text = "서버 IP :";
            // 
            // lbNickName
            // 
            this.lbNickName.AutoSize = true;
            this.lbNickName.Location = new System.Drawing.Point(26, 101);
            this.lbNickName.Name = "lbNickName";
            this.lbNickName.Size = new System.Drawing.Size(49, 12);
            this.lbNickName.TabIndex = 4;
            this.lbNickName.Text = "닉네임 :";
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(82, 46);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(168, 21);
            this.tbServerIP.TabIndex = 5;
            // 
            // Title
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 188);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.lbNickName);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.tbNickName);
            this.Controls.Add(this.btnLogin);
            this.Name = "Title";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbNickName;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.Label lbNickName;
        private System.Windows.Forms.TextBox tbServerIP;
    }
}