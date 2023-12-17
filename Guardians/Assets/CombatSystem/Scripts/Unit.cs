using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public enum UnitTypes
{
    Rabbit, Turtle, Fox, WoodCutter, Fairy, Deer, Heungbu, Nolbu, Swallow
}


public abstract class Unit : MonoBehaviour
{
    public enum Team
    {
        Player,
        Enemy
    }

    public Team team;
    public Unit enemy;
    public UnitStats stats;
    public UnitTypes unitTypes;
    public UnitStatsSO statsSO;
    public Vector2Int boardPosition;
    public Animator animator;
    public GameObject childItem;
    public float attackDelay;
    public bool enemyCheck;

    public abstract void MoveTo(Vector2Int newBoardPos);

    public abstract void Attack(List<Unit> enemies);

    public abstract void DestroyUnit(Unit unit);
}
