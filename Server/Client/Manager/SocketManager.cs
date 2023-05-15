using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Manager
{
    internal class SocketManager
    {
        private static SocketManager m_Inst; 

        private TcpClient m_ClientSocket = null;
        private NetworkStream m_Stream = null;
        private string m_strNickName;

        private SocketManager()
        {
            m_ClientSocket = new TcpClient();
        }

        public static SocketManager GetInst()
        {
            if (null == m_Inst)
                m_Inst = new SocketManager();

            return m_Inst;
        }

        public TcpClient Socket
        {
            get { return m_ClientSocket; }
        }

        public string NickName
        {
            get { return m_strNickName; }
        }

        public void Binding(string _strNickName)
        {
            m_ClientSocket.Connect("192.168.0.6", 9999);
            m_Stream = m_ClientSocket.GetStream();

            m_strNickName = _strNickName;
            byte[] buffer = Encoding.Unicode.GetBytes(_strNickName + "$");
            m_Stream.Write(buffer, 0, buffer.Length);
            m_Stream.Flush();
        }
    }
}
