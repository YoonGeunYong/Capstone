using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    public bool IsEnemy { get; set; }

    public Unit             unit;
    public MiniMapTile      CurrentTile { get; set; }  // �� ������ ���� ���� Ÿ��
    public Vector2Int       boardPosition;
    public Sprite           enemyIcon;

    private void Start()
    {
        if (unit.team == Unit.Team.Enemy)
            GetComponent<SpriteRenderer>().sprite = enemyIcon;
    }
}



