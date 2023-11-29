using UnityEngine;

public class BaseTower : MonoBehaviour
{

    public Team     team;
    public int      maxHealth = 100;
    public int      attack = 10;
    private int     currentHealth;

    public enum Team
    {

        Player,
        AI

    }

    private void Start()
    {

        currentHealth = maxHealth;

    }


    public void AttackEnemy(Unit enemy)
    {

        int damage = (int)Mathf.Max(0, attack - enemy.stats.defend);

        enemy.stats.healthPoint -= damage;

    }

}