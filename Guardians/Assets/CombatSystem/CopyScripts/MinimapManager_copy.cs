using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager_copy : MonoBehaviour
{
    public static MinimapManager_copy instance;
    public RectTransform minimapRect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}

