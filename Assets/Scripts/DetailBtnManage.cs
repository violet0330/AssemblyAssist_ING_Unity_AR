using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GlobalVariables;

public class DetailBtnManage : MonoBehaviour
{
    private string modelName = "Jellyfish";
    private string firstSub = "Axle";
    private string[] subjects = { "Axle Base Gear", "Base", "Base Lid", "Center Gear","Crank Axle","Crank Axle Gear",
        "Crank Handle", "Dome","Dome Under", "Gear", "Gears Base", "Tentacle"};
    private string[] components = { "Axle","Axle Base Gear", "Base", "Base Lid", "Center Gear","Crank Axle","Crank Axle Gear",
        "Crank Handle", "Dome","Dome Under", "Gear", "Gears Base", "Tentacle"};
    private string[] information = { "  Axle connects the axle base gear and the center gear.",     "   Axle Base Gear will convert crank axle gear's X-axis rotation motion to Y-axis rotation motion.",
        "   As the base of the whole product, it should have enough weight to prevent falls.",      "   Base Lid seal the base to fix components inside the base.",
        "   Center Gear is the center of the upper part, transmitting the rotation of the Y-axis to gears.", "   Crank Axle is the axle of crank, senting the rotation motion from hand to crank axle gear.",
        "   Crank Axle Gear will transmit crank's X-axis rotation motion to axle vase gear.",       "   Crank Handle is added to help user to held the crznk easily.",
        "   Dome is the hemispherical roof as the head of a jellyfish, hidding the gears inside.",  "   Dome Under makes the axle more attractive and forms a platform for placing gears and tentacles.",
        "   Gears are the small gears that transmit rotation from center gear to tentacles and are divided into internal and external in two rotation directions.",    "   Gears Base fixed the gears on the dome under and filled the inside of the dome.",
        "   Tentacles simulate the shape of jellyfish tentacles and are divided into internal and external in two rotation directions."};
    Vector3 btnPos;
    internal Button firstBtn;
    internal string btnName;
    bool split;
    public GameObject informationUI;
    private List<GameObject> m_Child;//所有子对象
    private List<GameObject> m_TargetChild;
    private List<Vector3> m_InitPoint = new List<Vector3>();
    private List<Vector3> m_InitScale = new List<Vector3>();

    private Vector3 m_TargetPoint = new Vector3(1.2f, 0, 0);
    private Vector3 m_TargetScale = new Vector3(1.5f, 1.5f, 1.5f);

    // Start is called before the first frame update
    void Start()
    {
        split = false;
        informationUI.GetComponent<TextMeshProUGUI>().text = "";
        InitailRecord();
        firstBtn = GameObject.Find(firstSub + "B").GetComponent<Button>();
        firstBtn.onClick.AddListener(delegate ()
        {
            this.OnClick(firstBtn);
        });
        btnPos = firstBtn.GetComponent<RectTransform>().anchoredPosition3D;
        for (int i = 0; i < subjects.Length; i++)
        {
            Button obj = Instantiate(firstBtn);
            obj.name = subjects[i].Replace(" ", "") + "B";
            obj.transform.SetParent(transform);
            obj.onClick.AddListener(delegate ()
            {
                this.OnClick(obj);
            });
            if (0 < i + 2 && i + 2 <= 4)
            {
                obj.GetComponent<RectTransform>().anchoredPosition3D = btnPos + (i + 1) * new Vector3(0, -85, 0);
            }
            else if (4 < i + 2 && i + 2 <= 8)
            {
                obj.GetComponent<RectTransform>().anchoredPosition3D = btnPos + (i + 1 - 4) * new Vector3(0, -85, 0) + new Vector3(-130, 0, 0);
            }
            else if (8 < i + 2 && i + 2 <= 13)
            {
                obj.GetComponent<RectTransform>().anchoredPosition3D = btnPos + (i + 1 - 8) * new Vector3(0, -85, 0) + 2 * new Vector3(-130, 0, 0);
            }
            obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            TextMeshProUGUI btnText = obj.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = subjects[i];
            //Debug.Log(subjects[i]) ;
        }
    }

    internal void OnClick(Button btn)
    {
        if (split == false)
        {
            GameObject.Find("wholeProject").GetComponentInChildren<Split>().SplitObject();
            split = true;
        }
        btnName = btn.name;
        ResetModel();
        Timer nowtimer = Timer.createTimer("nowTimer");
        nowtimer.startTiming(2.5f, false, OnCompleteNow, OnProcessNow, true, false, true);


    }

    internal void ModelMove(string btnName)
    {
        btnName = btnName.Substring(0, btnName.Length - 1);
        GameObject target = FindChild(btnName);
        target.transform.DOLocalMove(new Vector3(1.3f, 0, 0), 2f, false);
        target.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f);
        string info = information[StringSearch(btnName, components)];
        informationUI.GetComponent<TextMeshProUGUI>().text = info;
    }

    internal void InitailRecord()
    {
        Transform initModel = GameObject.Find(modelName + "Split").GetComponent<Transform>();
        Transform showModel = GameObject.Find(modelName).GetComponent<Transform>();
        m_TargetChild = GetChild(initModel);//获取所有子对象
        m_Child = GetChild(showModel);
        for (int i = 0; i < m_TargetChild.Count; i++)
        {
            m_InitPoint.Add(m_TargetChild[i].transform.position);
            m_InitScale.Add(m_TargetChild[i].transform.localScale);
            //Debug.Log(m_Child[i].gameObject.GetComponent<MeshRenderer>().material);
        }
    }

    private void ResetModel()
    {
        //Debug.Log("reset");
        for (int i = 0; i < m_Child.Count; i++)
        {
            //Debug.Log(m_Child[i].name);
            m_Child[i].transform.DOMove(m_InitPoint[i], 2f, false);
            m_Child[i].transform.DOScale(m_InitScale[i], 2f);

        }

    }

    private GameObject FindChild(string name)
    {
        return GameObject.Find(name);
    }


    //获取所有子对象
    public List<GameObject> GetChild(Transform obj)
    {
        List<GameObject> tempArrayobj = new List<GameObject>();
        foreach (Transform child in obj)
        {
            tempArrayobj.Add(child.gameObject);
        }
        return tempArrayobj;
    }

    internal int StringSearch(string content, string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].Replace(" ", "") == content)
            {
                return i;
            }
        }
        return -1;
    }

    internal void OnCompleteNow()
    {
        ModelMove(btnName);
    }

    internal void OnProcessNow(float p)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// change mode to observation mode
    /// </summary>
    public void ObservationB()
    {
        print("go to observation mode");

        SceneManager.LoadScene("WholeModelScene");
    }

    /// <summary>
    /// get back to the current assemble step 
    /// </summary>
    public void BackB()
    {

        string scene = currentScene;
        Debug.Log(scene);
        SceneManager.LoadScene(scene);
    }
}
