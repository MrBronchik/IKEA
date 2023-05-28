using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("tom");

            SendTCPData(_packet);
        }
    }

    public static void UDPTestReceived()
    {
        using (Packet _packet = new Packet((int) ClientPackets.udpTestReceived))
        {
            _packet.Write("Received a UDP packet");

            SendUDPData(_packet);
        }
    }

    public static void SendFurnitureCode(TMP_InputField _IFFurnituresCode)
    {
        using (Packet _packet = new Packet((int) ClientPackets.sendFurnituresCode))
        {
            _packet.Write(_IFFurnituresCode.text);

            SendTCPData(_packet);
            Debug.Log($"Furnitures code \"{_IFFurnituresCode.text}\" has been sent");
        }
    }

    public static void GetNews()
    {
        using (Packet _packet = new Packet((int) ClientPackets.getNews))
        {
            _packet.Write("Asking for news section!");

            SendTCPData(_packet);
            Debug.Log($"Client send a query for news section.");
        }
    }
    #endregion
}
