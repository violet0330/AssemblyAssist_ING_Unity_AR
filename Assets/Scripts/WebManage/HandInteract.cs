using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HandInteract : MonoBehaviour
{

    public EventSystem _mEventSystem;
    public GraphicRaycaster gra;

    bool open;

    int mouseEvent;
    List<RaycastResult> list;
    int frame = 0;
    bool click = true;

    Rect rectCanvas;
    float canvasScale;

    // Start is called before the first frame update
    void Start()
    {
        open = GlobalVariables.kinectOn;
        if (_mEventSystem == null)
        {
            _mEventSystem = EventSystem.current;
        }
       // rectCanvas = gameObject.transform.GetComponentInParent<Canvas>().pixelRect;
        //canvasScale = gameObject.transform.GetComponentInParent<Canvas>().scaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Vector2 pos = GlobalVariables.pointingPos;
            transform.position = pos;
            
            mouseEvent = GlobalVariables.handEvent;
           // Debug.Log("event " + mouseEvent);

            if (mouseEvent == 1)
            {
                if (click)
                {
                    Debug.Log("click");
                    OnClick();
                    click = false;

                }

            }


            if (!click)
            {
                frame++;
                if (frame == 60)
                {
                    click = true;
                    frame = 0;
                }
            }
        }

    }




    void OnClick()
    {
        list = GraphicRaycaster(GlobalVariables.pointingPos);
        foreach (var item in list)
        {
            if (item.gameObject.GetComponent<UnityEngine.UI.Button>() != null)
            {
                Debug.Log(item);
                item.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

            }

        }
    }




    private List<RaycastResult> GraphicRaycaster(Vector2 pos)
    {
        var mPointerEventData = new PointerEventData(_mEventSystem);
        mPointerEventData.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();

        gra.Raycast(mPointerEventData, results);
        return results;
    }


}
