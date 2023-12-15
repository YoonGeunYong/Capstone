using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtonControl : MonoBehaviour
{
    private Button[] button;

    void Start()
    {
        button = new Button[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            button[i] = transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
    
    public void ButtonActive()
    {
        var playerBase = GameController.instance.playerBaseObject.GetComponent<Base>();
        for (var i = 0; i < button.Length; i++)
        {
            if (GameController.instance.preStats[i]._stats.cost > playerBase.GetResource())
            {
                button[i].interactable = false;
            }
            else
            {
                button[i].interactable = true;
            }
        }
    }
}
