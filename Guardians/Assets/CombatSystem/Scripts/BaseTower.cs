using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{

    public Team     team;
    public int      currentHealth;
    public int      maxHealth;
    public int      attackDamage;

    private int attackRange;
    private MiniMapTile currentTile;

    public enum Team
    {

        Player,
        AI

    }

    private void Start()
    {

        currentHealth = 100;
        maxHealth = 100;
        attackDamage = 10;
        attackRange = 20;
        if(team == Team.Player) currentTile = MiniMap.instance.miniMapTiles[0, 0];
        else currentTile = MiniMap.instance.miniMapTiles[MiniMap.instance.width - 1, MiniMap.instance.height - 1];

    }

    private void Update()
    {
        List<Unit> units = new List<Unit>();

        if( team == Team.Player)
        {
            if (currentTile.enemyUnitsOnTile.Count == 0) return;
            
            foreach (UnitUI unitUI in currentTile.enemyUnitsOnTile)
            {
                units.Add(unitUI.unit);
            }
                 
        }
        else
        {
            if (currentTile.unitsOnTile.Count == 0) return;

            foreach (UnitUI unitUI in currentTile.unitsOnTile)
            {
                units.Add(unitUI.unit);
            }
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            
        }
    }


    public void AttackEnemy(Unit enemy)
    {

        int damage = (int)Mathf.Max(0, attackDamage - enemy.stats.defend);

        enemy.stats.healthPoint -= damage;

    }

}