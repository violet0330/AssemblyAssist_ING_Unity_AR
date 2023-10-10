using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UIElements;
using static GlobalVariables;

public class JFAssemble : MonoBehaviour
{

    public GameObject next;
    public GameObject timeOut;
    public GameObject great;
    public GameObject wholeModelButton;
    internal bool nextVB;

    internal int currentTask;

    internal MeshRenderer[] modelMesh;

    bool details;
    internal Vector3 MsPos;
    internal Vector3 MsScale;

    void TaskObserver()
    {
        if (SequenceCheck())
        {
            GlobalVariables.MusicClickPlay();
            great.SetActive(true);
            next.SetActive(true);
            nextVB = true;
        }
        else
        {

        }

    }

    public void ARObserver()
    {
        MusicClickPlay();
        great.SetActive(true);
        next.SetActive(true);
        nextVB = true;
    }

    public void SkipButton()
    {
        MusicClickPlay();
        //errorSelect++;
        GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + errorSelect;

        SceneManager.LoadScene("ComplexScene1");
    }

    public void NextButton()
    {
        Debug.Log("finish!");

        SceneManager.LoadScene("SettlementScene");
    }

    public void DetailButton()
    {
        int task = assembleNum;
        GameObject ModelShow = GameObject.Find("modelInstruct");
        RectTransform MsTrans = ModelShow.GetComponent<RectTransform>();
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

    }

    void Task()
    {
        HideModel("modelShow");
        int task = assembleNum;
        HideModel("complexM" + task.ToString());
        DisplayModel("simpleM" + task.ToString());
        //DelayOpenAR();
        Debug.Log("Task:" + task);
        if (task == 2)
        {
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  2/8";
        }
        else if (task == 4)
        {
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  5/18";
        }
        else if (task == 5)
        {
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  7/18";
        }
        else if (task == 8)
        {
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  11/18";
        }
        else if (task == 10)
        {
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  14/18";
        }
        else if (task == 12)
        {
            GameObject.Find("taskcount").GetComponent<TextMeshProUGUI>().text = "task number:  17/18";
        }
    }
        // Start is called before the first frame update
    void Start()
    {
        next.SetActive(false);
        timeOut.SetActive(false);
        great.SetActive(false);
        wholeModelButton.SetActive(false);
        Debug.Log("hide");

        if (GameObject.Find("ARGuideTimer") != null)
        {
            Timer timer = GameObject.Find("ARGuideTimer").GetComponent<Timer>();

            if (timer.isPaused == true)
            {
                timer.connitueTimer();
            }
        }
        // GameObject.Find("popOut").SetActive(false);
        if (isTiming == false)
        {
            ARGuideTimer();
        }

        GameObject ModelShow = GameObject.Find("modelInstruct");
        MsPos = ModelShow.transform.localPosition;
        MsScale = ModelShow.transform.localScale;
        details = false;
        currentTask = assembleNum;
        GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: assemble";
        GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "select: " + errorSelect;
        Task();
    }



    // Update is called once per frame
    void Update()
    {

    }
    public void Base1Track()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 0;
        TaskObserver();
    }
    public void Base2Track()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 2;
        TaskObserver();
    }
    public void Base3Track()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 3;
        TaskObserver();
    }
    public void Up1Track()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 6;
        TaskObserver();
    }
    public void Up2Track()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 8;
        TaskObserver();
    }
    public void Up3Track()
    {
        Debug.Log(currentTask + "Tracked");
        currentTask = 10;
        TaskObserver();
    }
    void OnComplete()
    {
        timeOut.SetActive(false);
        great.SetActive(false);
        // GameObject.Find("popOut").SetActive(false);
    }

    bool SequenceCheck()
    {
        int taskNumber = assembleNum;
        if (taskNumber == currentTask)
        {
            return true;
        }
        else
            return false;
    }

    internal void HideModel(string modelname)
    {
        GameObject modelShow = GameObject.Find(modelname);
        modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            if (child.enabled == true)
            {
                //Debug.Log("hide£º" + child.name);
                child.enabled = false;
            }

        }
    }
    internal void DisplayModel(string modelname)
    {
        GameObject modelShow = GameObject.Find(modelname);
        modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            if (child.enabled != true)
            {
                child.enabled = true;
            }
        }
    }
    // GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: assemble the connecting rod";

    // GameObject.Find("task").GetComponent<TextMeshProUGUI>().text = "task: assemble the piston";
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
    public void BackB()
    {
       // BackButton();
    }

}
