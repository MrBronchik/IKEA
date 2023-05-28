using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryFileMngr
{
    static string historyFilePath = Application.dataPath + @"/history.txt";

    public static string[] GetData()
    {
        return System.IO.File.ReadAllLines(historyFilePath);
    }

    public static void AddID(string _id)
    {
        foreach (string id in GetData())
        {
            if (_id != id) continue;
            EraseID(_id);
            break;
        }
        WriteID(_id);
    }

    public static void EraseID(string _id)
    {
        string tempFile = Path.GetTempFileName();
        using(var sw = new StreamWriter(tempFile))
        {
            foreach (string id in GetData()){
                if (id != _id)
                    sw.WriteLine(id);
            }
        }

        File.Delete(historyFilePath);
        File.Move(tempFile, historyFilePath);
    }

    private static void WriteID(string _id)
    {
        string tempFile = Path.GetTempFileName();
        using (var sw = new StreamWriter(tempFile))
        {
            foreach (string id in GetData())
            {
                sw.WriteLine(id);
            }
            sw.WriteLine(_id);
        }
        
        File.Delete(historyFilePath);
        File.Move(tempFile, historyFilePath);
    }
}
