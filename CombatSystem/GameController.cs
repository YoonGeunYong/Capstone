using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Board board;
    private Unit selectedUnit;

    public static GameController instance;
    public GameObject unitPrefab;
    public Button buttonPrefab;

    private void Start()
    {
        board = GetComponent<Board>();
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

        board.InitBoard(4, 4);
    }

    // This method is called by a UI button using Unity's event system.
    public void OnSpawnButtonClicked()
    {
        Unit spawnedUnit = board.playerBase.SpawnUnit(unitPrefab);

        // Add the spawned unit to the game board...

        Debug.Log("Spawned a new unit: " + spawnedUnit.name);
    }

    public void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
        // Highlight possible moves...
    }

    public void MoveSelectedUnitTo(Vector2Int position)
    {
        if (selectedUnit != null && IsValidMove(position))
        {
            selectedUnit.Move(position);
            DeselectCurrent();
        }
    }

    private bool IsValidMove(Vector2Int position)
    {
        // Add code here to check if moving to 'position' is valid or not.
        return true;
    }

    private void DeselectCurrent()
    {
        if (selectedUnit != null)
        {
            // Remove highlight from currently selected unit...
            selectedUnit = null;
        }
    }
}
