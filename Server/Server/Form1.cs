using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Packetdll;
using System.IO;


namespace Server
{
    public partial class Form1 : Form
    {
        TcpListener server = null;
        TcpClient client = null;
        Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();
        static uint num = 0;
        List<string> nickNames = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Thread thr = new Thread(Init);
            thr.IsBackground = true;
            thr.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }
        private void Init()
        {
            server = new TcpListener(IPAddress.Any, 9999);
            client = default(TcpClient);
            server.Start();
            ShowMessage("서버 오픈");
            while (true)
            {
                
                client = server.AcceptTcpClient();
                ShowMessage("> Connection From Client");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);

                Packet packet = (Packet)Packet.Deserialize(buffer);
                if (packet.packet_Type != PacketType.Login)
                    continue;
                Login login = (Login)Packet.Deserialize(buffer);

                //string nickName = Encoding.Unicode.GetString(buffer, 0, bytes);
                //nickName = nickName.Substring(0, nickName.IndexOf("$"));
                string nickName = login.nickName;
                clientList.Add(client, nickName);
                nickNames.Add(nickName);   
                SendMessageAll(nickName + " 님이 참여하셨습니다. ", "", false, packet);
                CClient c_Client = new CClient();
                c_Client.EDisconnected += new CClient.DisconnectedHandler(Disconnected);
                c_Client.EReceived += new CClient.ShowTextHandler(OnReceived);
                c_Client.StartClient(client, clientList);
                num++;
            }
        }
        private void OnReceived(string msg, string nickName, Packet packet)
        {
            string str = "From client " + nickName + " : " + msg;
            ShowMessage(str);
            SendMessageAll(msg, nickName, true, packet);
        }
        private void Disconnected(TcpClient clientSocket, string User)
        {
            
            Disconnect packet=new Disconnect();
            packet.packet_Type = PacketType.Disconnect;
            packet.UserName = User;
            SendMessageAll(User + "님이 접속을 종료했습니다.", "", false, packet);
            //nickList에서 지워주기
            for(int i=0;i<nickNames.Count;i++)
            {
                if (nickNames[i]== User)
                {
                    nickNames.RemoveAt(i);
                    break;
                }
            }
            num--;
            if (clientList.ContainsKey(clientSocket))
                clientList.Remove(clientSocket);
        }
        private void ShowMessage(string text)
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

        public void SendMessageAll(string msg, string nickName, bool b, Packet packet)
        {
            byte[] buff = new byte[1024];
            switch (packet.packet_Type)
            {
                case PacketType.Entry:
                    {
                        //Entry entry = (Entry)Packet.Deserialize(buff);
                        Entry entry = (Entry)packet;
                        EntryResult entryResult = new EntryResult();
                        entryResult.packet_Type = PacketType.Entry_Result;
                        entryResult.maxPlayerCount = num;
                        entryResult.kindOfGame = entry.kindOfGame;
                        Packet.Serialize(entryResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.RollStart:
                    {
                        //RollStart rollStart = (RollStart)Packet.Deserialize(buff);
                        RollStart rollStart = (RollStart)packet;
                        RollStartResult rollStartResult = new RollStartResult();
                        rollStartResult.remainRollCount = rollStart.remainRollCount;
                        Packet.Serialize(rollStartResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.RollEnd:
                    {
                        //RollEnd rollEnd = (RollEnd)Packet.Deserialize(buff);
                        RollEnd rollEnd = (RollEnd)packet;
                        RollEndResult rollEndResult = new RollEndResult();
                        rollEndResult.dice1 = rollEnd.dice1;
                        rollEndResult.dice2 = rollEnd.dice2;
                        rollEndResult.dice3 = rollEnd.dice3;
                        rollEndResult.dice4 = rollEnd.dice4;
                        rollEndResult.dice5 = rollEnd.dice5;
                        Packet.Serialize(rollEndResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.Lock:
                    {
                        //Lock clock = (Lock)Packet.Deserialize(buff);
                        Lock clock = (Lock)packet;
                        LockResult lockResult = new LockResult();
                        lockResult.lockNumber = clock.lockNumber;
                        lockResult.isLock = clock.isLock;
                        Packet.Serialize(lockResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.Select:
                    {
                        //Select cselect = (Select)Packet.Deserialize(buff);
                        Select cselect = (Select)packet;
                        SelectResult selectResult = new SelectResult();
                        selectResult.eScoreType = cselect.eScoreType;
                        selectResult.iScore = cselect.iScore;
                        Packet.Serialize(selectResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.GameOver:
                    {
                        //GameOver gameover = (GameOver)Packet.Deserialize(buff);
                        GameOver gameover = (GameOver)packet;
                        GameOverResult gameoverResult = new GameOverResult();
                        gameoverResult.result = gameover.result;
                        Packet.Serialize(gameoverResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.Chatting:
                    {
                        ChattingResult chattingResult = new ChattingResult();
                        chattingResult.chat = nickName+" : "+msg;
                        Packet.Serialize(chattingResult).CopyTo(buff, 0);
                    }
                    break;

                case PacketType.K_RollStart:
                    {
                        K_RollStart rollStart = (K_RollStart)packet;
                        K_RollStartResult rollStartResult = new K_RollStartResult();
                        Packet.Serialize(rollStartResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.K_RollEnd:
                    {
                        K_RollEnd rollEnd = (K_RollEnd)packet;
                        K_RollEndResult rollEndResult = new K_RollEndResult();
                        rollEndResult.result = rollEnd.result;
                        Packet.Serialize(rollEndResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.K_Select:
                    {
                        K_Select select = (K_Select)packet;
                        K_SelectResult selectResult = new K_SelectResult();
                        //Enum 정리
                        Packet.Serialize(selectResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.K_GameOver:
                    {
                        K_GameOver gameover = (K_GameOver)packet;
                        K_GameOverResult gameoverResult = new K_GameOverResult();
                        gameoverResult.result = gameover.result;
                        Packet.Serialize(gameoverResult).CopyTo(buff, 0);
                    }
                    break;
                case PacketType.Login:
                    {
                        LoginResult loginResult = new LoginResult();
                        loginResult.packet_Type = PacketType.Login_Result;
                        loginResult.uID = num;
                        loginResult.LoginMessage = msg;
                        loginResult.usernames = nickNames;
                        Packet.Serialize(loginResult).CopyTo(buff, 0);
                        //buff = Encoding.Unicode.GetBytes(msg);

                    }
                    break;
                case PacketType.Disconnect:
                    {
                        DisconnectResult disconnectResult = new DisconnectResult();
                        disconnectResult.UserName = ((Disconnect)packet).UserName;
                        disconnectResult.chat = msg;
                        Packet.Serialize(disconnectResult).CopyTo(buff, 0);
                    }
                    break;
            }
            foreach (KeyValuePair<TcpClient, string> pair in clientList)
            {
                TcpClient client = pair.Key as TcpClient;
                NetworkStream nStream = client.GetStream();
                
                nStream.Write(buff, 0, buff.Length);
                nStream.Flush();
                
            }
        }
    }


    class CClient
    {
        TcpClient clientSocket = null;
        public Dictionary<TcpClient, string> clientList = null;
        public delegate void DisconnectedHandler(TcpClient clientSocket, string User);
        public event DisconnectedHandler EDisconnected;
        public delegate void ShowTextHandler(string text, string nickName, Packet packet);
        public event ShowTextHandler EReceived;
        string sName;
        public void StartClient(TcpClient socket, Dictionary<TcpClient, string> List)
        {
            clientSocket = socket;
            clientList = List;
            Thread t = new Thread(Broadcast);
            t.IsBackground = true;
            t.Start();
            sName = clientList[clientSocket].ToString();
        }

        private void Broadcast()
        {
            NetworkStream nStream = null;
            try
            {
                byte[] buff = new byte[1024];
                string msg = string.Empty;
                int bytes = 0;
                while (true)
                {
                    nStream = clientSocket.GetStream();
                    bytes = nStream.Read(buff, 0, buff.Length);
                    //msg = Encoding.Unicode.GetString(buff, 0, bytes);
                    //msg = msg.Substring(0, msg.IndexOf("$"));
                    Packet packet = (Packet)Packet.Deserialize(buff);
                    if (packet == null)
                        continue;
                    if (packet.packet_Type == PacketType.Chatting)
                    {
                        Chatting chatting = (Chatting)Packet.Deserialize(buff);
                        msg = chatting.chat;
                    }
                    if (EReceived != null)
                        EReceived(msg, clientList[clientSocket].ToString(), packet);
                }
            }
            catch (SocketException e)
            {
                if (clientSocket != null)
                {
                    if (EDisconnected != null)
                        EDisconnected(clientSocket, sName);

                    clientSocket.Close();
                    nStream.Close();
                }
            }
            catch (Exception e)
            {
                if (clientSocket != null)
                {
                    if (EDisconnected != null)
                        EDisconnected(clientSocket, sName);
                    clientSocket.Close();
                    nStream.Close();
                }
            }
        }
    }
}
