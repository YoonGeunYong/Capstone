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

    public enum Team
    {
        Player,
        Enemy
    }

    public Team team;
    public UnitTypes unitTypes;
    public UnitStats stats;
    public Vector2Int boardPosition;
    
    public Vector2 tilePosition;

    public virtual void MoveTo(Vector2Int newBoardPos)
    {
        Vector3 targetPosition = new Vector3(newBoardPos.x * 10, newBoardPos.y * 10, transform.position.z);

        MiniMapTile targetTile = MiniMap.instance.GetTileAt(newBoardPos);

        targetTile.includedUnits.Add(this);
        
        // If no enemies, start moving
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
            Debug.Log(targetPosition);
            break;
        }

        transform.position = targetPosition; // Ensure the unit reaches the exact target position
        boardPosition = newBoardPos;         // Update the board position
        tilePosition = targetTile.transform.position;
    }
    
    public abstract void Attack(List<Unit> enemy);
    public abstract void DestroyUnit(Unit unit);
}
