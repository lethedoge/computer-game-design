using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    private static bool created = false;
    
    private void Awake()
    {
        if (!created)
        {
            // If this is the first instance of the script, mark the GameObject as DontDestroyOnLoad
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // If a previous instance of the script already exists, destroy this instance
            Destroy(this.gameObject);
        }
    }
}
