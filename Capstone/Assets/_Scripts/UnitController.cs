using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Unit
{
    RABBIT,
    TURTLE
}

[Serializable]
public class UnitStats
{
    public float attack;      // 공격력
    public float defend;      // 방어력
    public float healthPoint; // 체력
    public float attackSpeed; // 공격속도
    public float moveSpeed;   // 이동속도
    public float delay;       // 재사용시간
    public int attackType;    // 공격유형
    public float length;      // 사거리
    public int coast;         // 코스트
}


public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStatsSO[] _preStats;

    //[SerializeField] private UnitStatsSO _preStats;
    [SerializeField] private UnitStats _stats;

    public Unit unit;

    int index;

    // Start is called before the first frame update

    void Start()
    {

    }

    void Update()
    {
    }

    // Update is called once per frame


    public void SpawnUnit(int index, GameObject unit)
    {
        Instantiate(this);
        
        if (_preStats is not null)
        {
            _stats = _preStats[index]._stats;
        }

        GetComponent<SpriteRenderer>().sprite = _preStats[index]._image;
        GetComponent<Transform>().position = _preStats[index]._defaultPosition;
        GetComponent<Transform>().localScale = _preStats[index]._defaultScale;

        Debug.Log(unit);
    }
}