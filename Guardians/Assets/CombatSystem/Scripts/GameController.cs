using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController        instance;

    public EnemyAgent                   enemyAgent;
    public GameObject                   playerBaseTower;
    public GameObject                   enemyBaseTower;
    public GameObject                   playerBaseObject;
    public GameObject                   enemyBaseObject;
    public Board                        board;
    public MiniMap                      miniMap;
    public Button                       rabbitButton, turtleButton, foxButton, woodCutterButton,
                                        fairyButton, deerButton, heungbuButton, nolbuButton, swallowButton;
    public Button                       turnEndButton;
    public GameObject                   turnText;
    public List<GameObject>             unitUIs;
    public MinimapCameraController      minimapCameraController;
    public MainCameraController         mainCameraController;
    public CreateButtonControl          createButtonControl;
    public bool                         isPlayerTurn;
    public bool                         isEnemyTurn;
    public bool                         isFight;
    public bool                         wasMoved;
    public bool                         isMoving;
    public int                          width;
    public int                          height;

    private Base                        playerBase;
    private Base                        enemyBase;
    private CostManager                 costScript;

    public  UnitStatsSO[]               preStats;


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

        
        board                     =      GetComponent<Board>();
        miniMap                   =      GetComponent<MiniMap>();
        minimapCameraController   =      GetComponent<MinimapCameraController>();
        mainCameraController      =      GetComponent<MainCameraController>();
        enemyBaseObject           =      Instantiate(enemyBaseObject);


        // Assuming buttonPrefab is already assigned in the inspector
        rabbitButton.onClick.       AddListener(()     => OnSpawnUnit(UnitTypes.Rabbit));
        turtleButton.onClick.       AddListener(()     => OnSpawnUnit(UnitTypes.Turtle));
        foxButton.onClick.          AddListener(()     => OnSpawnUnit(UnitTypes.Fox));
        woodCutterButton.onClick.   AddListener(()     => OnSpawnUnit(UnitTypes.WoodCutter));
        fairyButton.onClick.        AddListener(()     => OnSpawnUnit(UnitTypes.Fairy));
        deerButton.onClick.         AddListener(()     => OnSpawnUnit(UnitTypes.Deer));
        heungbuButton.onClick.      AddListener(()     => OnSpawnUnit(UnitTypes.Heungbu));
        nolbuButton.onClick.        AddListener(()     => OnSpawnUnit(UnitTypes.Nolbu));
        swallowButton.onClick.      AddListener(()     => OnSpawnUnit(UnitTypes.Swallow));


        // Initialize the game board and the player base...
        board.                          InitBoard(width, height);
        miniMap.                        InitMiniMap(width, height);
        //minimapCameraController.        UpdateCameraSize(miniMap);
    }

    private void Start()
    {
        playerBase = playerBaseObject.GetComponent<Base>();
        enemyBase = enemyBaseObject.GetComponent<Base>();
        enemyAgent = GetComponent<EnemyAgent>();
        costScript = GetComponent<CostManager>();

        playerBase.InitPosition(miniMap.miniMapTiles[0, 0].gridPosition);
        enemyBase.InitPosition(miniMap.miniMapTiles[width - 1, height - 1].gridPosition);

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
    private void OnSpawnUnit(UnitTypes unitType)
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)unitType]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, unitType);
            playerBase.resources -= preStats[(int)unitType]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
            createButtonControl.ButtonActive();
        }
    }

    private void EnableturnButton()
    {
        turnEndButton.interactable = true;
    }

    private void DisableturnButton()
    {
        turnEndButton.interactable = false;
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;

        playerBase.EndTurnAndGetResource();

        createButtonControl.ButtonActive();

        EnableturnButton();
    }


    public void EndPlayerTurn()
    {
        if(isPlayerTurn)
        {
            isPlayerTurn = false;
            wasMoved = false;

            DisableturnButton();
            StartAITurn();

        }
    }


    private void StartAITurn()
    {
        isEnemyTurn = true;

        enemyBase.EndTurnAndGetResource();

        if (enemyAgent.behaviorTree is not null)
        {
            enemyAgent.behaviorTree.EnableBehavior();
            Debug.Log("Enemy Resource: " + enemyBase.resources);
        }
        else
        {
            Debug.LogError("EnemyAgent's behaviorTree is null.");
        }
    }



    public void EndAITurn()
    {
        //behaviorTree.OnBehaviorEnded();

        enemyAgent.behaviorTree.DisableBehavior();
        isEnemyTurn = false;

        //turnText.SetActive(true);

        StartPlayerTurn();

    }

}