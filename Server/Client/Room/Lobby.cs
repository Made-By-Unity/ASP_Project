using Client.Manager;
using Packetdll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Room
{
    public partial class Lobby : Form
    {
        Thread m_tHandler;

        public Lobby()
        {
            InitializeComponent();

            // 1번 플레이어(방장) 시 시작 버튼 활성화
            if(1 == SocketManager.GetInst().UID)
            {
                btnGameStart.Enabled = true;
            }

            // 플레이어 닉네임 작성
            UpdatePlayer();

            m_tHandler = new Thread(GetPacket);
            m_tHandler.Start();
        }

        private void GetPacket()
        {
            while (true)
            {
                byte[] buffer = new byte[1024 * 4];
                SocketManager.GetInst().Stream.Read(buffer, 0, buffer.Length);

                Packet packet = (Packet)Packet.Deserialize(buffer);
                if (packet.packet_Type == PacketType.Login_Result)
                {
                    LoginResult pkLoginResult = (LoginResult)Packet.Deserialize(buffer);
                    SocketManager.GetInst().NickNameList = pkLoginResult.usernames;
                    UpdatePlayer();
                }
                else if(packet.packet_Type == PacketType.Entry_Result)
                {
                    EntryResult pkEntryResult = (EntryResult)Packet.Deserialize(buffer);
                    
                    if(1 == pkEntryResult.kindOfGame)
                    {
                        Application.Run(new YachtDice());
                    }
                }

            }
        }

        private void UpdatePlayer()
        {
            List<string> listNickName = SocketManager.GetInst().NickNameList;
            for (int i = 0; i < listNickName.Count; i++)
            {
                TextBox tbPlayer = (Controls.Find("tbPlayer" + (i + 1).ToString(), true)[0] as TextBox);
                tbPlayer.Text = listNickName[i];
            }
        }

        private void btnGameStart_Click(object sender, EventArgs e)
        {
            string strSelected = lbGameList.SelectedItem.ToString();

            byte[] buff = new byte[1024 * 4];
            Entry pkEntry = new Entry();
            pkEntry.packet_Type = PacketType.Entry;

            if ("Yacht Dice" == strSelected)
            {
                pkEntry.kindOfGame = 1;
            }

            Packet.Serialize(pkEntry).CopyTo(buff, 0);
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();

            this.Visible = false;
        }
    }
}
