using System.Collections;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    public bool IsEnemy { get; set; }

    public Unit             unit;

    public MiniMapTile CurrentTile
    {
        get => currentTile;
        set => currentTile = value;
    } // 이 유닛이 현재 속한 타일

    public Vector2Int       boardPosition;
    [SerializeField] private MiniMapTile currentTile;
}



