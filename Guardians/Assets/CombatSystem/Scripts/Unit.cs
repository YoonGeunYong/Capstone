using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public enum UnitTypes
{
    Rabbit, Turtle, Fox, Deer, WoodCutter, Fairy, Heungbu, Nolbu, Swallow
}

public abstract class Unit : MonoBehaviour
{
    public UnitTypes unitTypes;
    public UnitStats stats;
    public Vector2Int boardPosition;

    public abstract IEnumerator MoveTo(Vector2Int newBoardPos);
    public abstract IEnumerator Attack(Unit enemy);
}
