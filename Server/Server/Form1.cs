using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
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
        static int counter = 0;
        public Form1()
        {
            InitializeComponent();
            // socket start
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void InitSocket()
        {
            server = new TcpListener(IPAddress.Any, 9999);
            client = default(TcpClient);
            server.Start();
            DisplayText(">> Server Started");

            while (true)
            {
                try
                {
                    counter++;
                    client = server.AcceptTcpClient();
                    DisplayText(">> Accept connection from client");

                    handleClient h_client = new handleClient();
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(DisplayText);
                    h_client.decreaseCount += new handleClient.ClientCounter(DecreaseCount);
                    h_client.startClient(client, counter);
                }
                catch (SocketException se)
                {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                }
            }
        }

        private void DecreaseCount()
        {
            counter--;
        }

        private void DisplayText(string text)
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }

            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }
    }
}
class handleClient
{
    TcpClient clientSocket;
    int i_ClientNumber;

    public void startClient(TcpClient ClientSocket, int clientNo)
    {
        this.clientSocket = ClientSocket;
        this.i_ClientNumber = clientNo;

        Thread t_hanlder = new Thread(Chat);
        t_hanlder.IsBackground = true;
        t_hanlder.Start();
    }

    public delegate void MessageDisplayHandler(string text);
    public event MessageDisplayHandler OnReceived;

    public delegate void ClientCounter();
    public event ClientCounter decreaseCount;

    private void Chat()
    {
        NetworkStream stream = null;
        try
        {
            byte[] buffer = new byte[1024];
            string msg = string.Empty;
            int bytes = 0;
            int MessageCount = 0;

            while (true)
            {
                MessageCount++;
                stream = clientSocket.GetStream();
                bytes = stream.Read(buffer, 0, buffer.Length);
                msg = Encoding.Unicode.GetString(buffer, 0, bytes);
                msg = msg.Substring(0, msg.IndexOf("$"));
                msg = "Data Received : " + msg;

                if (OnReceived != null)
                    OnReceived(msg);

                msg = "Server to client(" + i_ClientNumber.ToString() + ") " + MessageCount.ToString();
                if (OnReceived != null)
                    OnReceived(msg);

                byte[] sbuffer = Encoding.Unicode.GetBytes(msg);
                stream.Write(sbuffer, 0, sbuffer.Length);
                stream.Flush();

                msg = " >> " + msg;
                if (OnReceived != null)
                {
                    OnReceived(msg);
                    OnReceived("");
                }
            }
        }
        catch (SocketException se)
        {
            Trace.WriteLine(string.Format("doChat - SocketException : {0}", se.Message));

            if (clientSocket != null)
            {
                clientSocket.Close();
                stream.Close();
            }

            if (decreaseCount != null)
                decreaseCount();
        }
        catch (Exception ex)
        {
            Trace.WriteLine(string.Format("doChat - Exception : {0}", ex.Message));

            if (clientSocket != null)
            {
                clientSocket.Close();
                stream.Close();
            }

            if (decreaseCount != null)
                decreaseCount();
        }
    }
}
