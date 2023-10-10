using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettlementPage : GlobalVariables
{
    void Start()
    {
        UserRecord();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }


    void UserRecord()
    {
        ErrorRecord();

        GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = "Name: " + userName;
        GameObject.Find("PlayerID").GetComponent<TextMeshProUGUI>().text = "ID: " + userUID;

        GameObject.Find("errorSelect").GetComponent<TextMeshProUGUI>().text = "selcet error: " + errorSelect;
        GameObject.Find("errorAssemble").GetComponent<TextMeshProUGUI>().text = "Nassembled error: " + errorAssemble;
        GameObject.Find("errorTimeOut").GetComponent<TextMeshProUGUI>().text = "TimeOut error: " + errorTimeOut;
    }

    void ErrorRecord()
    {
        ErrorDetail errorDetail;
        errorDetail.Select = errorSelect;
        errorDetail.Assemble = errorSelect;
        errorDetail.timeOut = errorTimeOut;
    }



}
