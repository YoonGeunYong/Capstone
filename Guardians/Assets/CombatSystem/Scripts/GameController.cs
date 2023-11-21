using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Base                     playerBase;
    public Base                     enemyBase;
    public Board                    board;
    public MiniMap                  miniMap;
    public Button                   RabitButton;
    public List<GameObject>         unitUIs;
    public MinimapCameraController  minimapCameraController;
    public MainCameraController     mainCameraController;
    public bool                     isPlayerTurn;
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

        isPlayerTurn = true;

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


    // This method is called by a UI button using Unity's event system.
    public void OnSpawnRabbit()
    {
        playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player);
    }


    // 플레이어의 턴 종료 메소드
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartAITurn(); // 인공지능 턴으로 전환
    }


    // 인공지능의 턴 시작 메소드
    private void StartAITurn()
    {
        // 인공지능의 턴 동작을 구현
        // 유닛 생산 또는 이동 등의 로직을 작성

        // 턴 종료 후 다시 플레이어 턴으로 전환
        EndAITurn();
    }


    // 인공지능의 턴 종료 메소드
    private void EndAITurn()
    {
        isPlayerTurn = true;
        // 플레이어 턴으로 전환
    }
}
