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
            // ���õ� Ÿ���� ���ٸ� ���и� ��ȯ
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

        // ���� ���õ� Ÿ���� ��ǥ�� ��������
        int currentX = MiniMap.instance.selectedMiniMapTile.originalPosition.x;
        int currentY = MiniMap.instance.selectedMiniMapTile.originalPosition.y;

        int newX = currentX;
        int newY = currentY;

        // selectedMoveTileIndex.Value�� ���� �ٸ� �������� �̵��� ��ǥ ����
        switch (selectedMoveTileIndexValue)
        {
            case 0: newY -= 1; break; // �Ʒ��� �̵�
            case 1: newX += 1; break; // ���������� �̵�
            case 2: newY += 1; break; // ���� �̵�
            case 3: newX -= 1; break; // �������� �̵�
            default:
                // ���� ó��: �߸��� �ε����� ���� �⺻��
                Debug.LogError("Invalid selectedMoveTileIndex.Value: " + selectedMoveTileIndex.Value);
                return MiniMap.instance.selectedMiniMapTile; // ���� Ÿ�� ��ȯ
        }

        // ���� üũ �� ��ȯ
        if (newX < 0 || newX >= MiniMap.instance.width || newY < 0 || newY >= MiniMap.instance.height)
        {
            Debug.Log("Invalid tile index: " + newX + ", " + newY);

            return MiniMap.instance.selectedMiniMapTile;
        }

        Debug.Log("Move Unit: " + newX + ", " + newY);

        return MiniMap.instance.miniMapTiles[newX, newY];
    }


}
