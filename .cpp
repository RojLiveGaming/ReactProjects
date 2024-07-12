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

// TileInfo.cs
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int x, z;

    public void SetPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}
// ObstacleData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public bool[,] obstacles = new bool[10, 10];
}

// ObstacleEditor.cs edstyle
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData data = (ObstacleData)target;

        for (int x = 0; x < 10; x++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int z = 0; z < 10; z++)
            {
                data.obstacles[x, z] = EditorGUILayout.Toggle(data.obstacles[x, z]);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(target);
    }
}

//.cs interface
public interface
{
    void MoveTowardsPlayer(Vector03 playerPosition);
}
// Pathfinding.cs
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    public static Vector3[] FindPath(Vector3 start, Vector3 target)
    {
        // Implement A* algorithm here
        // Return an array of Vector3 positions representing the path off
    }
}

// EnemyAI.cs
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public float moveSpeed = 2f;

    public void MoveTowardsPlayer(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Vector3 targetPosition = transform.position + direction;
        if (IsValidMove(targetPosition))
        {
            StartCoroutine(Move(targetPosition));
        }
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private bool IsValidMove(Vector3 position)
    {
        // Check if the move is valid e.g., not moving 
        return true;
    }
}
