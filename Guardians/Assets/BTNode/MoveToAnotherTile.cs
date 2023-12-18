using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToAnotherTile : Action
{
    public static bool IsNodeRunning { get; private set; } = false;

    public SharedInt selectedMoveTileIndex;

    public override TaskStatus OnUpdate()
    {
        IsNodeRunning = true;

        if (MiniMap.instance.selectedMiniMapTile != null)
        {
            MiniMap.instance.MoveUnitTo(GetSelectedMoveTile(), false);

            IsNodeRunning = false;

            EnemyAgent.instance.AddReward(EnemyAgent.instance.RewardFunc());

            GameController.instance.EndAITurn();

            return TaskStatus.Success;
        }
        else
        {
            // 선택된 타일이 없다면 실패를 반환
            IsNodeRunning = false;

            GameController.instance.EndAITurn();

            return TaskStatus.Failure;
        }
    }

    private MiniMapTile GetSelectedMoveTile()
    {
        selectedMoveTileIndex = Owner.GetVariable("selectedMoveTileIndex") as SharedInt;
        int selectedMoveTileIndexValue = selectedMoveTileIndex.Value;

        Debug.Log("selectedMoveTileIndex.Value: " + selectedMoveTileIndexValue);

        // 현재 선택된 타일의 좌표를 가져오기
        int currentX = MiniMap.instance.selectedMiniMapTile.originalPosition.x;
        int currentY = MiniMap.instance.selectedMiniMapTile.originalPosition.y;

        int newX = currentX;
        int newY = currentY;

        // selectedMoveTileIndex.Value에 따라 다른 방향으로 이동할 좌표 설정
        switch (selectedMoveTileIndexValue)
        {
            case 0: newY -= 1; break; // 아래로 이동
            case 1: newX += 1; break; // 오른쪽으로 이동
            case 2: newY += 1; break; // 위로 이동
            case 3: newX -= 1; break; // 왼쪽으로 이동
            default:
                // 예외 처리: 잘못된 인덱스에 대한 기본값
                Debug.LogError("Invalid selectedMoveTileIndex.Value: " + selectedMoveTileIndex.Value);
                return MiniMap.instance.selectedMiniMapTile; // 현재 타일 반환
        }

        // 범위 체크 후 반환
        if (newX < 0 || newX >= MiniMap.instance.width || newY < 0 || newY >= MiniMap.instance.height)
        {
            Debug.Log("Invalid tile index: " + newX + ", " + newY);

            return MiniMap.instance.selectedMiniMapTile;
        }

        Debug.Log("Move Unit: " + newX + ", " + newY);

        return MiniMap.instance.miniMapTiles[newX, newY];
    }


}
