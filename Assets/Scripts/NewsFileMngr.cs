using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsFileMngr
{
    static string newsFilePath = Application.dataPath + @"/news";

    public static int GetNumberOfDirectories()
    {
        int directoryCount = System.IO.Directory.GetDirectories(newsFilePath).Length;
        return directoryCount;
    }
}
