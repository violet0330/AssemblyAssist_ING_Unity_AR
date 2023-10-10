using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GlobalVariables;

public class UserSignIn: MonoBehaviour
{
    public TextMeshProUGUI Info;
    public TMP_InputField currentName;
    public TMP_InputField currentUID;

    public GameObject HomeCanvas;
    public GameObject SettingCanvas;


    public void SignInB()
    {
        if (currentName.text == ">User Name" || currentUID.text == ">User UID" || currentName.text == null || currentUID.text == null)
        {
            Info.text = "please finish info inputting";
            GlobalVariables.MusicErAlPlay();
        }
        else
        {
            if (UserRecord(currentName.text, currentUID.text))
            {
                Info.text = "*recorded UID*";
                MusicErAlPlay();
            }
            else
            {
                gameObject.SetActive(false);
                SettingCanvas.SetActive(true);
            }

        }
    }

    public void BackB()
    {
        gameObject.SetActive(false);
        HomeCanvas.SetActive(true);
    }

    public bool UserRecord(string Name, string UID)
    {
        UserInfo user = new();
        try
        {
            user = userList.Find(x => x.userUID == UID);
            return false;


        }
        catch (ArgumentNullException)
        {
            UserInfo userInfo = new()
            {
                userNo = userNumber,
                userName = Name,
                userUID = UID,
                userScore = 0,
                userLevel = 0,
            };
            userList.Add(userInfo);
            GlobalVariables.userUID = UID;
            GlobalVariables.userName = Name;
            return true;
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        Info.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
