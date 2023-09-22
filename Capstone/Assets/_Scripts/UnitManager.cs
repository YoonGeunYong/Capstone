using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit
{
    public float atk; // 공격력 Attack
    public float def; // 방어력 defend
    public float hp; // 체력 health point
	public float asd; // 공격속도 attackspeed
	public float msd; // 이동속도 movespeed
    public float delay; // 재사용시간
    public int atacktype; // 공격유형
    public float length; // 사거리
    public int coast; // 코스트
}

public class UnitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
