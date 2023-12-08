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

    public Team             team;
    public Unit             enemy;  //12.08 'pursuit to enemy' add
    public UnitTypes        unitTypes;
    public UnitStats        stats;
    public UnitStatsSO      statsSO;
    public Vector2Int       boardPosition;
    public Animator         animator;
    public GameObject       childItem;
    public float            attackDelay; //12.08 The time between attacks
    public bool             enemyCheck;  //12.08 near enemy check

    public abstract void    MoveTo(Vector2Int newBoardPos);
    public abstract void    Attack(List<Unit> enemy);
    public abstract void    DestroyUnit(Unit unit);
}
