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
