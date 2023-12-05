using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;


public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStatsSO[] preStats;
    [SerializeField] private UnitStats _stats;

    public Vector2Int boardPosition;
    
    public UnitTypes unit;
    public int index;
    

    // Start is called before the first frame update

    void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = preStats[index]._anicontroller;

        Debug.Log(unit);
    }

    void Update()
    {
    }

    // Update is called once per frame


    public void SpawnUnit(int num)
    {
        index = num;
        Instantiate(this);
    }
}