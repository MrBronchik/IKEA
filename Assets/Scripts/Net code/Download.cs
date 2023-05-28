using System;
using System.Net;
using System.IO;
using UnityEngine;

public class Download
{
    public static void DownloadAnimation(string _urlDownload, string _urlPlaceholder)
    {
        WebClient client = new WebClient();
        client.DownloadFile(_urlDownload, _urlPlaceholder + @"/animation.prefab");
    }

    public static void DownloadLogo(string _urlDownload, string _urlPlaceholder)
    {
        WebClient client = new WebClient();
        client.DownloadFile(_urlDownload, _urlPlaceholder + @"/logo.jpg");
    }

    public static void DownloadDescription(string _urlDownload, string _urlPlaceholder)
    {
        WebClient client = new WebClient();
        client.DownloadFile(_urlDownload, _urlPlaceholder + @"/description.txt");
    }
}
