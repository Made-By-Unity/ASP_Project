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
        public Login() { packet_Type = PacketType.Login; }
        public string nickName { get; set; }

    }
    [Serializable]
    public class LoginResult : Packet
    {
        public LoginResult() { packet_Type = PacketType.Login_Result; }
        public uint uID { get; set; }
        public List<string> usernames { get; set; }
        public string LoginMessage { get; set; }
    }
    [Serializable]
    public class Entry : Packet
    {
        public Entry() { packet_Type = PacketType.Entry; }
        public int kindOfGame { get; set; }
    }
    [Serializable]
    public class EntryResult : Packet
    {
        public EntryResult() { packet_Type = PacketType.Entry_Result; }
        public uint maxPlayerCount { get; set; }
        public int kindOfGame { get; set; }
    }
    [Serializable]
    public class RollStart : Packet
    {
        public RollStart() { packet_Type = PacketType.RollStart; }
        public int remainRollCount { get; set; }
    }
    [Serializable]
    public class RollStartResult : Packet
    {
        public RollStartResult() { packet_Type = PacketType.RollStart_Result; }
        public int remainRollCount { get; set; }
    }
    [Serializable]
    public class RollEnd : Packet
    {
        public RollEnd() { packet_Type = PacketType.RollEnd; }
        public int dice1 { get; set; }
        public int dice2 { get; set; }
        public int dice3 { get; set; }
        public int dice4 { get; set; }
        public int dice5 { get; set; }
    }
    [Serializable]
    public class RollEndResult : Packet
    {
        public RollEndResult() { packet_Type = PacketType.RollEnd_Result; }
        public int dice1 { get; set; }
        public int dice2 { get; set; }
        public int dice3 { get; set; }
        public int dice4 { get; set; }
        public int dice5 { get; set; }
    }
    [Serializable]
    public class Lock : Packet
    {
        public Lock() { packet_Type = PacketType.Lock; }
        public int lockNumber { get; set; }
        public bool isLock { get; set; }
    }
    [Serializable]
    public class LockResult : Packet
    {
        public LockResult() { packet_Type = PacketType.Lock_Result; }
        public int lockNumber { get; set; }
        public bool isLock { get; set; }
    }
    [Serializable]
    public class Select : Packet
    {
        public Select() { packet_Type = PacketType.Select; }
        //Enum 알아서 정의해주세요
    }
    public class SelectResult : Packet
    {
        public SelectResult() { packet_Type = PacketType.Select_Result; }
        //Enum 알아서 정의해주세요
    }
    [Serializable]
    public class GameOver : Packet
    {
        public GameOver() { packet_Type = PacketType.GameOver; }
        public string result { get; set; }
    }
    [Serializable]
    public class GameOverResult : Packet
    {
        public GameOverResult() { packet_Type = PacketType.GameOver_Result; }
        public string result { get; set; }
    }
    [Serializable]
    public class Chatting : Packet
    {
        public Chatting() { packet_Type = PacketType.Chatting; }
        public string chat { get; set; }
    }
    [Serializable]
    public class ChattingResult : Packet
    {
        public ChattingResult() { packet_Type = PacketType.Chatting_Result; }
        public string chat { get; set; }
    }
    [Serializable]
    public class Disconnect : Packet
    {
        public Disconnect() { packet_Type = PacketType.Disconnect; }
        public string chat { get; set; }
    }
}
