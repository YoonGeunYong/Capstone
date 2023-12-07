using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    public GameObject baseObject;
    public Text resourceText;

    private Base baseScript;

    private void Awake()
    {
        if (baseObject != null)
        {
            baseObject = Instantiate(baseObject);
            GameController.instance.playerBaseObject = baseObject;
            baseScript = baseObject.GetComponent<Base>();
        }
    }

    private void Update()
    {
        if (baseScript != null && resourceText != null)
        {
            // Base 스크립트에서 자원 값을 가져와 UI에 표시
            resourceText.text = "Resources: " + baseScript.GetResource().ToString();
        }
    }
}
