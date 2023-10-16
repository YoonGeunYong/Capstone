using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public enum Unit
{
    Rabbit, Turtle, Fox, Deer, WoodCutter, Fairy, Heungbu, Nolbu, Swallow 
}

[Serializable]
public class UnitStats
{
    public float attack;      // ���ݷ�
    public float defend;      // ����
    public float healthPoint; // ü��
    public float attackSpeed; // ���ݼӵ�
    public float moveSpeed;   // �̵��ӵ�
    public float delay;       // ����ð�
    public int attackType;    // ��������
    public float length;      // ��Ÿ�
    public int coast;         // �ڽ�Ʈ
}


public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitStatsSO[] _preStats;

    //[SerializeField] private UnitStatsSO _preStats;
    [SerializeField] private UnitStats _stats;

    public Unit unit;
    public int index;
    

    // Start is called before the first frame update

    void Start()
    {
        if (_preStats is not null)
        {
            _stats = _preStats[index]._stats;
        }
        unit = (Unit)index;

        GetComponent<SpriteRenderer>().sprite = _preStats[index]._image;
        GetComponent<Transform>().position = _preStats[index]._defaultPosition;
        GetComponent<Transform>().localScale = _preStats[index]._defaultScale;
        GetComponent<Animator>().runtimeAnimatorController = _preStats[index]._anicontroller;

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