using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entitiy(name)", menuName = "Scriptable Object Asset/Entity (name)")]
public class UnitStatsSO : ScriptableObject
{
    public UnitStats _stats;
    public Animator _animator;
    public Sprite _image;
    public Vector3 _defaultPosition;
    public Vector3 _defaultScale;
    
}