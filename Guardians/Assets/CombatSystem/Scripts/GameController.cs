using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController instance;

    public BehaviorTree             behaviorTree;
    public GameObject               playerBaseTower;
    public GameObject               enemyBaseTower;
    public Base                     playerBase;
    public Base                     enemyBase;
    public Board                    board;
    public MiniMap                  miniMap;
    public Button                   RabitButton;
    public List<GameObject>         unitUIs;
    public MinimapCameraController  minimapCameraController;
    public MainCameraController     mainCameraController;
    public bool                     isPlayerTurn;
    public bool                     isEnemyTurn;
    public int                      width;
    public int                      height;

    

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        isPlayerTurn            = true;
        isEnemyTurn             = false;

        behaviorTree            = GetComponent<BehaviorTree>();
        board                   = GetComponent<Board>();
        playerBase              = GetComponent<Base>();
        enemyBase               = GetComponent<Base>();
        miniMap                 = GetComponent<MiniMap>();
        minimapCameraController = GetComponent<MinimapCameraController>();
        mainCameraController    = GetComponent<MainCameraController>();


        // Assuming buttonPrefab is already assigned in the inspector
        RabitButton.onClick.AddListener(OnSpawnRabbit);


        // Initialize the game board and the player base...
        board.                    InitBoard(width, height);
        miniMap.                  InitMiniMap(width, height);
        playerBase.               InitPlayerPosition(new Vector2Int(0, 0));
        enemyBase.                InitEnemyPosition(new Vector2Int((width * 10) - 10, (height * 10) - 10));
        minimapCameraController.  UpdateCameraSize(miniMap);
        mainCameraController.     MoveMainCamera(new Vector3(0, mainCameraController.mainCamera.transform.position.y, 0));

        
    }

    private void Start()
    {
        GameObject PlayerBaseTower  = Instantiate(playerBaseTower, new Vector3(playerBase.playerPosition.x, 0, playerBase.playerPosition.y), Quaternion.identity);
        GameObject EnemyBaseTower   = Instantiate(enemyBaseTower, new Vector3(enemyBase.playerPosition.x, 0, enemyBase.playerPosition.y), Quaternion.identity);

        PlayerBaseTower.              GetComponent<BaseTower>().team = BaseTower.Team.Player;
        EnemyBaseTower.               GetComponent<BaseTower>().team = BaseTower.Team.AI;

        
        if (instance != null)
        {
            // �̹� instance�� ������ ���, �߰� �������� ����.
            return;
        }
    }

    // This method is called by a UI button using Unity's event system.
    public void OnSpawnRabbit()
    {
        playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player);
    }

    // �÷��̾��� �� ���� �޼ҵ�
    public void StartPlayerTurn()
    {
        playerBase.EndTurnAndGetResource();

        Debug.Log("Player Resource: " + playerBase.resources);

        isPlayerTurn    = true;
    }

    // �÷��̾��� �� ���� �޼ҵ�
    public void EndPlayerTurn()
    {
        isPlayerTurn    = false;
        
        StartAITurn(); // �ΰ����� ������ ��ȯ
    }


    // �ΰ������� �� ���� �޼ҵ�
    private void StartAITurn()
    {
        
        isEnemyTurn = true;
        enemyBase.EndTurnAndGetResource(); // �� ���� �� �ڿ� ȹ��

        Debug.Log("Enemy Resource: " + enemyBase.resources);

        behaviorTree.EnableBehavior(); // �ΰ����� �ൿ ����

        EndAITurn(); // �ΰ����� �� ����
    }


    // �ΰ������� �� ���� �޼ҵ�
    private void EndAITurn()
    {
        behaviorTree.OnBehaviorEnded(); // �ΰ����� �ൿ ����
        
        
        // �÷��̾� ������ ��ȯ
        StartPlayerTurn();

    }



    
}
