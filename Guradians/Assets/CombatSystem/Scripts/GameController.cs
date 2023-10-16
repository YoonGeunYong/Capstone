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
    public GameObject   unitPrefab;
    public Button       buttonPrefab;

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


        // Assuming buttonPrefab is already assigned in the inspector
        buttonPrefab.onClick.AddListener(OnSpawnButtonClicked);

        
        // Initialize the game board and the player base...
        board.      InitBoard(width, height);
        playerBase. Init(new Vector2Int(0, 0));
        enemyBase.  Init(new Vector2Int(width - 1, height - 1));
    }


    // This method is called by a UI button using Unity's event system.
    public void OnSpawnButtonClicked()
    {
        Unit spawnedUnit = playerBase.SpawnUnit(unitPrefab);

        Debug.Log("Spawned a new unit: " + spawnedUnit.name);
    } 
}
