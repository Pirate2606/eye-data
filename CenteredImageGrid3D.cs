using UnityEngine;
using System.Collections.Generic;

public class CenteredImageGrid3D : MonoBehaviour
{
    public GameObject generalCellPrefab; // Default prefab for regular cells
    public Vector3 centerCoordinate; // Center position of the grid
    public int rows = 3;
    public int cols = 3;
    public float gap = 0.2f; // Gap between images

    [System.Serializable]
    public struct SpecialCell
    {
        public Vector2Int position; // Grid position (row, col)
        public GameObject prefab;   // Prefab for this special cell
    }

    public List<SpecialCell> specialCells; // List of predefined special cells

    private GameObject[,] gridObjects; // Store grid elements

    void Start()
    {
        CreateImageGrid();
    }

    void Update()
    {
        DetectTouch();
    }

    void CreateImageGrid()
    {
        if (generalCellPrefab == null)
        {
            Debug.LogError("General Cell Prefab not assigned!");
            return;
        }

        gridObjects = new GameObject[rows, cols];

        // Get the cell size (assuming it's a Quad)
        Renderer renderer = generalCellPrefab.GetComponent<Renderer>();
        Vector3 cellSize = renderer.bounds.size; // Get width & height

        // Calculate total grid size
        float totalWidth = cols * cellSize.x + (cols - 1) * gap;
        float totalHeight = rows * cellSize.y + (rows - 1) * gap;

        // Calculate the top-left starting position
        Vector3 startPosition = new Vector3(
            centerCoordinate.x - totalWidth / 2 + cellSize.x / 2,
            centerCoordinate.y + totalHeight / 2 - cellSize.y / 2,
            centerCoordinate.z // Keep Z constant
        );

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Check if this position has a special cell
                GameObject prefabToUse = generalCellPrefab;
                foreach (var specialCell in specialCells)
                {
                    if (specialCell.position == new Vector2Int(row, col))
                    {
                        prefabToUse = specialCell.prefab;
                        break;
                    }
                }

                // Calculate new position for each cell
                Vector3 position = new Vector3(
                    startPosition.x + col * (cellSize.x + gap),
                    startPosition.y - row * (cellSize.y + gap),
                    centerCoordinate.z // Keep Z constant
                );

                // Instantiate the cell at the new position
                GameObject cell = Instantiate(prefabToUse, position, Quaternion.identity, transform);
                cell.AddComponent<BoxCollider>(); // Add collider for touch detection
                gridObjects[row, col] = cell;
            }
        }
    }

    void DetectTouch()
    {
        if (Input.touchCount == 1) // Single touch for selection
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject selectedCell = hit.collider.gameObject;
                    Debug.Log("Selected Cell: " + selectedCell.name);
                    HighlightCell(selectedCell);
                }
            }
        }
    }

    void HighlightCell(GameObject cell)
    {
        // Reset all cells to original color
        foreach (GameObject obj in gridObjects)
        {
            if (obj != null)
            {
                obj.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        // Highlight selected cell
        cell.GetComponent<Renderer>().material.color = Color.blue;
    }
}
