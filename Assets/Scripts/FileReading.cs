/*
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

public class FileReading : GlobalVariables
{
    //internal string[] jpgPath;
    public TextMeshProUGUI DatabaseInfo;
    public TextMeshProUGUI TaskUIInfo;
    public TextMeshProUGUI GuideUIInfo;
    public TextMeshProUGUI productNameInfo;
    public TMP_Dropdown modelChange;
    public TMP_Dropdown databaseChoose;
    public GameObject settingMenu;

    string xmlPathT;
    string txtPathT;
    ArrayList txtList = new ArrayList();
    string[] txtText;

    string[] databaseList;

    bool DatabaseE = true;
    bool TaskUIE = true;
    bool GuideUIE = true;

    UITask[] taskList;
    UIGuide[] guideList;

#if UNITY_EDITOR   //Unityœ¬

    void OpenFileinUnity(string task, string docEnd)
    {
        string path = EditorUtility.OpenFilePanel("Load" + task + docEnd, "", "");

        // Stream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        jpgPath = new Dictionary<string, string>();

        if (path.Length != 0 && path.EndsWith(docEnd))
        {
            if (docEnd == ".xml")
            {
                xmlPathT = path;
                GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().xmlDocumentUnity.Load(xmlPathT);
                DatabaseInfo.text = xmlPathT;
                DatabaseE = false;
                ProductName(null);
            }

            else if (docEnd == ".jpg")
            {
                UnityEngine.Debug.Log(path);
                UnityEngine.Debug.Log(Path.GetFileNameWithoutExtension(path));
                string fullpath = path;
                string name = Path.GetFileNameWithoutExtension(fullpath);
                GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().jpgPath.Add(name, fullpath);

            }

            else if (docEnd == ".txt")
            {
                txtPathT = path;
                txtText = File.ReadAllLines(txtPathT);
                TxtReading(task);
            }
        }
        else
            DatabaseInfo.text = "Choose Right File";


        //info.text += path;
        //fs.Dispose();
    }
#elif UNITY_WSA//UWPœ¬
        
        internal Windows.Storage.StorageFile xmlFile;
        internal Windows.Storage.StorageFile txtFile;
        internal Windows.Data.Xml.Dom.XmlDocument xmlDocument;

        Stream txtStream;

        void OpenInUWP(string task, string docEnd)
        {
            UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add(".xml");
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".txt");
                picker.FileTypeFilter.Add("*");
                var files = await picker.PickMultipleFilesAsync();
                if (files.Count > 0)
                {
                    jpgPath = new Dictionary<string, string>();

                    // Application now has read/write access to the picked file(s)
                    foreach (Windows.Storage.StorageFile file in files)
                    {
                        if (file.Path != null && file.FileType == docEnd)
                        {
                            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file);

                            if (docEnd == ".xml")
                            {
                                xmlFile = file;
                                xmlPathT = file.Path;
                                xmlDocument = await Windows.Data.Xml.Dom.XmlDocument.LoadFromFileAsync(file);
                            }

                            else if (docEnd == ".jpg")
                            {
                                jpgPath.Add(Path.GetFileNameWithoutExtension(file.Path), file.Path);

                            }

                            else if (docEnd == ".txt")
                            {
                                txtPathT = file.Path;
                                txtStream = await file.OpenStreamForReadAsync();
                                StreamReader txtSR = new StreamReader(txtStream);
                                string line = null; 
                                while ((line = txtSR.ReadLine()) != null)
                                {
                                    txtList.Add(line);
                                }
                                txtText = (string[])txtList.ToArray(typeof(string));
                                txtSR.Close();
                                txtSR.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    //this.textBlock.Text = "Operation cancelled.";
                }

                UnityEngine.WSA.Application.InvokeOnAppThread(() =>
                {
                    if (docEnd == ".xml" && xmlPath != null)
                    {
                        GlobalVariables.file_demonstration = xmlFile;
                         GameObject.Find("GlobalVariables").GetComponent<Variable>().xmlDocumentWSA = xmlDocument;
                        DatabaseInfo.text = xmlPathT;
                        DatabaseE = false;
                        ProductName(null);
                    }

                    else if (docEnd == ".jpg" && jpgPath != null)
                    {
                        GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().jpgPath = jpgPath;

                    }

                    else if (docEnd == ".txt")
                    {
                        TxtReading(task);
                    }
                    // Process the computed result, it's safe to interact with all scene objects and Unity API here.
                }, false);

            }, false);
        }

        void TxtLoad(string task)
        {
            UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
            {
                if(txtFile != null)
                {
                    txtPathT = txtFile.Path;
                    txtStream = await txtFile.OpenStreamForReadAsync();
                    if(txtStream != null)
                    {
                        StreamReader txtSR = new StreamReader(txtStream);
                        string line = null;    
                        if (txtSR != null)
                        {
                            while ((line = txtSR.ReadLine()) != null)
                            {
                                txtList.Add(line);
                            }
                            txtText = (string[])txtList.ToArray(typeof(string));
                            txtSR.Close();
                            txtSR.Dispose();
                            TxtReading(task);
                        }
                        else
                        {
                            TaskUIInfo.text = "txtSR=null";
                        }
                    }
                    else
                    {
                        TaskUIInfo.text = "txtStream=null";
                    }
                }


                UnityEngine.WSA.Application.InvokeOnAppThread(() =>
                {
                    //TxtReader(task);
                    // Process the computed result, it's safe to interact with all scene objects and Unity API here.
                }, false);

            }, false);
        }


#endif

    void ThroughSA()
    {
        var path = Application.streamingAssetsPath + "/Vuforia";

        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*.xml", SearchOption.AllDirectories);
            databaseList = new string[files.Length];
            //ArrayList databaseOption = new();
            for (int i = 0; i < files.Length; i++)
            {
                databaseList[i] = files[i].Name;
                TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
                newOption.text = files[i].Name;
                databaseChoose.options.Add(newOption);
                UnityEngine.Debug.Log(newOption.text);
            }
            TMP_Dropdown.OptionData optionData = new();
            optionData.text = "add new database";
            databaseChoose.options.Add(optionData);
        }
    }

    void AddDatabase()
    {
        TMP_Dropdown.OptionData m_NewData = new();
        foreach (string databaseName in databaseList)
        {
            m_NewData.text = databaseName;
            databaseChoose.options.Add(m_NewData);
        }
        TMP_Dropdown.OptionData optionData = new();
        optionData.text = "add new database";
        databaseChoose.options.Add(optionData);
    }

    public void UIChangeClick()
    {
#if UNITY_EDITOR
        OpenFileinUnity("UI", ".txt");
#elif UNITY_WSA
        OpenInUWP("UI", ".txt");  
#endif
    }
    public void FileChoose()
    {
#if UNITY_EDITOR
        OpenFileinUnity("database", ".xml");
#elif UNITY_WSA
        OpenInUWP("database", ".xml");
#endif
    }
    public void UIGuideChangeClick()
    {
#if UNITY_EDITOR
        OpenFileinUnity("Guide", ".txt");
#elif UNITY_WSA
        OpenInUWP("Guide", ".txt");
              
#endif
    }

    void TxtReading(string use)
    {
        if (use == "UI")
        {
            productName = txtText[0];
            int length = (txtText.Length - 1) / 3;
            taskList = new UITask[length];
            for (int i = 1, j = 0; i < txtText.Length; i += 3, j++)
            {
                UITask task = new UITask();
                if (int.TryParse(txtText[i], out int n))
                {
                    task.sequence = n;
                    task.targetName = txtText[i + 1];
                    task.task = txtText[i + 2];
                    taskList[j] = task;
                }
            }
            TaskUIE = false;
            TaskUIInfo.text = txtPathT;
        }
        else if (use == "Guide")
        {
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName = txtText[0];
            ProductName(productName);
            int length = (txtText.Length - 1) / 3;
            guideList = new UIGuide[length];
            for (int i = 1, j = 0; i < txtText.Length; i += 3, j++)
            {
                UIGuide guide = new UIGuide();
                if (int.TryParse(txtText[i], out int n))
                {
                    guide.sequence = n;
                    guide.componentName = txtText[i + 1];
                    guide.introduction = txtText[i + 2];
                    guideList[j] = guide;
                }
            }
            GuideUIE = false;
            GuideUIInfo.text = txtPathT;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //RuntimePlatform platform = Application.platform;
        //UnityEngine.Debug.Log(platform);
        DatabaseE = true;
        TaskUIE = true;
        GuideUIE = true;
        ThroughSA();
        //AddDatabase();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ProductName(string name)
    {
        if (name != null)
        {
            if (GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName == null)
            {
                GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName = name;
                productNameInfo.text = "Product: " + GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName;
            }
            else if (productName != name)
            {
                productNameInfo.text = "! chose a different product";
            }
            else
            {
                productNameInfo.text = "Product: " + GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName;
            }
        }
        else
        {
            if (GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName == null)
            {
                productNameInfo.text = "Product Name";
            }
            else
            {
                productNameInfo.text = "Product: " + GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName;
            }
        }

    }

    public void ConfirmB()
    {
        if (databaseChoose.options[databaseChoose.value].text == "add new database" || TaskUIE || GuideUIE)
        {
            productNameInfo.text = "Please complete all options";
        }
        else
        {
            xmlPathT = Application.streamingAssetsPath + "/Vuforia/" + databaseChoose.options[databaseChoose.value].text;
            string productName = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().productName;
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().uiGuideList.Add(productName, guideList);
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().uiTaskList.Add(productName, taskList);
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().xmlPath.Add(productName, xmlPathT);
            GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>().database.Add(productName, databaseChoose.options[databaseChoose.value].text);
            TMP_Dropdown.OptionData m_NewData = new();
            m_NewData.text = productName;
            modelChange.options.Add(m_NewData);
            GameObject.Find("display2Canvas").GetComponent<SettingPage>().ConfirmB();
        }

    }

    internal void ResetInfo()
    {
        DatabaseInfo.text = "Please choose your target database";
        TaskUIInfo.text = "Please choose your assembly tasks txt";
        GuideUIInfo.text = "Please choose your components info txt";
        productNameInfo.text = "Product Name";
        
        
        DatabaseE = true;
        TaskUIE = true;
        GuideUIE = true;
        productName = null;
        xmlPathT = null;
        settingMenu.transform.localScale = Vector3.zero;
        UnityEngine.Debug.Log("11");
    }
}


//*/
//*/