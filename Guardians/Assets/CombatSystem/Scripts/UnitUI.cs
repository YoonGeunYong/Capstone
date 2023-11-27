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
    } // �� ������ ���� ���� Ÿ��

    public Vector2Int       boardPosition;
    [SerializeField] private MiniMapTile currentTile;
}



