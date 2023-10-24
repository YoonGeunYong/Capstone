using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Entitiy(name)", menuName = "Scriptable Object Asset/Entity (name)")]
public class UnitStatsSO : ScriptableObject
{
    public UnitStats _stats;
    public AnimatorController _anicontroller;
    public Sprite _image;
    public Vector3 _defaultPosition;
    public Vector3 _defaultScale;
    
}