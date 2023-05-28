﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System.Threading;
using System.IO;

public class ClientHandle : MonoBehaviour
{
    [SerializeField] TextAsset JSONHistory;

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UDPTest(Packet _packet)
    {
        string msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP which contains the message: {msg}");
        ClientSend.UDPTestReceived();
    }

    public static void WrongSentCode(Packet _packet)
    {
        string wrongCode = _packet.ReadString();

        Debug.Log($"Server has not found such furniture with such code as {wrongCode}");
    }

    public static void DownloadFurniture(Packet _packet)
    {
        string code = _packet.ReadString();
        string urlAnim = _packet.ReadString();
        string urlLogo = _packet.ReadString();
        string urlDescr = _packet.ReadString();

        string furnitPath = Application.dataPath + @"/database/" + code;

        if (!Directory.Exists(furnitPath))
        {
            Directory.CreateDirectory(furnitPath);
            Debug.Log($"New directory {code} in database has been created");
        } else {
            Debug.Log($"Directory {code} in database already exists");
        }

        Thread animDownloadThread = new Thread(() => Download.DownloadAnimation(urlAnim, furnitPath));
        Thread logoDownloadThread = new Thread(() => Download.DownloadLogo(urlLogo, furnitPath));
        Thread descrDownloadThread = new Thread(() => Download.DownloadDescription(urlDescr, furnitPath));

        Debug.Log($"Received an animation url \"{urlAnim}\" to download the animation\nDownloading started!");
        animDownloadThread.Start();
        Debug.Log($"Received a logo's url \"{urlLogo}\" to download the logo \nDownloading started!");
        logoDownloadThread.Start();
        Debug.Log($"Received a description's url \"{urlDescr}\" to download the description \nDownloading started!");
        descrDownloadThread.Start();

        // AFTER ALL THREADS FINISHES
        Debug.Log($"Download complete????????????????? TO FIX!!");
        HistoryFileMngr.AddID(code);
        // --------------------

    }

    public static void ReceiveNews(Packet _packet)
    {
        int numberOfNews = _packet.ReadInt();

        for (int i = 1; i <= numberOfNews; i++)
        {
            string descriptionURL = _packet.ReadString();
            string logoURL = _packet.ReadString();

            string newsPlaceholderPath = Application.dataPath + @"/news/" + i.ToString();

            if (!Directory.Exists(newsPlaceholderPath))
            {
                Directory.CreateDirectory(newsPlaceholderPath);
                Debug.Log($"New directory {i.ToString()} in news has been created");
            }
            else
            {
                Debug.Log($"Directory {i.ToString()} in news already exists");
            }

            Thread logoDownloadThread = new Thread(() => Download.DownloadLogo(logoURL, newsPlaceholderPath));
            Thread descrDownloadThread = new Thread(() => Download.DownloadDescription(descriptionURL, newsPlaceholderPath));

            Debug.Log($"Received a logo's url \"{logoURL}\" to download the logo \nDownloading started!");
            logoDownloadThread.Start();
            Debug.Log($"Received a description's url \"{descriptionURL}\" to download the description \nDownloading started!");
            descrDownloadThread.Start();

            Debug.Log($"Download complete????????????????? TO FIX!!");
        }
    }
}