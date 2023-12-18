using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Rabbit : Unit
{

    private void Start()
    {
        enemies.Clear();

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

        StartCoroutine(CheckForEnemiesRoutine());
    }

    private void Update()
    {

    }

    private IEnumerator CheckForEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        List<UnitUI> enemiesInRange = GetEnemiesInRange();
        if (enemiesInRange.Count > 0)
        {
            UnitUI selectedEnemy = ChooseEnemy(enemiesInRange);
            StartCoroutine(AttackToEnemy(selectedEnemy, currentTile));
        }
    }

    private List<UnitUI> GetEnemiesInRange()
    {
        enemies.Clear();

        List<UnitUI> enemiesInRange = new List<UnitUI>();

        if (team == Team.Player && currentTile.enemyUnitsOnTile.Count != 0)
        {
            enemiesInRange = currentTile.enemyUnitsOnTile.ConvertAll(enemyUI => enemyUI)
                .Where(IsWithinAttackRange)
                .ToList();
        }
        else if (team == Team.Enemy && currentTile.unitsOnTile.Count != 0)
        {
            enemiesInRange = currentTile.unitsOnTile.ConvertAll(playerUnitUI => playerUnitUI)
                .Where(IsWithinAttackRange)
                .ToList();
        }

        return enemiesInRange;
    }



    private bool IsWithinAttackRange(UnitUI enemy)
    {
        float distance = Vector2Int.Distance(gridPosition, enemy.unit.gridPosition);

        if (distance <= stats.length)
        {
            Debug.Log("LENFENFELFNENFEL");
            return true;
        }

        return false;
    }

    private UnitUI ChooseEnemy(List<UnitUI> enemiesInRange)
    {
        UnitUI selectedEnemy = null;
        float closset = float.MaxValue;

        foreach (UnitUI enemy in enemiesInRange)
        {
            float distance = Vector2Int.Distance(gridPosition, enemy.unit.gridPosition);

            if (distance < closset)
            {
                closset = distance;
                selectedEnemy = enemy;
            }
        }

        return selectedEnemy;
    }


    public override void MoveTo(Vector2Int newBoardPos, MiniMapTile targetTile)
    {
        Vector3 targetPosition = new(newBoardPos.x, newBoardPos.y, transform.position.z);
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.01f * Time.deltaTime);
        }

        gridPosition = newBoardPos;
        currentTile = targetTile;
    }


    private IEnumerator AttackToEnemy(UnitUI enemy, MiniMapTile targetTile)
    {
        if(enemy == null)
        {
            yield break;
        }

        GameController.instance.isFight = true;

        enemy.ChangeBattleImage();

        animator.SetTrigger("AttackDelay");
        animator.SetBool("Attack", true);

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
                enemy.unit.stats.healthPoint -= stats.attack;
                break;
        }

        if (enemy.unit.stats.healthPoint <= 0)
        {
            DestroyUnit(enemy);
        }

        yield return new WaitForSeconds(GameController.instance.preStats[(int)unitTypes]._stats.attackSpeed);

        GameController.instance.isFight = false;



        enemy.ChangeBattleImage();
    }


    public override void DestroyUnit(UnitUI unit)
    {
        if(unit == null)
        {
            return;
        }

        Destroy(unit.gameObject);
        Destroy(unit.unit.gameObject);

        if (unit.unit.team == Team.Player)
        {
            currentTile.unitsOnTile.Remove(unit.GetComponent<UnitUI>());
        }
        else if (unit.unit.team == Team.Enemy)
        {
            currentTile.enemyUnitsOnTile.Remove(unit.GetComponent<UnitUI>());
        }
    }
}

