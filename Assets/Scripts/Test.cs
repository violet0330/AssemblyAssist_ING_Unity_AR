using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GlobalVariables;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{
    public GameObject BeginButton;
    public GameObject TestContent;
    public GameObject ScoreShowing;

    public GameObject InstructCanva;

    UserInfo userInfo;
    string[] target = {"Axle","AxleBaseGear", "Base", "BaseLid", "CenterGear","CrankAxle","CrankAxleGear",
        "CrankHandle","Dome","DomeRing","DomeUnder","Gear","GearsBase2","Tentacle" };
    Step[] Task;
    Step[] Question;
    int score;
    /// <summary>
    /// current display question number
    /// </summary>
    int NoQ;
    /// <summary>
    /// fullScore = num of question num
    /// </summary>
    int fullScore;
    /// <summary>
    /// the random correct answer num
    /// </summary>
    int answer;

    /// <summary>
    /// Userlist[index] = current userInfo
    /// </summary>
    int index;

    GameObject AChoice;
    GameObject BChoice;
    GameObject CChoice;
    GameObject DChoice;
    GameObject AButton;
    GameObject BButton;
    GameObject CButton;
    GameObject DButton;

    Button choseB;

    internal struct Step
    {
        internal int sequence;
        internal string targetName;
    }




    // Start is called before the first frame update
    void Start()
    {
        BeginButton.SetActive(true);
        TestContent.SetActive(false);
        ScoreShowing.SetActive(false);

        userInfo = new();
        userInfo = userList.Find(x => x.userNo == userNumber);
        userList.Remove(userInfo);
        HideModel("Jellyfish");

        ReadTask();
    }

    public void Begin()
    {
        TestContent.SetActive(true);
        BeginButton.SetActive(false);

        AChoice = GameObject.Find("AText");
        BChoice = GameObject.Find("BText");
        CChoice = GameObject.Find("CText");
        DChoice = GameObject.Find("DText");
        AButton = GameObject.Find("AButton");
        BButton = GameObject.Find("BButton");
        CButton = GameObject.Find("CButton");
        DButton = GameObject.Find("DButton");

        QuestionPaper();
        QuestionCreate();
    }

    /// <summary>
    /// random the task sequence to create a test paper
    /// </summary>
    internal void QuestionPaper()
    {
        Question = Task;
        //´òÂÒ
        for (int i = 0; i < Question.Length; i++)
        {
            Step temp = Question[i];
            int randomIndex = UnityEngine.Random.Range(i, Question.Length);
            Question[i] = Question[randomIndex];
            Question[randomIndex] = temp;
        }
        fullScore = Question.Length / 4;
        NoQ = 0;
        score = 0;
    }

    internal void QuestionCreate()
    {
        int[] QSequence = Shuffle(4);
        string Q = Question[4 * NoQ].targetName;
        DisplayModel(Q);
        Button[] Button = { AButton.GetComponent<Button>(), BButton.GetComponent<Button>(), CButton.GetComponent<Button>(), DButton.GetComponent<Button>()};
        TextMeshProUGUI[] Choice = { AChoice.GetComponent<TextMeshProUGUI>(), BChoice.GetComponent<TextMeshProUGUI>(), CChoice.GetComponent<TextMeshProUGUI>(), DChoice.GetComponent<TextMeshProUGUI>() };
        for (int i = 0; i < 4; i++)
        {
            string choice = Question[QSequence[i] + 4 * NoQ].targetName;
            Choice[i].text = choice;

            if (Q == choice)
            {
                answer = i;
            }

        }
    }

    internal void Marking(int choice)
    {
        if (choice == answer)
        {
            score++;
            choseB.GetComponent<Image>().color = Color.green;
        }
        else
        {
            choseB.GetComponent<Image>().color = Color.red;

        }

        Timer nowtimer = Timer.createTimer("nowTimer");
        nowtimer.startTiming((float)0.5, false, OnComplete, OnProcess, true, false, true);
    }

    internal string LevelJudge(int limit)
    {
        decimal bound = Math.Round((decimal)((double)limit / (double)fullScore), 2);
        userInfo.userNo = GlobalVariables.userNumber;
        userInfo.userScore = score;

        // Debug.Log(bound.ToString());
        if (score >= limit)
        {
            userInfo.userLevel = 2;
            level = 1;
            userList.Add(userInfo);
            return String.Format("the score is >={0}%, \r\nmode is changed to level {1}", bound * 100, 1);
        }
        else
        {
            userInfo.userLevel = 1;
            level = 0;
            userList[index] = userInfo;
            return String.Format("the score is <{0}%, \r\nmode is changed to level {1}", bound * 100, 2);
        }
    }
    internal int[] Shuffle(int num)
    {
        int[] result = new int[num];
        for (int i = 0; i < num; i++)
        {
            result[i] = i;
        }

        for (int i = 0; i < num; i++)
        {
            int temp = result[i];   
            int randomIndex = UnityEngine.Random.Range(i, num);
            result[i] = result[randomIndex];
            result[randomIndex] = temp;
        }
        return result;
    }


    // Update is called once per frame
    void Update()
    {

    }

    internal void ReadTask()
    {
        Step steps = new();
        Step[] Tasks = new Step[target.Length];
        for (int i = 0; i < target.Length; i++)
        {
            steps.sequence = i + 1;
            steps.targetName = target[i];
            Tasks[i] = steps;
        }
        Task = Tasks;
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
        modelShow = GameObject.Find(modelname);
        MeshRenderer[] modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();
        //modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            child.enabled = true;
        }
    }

    public void StartButtonPress()
    {
        gameObject.SetActive(false);
        InstructCanva.SetActive(true);
        if (model == "complex")
        {
            SceneManager.LoadSceneAsync("ComplexScene",LoadSceneMode.Additive);
        }
        
    }

    public void AButtonPress()
    {
        NoQ++;
        choseB = AButton.GetComponent<Button>();
        Marking(0);

    }
    public void BButtonPress()
    {
        NoQ++;
        choseB = BButton.GetComponent<Button>();
        Marking(1);
    }
    public void CButtonPress()
    {
        NoQ++;
        choseB = CButton.GetComponent<Button>();
        Marking(2);

    }
    public void DButtonPress()
    {
        NoQ++; 
        choseB = DButton.GetComponent<Button>();
        Marking(3);
    }

    void OnComplete()
    {
        HideModel("Jellyfish");
        choseB.GetComponent<Image>().color = Color.white;
        if (NoQ < fullScore)
            QuestionCreate();
        else
        {
            TestContent.SetActive(false);
            ScoreShowing.SetActive(true);

            GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = String.Format("Your Score is {0} / {1}", score, fullScore);
            GameObject.Find("Tips").GetComponent<TextMeshProUGUI>().text = LevelJudge(2);

        }
    }

    
    void OnProcess(float p)
    {

    }
}
