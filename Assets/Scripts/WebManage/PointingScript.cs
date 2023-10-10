using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.EventSystems;
using static GlobalVariables;
using Unity.VisualScripting;

public class PointingScript : NetworkBehaviour
{
    [SyncVar] Vector3 mousePosition;
    [SyncVar] int mouseEvent;
    [SyncVar] float mouseProgress;



    [Tooltip("The image that may be used to show the hand-moved cursor on the screen or not. The sprite textures below need to be set too.")]
    public Image guiHandCursor;
    [Tooltip("Hand-cursor sprite texture, for the hand-grip state.")]
    public Sprite gripHandTexture;
    [Tooltip("Hand-cursor sprite texture, for the non-tracked state.")]
    public Sprite normalHandTexture;

    private Image cursorProgressBar;

    Rect rectCanvas;
    float canvasScale;


    // Start is called before the first frame update
    void Start()
    {

        if (isClientOnly)
        {
            transform.SetParent(GameObject.Find("WorkspaceCanvas").transform);

        }
        if (isServer)
        {

            transform.SetParent(GameObject.Find("WebCanvas").transform);
        }

        rectCanvas = guiHandCursor.canvas.pixelRect;
        canvasScale = guiHandCursor.canvas.scaleFactor;

        transform.position = new Vector3(16, 16, 0);
        GameObject objProgressBar = guiHandCursor && guiHandCursor.gameObject.transform.childCount > 0 ? guiHandCursor.transform.GetChild(0).gameObject : null;
        cursorProgressBar = objProgressBar ? objProgressBar.GetComponent<Image>() : null;

    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        { GetPos(); GetEvent(); GetProcess(); }
        if (isClient)
        {
            Sprite cursorTexture = null;
            handEvent = mouseEvent;
            //Vector2 pos = new Vector2(transform.position.x * canvasScale / rectCanvas.width, transform.position.y * canvasScale / rectCanvas.width);
            pointingPos = transform.position;
            clickProgress = mouseProgress;

            if (mouseEvent == 0)
            {
                cursorTexture = normalHandTexture;
            }
            else
            {
                cursorTexture = gripHandTexture;

                // */
            }
            if (cursorProgressBar)
            {
                cursorProgressBar.fillAmount = GlobalVariables.clickProgress;
            }



            if (cursorTexture != null) { gameObject.GetComponent<Image>().sprite = cursorTexture; }
            else { gameObject.GetComponent<Image>().sprite = normalHandTexture; }


        }
    }




    [Server]
    void GetPos()
    {
        //Vector2 popSprite = new Vector2(pointingPos.x * rectCanvas.width / canvasScale, pointingPos.y * rectCanvas.height / canvasScale);
       // transform.position = popSprite;
        transform.position = GlobalVariables.pointingPos;
    }

    [Server]
    void GetEvent()
    {
        mouseEvent = GlobalVariables.handEvent;
    }

    [Server]
    void GetProcess()
    {
        mouseProgress = GlobalVariables.clickProgress;
    }


}
