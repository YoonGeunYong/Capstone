using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    public static CostManager CM;

    public Text costText;
    
    void Start()
    {
        CM = GetComponent<CostManager>();
    }

    
    void Update()
    {
       
    }
}
