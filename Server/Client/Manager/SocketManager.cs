using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Manager
{
    internal class SocketManager
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default(NetworkStream);

        public void Binding()
        {
            clientSocket.Connect("192.168.0.6", 9999);
            stream = clientSocket.GetStream();

            message = "Connected to Chat Server";
            DisplayText(message);

            byte[] buffer = Encoding.Unicode.GetBytes(this.textBoxNickName.Text + "$");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();

            Thread t_handler = new Thread(GetMessage);
            t_handler.IsBackground = true;
            t_handler.Start();
        }
    }
}
