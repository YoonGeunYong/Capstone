using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Rabbit : Unit
{
    public override void MoveTo(Vector2Int newBoardPos)
    {
        Vector3 targetPosition = new Vector3(newBoardPos.x * 20, newBoardPos.y * 20, transform.position.z);

        MiniMapTile targetTile = MiniMap.instance.GetTileAt(newBoardPos);

        // If no enemies, start moving
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
            Debug.Log(targetPosition);
            break;
        }

        transform.position = targetPosition; // Ensure the unit reaches the exact target position
        boardPosition = newBoardPos; // Update the board position
    }

    public override void Attack(List<Unit> enemies)
    {
        Debug.Log("Rabbit Attack");

        // Attack all enemies on the tile
        for (int i = 0; i < enemies.Count; i++)
        {
            Unit enemy = enemies[i];

            // Attack the enemy
            //enemy.stats.healthPoint -= stats.attack;

            // Check if the enemy is still alive
            if (enemy.stats.healthPoint <= 0)
            {
                // Remove the enemy from the list
                enemies.RemoveAt(i);
                i--;
                DestroyUnit(enemy); // Destroy the enemy unit
            }
        }

        // If no more enemies, continue moving
        if (enemies.Count == 0)
        {
            MoveTo(boardPosition);
        }
    }

    public override void DestroyUnit(Unit unit)
    {
        // Check if the unit has a UnitUI component attached
        UnitUI unitUI = unit.GetComponent<UnitUI>();

        // Remove the unit from the game
        Destroy(unit.gameObject);

        
        if (unitUI != null)
        {
            // Remove the associated UnitUI from the game
            Destroy(unitUI.gameObject);
        }

        // Perform any necessary cleanup or additional logic here
        // ...
    }

}

