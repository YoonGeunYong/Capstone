using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Rabbit : Unit
{
    private void Start()
    {
        unitTypes = UnitTypes.Rabbit;
        stats = new UnitStats();
        stats.attack = 10;
        stats.defend = 0;
        stats.healthPoint = 100;
        stats.attackSpeed = 1;
        stats.moveSpeed = 20;
        stats.delay = 0;
        stats.attackType = 0;
        stats.length = 1;
        stats.coast = 1;
    }

    public override IEnumerator MoveTo(Vector2Int newBoardPos)
    {
        Vector3 targetPosition = new Vector3(newBoardPos.x * 10, transform.position.y, newBoardPos.y * 10);

        MiniMapTile targetTile = MiniMap.instance.GetTileAt(newBoardPos);
        Unit enemy = targetTile.GetEnemy();

        // If there are enemies in the target tile, start attacking
        if (enemy != null)
        {
            yield return Attack(enemy);
            yield break;
        }

        // If no enemies, start moving
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Ensure the unit reaches the exact target position
        boardPosition = newBoardPos; // Update the board position
    }


    public override IEnumerator Attack(Unit enemy)
    {
        Debug.Log("Rabbit Attack");
        // Attack logic
        while (enemy != null && enemy.stats.healthPoint > 0)
        {
            // Attack delay
            yield return new WaitForSeconds(stats.attackSpeed);

            // Attack the enemy
            enemy.stats.healthPoint -= stats.attack;

            // Check if the enemy is still alive
            if (enemy.stats.healthPoint <= 0)
            {
                // Remove the enemy
                enemy = null;
            }
        }

        // If no more enemies, continue moving
        if (enemy == null)
        {
            yield return MoveTo(boardPosition);
        }
    }
}

