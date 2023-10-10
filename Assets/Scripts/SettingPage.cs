using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Xml.Schema;
using TMPro;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using static GlobalVariables;
//using UnityEditor.PackageManager.UI;

#if NETFX_CORE  //UWPœ¬±‡“Î
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Data.Xml.Dom;
#endif

public class SettingPage: MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject fileReadingCanvas;
    public GameObject InstructCanvas;
    public TMP_Dropdown modelChange;
    public TextMeshProUGUI modelChangeTips;
    public GameObject basicSettings;

    public TMP_Dropdown databaseChoose;
    public TextMeshProUGUI DatabaseInfo;

    public Toggle kinectSwitch;

    public GameObject TestCanvas;

    public void VolumeC()
    {
        Scrollbar volumeC = GameObject.Find("volumeC").GetComponent<Scrollbar>();
        TextMeshProUGUI volumeT = GameObject.Find("volumeT").GetComponent<TextMeshProUGUI>();
        float v = volumeC.value * 10f;
        volumeT.text = "Volume--" + v.ToString("0.0");
        GameObject.Find("MusicPLay").GetComponent<MusicPlay>().MusicControl(volumeC.value);
    }

    public void HardLevelChange()
    {
        TMP_Dropdown levelChange = GameObject.Find("levelC").GetComponent<TMP_Dropdown>();
        int levelNum = levelChange.value;
        level = levelNum;
        LevelSetting();
    }

    public void ModelChange()
    {
        //UnityEngine.Debug.Log(modelChange.options[0]);
        GameObject.Find("GlobalVariables").GetComponent<Variable>().modelListValue = modelChange.value;
        
        string modelName = modelChange.options[modelChange.value].text;
        if (modelName == "Add New Guidance")//modifiable level
        {
            modelChangeTips.text = "---Model not installed at present---";
            //settingMenu.SetActive(false);
            //fileReadingCanvas.SetActive(true);
           // fileReadingCanvas.GetComponent<FileReading>().ResetInfo();
        }
        else if (modelName == "demo")
        {
            model = modelName; 
            modelChangeTips.text = "---Model not installed at present---";
        }
        else
        {
            model = modelName;
            modelChangeTips.text = " ";

        }
    }

    public void KinectSwitch()
    {
        kinectOn = kinectSwitch.isOn;
    }
    public void DatabaseChoose()
    {
        string databaseName = databaseChoose.options[databaseChoose.value].text;
        if (databaseName == "add new database")
        {
            DatabaseInfo.text = "a function to be developed";
        }
        else
        {
            databasePath = databaseName;
            DatabaseInfo.text = "chose database:" + databaseName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        settingMenu.SetActive(true);
        fileReadingCanvas.SetActive(false);

        TMP_Dropdown modelChange = GameObject.Find("modelC").GetComponent<TMP_Dropdown>();
        modelChange.value = GameObject.Find("GlobalVariables").GetComponent<Variable>().modelListValue;
        TMP_Dropdown levelChange = GameObject.Find("levelC").GetComponent<TMP_Dropdown>();
        levelChange.value = level;
        Scrollbar volumeC = GameObject.Find("volumeC").GetComponent<Scrollbar>();
        TextMeshProUGUI volumeT = GameObject.Find("volumeT").GetComponent<TextMeshProUGUI>();
        float v = volume * 10f;
        volumeC.value = volume;
        volumeT.text = "Volume--" + v.ToString("0.0");

        GlobalVariables.kinectState = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void startB()
    {
        if (model == "demo")
        {
            modelChangeTips.text = "---Model not installed at present---";
            return;
        }
        else if(model == "complex")
        {
            modelChangeTips.text = " ";
            //SceneManager.LoadScene("ComplexScene1");
        }
        else
        {
            modelChangeTips.text = "---Model not installed at present---";
            return;
        }



        if (level == 2)
            TestCanvas.SetActive(true);
        else
        {
            InstructCanvas.SetActive(true);
            SceneManager.LoadSceneAsync("ComplexScene", LoadSceneMode.Additive);
            if (kinectOn == true)
            {
                GlobalVariables.kinectState = 0;
                SceneManager.LoadSceneAsync("WebManageScene", LoadSceneMode.Additive);
            }
        }
        
        gameObject.SetActive(false);
    }

    public void ConfirmB()
    {
        settingMenu.transform.localScale = Vector3.one;
        fileReadingCanvas.transform.localScale = Vector3.zero;
        ;
        TMP_Dropdown modelChange = GameObject.Find("modelC").GetComponent<TMP_Dropdown>();
        GameObject.Find("GlobalVariables").GetComponent<Variable>().modelListValue = modelChange.options.Count;
        modelChange.value = GameObject.Find("GlobalVariables").GetComponent<Variable>().modelListValue;
        string modelName = modelChange.options[GameObject.Find("GlobalVariables").GetComponent<Variable>().modelListValue].text;
        model = modelName;
        UnityEngine.Debug.Log(modelName);
        settingMenu.transform.localScale = Vector3.one;
        basicSettings.SetActive(true);
    }

    public void CancelB()
    {
        settingMenu.transform.localScale = Vector3.one;
        fileReadingCanvas.transform.localScale = Vector3.zero;
        TMP_Dropdown modelChange = GameObject.Find("modelC").GetComponent<TMP_Dropdown>();
        modelChange.value = GameObject.Find("GlobalVariables").GetComponent<Variable>().modelListValue;
        settingMenu.transform.localScale = Vector3.one;
        basicSettings.SetActive(true);
    }


    internal void LevelSetting()
    {
        if (level == 0)//easy
        {
            timeSetShort = 120;
            timeSetLong = 1200;

        }
        else if (level == 1) //hard
        {
            timeSetShort = 60;
            timeSetLong = 900;
        }
        else//test
        {


        }
    }

}
