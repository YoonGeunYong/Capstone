using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAnimation : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("OnTurn");
    }
    
    IEnumerator OnTurn()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
