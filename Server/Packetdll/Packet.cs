using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Packetdll
{
    public enum PacketType : int
    {
        Login = 0,
        Login_RESULT,
        Member_REGISTER,
        Member_REGISTER_RESULT
    }
    public class Packet
    {
        public int packet_Type;
        public int packet_Length;

        public Packet()
        {
            this.packet_Type = 0;
            this.packet_Length = 0;
        }
        public static byte[] Serialize(Object data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(1024 * 4); // packet size will be maximum 4k
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, data);
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static Object Deserialize(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(1024 * 4);
                ms.Write(data, 0, data.Length);

                ms.Position = 0;
                BinaryFormatter bf = new BinaryFormatter();
                Object obj = bf.Deserialize(ms);
                ms.Close();
                return obj;
            }
            catch
            {
                return null;
            }
        }
    }
    [Serializable]
    public class Login : Packet
    {
        public string id_str { get; set; }
        public string pw_str { get; set; }

    }
}
