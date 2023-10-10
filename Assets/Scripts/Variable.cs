using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static GlobalVariables;
using System.Xml;

public class Variable : MonoBehaviour
{
    internal int totalSecond;
    GameObject[] canvasList;



    internal int modelListValue;

#if UNITY_EDITOR
    internal XmlDocument xmlDocumentUnity = new();
#elif UNITY_WSA
    internal Windows.Storage.StorageFile file_demonstration;       
    internal Windows.Data.Xml.Dom.XmlDocument xmlDocumentWSA = new();
#endif

    

    internal void ARGuideTimer()
    {
        isTiming = true;
        int timeLeft = timeSetLong;
        Timer timer = Timer.createTimer("ARGuideTimer");
        DontDestroyOnLoad(timer);
        //开始计时
        timer.startTiming(timeLeft, false, OnComplete, OnProcess, true, false, true);
    }


    // Start is called before the first frame update
    void Start()
    {
        kinectOn = false;

        canvasList = GameObject.FindGameObjectsWithTag("Canvas");

        foreach (GameObject canvas in canvasList)
        {
            if (canvas.name == "HomeCanvas")
                canvas.SetActive(true);
            else
                canvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OnComplete()
    {
        MusicErAlPlay();
        errorTimeOut++;
        ErrorJudge();
        GameObject.Find("timeOut").SetActive(true);
        Timer nowtimer = Timer.createTimer("nowTimer");
        nowtimer.startTiming(2, false, OnCompleteNow, OnProcessNow, true, false, true);
        //sUnityEngine.Debug.Log("complete !");
    }

    internal void OnProcess(float p)
    {
        totalSecond = (int)p;
        TextMeshProUGUI timeText = GameObject.Find("time").GetComponent<TextMeshProUGUI>();
        //timeText.text = "time: " + (1200 - (int)p);
        timeText.text = "time: " + (timeSetLong - (int)p);
        //UnityEngine.Debug.Log("on process " + p);
    }

    internal void OnCompleteNow()
    {
        GameObject.Find("timeOut").SetActive(false);
        SceneManager.LoadScene("BeginScene");
    }
    internal void OnProcessNow(float p)
    {

    }

}
