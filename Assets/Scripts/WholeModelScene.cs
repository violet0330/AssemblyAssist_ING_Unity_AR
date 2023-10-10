using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeModelScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("ARGuideTimer") != null)
        {
            Timer timer = GameObject.Find("ARGuideTimer").GetComponent<Timer>();

            if (timer.isActiveAndEnabled == true)
            {
                timer.pauseTimer();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
