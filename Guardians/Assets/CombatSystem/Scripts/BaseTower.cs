using UnityEngine;

public class BaseTower : MonoBehaviour
{

    public Team     team;
    public int      currentHealth;
    public int      maxHealth;
    public int      attackDamage;

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

    }


    public void AttackEnemy(Unit enemy)
    {

        int damage = (int)Mathf.Max(0, attackDamage - enemy.stats.defend);

        enemy.stats.healthPoint -= damage;

    }

}