using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Base playerBase;
    public Board board;
    public static GameController instance;
    public GameObject unitPrefab;
    public Button buttonPrefab;
    public int width;
    public int height;

    private void Awake()
    {
        board = GetComponent<Board>();
        playerBase = GetComponent<Base>();

        // Assuming buttonPrefab is already assigned in the inspector
        buttonPrefab.onClick.AddListener(OnSpawnButtonClicked);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        
        playerBase.Init(board.InitBoard(width, height));
    }

    // This method is called by a UI button using Unity's event system.
    public void OnSpawnButtonClicked()
    {
        Unit spawnedUnit = playerBase.SpawnUnit(unitPrefab);

        // Add the spawned unit to the game board...

        Debug.Log("Spawned a new unit: " + spawnedUnit.name);
    } 
}
