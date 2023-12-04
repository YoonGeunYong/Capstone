using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using System;

[CreateAssetMenu(fileName = "Entitiy(name)", menuName = "Scriptable Object Asset/Entity (name)")]
public class UnitStatsSO : ScriptableObject
{
    public UnitStats _stats;
    public RuntimeAnimatorController _anicontroller;
    public Sprite _image;
    public Vector3 _defaultPosition;
    public Vector3 _defaultScale;
    
}

[Serializable]
public  class UnitStats
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

    public void InitStats() { }
}