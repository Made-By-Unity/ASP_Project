using Client.Game;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Room
{
    public partial class Lobby : Form
    {
        Thread m_tHandler;
        ChattingRoom m_fChat;

        public ChattingRoom ChattingRoom
        {
            get { return m_fChat; }
            set { m_fChat = value; }
        }
        

        public Lobby()
        {
            InitializeComponent();

            ChattingRoom = new ChattingRoom();
            ChattingRoom.Show();

            // 0번 플레이어(방장) 시 시작 버튼 활성화
            if (0 == SocketManager.GetInst().UID)
            {
                btnGameStart.Enabled = true;
            }

            // 플레이어 닉네임 작성
            UpdatePlayer();

            cbGame.SelectedIndex = 0;
            m_tHandler = new Thread(GetPacket);
            m_tHandler.IsBackground = true;
            m_tHandler.Start();
        }

        private void GetPacket()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                SocketManager.GetInst().Stream.Read(buffer, 0, buffer.Length);

                Packet packet = (Packet)Packet.Deserialize(buffer);
                if (packet.packet_Type == PacketType.Login_Result)
                {
                    LoginResult pkLoginResult = (LoginResult)packet;
                    SocketManager.GetInst().NickNameList = pkLoginResult.usernames;

                    if(m_fChat != null)
                        m_fChat.DisplayText(pkLoginResult.LoginMessage);

                    UpdatePlayer();
                }
                else if (packet.packet_Type == PacketType.Disconnect_Result)
                {
                    DisconnectResult pkDisconncetResult = (DisconnectResult)packet;
                    SocketManager.GetInst().NickNameList.Remove(pkDisconncetResult.UserName);

                    if (m_fChat != null)
                        m_fChat.DisplayText(pkDisconncetResult.chat);

                    UpdatePlayer();
                }
                else if(packet.packet_Type == PacketType.Entry_Result)
                {
                    EntryResult pkEntryResult = (EntryResult)packet;
                    
                    if(1 == pkEntryResult.kindOfGame)
                    {
                        YachtDice fYacht = new YachtDice();
                        fYacht.Lobby = this;
                        fYacht.ChattingRoom = ChattingRoom;
                        this.Invoke(new Action(()=>this.Visible = false));
                        
                        Application.Run(fYacht);
                    }
                    else if(2 == pkEntryResult.kindOfGame)
                    {
                        Knucklebone fKB = new Knucklebone();
                        fKB.Lobby = this;
                        this.Invoke(new Action(() => this.Visible = false));

                        Application.Run(fKB);
                    }
                    
                }
                else if(packet.packet_Type == PacketType.Chatting_Result)
                {
                    ChattingResult pkChattingResult = (ChattingResult)packet;

                    if(m_fChat != null)
                        ChattingRoom.DisplayText(pkChattingResult.chat);
                }
            }
        }

        public void Return()
        {
            this.Invoke(new Action(() => {
                this.Visible = true;
                m_tHandler = new Thread(GetPacket);
                m_tHandler.Start();
            }));            
        }

        private void UpdatePlayer()
        {
            List<string> listNickName = SocketManager.GetInst().NickNameList;

            if(this.IsHandleCreated)
            {
                tbPlayer1.Invoke(new Action(() => { tbPlayer1.Text = ""; }));
                tbPlayer2.Invoke(new Action(() => { tbPlayer2.Text = ""; }));
                tbPlayer3.Invoke(new Action(() => { tbPlayer3.Text = ""; }));
                tbPlayer4.Invoke(new Action(() => { tbPlayer4.Text = ""; }));
            }
            
            for (int i = 0; i < listNickName.Count; i++)
            {
                TextBox tbPlayer = (Controls.Find("tbPlayer" + (i + 1).ToString(), true)[0] as TextBox);

                if(this.IsHandleCreated)
                {
                    tbPlayer.Invoke(new MethodInvoker(delegate
                    {
                        tbPlayer.Text = listNickName[i];
                    }));
                }
                else
                {
                    tbPlayer.Text = listNickName[i];
                }
            }
        }

        private void btnGameStart_Click(object sender, EventArgs e)
        {
            string strSelected = cbGame.SelectedItem.ToString();

            byte[] buff = new byte[1024];
            Entry pkEntry = new Entry();
            pkEntry.packet_Type = PacketType.Entry;

            if ("Yacht Dice" == strSelected)
            {
                pkEntry.kindOfGame = 1;
            }
            else if("Knucklebone" == strSelected)
            {
                pkEntry.kindOfGame = 2;
            }

            Packet.Serialize(pkEntry).CopyTo(buff, 0);
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();
        }

        private void Lobby_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_tHandler.Abort();
        }

        private void CbGame_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
