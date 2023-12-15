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
    public Button                       rabbitButton, turtleButton, foxButton, woodCutterButton,
                                        fairyButton, deerButton, heungbuButton, nolbuButton, swallowButton;
    public Button                       turnEndButton;
    public GameObject                   turnText;
    public List<GameObject>             unitUIs;
    public MinimapCameraController      minimapCameraController;
    public MainCameraController         mainCameraController;
    public CreateButtonControl          createButtonControl;    //12.13
    public bool                         isPlayerTurn;
    public bool                         isEnemyTurn;
    public bool                         isFight;  //12.08 fight check
    public bool                         wasMoved; //12.07 moved check
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

        behaviorTree              =      GetComponent<BehaviorTree>();
        board                     =      GetComponent<Board>();
        miniMap                   =      GetComponent<MiniMap>();
        minimapCameraController   =      GetComponent<MinimapCameraController>();
        mainCameraController      =      GetComponent<MainCameraController>();
        enemyBaseObject           =      Instantiate(enemyBaseObject);


        // Assuming buttonPrefab is already assigned in the inspector
        rabbitButton.onClick.            AddListener(OnSpawnRabbit);
        turtleButton.onClick.            AddListener(OnSpawnTurtle);
        foxButton.onClick.               AddListener(OnSpawnFox);
        woodCutterButton.onClick.        AddListener(OnSpawnWoodCutter);
        fairyButton.onClick.             AddListener(OnSpawnFairy);
        deerButton.onClick.              AddListener(OnSpawnDeer);
        heungbuButton.onClick.           AddListener(OnSpawnHeungbu);
        nolbuButton.onClick.             AddListener(OnSpawnNolbu);
        swallowButton.onClick.           AddListener(OnSpawnSwallow);

        // Initialize the game board and the player base...
        board.                          InitBoard(width, height);
        miniMap.                        InitMiniMap(width, height);
        //minimapCameraController.        UpdateCameraSize(miniMap);
        mainCameraController.           MoveMainCamera(new Vector3(0, 0f, mainCameraController.mainCamera.transform.position.z));

    }

    private void Start()
    {
        playerBase = playerBaseObject.GetComponent<Base>();
        enemyBase = enemyBaseObject.GetComponent<Base>();
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
    public void OnSpawnRabbit()
    { 
        if(isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Rabbit]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Rabbit);
            playerBase.resources -= preStats[(int)UnitTypes.Rabbit]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnTurtle()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Turtle]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Turtle);
            playerBase.resources -= preStats[(int)UnitTypes.Turtle]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnFox()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Fox]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Fox);
            playerBase.resources -= preStats[(int)UnitTypes.Fox]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnWoodCutter()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.WoodCutter]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.WoodCutter);
            playerBase.resources -= preStats[(int)UnitTypes.WoodCutter]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnFairy()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Fairy]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Fairy);
            playerBase.resources -= preStats[(int)UnitTypes.Fairy]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnDeer()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Deer]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Deer);
            playerBase.resources -= preStats[(int)UnitTypes.Deer]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnHeungbu()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Heungbu]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Heungbu);
            playerBase.resources -= preStats[(int)UnitTypes.Heungbu]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnNolbu()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Nolbu]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Nolbu);
            playerBase.resources -= preStats[(int)UnitTypes.Nolbu]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
        }
    }

    public void OnSpawnSwallow()
    {
        if (isPlayerTurn && playerBase.resources >= preStats[(int)UnitTypes.Swallow]._stats.cost)
        {
            playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, UnitTypes.Swallow);
            playerBase.resources -= preStats[(int)UnitTypes.Swallow]._stats.cost;
            costScript.resourceText.text = "Resources: " + playerBase.GetResource().ToString();
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
        
        createButtonControl.ButtonActive(); //12.13

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

        behaviorTree.EnableBehavior();

        Debug.Log("Enemy Resource: " + enemyBase.resources);

    }


    public void EndAITurn()
    {
        behaviorTree.OnBehaviorEnded();

        isEnemyTurn = false;
        
        turnText.SetActive(true);

        StartPlayerTurn();

    }

}