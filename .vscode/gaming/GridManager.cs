// GridManager.cs
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 10;
    private GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                tile.AddComponent<TileInfo>().SetPosition(x, z);
                grid[x, z] = tile;
            }
        }
    }
}