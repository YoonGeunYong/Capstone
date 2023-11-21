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

    public override void MoveTo(Vector2Int newBoardPos)
    {
        Vector3 targetPosition = new Vector3(newBoardPos.x * 10, transform.position.y, newBoardPos.y * 10);

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

        foreach (Unit enemy in enemies)
        {
            // ���� ��Ÿ� üũ
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= stats.length)
            {
                // ���� ������ üũ
                if (Time.time >= stats.delay)
                {
                    Debug.Log("Rabbit dealing damage to enemy");

                    // ���� ������ ����� ���� ���ط� ���
                    int damage = (int)Mathf.Max(0, stats.attack - enemy.stats.defend);

                    // ������ ���� ������
                    enemy.stats.healthPoint -= damage;

                    // ���� �� ������ ����
                    stats.delay = Time.time + 1.0f / stats.attackSpeed;

                    // ü���� 0 ������ ��� �� ���� ����
                    if (enemy.stats.healthPoint <= 0)
                    {
                        DestroyUnit(enemy);
                    }
                }
                else
                {
                    Debug.Log("Rabbit attack on cooldown");
                }
            }
            else
            {
                Debug.Log("Rabbit out of attack range");
            }
        }

        // ���� �� �̵�
        MoveTo(boardPosition);
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

