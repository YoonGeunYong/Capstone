using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Base         playerBase;
    public Base         enemyBase;
    public Board        board;
    public MiniMap      miniMap;
    public GameObject   unitPrefab;
    public Button       buttonPrefab;
    public Unit unit;

    public int          width;
    public int          height;


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


        board      =    GetComponent<Board>();
        playerBase =    GetComponent<Base>();
        enemyBase  =    GetComponent<Base>();
        miniMap    =    GetComponent<MiniMap>();


        // Assuming buttonPrefab is already assigned in the inspector
        buttonPrefab.onClick.AddListener(OnSpawnButtonClicked);

        
        // Initialize the game board and the player base...
        board.      InitBoard(width, height);
        miniMap.    InitMiniMap(width, height);
        playerBase. InitPlayerPosition(new Vector2Int(0, 0));
        enemyBase.  InitEnemyPosition(new Vector2Int((width * 10) - 10, (height * 10) - 10));
    }


    // This method is called by a UI button using Unity's event system.
    public void OnSpawnButtonClicked()
    {
        playerBase.SpawnUnit(unit.unitPrefab);

        miniMap.AddUnitToMinimap(unit, playerBase.GetPlayerPosition());
    } 
}
