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



namespace Server
{
    public partial class Form1 : Form
    {
        TcpListener server = null;
        TcpClient client = null;
        Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();
        static uint num = 0;


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

        private void Init()
        {
            server = new TcpListener(IPAddress.Any, 5417);
            client = default(TcpClient);
            server.Start();
            ShowMessage("서버 오픈");

            while (true)
            {
                num++;
                client = server.AcceptTcpClient();
                ShowMessage("> Connection From Client");

                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string nickName = Encoding.Unicode.GetString(buffer, 0, bytes);
                nickName = nickName.Substring(0, nickName.IndexOf("$"));
                clientList.Add(client, nickName);
                SendMessageAll(nickName + " 님이 참여하셨습니다. ", "", false);
                CClient c_Client = new CClient();
                c_Client.EDisconnected += new CClient.DisconnectedHandler(Disconnected);
                c_Client.EReceived += new CClient.ShowTextHandler(OnReceived);
                c_Client.StartClient(client, clientList);
            }
            client.Close();
            server.Stop();
        }
        private void OnReceived(string msg, string nickName)
        {
            string str = "From client " + nickName + " : " + msg;
            ShowMessage(str);
            SendMessageAll(msg, nickName, true);
        }
        private void Disconnected(TcpClient clientSocket)
        {
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

        public void SendMessageAll(string msg, string nickName, bool b)
        {
            foreach (KeyValuePair<TcpClient, string> pair in clientList)
            {
                TcpClient client = pair.Key as TcpClient;
                NetworkStream nStream = client.GetStream();
                byte[] buff = null;
                if (b)
                {
                    buff = Encoding.Unicode.GetBytes(nickName + " : " + msg);
                }
                else
                {
                    buff = Encoding.Unicode.GetBytes(msg);
                }
                nStream.Write(buff, 0, buff.Length);
                nStream.Flush();
            }
        }
    }


    class CClient
    {
        TcpClient clientSocket = null;
        public Dictionary<TcpClient, string> clientList = null;
        public delegate void DisconnectedHandler(TcpClient clientSocket);
        public event DisconnectedHandler EDisconnected;
        public delegate void ShowTextHandler(string text, string nickName);
        public event ShowTextHandler EReceived;
        public void StartClient(TcpClient socket, Dictionary<TcpClient, string> List)
        {
            clientSocket = socket;
            clientList = List;
            Thread t = new Thread(ChattingSystem);
            t.IsBackground = true;
            t.Start();
        }

        private void ChattingSystem()
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
                    msg = Encoding.Unicode.GetString(buff, 0, bytes);
                    msg = msg.Substring(0, msg.IndexOf("$"));
                    if (EReceived != null)
                        EReceived(msg, clientList[clientSocket].ToString());
                }
            }
            catch (SocketException e)
            {
                if (clientSocket != null)
                {
                    if (EDisconnected != null)
                        EDisconnected(clientSocket);

                    clientSocket.Close();
                    nStream.Close();
                }
            }
            catch (Exception e)
            {
                if (clientSocket != null)
                {
                    if (EDisconnected != null)
                        EDisconnected(clientSocket);
                    clientSocket.Close();
                    nStream.Close();
                }
            }
        }
    }
}
