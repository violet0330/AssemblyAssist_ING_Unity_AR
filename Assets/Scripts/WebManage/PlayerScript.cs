using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Globalization;
using UnityEngine.UI;

public class PlayerScript : NetworkBehaviour
{
    public GameObject projectilePrefab;

    public override void OnStartLocalPlayer()
    {
        // Camera.main.transform.SetParent(transform);
        // Camera.main.transform.localPosition = new Vector3(0, 0, 0);

    }

    void Start()
    {
        if (isClientOnly)
        {
            int state = GlobalVariables.kinectState;
            Button wholeModelB = GameObject.Find("WholeModelB").GetComponent<Button>();
            wholeModelB.onClick.AddListener(KinectGestureOn);
            CmdStateChange(state);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("1");
            Pointing();
        }
        if (!isLocalPlayer) { return; }

        if (!Application.isFocused) return;

        if (isLocalPlayer)
        {
        }


    }

    void StateChange()
    {

    }



    /// <summary>
    /// called when the state of kinect need to be changed
    /// </summary>
    /// <param name="state">0 = interaction; 1 = gesture; 2 = close</param>
    [Command]
    internal void CmdStateChange(int state)
    {
        GlobalVariables.kinectState = state;
        if (state == 0)
        {
            Pointing();
        }
        else if(state == 1)
        {

        }
        else
        {

        }

    }


    // this is called on the server
    [Server]
    void Pointing()
    {
        GameObject projectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.EulerAngles(Vector3.zero));
        NetworkServer.Spawn(projectile);

    }

    void KinectGestureOn()
    {
        CmdStateChange(1);
    }
}
