using BehaviorDesigner.Runtime;
#if Palmmedia
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;


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
    public Text                         gameOverText;
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
    public bool                         usePlayerAI;
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

        GameObject PlayerBaseTower  =  Instantiate(playerBaseTower, new Vector3(playerBase.position.x, playerBase.position.y, 0), Quaternion.identity);
        GameObject EnemyBaseTower   =  Instantiate(enemyBaseTower, new Vector3(enemyBase.position.x, enemyBase.position.y, 0), Quaternion.identity);

        PlayerBaseTower.               GetComponent<BaseTower>().team = BaseTower.Team.Player;
        EnemyBaseTower.                GetComponent<BaseTower>().team = BaseTower.Team.AI;

        gameOverText.gameObject.SetActive(false);

        if(usePlayerAI) StartCoroutine(PlayerAI());

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

    public void GameOver(Unit.Team team)
    {
        if(team == Unit.Team.Player)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You Lose!";
            EnemyAgent.instance.AddReward(100f);
        }

        else if(team == Unit.Team.Enemy)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You Win!";
            EnemyAgent.instance.AddReward(-100f);
        }

        
        EnemyAgent.instance.EndEpisode();

        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator PlayerAI()
    {
        while (true)
        {
            if (isPlayerTurn && !isMoving)
            {
                int randint = UnityEngine.Random.Range(0, 3);

                yield return new WaitForSeconds(0.5f);

                playerBase.SpawnUnit(unitUIs[0], Unit.Team.Player, playerBase.position, (UnitTypes)randint);

                yield return new WaitForSeconds(1.0f);

                Debug.Log(MiniMap.instance.miniMapTiles[0, 0].unitsOnTile.Count);

                MiniMap.instance.selectedMiniMapTile = GetTilesWithEnemyUnits();


                MiniMap.instance.MoveUnitTo(GetSelectedMoveTile(), true);

                EndPlayerTurn();
            }

            yield return null;
        }
    }

    private MiniMapTile GetTilesWithEnemyUnits()
    {
        Debug.Log(MiniMap.instance.miniMapTiles[0, 0].unitsOnTile.Count);
        List<MiniMapTile> unitTiles = new List<MiniMapTile>();
        
        for (int i = 0; i < MiniMap.instance.width; i++)
        {
            for (int j = 0; j < MiniMap.instance.height; j++)
            {
                if (MiniMap.instance.miniMapTiles[i, j].unitsOnTile.Count > 0)
                {
                    unitTiles.Add(MiniMap.instance.miniMapTiles[i, j]);
                    Debug.Log("ADd");
                }
            }
        }
        int randint = UnityEngine.Random.Range(0, unitTiles.Count);
        Debug.Log("randint : " + randint);
        return unitTiles[randint];
    }

    private MiniMapTile GetSelectedMoveTile()
    {
        // 현재 선택된 타일의 좌표를 가져오기
        int currentX = MiniMap.instance.selectedMiniMapTile.originalPosition.x;
        int currentY = MiniMap.instance.selectedMiniMapTile.originalPosition.y;

        int newX = currentX;
        int newY = currentY;

        int randint = UnityEngine.Random.Range(0, 3);

        // selectedMoveTileIndex.Value에 따라 다른 방향으로 이동할 좌표 설정
        switch (randint)
        {
            case 0: newY -= 1; break; // 아래로 이동
            case 1: newX += 1; break; // 오른쪽으로 이동
            case 2: newY += 1; break; // 위로 이동
            case 3: newX -= 1; break; // 왼쪽으로 이동
            default:
                // 예외 처리: 잘못된 인덱스에 대한 기본값
                
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