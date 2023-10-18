using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Entitiy(name)", menuName = "Scriptable Object Asset/Entity (name)")]
public class UnitStatsSO : ScriptableObject
{
    public UnitStats stats;
    public AnimatorController animatorController;
    public Sprite image;
    public Vector3 defaultPosition;
    public Vector3 defaultScale;
    public Vector3 defaultRotation;
    
}