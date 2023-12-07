using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class DelayCondition : Conditional
{
    public float delayTime = 1.0f; // 지연 시간

    private float startTime; // 시작 시간

    public override void OnStart()
    {
        startTime = Time.time; // 시작 시간 설정
    }

    public override TaskStatus OnUpdate()
    {
        // 현재 시간과 시작 시간의 차이가 지연 시간보다 크거나 같으면
        if (Time.time - startTime >= delayTime)
        {
            return TaskStatus.Success; // 성공 상태 반환
        }

        return TaskStatus.Running; // 실행 중 상태 반환
    }
}
