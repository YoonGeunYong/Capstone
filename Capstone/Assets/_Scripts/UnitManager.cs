using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitStats
{
    public float atk; // ���ݷ� Attack
    public float def; // ���� defend
    public float hp; // ü�� health point
	public float asd; // ���ݼӵ� attackspeed
	public float msd; // �̵��ӵ� movespeed
    public float delay; // ����ð�
    public int atacktype; // ��������
    public float length; // ��Ÿ�
    public int coast; // �ڽ�Ʈ
}


public class UnitManager : MonoBehaviour
{
    [SerializeField] private UnitStatsSO _preStats;
    private UnitStats _stats;

    // Start is called before the first frame update
    void Start()
    {
        if (_preStats is not null)
        {
            _stats = _preStats._stats;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
