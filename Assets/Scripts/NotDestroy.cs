using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestroy : MonoBehaviour
{


    /// <summary>
    /// don't move!
    /// </summary>
    private static NotDestroy instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
