using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class DelayCondition : Conditional
{
    public float delayTime = 1.0f; // ���� �ð�

    private float startTime; // ���� �ð�

    public override void OnStart()
    {
        startTime = Time.time; // ���� �ð� ����
    }

    public override TaskStatus OnUpdate()
    {
        // ���� �ð��� ���� �ð��� ���̰� ���� �ð����� ũ�ų� ������
        if (Time.time - startTime >= delayTime && !GameController.instance.isMoving)
        {
            return TaskStatus.Success; // ���� ���� ��ȯ
        }

        return TaskStatus.Running; // ���� �� ���� ��ȯ
    }
}
