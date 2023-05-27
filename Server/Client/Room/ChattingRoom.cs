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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Room
{
    public partial class ChattingRoom : Form
    {
        Thread m_tHandler = null;
        public ChattingRoom()
        {
            InitializeComponent();

            m_tHandler = new Thread(GetPacket);
            m_tHandler.Start();
        }

        private void ChattingRoom_Load(object sender, EventArgs e)
        {

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] buff = new byte[1024];
            Chatting chat = new Chatting();
            chat.chat = textBox2.Text;
            Packet.Serialize(chat).CopyTo(buff, 0);
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
        private void GetPacket()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                SocketManager.GetInst().Stream.Read(buffer, 0, buffer.Length);

                Packet packet = (Packet)Packet.Deserialize(buffer);
                if (packet.packet_Type == PacketType.Chatting_Result)
                {
                    ChattingResult pkChattingResult = (ChattingResult)packet;
                    DisplayText(pkChattingResult.chat);
                }


            }
        }
    }
}
