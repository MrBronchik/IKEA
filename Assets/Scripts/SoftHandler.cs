using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoftHandler : MonoBehaviour
{
    [SerializeField] GameObject historyPreFab;
    [SerializeField] GameObject newsPreFab;
    [SerializeField] GameObject historyContent;
    [SerializeField] GameObject newsContent;
    [SerializeField] GameObject connectionIssuesContent;
    public bool connectionIssues = true;

    private void Start()
    {
        StartCoroutine(UpdateNews());
    }

    void OnApplicationQuit()
    {
        string newsDirectory = Application.dataPath + @"/news";
        Directory.Delete(Application.dataPath + @"/news", true);
        Directory.CreateDirectory(newsDirectory);
    }

    public void DestroyHistory()
    {
        foreach (Transform child in historyContent.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void LoadHistory()
    {
        string[] history_ids = HistoryFileMngr.GetData();
        Debug.Log("History has been loaded");

        foreach (string instr_id in history_ids)
        {
            string dataPath = Application.dataPath + @"/database/" + instr_id;

            if(!Directory.Exists(dataPath)) {
                Debug.Log("Found unexisted id - " + instr_id + "\nSkip");
                continue;
            }

            string[] data = System.IO.File.ReadAllLines(dataPath + @"/description.txt");

            GameObject historicalGO = Instantiate(historyPreFab, new Vector3(0, 0, 0), Quaternion.identity, historyContent.transform);

            // NAME
            historicalGO.transform.GetChild(0).GetComponent<Text>().text = data[0];
            // SPECIFICATIONS
            historicalGO.transform.GetChild(1).GetComponent<Text>().text = data[1];
            // SIZE
            historicalGO.transform.GetChild(2).GetComponent<Text>().text = data[2];
            // ID
            historicalGO.transform.GetChild(3).GetComponent<Text>().text = data[3];
            // IMAGE
            historicalGO.transform.GetChild(4).GetComponent<Image>().sprite = ImageConverter.Convert(dataPath + @"/logo.jpg");
            // PATH
            historicalGO.transform.GetChild(5).GetChild(0).name = dataPath;
        }
    }

    public void LoadNews()
    {
        int numberOfDirectories = NewsFileMngr.GetNumberOfDirectories();

        if (numberOfDirectories == 0) { connectionIssues = true; return; }
        else connectionIssues = false;

        connectionIssuesContent.SetActive(false);

        for (int i = 1; i <= numberOfDirectories; i++)
        {
            string dataPath = Application.dataPath + @"/news/" + i.ToString();


            string[] data = System.IO.File.ReadAllLines(dataPath + @"/description.txt");

            GameObject newsGO = Instantiate(newsPreFab, new Vector3(0, 0, 0), Quaternion.identity, newsContent.transform);

            // TITLE
            newsGO.transform.GetChild(0).GetComponent<Text>().text = data[0];
            // TEXT
            newsGO.transform.GetChild(1).GetComponent<Text>().text = data[1];
            // IMAGE
            newsGO.transform.GetChild(2).GetComponent<Image>().sprite = ImageConverter.Convert(dataPath + @"/logo.jpg");
            // PATH
            newsGO.transform.GetChild(3).GetChild(0).name = dataPath;
        }
    }

    public void LoadConnectionIssuesContent()
    {
        connectionIssuesContent.SetActive(true);
    }

    IEnumerator UpdateNews()
    {
        LoadConnectionIssuesContent();
        while (connectionIssues)
        {
            yield return new WaitForSeconds(5.0f);
            ClientSend.GetNews();
            LoadNews();
        }
    }
}
