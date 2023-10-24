using System;
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
    public Button       buttonPrefab;
    public List<GameObject>   unitUIs;
    public UnitUI       unitUI;
    public Camera       miniMapCamera;

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
        miniMap = GetComponent<MiniMap>();
        unitUI = GetComponent<UnitUI>();


        // Assuming buttonPrefab is already assigned in the inspector
        buttonPrefab.onClick.AddListener(OnSpawnButtonClicked);

        
        // Initialize the game board and the player base...
        board.          InitBoard(width, height);
        miniMap.        InitMiniMap(width, height);
        playerBase.     InitPlayerPosition(new Vector2Int(0, 0));
        enemyBase.      InitEnemyPosition(new Vector2Int((width * 10) - 10, (height * 10) - 10));
    }


    // This method is called by a UI button using Unity's event system.
    public void OnSpawnButtonClicked()
    {
        UnitUI unitUI = unitUIs[0].GetComponent<UnitUI>();
        playerBase.SpawnUnit(unitUI);

        miniMap.AddUnitToMinimap(unitUIs[0], miniMap.miniMapTiles);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = miniMapCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            // Sort the hits by distance so that the closest one is first.
            Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

            foreach (RaycastHit hit in hits)
            {
                MiniMapTile tile = hit.transform.GetComponent<MiniMapTile>();
                if (tile != null && tile.IsMovable)
                {
                    Debug.Log("Tile clicked!");
                    // Found a movable tile! Now do something with it...
                    MiniMap.instance.MoveUnitTo(tile);
                    break;
                }
            }
        }
    }

}
