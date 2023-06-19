using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SoftHandler : MonoBehaviour
{
    [SerializeField] GameObject historyPreFab;
    [SerializeField] GameObject newsPreFab;
    [SerializeField] GameObject historyContent;
    [SerializeField] TextMeshProUGUI newsTitle;
    [SerializeField] TextMeshProUGUI newsArticle;
    [SerializeField] GameObject connectionIssuesContent;
    public bool connectionIssues = true;

    public static bool newsAreReceived = false;
    public static int numberOfNews;
    public static List<string[]> newsData;
    public static int shownNewsID;

    private void Start()
    {
        StartCoroutine(UpdateNews());
    }

    void OnApplicationQuit()
    {
        string newsDirectory = Application.dataPath + @"/news";
        if (Directory.Exists(newsDirectory))
            Directory.Delete(Application.dataPath + @"/news", true);
        //Directory.CreateDirectory(newsDirectory);
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
            // BUTTON
            // historicalGO.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(delegate {}) = dataPath;
        }
    }

    // Loads news data receive from function ReceiveNews(_packet), newsID starts from 0
    public void LoadNews(int newsID)
    {
        newsTitle.text = newsData[newsID][0];
        newsArticle.text = newsData[newsID][1];
        shownNewsID = newsID;

        Debug.Log("News have been loaded");
    }

    public void LoadNextNews()
    {
        if (shownNewsID == numberOfNews - 1)
        {
            Debug.Log("There is no more news!!");
        }
        else
        {
            LoadNews(shownNewsID + 1);
        }
    }
    public void LoadPreviousNews()
    {
        if (shownNewsID == 0)
        {
            Debug.Log("You cannot read negative news!!");
        }
        else
        {
            LoadNews(shownNewsID - 1);
        }
    }

    public void OpenNews()
    {
        System.Diagnostics.Process.Start(newsData[shownNewsID][2]);
    }

    public void LoadConnectionIssuesContent()
    {
        connectionIssuesContent.SetActive(true);
    }

    IEnumerator UpdateNews()
    {
        // Awaiting for loading
        yield return new WaitForSeconds(1.0f);

        ClientSend.GetNews();
        Debug.Log("We asked for news");

        // Wait a second for derver to respond
        yield return new WaitForSeconds(1.0f);

        Debug.Log("We have received " + numberOfNews.ToString() + " news");

        if (newsAreReceived)
            LoadNews(0);
        else
            connectionIssues = true;
    }
}
