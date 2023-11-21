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

    // �÷��̾ �ΰ������� ȣ���ϴ� �޼ҵ��, ���� ���� ������ ���� �� ȣ��˴ϴ�.
    public void AttackEnemy(Unit enemy)
    {
        // ���� ������ ����� ���� ���ط� ���
        int damage = (int)Mathf.Max(0, attack - enemy.stats.defend);

        // ������ ���� ������
        enemy.stats.healthPoint -= damage;
    }

}
