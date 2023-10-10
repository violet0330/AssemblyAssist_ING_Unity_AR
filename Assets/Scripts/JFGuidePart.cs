using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static GlobalVariables;

public class JFGuidePart : MonoBehaviour
{
    internal int taskNumber;

    internal int currentTask;
    internal int assembleTask;
    internal bool assembleOn;
    string currentModel;

    public GameObject next;
    public GameObject congratulations;
    public GameObject timeOut;
    public GameObject wrongObject;
    public GameObject great;

    public Material red;
    public Material white;

    public GameObject wholeModelButton;

    bool details = false;
    internal Vector3 MsPos;
    internal Vector3 MsScale;

    void TaskObserver()
    {
        if (SequenceCheck())
        {
            MusicClickPlay();
            taskNumber++;
            taskNum = taskNumber;
            //if (taskNumber <= 4)
            great.SetActive(true);
            Timer nowtimer = Timer.createTimer("nowTimer");
            nowtimer.startTiming(1, false, OnComplete, OnProcess, true, false, true);
            // assembleOn = true;
            assembleTask++;
            assembleNum = assembleTask;

            Task(taskNumber);
            //AssembleTask();
            //assembleTask++;
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
        Debug.Log(currentTask + "//" + taskNumber);
    }


    public void SkipButton()
    {
        Debug.Log("skip:" + taskNumber + "//" + assembleTask);

        MusicClickPlay();

        //GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().errorSelect++;
        //GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().errorSelect;

        taskNumber++;
        taskNum = taskNumber;
        wrongObject.SetActive(true);
        errorSelect++;
        ErrorJudge();
        Timer nowtimer = Timer.createTimer("nowTimer");
        nowtimer.startTiming(1, false, OnComplete, OnProcess, true, false, true);
        assembleTask++;
        assembleNum = assembleTask;

        Task(taskNumber);
        //AssembleTask();
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
        Debug.Log("Task:" + taskNumber + "//" + assembleTask);
        HideModel("components");
        if (task == 0)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find the Base";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  0/18";
            currentModel = "Base";
            DisplayModel("Base");
        }
        else if (task == 1)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Crank-Axle Gear(small one)";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  1/18";
            currentModel = "CrankAxleGear";
            DisplayModel("CrankAxleGear");
        }
        else if (task == 2)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Crank Axle";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  3/18";
            currentModel = "CrankAxle";
            DisplayModel("CrankAxle");
        }
        else if (task == 3)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Axle Base Gear ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  4/18";
            currentModel = "AxleBaseGear";
            DisplayModel("AxleBaseGear");
        }
        else if (task == 4)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Base Lid ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  6/18";
            currentModel = "BaseLid";
            DisplayModel("BaseLid");
        }
        else if (task == 5)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Axle ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  8/18";
            currentModel = "Axle";
            DisplayModel("Axle");
        }
        else if (task == 6)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Center Gear ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  9/18";
            currentModel = "CenterGear";
            DisplayModel("CenterGear");
        }
        else if (task == 7)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Dome Under ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  10/18";
            currentModel = "DomeUnder";
            DisplayModel("DomeUnder");
        }
        else if (task == 8)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find internal and external tentacle ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  12/18";
            currentModel = "Tentacle";
            DisplayModel("Tentacle");
        }
        else if (task == 9)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find both tentacle gear ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  13/18";
            currentModel = "TentacleGear";
            DisplayModel("TentacleGear");
        }
        else if (task == 10)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Gear Base ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  15/18";
            currentModel = "GearsBase2";
            DisplayModel("GearsBase2");
        }
        else if (task == 11)
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: find Dome ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  16/18";
            currentModel = "Dome";
            DisplayModel("Dome");
        }
        else
        {
            GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task finish! ";
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  18/18";
            congratulations.SetActive(true);
            next.SetActive(true);
        }
    }

    /// <summary>
    /// AssembleTaskList
    /// </summary>
    /// <param name="assembletask"></param>
    /// <param name="assembleTesk"></param>
    void AssembleTask()
    {
        // DelayOpenAR();
        int i = assembleTask - 1;
        // GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().assembletask = i;

        if (i == 1 || i == 3 || i == 4 || i == 7 || i == 9 || i == 11)
        {
    
            SceneManager.LoadScene("ComplexScene2");
        }
        else
        {
            Task(taskNumber);
        }
    }



    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.Find("ARGuideTimer") != null)
        {
            Timer timer = GameObject.Find("ARGuideTimer").GetComponent<Timer>();

            if (timer.isPaused == true)
            {
                timer.connitueTimer();
            }
        }


        if (isTiming == false)
        {
            ARGuideTimer();
        }
        Debug.Log("start" + taskNumber + "//" + assembleTask);


        next.SetActive(false);
        congratulations.SetActive(false);
        timeOut.SetActive(false);
        wrongObject.SetActive(false);
        great.SetActive(false);
        wholeModelButton.transform.DOScale(Vector3.zero,0.1f);

        Debug.Log("startt1" + taskNumber + "//" + assembleTask);
        taskNumber = taskNum;
        assembleTask = assembleNum;
        Debug.Log("startt2" + taskNumber + "//" + assembleTask);
        GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + errorSelect;
        //*/
        HideModel("modelShow");
        Task(taskNumber);
        GameObject ModelShow = GameObject.Find("modelInstruct");
        MsPos = ModelShow.transform.localPosition;
        MsScale = ModelShow.transform.localScale;
        details = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DetailButton()
    {
        GameObject ModelShow = GameObject.Find("modelInstruct");
        RectTransform MsTrans = ModelShow.GetComponent<RectTransform>();
        if (details == false)
        {
            details = true;
            HideModel("components");
            DisplayModel("wholeModel");
            MaterialChange(currentModel, red);
            wholeModelButton.transform.DOScale(Vector3.one, 0.1f);
            MsTrans.DOLocalMove(new Vector3(20, -50, 0), 1f);
            MsTrans.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 1f);

        }
        else
        {
            details = false;
            HideModel("wholeModel");
            DisplayModel(currentModel);
            MaterialChange("wholeModel", white);
            wholeModelButton.transform.DOScale(Vector3.zero, 0.1f);
            MsTrans.DOLocalMove(MsPos, 1f);
            MsTrans.DOScale(MsScale, 1f);
        }

    }

    internal void MaterialChange(string modelname, Material color)
    {
        if (modelname != "wholeModel")
            modelname = "wholeModel/" + modelname;
        GameObject modelShow = GameObject.Find(modelname);
        MeshRenderer[] modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            child.material = color;
        }
    }

    internal void HideModel(string modelname)
    {
        GameObject modelShow = GameObject.Find(modelname);
        MeshRenderer[] modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            //Debug.Log(child.name);
            child.enabled = false;
        }
    }
    internal void DisplayModel(string modelname)
    {
        GameObject modelShow;
        if (details == false)
        {
            modelShow = GameObject.Find("components/" + modelname);

        }
        else
        {
            modelShow = GameObject.Find(modelname);
        }
        MeshRenderer[] modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            child.enabled = true;
            //Debug.Log(child.enabled);
        }
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
        else if (nowModel == "complex")
        {
            SceneManager.LoadScene("WholeModelScene");
        }
        else
        {

        }

    }

    /// <summary>
    /// check if the model tracked is the right one
    /// </summary>
    bool SequenceCheck()
    {
        if (taskNumber == currentTask)
        {
            return true;
        }
        else
            return false;
    }

    public void BaseTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 0;
        TaskObserver();
    }
    public void CAGearTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 1;
        TaskObserver();
    }
    public void ABGearTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 3;
        TaskObserver();
    }
    public void CrankAxleTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 2;
        TaskObserver();
    }
    public void BaseLidTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 4;
        TaskObserver();
    }
    public void AxleTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 5;
        TaskObserver();
    }
    public void CenterGearTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 6;
        TaskObserver();
    }
    public void DomeUnderTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 7;
        TaskObserver();
    }
    public void ITentacleTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 8;
        TaskObserver();
    }
    public void ETentacleTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 9;
        TaskObserver();
    }
    public void IGearTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 10;
        TaskObserver();
    }
    public void EGearTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 11;
        TaskObserver();
    }
    public void GearsBaseTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 12;
        TaskObserver();
    }
    public void DomeTrack()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 13;
        TaskObserver();
    }

    void OnComplete()
    {
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
