using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController_copy : MonoBehaviour
{
    public static GameController_copy instance;

    public Base_copy                     playerBase;
    public Base_copy                     enemyBase;
    public Board_copy                    board;
    public MiniMap_copy                  miniMap;
    public Button                   RabitButton;
    public List<GameObject>         unitUIs;
    public MinimapCameraController_copy  minimapCameraController;
    public MainCameraController_copy     mainCameraController;

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

        board                   = GetComponent<Board_copy>();
        playerBase              = GetComponent<Base_copy>();
        enemyBase               = GetComponent<Base_copy>();
        miniMap                 = GetComponent<MiniMap_copy>();
        minimapCameraController = GetComponent<MinimapCameraController_copy>();
        mainCameraController    = GetComponent<MainCameraController_copy>();


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
        playerBase.SpawnUnit(unitUIs[0]);
    }

}
