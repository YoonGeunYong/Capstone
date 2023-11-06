using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public enum UnitTypes
{
    Rabbit, Turtle, Fox, Deer, WoodCutter, Fairy, Heungbu, Nolbu, Swallow 
}

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
        if (preStats is not null)
        {
            _stats = preStats[index]._stats;
        }
        unit = (UnitTypes)index;

        GetComponent<SpriteRenderer>().sprite = preStats[index]._image;
        GetComponent<Transform>().position = preStats[index]._defaultPosition;
        GetComponent<Transform>().localScale = preStats[index]._defaultScale;
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