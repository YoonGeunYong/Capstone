using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Rabbit : Unit
{
    private void Start()
    {
        unitTypes = statsSO.unitType;
        stats = UnitStats.Clone(unitTypes);
        animator = GetComponent<Animator>();
        if (team == Team.Enemy)
        {
            GetComponent<SpriteRenderer>().sprite = statsSO._enemyImage;
            animator.runtimeAnimatorController = statsSO._enemyAnicontroller;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = statsSO._image;
            animator.runtimeAnimatorController = statsSO._anicontroller;
        }
        if (statsSO._childItem is not null)
            childItem = statsSO._childItem;
    }
    private void Update()
    {                                                                                                                                                                                                                                                    
        if (!GameController.instance.isFight && !enemyCheck)
            return;

        attackDelay += Time.deltaTime;
    }

    public override void MoveTo(Vector2Int newBoardPos)
    {
        Vector3 targetPosition = new Vector3(newBoardPos.x * 20, newBoardPos.y * 20, transform.position.z);

        MiniMapTile targetTile = MiniMap.instance.GetTileAt(newBoardPos);

        // If no enemies, start moving
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 20 * Time.deltaTime);
            Debug.Log(targetPosition);
            break;
        }

        transform.position = targetPosition; // Ensure the unit reaches the exact target position
        boardPosition = newBoardPos; // Update the board position
    }

    public override void Attack(List<Unit> enemies)
    {
        Debug.Log("Rabbit Attack");

        if (!GameController.instance.isFight)
            return;
        
        StartCoroutine(AttacktoEnemy(enemies));
        
        
        // Attack all enemies on the tile
        /*for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemyCheck)
            {
                enemy = enemies[i];
                enemyCheck = true;
            }

            // Attack the enemy
            //enemy.stats.healthPoint -= stats.attack;

            // Attack enemy Animation And Health Loss
            // but this method can't work
            StartCoroutine(AttacktoEnemy(enemies));

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
        }*/
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

    IEnumerator AttacktoEnemy(List<Unit> enemies)
    {
        while (enemies.Count != 0)
        {
            animator.SetTrigger("AttackDelay");
            EnemyCheck(enemies);

            yield return new WaitForSeconds(1f);
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            switch (unitTypes)
            {
                case UnitTypes.Fox: //solo long range
                    Instantiate(childItem, position, Quaternion.identity).transform.parent = transform;
                    break;
                case UnitTypes.Fairy: //solo long range
                    Instantiate(childItem, position, Quaternion.identity).transform.parent = transform;
                    break;
                case UnitTypes.Swallow: //solo long range
                    Instantiate(childItem, position, Quaternion.identity).transform.parent = transform;
                    break;
                case UnitTypes.Nolbu: //solo long range
                    Instantiate(childItem, position, Quaternion.identity).transform.parent = transform;
                    break;
                case UnitTypes.Heungbu: //multi long range
                    Instantiate(childItem, position, Quaternion.identity).transform.parent = transform;
                    break;
                default: // Only Attack 12.12
                    enemy.stats.healthPoint -= stats.attack;
                    break;
            }
            yield return new WaitForSeconds(GameController.instance.preStats[(int)unitTypes]._stats.attackSpeed);
        } 
        
        GameController.instance.isFight = false;
        MoveTo(boardPosition);
        //MiniMap.instance.miniMapTiles[boardPosition.x, boardPosition.y].enemyUnitsOnTile.Clear();
    }

    private void EnemyCheck(List<Unit> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemyCheck)
            {
                enemy = enemies[i];
                enemyCheck = true;
            }

            // Check if the enemy is still alive
            if (!(enemy.stats.healthPoint <= 0)) continue;
            
            // Remove the enemy from the list
            enemyCheck = false;
            enemies.RemoveAt(i);
            i--;
            DestroyUnit(enemy); // Destroy the enemy unit
        }
    }

}

