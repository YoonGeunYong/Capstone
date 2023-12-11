using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SpawnUnitNode : Action
{
    public SharedGameObject baseGameObject;
    public GameObject unitUIPrefab;
    
    public override TaskStatus OnUpdate()
    {
        
        if (baseGameObject == null || baseGameObject.Value == null)
        {
            return TaskStatus.Failure;
        }

        Base baseScript = baseGameObject.Value.GetComponent<Base>();

        if (baseScript == null)
        {
            return TaskStatus.Failure;
        }

        // 자원이 충분한지 확인
        if (baseScript.GetResource() >= unitUIPrefab.GetComponent<UnitUI>().unit.stats.cost)
        {
            // 자원이 충분하면 유닛 소환
            Debug.Log(baseScript.position / 10);
            
            baseScript.SpawnUnit(unitUIPrefab, Unit.Team.Enemy, baseScript.position, UnitTypes.Rabbit);
            
            GameController.instance.EndAITurn();

            return TaskStatus.Success;
        }
        else
        {
            // 자원이 부족하면 실패
            return TaskStatus.Failure;
        }
       
    }
}
