using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packetdll;
using Client.Manager;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Client.Room;

namespace Client.Title
{
    public partial class Title : Form
    {
        Thread m_tHandler;

        public Title()
        {
            InitializeComponent();
        }
        private void GetPacket()
        {
            while (true)
            {
                byte[] buffer = new byte[1024 * 4];
                SocketManager.GetInst().Stream.Read(buffer, 0, buffer.Length);

                Packet packet = (Packet)Packet.Deserialize(buffer);
                if (packet.packet_Type != PacketType.Login_Result)
                    continue;

                LoginResult pkLoginResult = (LoginResult)Packet.Deserialize(buffer);
                SocketManager.GetInst().UID = pkLoginResult.uID;
                SocketManager.GetInst().NickNameList = pkLoginResult.usernames;

                DialogResult dr = MessageBox.Show(pkLoginResult.LoginMessage, "어서오세요!", MessageBoxButtons.OK);
                if(dr == DialogResult.OK)
                {
                    OpenLobby();
                }
            }
        }

        private void OpenLobby()
        {
            Lobby lobby = null;

            Thread thread = new Thread(() =>
            {
                lobby = new Lobby();
                Application.Run(lobby);
            });

            thread.Start();

            while(lobby == null || !lobby.IsHandleCreated || !lobby.Visible)
            {
                Thread.Sleep(10);
            }

            this.Invoke(new MethodInvoker(delegate { this.Close(); }));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SocketManager.GetInst().Binding(tbServerIP.Text, tbNickName.Text);

            m_tHandler = new Thread(GetPacket);
            m_tHandler.IsBackground = true;
            m_tHandler.Start();
        }
    }
}
