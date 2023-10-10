using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;
using DG.Tweening;
using TMPro;
using System;
using System.Threading;
using UnityEngine.UI;

public class LevelChangeUI : MonoBehaviour
{
    public GameObject ModelImgShow;
    public TextMeshProUGUI LevelChangeTips;
    public Button LevelChangeSwitch;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(level);
        levelSwitch(level);
        LevelChangeSwitch.GetComponentInChildren<TextMeshProUGUI>().text = String.Format("Level {0}", level + 1);

    }



    

   internal void levelSwitch(int newLevel)
    {
        LevelChangeTips.text = String.Format("Level Change to L{0}", newLevel+1);

        if (newLevel == 0)
        {
            ModelImgShow.SetActive(true);
            timeSetShort = 120;
            timeSetLong = 1200;

        }
        else if (newLevel == 1)
        {
            ModelImgShow.SetActive(false);
            timeSetShort = 60;
            timeSetLong = 900;

        }
        LevelChangeSwitch.GetComponentInChildren<TextMeshProUGUI>().text = String.Format("Level {0}", level + 1);

        Timer nTimer = Timer.createTimer("nTimer");
        nTimer.startTiming(1, false, OnComplete, OnProcess, true, false, true);
    }

    public void levelSwitchButton()
    {
        if (level == 0)
        {
            level = 1;
            levelSwitch(1);
        }
        else if (level == 1)
        {
            level = 0;
            levelSwitch(0);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }





    void OnComplete()
    {
        LevelChangeTips.text = "";
        // GameObject.Find("popOut").SetActive(false);
    }
    void OnProcess(float p)
    {

        //UnityEngine.Debug.Log("on process " + p);
    }
}
