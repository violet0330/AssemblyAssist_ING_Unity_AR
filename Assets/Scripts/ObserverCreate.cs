using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;
using static GlobalVariables;

#if NETFX_CORE  //UWPœ¬±‡“Î
using Windows.Storage;
using Windows.Data.Xml.Dom;
#endif


public class ObserverCreate: MonoBehaviour
{
    //ObserverBehaviour imageTargetBehaviour;

    internal string xmlPathT;
    internal string databasePathT;
    string product;

    XmlDocument xmlDocumentUnity;
    bool modelTargetCreate;
    bool imgTargetCreate;


    // Start is called before the first frame update
    void Start()
    {
        
        product = model;
        Debug.Log(product);
        //info = GameObject.Find("info").GetComponent<TextMeshProUGUI>();
        xmlPathT = xmlPath[product];
        databasePathT = "Vuforia/" + database[product];
        modelTargetCreate = false;
        imgTargetCreate = false;
        //*/
    }


    public void OnClick()
    {
        /*
        TargetCreate();
        
        Debug.Log("begin");
        var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget("Vuforia/VuforiaTest.xml ", "hexagram");

        mImageTarget.enabled = true;
        mImageTarget.AddComponent<DefaultObserverEventHandler>();
        
        //*///
            //var mDatabase = VuforiaBehaviour.Instance.ObserverFactory.CreateBehavioursFromDatabase("Vuforia/fypdemo.xml");
            //var mModelTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateModelTarget("Vuforia/fypdemo.xml", "connectingRod");
            //var mModelTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateModelTarget("C:/Users/fengy/Downloads/JFAssemble/JFAssemble.xml", "upStep1m");

        TargetCreate();

        //imageTargetBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;

    }

    internal void TargetCreate()
    {

        if (!modelTargetCreate)
        {

            XMLReading();



        }
        else if (!imgTargetCreate)
        {
            //ImageTargetCreate();

        }

    }

    void XMLReading()
    {
        modelTargetCreate = true;
#if UNITY_EDITOR
        xmlDocumentUnity = GameObject.Find("GlobalVariables").GetComponent<Variable>().xmlDocumentUnity;
        xmlDocumentUnity.Load(xmlPathT);
        if (xmlDocumentUnity.SelectSingleNode("QCARConfig") != null)
        {
            XmlNodeList xmlNodeList = xmlDocumentUnity.SelectSingleNode("QCARConfig").ChildNodes;
            string models = null;
            foreach (XmlElement tracking in xmlNodeList)
            {
                if (tracking.Name == "Tracking")
                {
                    foreach (XmlElement modelTarget in tracking.ChildNodes)
                    {
                        string model = modelTarget.GetAttribute("name");
                        Debug.Log(model);
                        models = models + model + ";";
                        //ModelTargetCreate(model);

                    }
                }

            }
        }
        else
        {
            Debug.Log("null");
        }//*/
        TargetCreate();

#elif UNITY_WSA 

            if ( GameObject.Find("GlobalVariables").GetComponent<Variable>().xmlDocumentWSA.SelectSingleNode("QCARConfig") != null)
            {
                Windows.Data.Xml.Dom.XmlNodeList xmlNodeList =  GameObject.Find("GlobalVariables").GetComponent<Variable>().xmlDocumentWSA.GetElementsByTagName("ModelTarget");
                string models = null;
                foreach (Windows.Data.Xml.Dom.XmlElement modelTarget in xmlNodeList)
                {
                    string model = modelTarget.GetAttribute("name");
                    models = models + model + ";";
                    ModelTargetCreate(model);

                }

            }
        TargetCreate();

#endif       

    }


    void ModelTargetCreate(string model)
    {

        if (GameObject.Find(model) == null)
        {
            Debug.Log(databasePathT + "/" + model);
            var mModelTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateModelTarget(databasePathT, model);
            mModelTarget.enabled = true;
            mModelTarget.AddComponent<DefaultObserverEventHandler>();
        }

    }

    void ImageTargetCreate()
    {
        foreach (var jpg in GlobalVariables.jpgPath)
        {
            var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(jpg.Value, 1, jpg.Key);
            mImageTarget.enabled = true;
            mImageTarget.AddComponent<DefaultObserverEventHandler>();
        }
        imgTargetCreate = true;
        TargetCreate();
    }
    //*/


    // Update is called once per frame
    void Update()
    {

    }
}
