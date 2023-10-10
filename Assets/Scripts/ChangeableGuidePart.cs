using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static GlobalVariables;

public class ChangeableGuidePart : MonoBehaviour
{
    internal int taskNumber;

    public GameObject next;
    public GameObject congratulations;
    public GameObject timeOut;
    public GameObject wrongObject;
    public GameObject great;

    public TextMeshProUGUI taskText;
    public TextMeshProUGUI taskCount;

    internal MeshRenderer[] modelMesh;

    UITask[] taskList;
    UITask currentTarget;

    bool details;
    public GameObject wholeModelButton;
    internal Vector3 MsPos;
    internal Vector3 MsScale;

    public void OnClick()
    {
        next.SetActive(false);
        congratulations.SetActive(false);
        timeOut.SetActive(false);
        wrongObject.SetActive(false);
        great.SetActive(false);

        GameObject ModelShow = GameObject.Find("modelInstruct");
        MsPos = ModelShow.transform.localPosition;
        MsScale = ModelShow.transform.localScale;
        details = false;

        if (GameObject.Find("ARGuideTimer") != null)
        {
            Timer timer = GameObject.Find("ARGuideTimer").GetComponent<Timer>();

            if (timer.isPaused == true)
            {
                timer.connitueTimer();
            }
        }
        if (GlobalVariables.isTiming == false)
        {
            ARGuideTimer();
        }

        Task(taskNumber);
    }

    public void DetailButton()
    {
        int task = assembleNum;
        GameObject ModelShow = GameObject.Find("modelInstruct");
        if (details == false)
        {
            wholeModelButton.SetActive(true);
            details = true;
        }
        else
        {
            details = false;
        }
        /*
         *         RectTransform MsTrans = ModelShow.GetComponent<RectTransform>();

        if (details == false)
        {
            HideModel("simpleM" + task.ToString());
            DisplayModel("complexM" + task.ToString());
            wholeModelButton.SetActive(true);
            MsTrans.DOLocalMove(new Vector3(40, -60, 0), 1f);
            MsTrans.DOScale(new Vector3(3.5f, 3.5f, 3.5f), 1f);
            details = true;
        }
        else
        {
            HideModel("complexM" + task.ToString());
            DisplayModel("simpleM" + task.ToString());
            wholeModelButton.SetActive(false);
            MsTrans.DOLocalMove(MsPos, 1f);
            MsTrans.DOScale(MsScale, 1f);
            details = false;
        }
        //*/
    }

    public void WModelGuideB()
    {
        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene.name;
        Debug.Log(scene.name);
        string nowModel = model;
        if (nowModel == "demo")
        {
            SceneManager.LoadScene("WholeModelScene");
        }
        else if (nowModel == "JF")
        {
            SceneManager.LoadScene("WholeModelScene");
        }
        else
        {

        }

    }

    internal void TaskObserver(string trackTarget)
    {
        if (SequenceCheck(trackTarget))
        {
            MusicClickPlay();
            taskNumber++;
            taskNum = taskNumber;

            Task(taskNumber);

        }
        else
        {
            MusicErAlPlay();
            errorSelect++;
            GlobalVariables.ErrorJudge();
            GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + errorSelect;
            wrongObject.SetActive(true);
            Timer nowtimer = Timer.createTimer("nowTimer");
            nowtimer.startTiming(2, false, OnComplete, OnProcess, true, false, true);
        }
    }



    public void SkipButton()
    {

        MusicClickPlay();

        //errorSelect++;
        //GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + errorSelect;
        taskNumber++;
        taskNum = taskNumber;
        FindRightTarget();
        great.SetActive(true);
        Timer nowtimer = Timer.createTimer("nowTimer");
        nowtimer.startTiming(2, false, OnComplete, OnProcess, true, false, true);

        Task(taskNumber);
        //assembleTask++;

    }


    public void SettlementButton()
    {
        Destroy(GameObject.Find("ARGuideTimer"));

        SceneManager.LoadScene("SettlementScene");
    }

    /// <summary>
    /// task list
    /// </summary>
    void Task(int task)
    {
        //DelayOpenAR();
        //HideModel("components");

        if (FindRightTarget())
        {
            int i = task - 1;
            if (i > 0)
            {
                great.SetActive(true);
                Timer nowtimer = Timer.createTimer("nowTimer");
                nowtimer.startTiming(1, false, OnComplete, OnProcess, true, false, true);
            }
            taskText.text = "task: " + currentTarget.task;
            taskCount.text = "task number: " + i + "/" + (taskList.Length - 1);
            //DisplayModel(currentTarget.targetName+"M");
        }
        else
        {
            taskText.text = "task finish! ";
            taskCount.text = "task number: " + (taskList.Length - 1) + "/" + (taskList.Length - 1);
            congratulations.SetActive(true);
            next.SetActive(true);
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        next.SetActive(false);
        congratulations.SetActive(false);
        timeOut.SetActive(false);
        wrongObject.SetActive(false);
        great.SetActive(false);

        details = false;
        wholeModelButton.SetActive(false);

        taskList = uiTaskList[productName];
        taskNumber = taskNum;
        if (taskNumber == 0)
            taskNumber = 1;
        GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + errorSelect;
        FindRightTarget();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SkipButton();
        }
    }

    internal void HideModel(string modelname)
    {
        GameObject modelShow = GameObject.Find(modelname);
        modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            //Debug.Log(child.name);
            child.enabled = false;
        }
    }

    internal void DisplayModel(string modelname)
    {
        GameObject modelShow = GameObject.Find(modelname);
        if (modelShow.GetComponentsInChildren<MeshRenderer>() != null)
        {
            modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer child in modelMesh)
            {
                Debug.Log(child.name);
                child.enabled = true;
                Debug.Log(child.enabled);
            }
        }

    }

    /// <summary>
    /// check if the model tracked is the right one
    /// </summary>
    bool SequenceCheck(string targetna)
    {
        Debug.Log(targetna + currentTarget.targetName);
        if (targetna == currentTarget.targetName)
            return true;
        else
            return false;
    }

    bool FindRightTarget()
    {
        foreach (UITask task in taskList)
        {
            if (task.sequence == taskNumber)
            {
                currentTarget = task;
                return true;
            }
        }
        return false;
    }

    void OnComplete()
    {
        //next.SetActive(false);
        //congratulations.SetActive(false);
        timeOut.SetActive(false);
        wrongObject.SetActive(false);
        great.SetActive(false);
        // GameObject.Find("popOut").SetActive(false);
    }
    void OnProcess(float p)
    {

        //UnityEngine.Debug.Log("on process " + p);
    }

    public void BackButton()
    {

        SceneManager.LoadScene("HomeScene");
    }
}
