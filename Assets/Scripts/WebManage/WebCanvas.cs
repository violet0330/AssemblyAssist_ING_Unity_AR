using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;

public class WebCanvas : MonoBehaviour
{
    NetworkManager manager;

    [Header("UI Elements")]
    internal TextMeshProUGUI msgText;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    private void Start()
    {

        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
#if UNITY_WSA
            manager.StartClient();
            //SceneManager.LoadSceneAsync("VuforiaScene", LoadSceneMode.Additive);
#else
            manager.StartHost();
            SceneManager.LoadSceneAsync("MouseInteraction", LoadSceneMode.Additive);
#endif
        }


#if UNITY_EDITOR


#else
        if (Display.displays.Length > 1)
        {
            // Activate the display 1 (second monitor connected to the system).
            Display.displays[1].Activate();
        }
#endif
    }


}
