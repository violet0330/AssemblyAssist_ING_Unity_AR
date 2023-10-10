using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static GlobalVariables;

public class DetailButton : MonoBehaviour
{

    internal MeshRenderer[] modelMesh;
    public GameObject wholeModelButton;
    
    bool details;
    internal Vector3 MsPos;
    internal Vector3 MsScale;

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
        else if (nowModel == "JF")
        {
            SceneManager.LoadScene("WholeModel2Scene");
        }
        else
        {

        }

    }

    public void DetailComponentB()
    {

    }


    public void DetailAssemblyB()
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

    internal void HideModel(string modelname)
    {
        GameObject modelShow = GameObject.Find(modelname);
        modelMesh = modelShow.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in modelMesh)
        {
            if (child.enabled == true)
            {
                Debug.Log("hide£º" + child.name);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
