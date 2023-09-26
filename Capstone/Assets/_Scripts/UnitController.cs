using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Unit { RABBIT, TURTLE}

[Serializable]
public class UnitStats
{
    public float attack; // ���ݷ�
    public float defend; // ����
    public float healthPoint; // ü��
	public float attackSpeed; // ���ݼӵ�
	public float moveSpeed; // �̵��ӵ�
    public float delay; // ����ð�
    public int atacktype; // ��������
    public float length; // ��Ÿ�
    public int coast; // �ڽ�Ʈ
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
        if (_preStats is not null)
        {
            _stats = _preStats[(int)unit]._stats;
        }
        GetComponent<SpriteRenderer>().sprite = _preStats[(int)unit]._image;
        GetComponent<Transform>().position = _preStats[(int)unit]._defaultPosition;
        GetComponent<Transform>().localScale = _preStats[(int)unit]._defaultScale;
        Debug.Log(unit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnit(int index)
    {
        unit = (Unit)index;
        Instantiate(this);
    }
}
