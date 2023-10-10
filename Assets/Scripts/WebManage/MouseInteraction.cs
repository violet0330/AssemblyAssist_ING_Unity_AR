using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GlobalVariables.pointingPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
            GlobalVariables.handEvent = 1;
        else if(Input.GetMouseButtonUp(0))
            GlobalVariables.handEvent = 0;
    }
}
