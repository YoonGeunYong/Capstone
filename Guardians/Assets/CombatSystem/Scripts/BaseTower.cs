using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public enum Team
    {
        Player,
        AI
    }

    public Team team;
    public int maxHealth = 100;
    public int attack = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // 플레이어나 인공지능이 호출하는 메소드로, 적이 공격 범위에 들어올 때 호출됩니다.
    public void AttackEnemy(Unit enemy)
    {
        // 적의 방어력을 고려한 실제 피해량 계산
        int damage = (int)Mathf.Max(0, attack - enemy.stats.defend);

        // 적에게 피해 입히기
        enemy.stats.healthPoint -= damage;
    }

}
