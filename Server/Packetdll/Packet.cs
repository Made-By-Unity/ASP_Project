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
        Login_Result,
        Entry,
        Entry_Result,
        RollStart,
        RollStart_Result,
        RollEnd,
        RollEnd_Result,
        Lock,
        Lock_Result,
        Select,
        Select_Result,
        GameOver,
        GameOver_Result,
        Chatting,
        Chatting_Result,
        Disconnect,
    }
    [Serializable]
    public class Packet
    {
        public PacketType packet_Type;
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
        public string nickName { get; set; }

    }
    [Serializable]
    public class LoginResult : Packet
    {
        public uint uID { get; set; }
        public List<string> usernames { get; set; }
        public string LoginMessage { get; set; }
    }
    [Serializable]
    public class Entry : Packet
    {
        public int kindOfGame { get; set; }
    }
    [Serializable]
    public class EntryResult : Packet
    {
        public uint maxPlayerCount { get; set; }
        public int kindOfGame { get; set; }
    }
    [Serializable]
    public class RollStart : Packet
    {
        public int remainRollCount { get; set; }
    }
    [Serializable]
    public class RollStartResult : Packet
    {
        public int remainRollCount { get; set; }
    }
    [Serializable]
    public class RollEnd : Packet
    {
        public int dice1 { get; set; }
        public int dice2 { get; set; }
        public int dice3 { get; set; }
        public int dice4 { get; set; }
        public int dice5 { get; set; }
    }
    [Serializable]
    public class RollEndResult : Packet
    {
        public int dice1 { get; set; }
        public int dice2 { get; set; }
        public int dice3 { get; set; }
        public int dice4 { get; set; }
        public int dice5 { get; set; }
    }
    [Serializable]
    public class Lock : Packet
    {
        public int lockNumber { get; set; }
        public bool isLock { get; set; }
    }
    [Serializable]
    public class LockResult : Packet
    {
        public int lockNumber { get; set; }
        public bool isLock { get; set; }
    }
    [Serializable]
    public class Select : Packet
    {
        //Enum 알아서 정의해주세요
    }
    public class SelectResult : Packet
    {
        //Enum 알아서 정의해주세요
    }
    [Serializable]
    public class GameOver : Packet
    {
        public string result { get; set; }
    }
    [Serializable]
    public class GameOverResult : Packet
    {
        public string result { get; set; }
    }
    [Serializable]
    public class Chatting : Packet
    {
        public string chat { get; set; }
    }
    [Serializable]
    public class ChattingResult : Packet
    {
        public string chat { get; set; }
    }
    [Serializable]
    public class Disconnect : Packet
    {
        public string chat { get; set; }
    }
}
