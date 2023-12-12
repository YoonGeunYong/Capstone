using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using UnityEngine;
using UnityEngine.UI;

public class CutToonController : MonoBehaviour
{
    public GameObject[] image;
    public GameObject nextSceneObj;
    public int index = -1;
    private float time = 1.5f;

    private void Update()
    {
        time += Time.deltaTime;

        if (index == 6 && time > 1.5f)
        {
            nextSceneObj.SetActive(true);
            transform.Find("Next").gameObject.SetActive(false);
        }
    }

    public void ClickDisplay()
    {
        if (time > 1.5f && index != 6)
        {
            index++;
            image[index].SetActive(true);
            time = 0;
        }
    }
}
