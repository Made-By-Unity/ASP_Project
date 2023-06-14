using Packetdll;
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
        private string m_strNickName = String.Empty;
        private uint m_iID;
        private List<string> m_listNickName = new List<string>();

        private SocketManager()
        {
            //m_ClientSocket = new TcpClient();
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

        public Stream Stream
        {
            get { return m_Stream; }
        }

        public string NickName
        {
            get { return m_strNickName; }
        }

        public uint UID
        {
            get { return m_iID; }
            set { m_iID = value; }
        }
        public List<string> NickNameList
        {
            get { return m_listNickName; }
            set { m_listNickName = value; }
        }

        public bool Binding(string _strServerIP, string _strNickName)
        {
            //m_ClientSocket.Connect(_strServerIP, 9999);

            try
            {
                m_ClientSocket = new TcpClient(_strServerIP, 9999);
            }
            catch
            {
                return false;
            }

            m_Stream = m_ClientSocket.GetStream();
            m_strNickName = _strNickName;

            byte[] buff = new byte[1024 * 4];
            Login pkLogin = new Login();
            pkLogin.packet_Type = PacketType.Login;
            pkLogin.nickName = m_strNickName;
            Packet.Serialize(pkLogin).CopyTo(buff, 0);
            m_Stream.Write(buff, 0, buff.Length);
            m_Stream.Flush();
            return true;
        }
    }
}
