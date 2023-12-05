using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController        instance;

    public BehaviorTree                 behaviorTree;
    public GameObject                   playerBaseTower;
    public GameObject                   enemyBaseTower;
    public GameObject                   playerBaseObject;
    public GameObject                   enemyBaseObject;
    public Board                        board;
    public MiniMap                      miniMap;
    public Button                       rabbitButton;
    public List<GameObject>             unitUIs;
    public MinimapCameraController      minimapCameraController;
    public MainCameraController         mainCameraController;
    public bool                         isPlayerTurn;
    public bool                         isEnemyTurn;
    public int                          width;
    public int                          height;

    private Base                        playerBase;
    private Base                        enemyBase;


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

        isPlayerTurn              =      true;
        isEnemyTurn               =      false;

        behaviorTree              =      GetComponent<BehaviorTree>();
        board                     =      GetComponent<Board>();
        miniMap                   =      GetComponent<MiniMap>();
        minimapCameraController   =      GetComponent<MinimapCameraController>();
        mainCameraController      =      GetComponent<MainCameraController>();
        playerBase = playerBaseObject.   GetComponent<Base>();
        enemyBase  = enemyBaseObject.    GetComponent<Base>();


        // Assuming buttonPrefab is already assigned in the inspector
        rabbitButton.onClick.            AddListener(OnSpawnRabbit);


        // Initialize the game board and the player base...
        board.                          InitBoard(width, height);
        miniMap.                        InitMiniMap(width, height);
        playerBase.                     InitPosition(miniMap.miniMapTiles[0,0].gridPosition);
        enemyBase.                      InitPosition(miniMap.miniMapTiles[width -1, height -1].gridPosition);
        minimapCameraController.        UpdateCameraSize(miniMap);
        mainCameraController.           MoveMainCamera(new Vector3(0, 0f, mainCameraController.mainCamera.transform.position.z));

    }

    private void Start()
    {
        GameObject PlayerBaseTower  =  Instantiate(playerBaseTower, new Vector3(playerBase.position.x, 0, playerBase.position.y), Quaternion.identity);
        GameObject EnemyBaseTower   =  Instantiate(enemyBaseTower, new Vector3(enemyBase.position.x, 0, enemyBase.position.y), Quaternion.identity);

        PlayerBaseTower.               GetComponent<BaseTower>().team = BaseTower.Team.Player;
        EnemyBaseTower.                GetComponent<BaseTower>().team = BaseTower.Team.AI;


        if (instance != null)
        {
            return;
        }
    }


    // This method is called by a UI button using Unity's event system.
    public void OnSpawnRabbit()
    {

        playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position);

    }

    private IEnumerator DelayedBehaviorExecution(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);

        
        yield return null;



    }

    public void StartPlayerTurn()
    {

        isPlayerTurn = true;

        playerBase.EndTurnAndGetResource();

    }


    public void EndPlayerTurn()
    {

        isPlayerTurn = false;

        StartAITurn();

    }


    private void StartAITurn()
    {
        
        isEnemyTurn = true;

        enemyBase.EndTurnAndGetResource();

        behaviorTree.EnableBehavior();

        Debug.Log("Enemy Resource: " + enemyBase.resources);

    }


    public void EndAITurn()
    {
        behaviorTree.OnBehaviorEnded();

        isEnemyTurn = false;

        StartPlayerTurn();

    }

}