﻿namespace Client.Title
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Title));
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbNickName = new System.Windows.Forms.TextBox();
            this.lbIP = new System.Windows.Forms.Label();
            this.lbNickName = new System.Windows.Forms.Label();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(120, 188);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(76, 31);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbNickName
            // 
            this.tbNickName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNickName.Location = new System.Drawing.Point(79, 156);
            this.tbNickName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbNickName.Name = "tbNickName";
            this.tbNickName.Size = new System.Drawing.Size(181, 22);
            this.tbNickName.TabIndex = 1;
            this.tbNickName.TextChanged += new System.EventHandler(this.TbNickName_TextChanged);
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIP.Location = new System.Drawing.Point(13, 129);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(56, 16);
            this.lbIP.TabIndex = 3;
            this.lbIP.Text = "서버 IP :";
            this.lbIP.Click += new System.EventHandler(this.LbIP_Click);
            // 
            // lbNickName
            // 
            this.lbNickName.AutoSize = true;
            this.lbNickName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNickName.Location = new System.Drawing.Point(20, 156);
            this.lbNickName.Name = "lbNickName";
            this.lbNickName.Size = new System.Drawing.Size(49, 16);
            this.lbNickName.TabIndex = 4;
            this.lbNickName.Text = "닉네임 :";
            this.lbNickName.Click += new System.EventHandler(this.LbNickName_Click);
            // 
            // tbServerIP
            // 
            this.tbServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerIP.Location = new System.Drawing.Point(79, 124);
            this.tbServerIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(181, 22);
            this.tbServerIP.TabIndex = 0;
            this.tbServerIP.TextChanged += new System.EventHandler(this.TbServerIP_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(100, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Title
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 236);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.lbNickName);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.tbNickName);
            this.Controls.Add(this.btnLogin);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Title";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Title_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbNickName;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.Label lbNickName;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}