using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariables
{
    //Setting Part Information
    internal static string model;

    internal static int level;
    internal static float volume;
    internal static bool kinectOn = false;

    internal static int timeSetShort = 60;
    internal static int timeSetLong = 1200;

    internal static int errorSelect = 0;
    internal static int errorAssemble = 0;
    internal static int errorTimeOut = 0;

    internal static int levelThreshold = 3;

    /// <summary>
    /// the struct of one task in the sequence, including sequence num, target name and task info
    /// </summary>
    internal struct UITask
    {
        internal int sequence;
        internal string targetName;
        internal string task;
    }

    internal struct UIGuide
    {
        internal int sequence;
        internal string componentName;
        internal string introduction;
    }

    internal static Dictionary<string, UITask[]> uiTaskList = new();
    internal static Dictionary<string, UIGuide[]> uiGuideList = new();
    internal static Dictionary<string, string> jpgPath = new();
    internal static Dictionary<string, string> xmlPath = new();
    internal static Dictionary<string, string> database = new();

    /// <summary>
    /// chose product name
    /// </summary>
    internal static string productName;
    internal static string databasePath;



    //User Information
    internal static string userUID;
    internal static string userName;
    internal static int userNumber = 0;


    //*/

    /// <summary>
    /// user's detail userName,userUID,score and level
    /// </summary>
    public struct UserInfo
    {
        public int userNo;
        public string userName;
        public string userUID;
        public int userScore;
        public int userLevel;
    }
    /// <summary>
    /// information during learning time,errorNUM,errorType 
    /// </summary>
    public struct ErrorDetail
    {
        public int Select;
        public int Assemble;
        public int timeOut;
    }
    /// <summary>
    /// list of struct userInfo
    /// </summary>
    internal static List<UserInfo> userList = new();
    /// <summary>
    /// recording error num for each user, key=usernumber
    /// </summary>
    internal static Dictionary<string, ErrorDetail> errorList = new();

    //Changeable Guide Part
    internal static bool isTiming = false;
    internal static int taskNum = 0;
    internal static int assembleNum = 0;

    internal Quaternion wholeModelRotation;
    internal Vector3 wholeModelScale;
    internal bool wholeModelChange = false;

    internal static string currentScene;

    /*
    internal static void ScoreRecord()
    {
        ScoreDetail score;
        score.Select = errorSelect;
        score.Assemble = errorSelect;
        score.timeOut = errorTimeOut;
        scoreList.Add(GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().userUID, score);
    }
    //*/

    //WebManage Variables
    internal static Vector3 pointingPos = Vector3.zero;
    /// <summary>
    /// 0 - release 1-grip
    /// </summary>
    internal static int handEvent;
    internal static float clickProgress;
    /// <summary>
    /// 0 = interaction; 1 = gesture; 2 = close
    /// </summary>
    internal static int kinectState;


    internal void TrackingFound(string trackTarget)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "ChangeableScene")
        {
            GameObject.Find("Target").GetComponent<ChangeableGuidePart>().TaskObserver(trackTarget);
        }
    }

    public static void MusicClickPlay()
    {
        GameObject.Find("GlobalVariables").GetComponent<MusicPlay>().ClickSoundPlay(volume);
    }
    public static void MusicErAlPlay()
    {
        GameObject.Find("GlobalVariables").GetComponent<MusicPlay>().ErrorSoundPlay(volume);
    }

    internal static void ARGuideTimer()
    {
        GameObject.Find("GlobalVariables").GetComponent<Variable>().ARGuideTimer();
    }

    internal void Restart()
    {
        errorSelect = 0;
        errorAssemble = 0;
        errorTimeOut = 0;

        timeSetShort = 60;
        timeSetLong = 1200;

        isTiming = false;
        taskNum = 0;

        userNumber = 0;
    }

    internal void BackButton()
    {
        //SceneManager.LoadScene("HomeScene");
    }

    internal static void ErrorJudge()
    {
        int errorTotal;
        errorTotal = errorSelect + errorAssemble + errorTimeOut;
        Debug.Log(errorTotal);
        if (errorTotal == 3 )
        {
            GameObject.Find("ARCanvas").GetComponent<LevelChangeUI>().levelSwitch(0);
        }
    }


}
