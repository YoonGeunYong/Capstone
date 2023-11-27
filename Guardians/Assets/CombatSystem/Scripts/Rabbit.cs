using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Rabbit : Unit
{
    private void Start()
    {
        unitTypes = UnitTypes.Rabbit;
        team = Team.Player;
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
        // Remove the unit from the game
        Destroy(unit.gameObject);

        // Check if the unit has a UnitUI component attached
        UnitUI unitUI = unit.GetComponent<UnitUI>();
        if (unitUI != null)
        {
            // Remove the associated UnitUI from the game
            Destroy(unitUI.gameObject);
        }

        // Perform any necessary cleanup or additional logic here
        // ...
    }




}

