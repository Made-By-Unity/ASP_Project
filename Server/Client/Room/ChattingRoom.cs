using Client.Manager;
using Packetdll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Room
{
    public partial class ChattingRoom : Form
    {
        public ChattingRoom()
        {
            InitializeComponent();
        }

        private void ChattingRoom_Load(object sender, EventArgs e)
        {

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] buff = new byte[1024 * 4];
            Chatting chat = new Chatting();
            chat.chat = textBox2.Text;
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();
            
            textBox2.Text = "";
            textBox2.Focus();
        }
        public void DisplayText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    textBox1.AppendText(text + Environment.NewLine);
                }));
            }
            else
                textBox1.AppendText(text + Environment.NewLine);
        }
    }
}
