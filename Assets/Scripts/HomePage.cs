using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour
{
   public GameObject UserSignInCanvas;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR


#else
        if (Display.displays.Length > 1)
        {
            // Activate the display 1 (second monitor connected to the system).
            Display.displays[1].Activate();
        }
#endif

    }

    private void Awake()
    {

    }

    public void BeginB()
    {
        UserSignInCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitB()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
