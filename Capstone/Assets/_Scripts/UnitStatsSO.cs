using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Scriptable Object Asset/UnitStats")]
public class UnitStatsSO : ScriptableObject
{
    public UnitStats _stats;
    public Sprite[] _images;
    public AudioClip _sound;
}