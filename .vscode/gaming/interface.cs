// Pathfinding.cs
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    public static Vector3[] FindPath(Vector3 start, Vector3 target)
    {
        List<Vector3> path = new List<Vector3>();
        
        // Define a priority queue for the open set
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        // Initialize the start node
        Node startNode = new Node(start, null, 0, GetDistance(start, target));
        openSet.Enqueue(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.Dequeue();

            if (currentNode.Position == target)
            {
                // Path found, reconstruct it
                while (currentNode != null)
                {
                    path.Add(currentNode.Position);
                    currentNode = currentNode.Parent;
                }
                path.Reverse();
                return path.ToArray();
            }

            closedSet.Add(currentNode);

            // Get neighboring nodes
            foreach (Vector3 neighborPosition in GetNeighbors(currentNode.Position))
            {
                if (closedSet.Contains(new Node(neighborPosition))) continue;

                float newCostToNeighbor = currentNode.GCost + GetDistance(currentNode.Position, neighborPosition);
                Node neighborNode = new Node(neighborPosition, currentNode, newCostToNeighbor, GetDistance(neighborPosition, target));

                if (!openSet.Contains(neighborNode) || newCostToNeighbor < neighborNode.GCost)
                {
                    neighborNode.GCost = newCostToNeighbor;
                    neighborNode.HCost = GetDistance(neighborPosition, target);
                    neighborNode.Parent = currentNode;

                    if (!openSet.Contains(neighborNode))
                    {
                        openSet.Enqueue(neighborNode);
                    }
                }
            }
        }

        // No path found
        return new Vector3[0];
    }

    private static IEnumerable<Vector3> GetNeighbors(Vector3 position)
    {
        // Assuming a grid-based system
        List<Vector3> neighbors = new List<Vector3>
        {
            position + Vector3.up,
            position + Vector3.down,
            position + Vector3.left,
            position + Vector3.right
        };
        return neighbors;
    }

    private static float GetDistance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    private class Node : IComparable<Node>
    {
        public Vector3 Position { get; }
        public Node Parent { get; set; }
        public float GCost { get; set; }
        public float HCost { get; set; }
        public float FCost => GCost + HCost;

        public Node(Vector3 position, Node parent = null, float gCost = 0, float hCost = 0)
        {
            Position = position;
            Parent = parent;
            GCost = gCost;
            HCost = hCost;
        }

        public int CompareTo(Node other)
        {
            return FCost.CompareTo(other.FCost);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Node)) return false;
            Node node = (Node)obj;
            return Position == node.Position;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }

    private class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;

        public PriorityQueue()
        {
            data = new List<T>();
        }

        public void Enqueue(T item)
        {
            data.Add(item);
            int childIndex = data.Count - 1;

            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (data[childIndex].CompareTo(data[parentIndex]) >= 0) break;

                T tmp = data[childIndex];
                data[childIndex] = data[parentIndex];
                data[parentIndex] = tmp;
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            int lastIndex = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[lastIndex];
            data.RemoveAt(lastIndex);

            --lastIndex;
            int parentIndex = 0;
            while (true)
            {
                int leftChildIndex = parentIndex * 2 + 1;
                if (leftChildIndex > lastIndex) break;

                int rightChildIndex = leftChildIndex + 1;
                if (rightChildIndex <= lastIndex && data[rightChildIndex].CompareTo(data[leftChildIndex]) < 0)
                {
                    leftChildIndex = rightChildIndex;
                }

                if (data[parentIndex].CompareTo(data[leftChildIndex]) <= 0) break;

                T tmp = data[parentIndex];
                data[parentIndex] = data[leftChildIndex];
                data[leftChildIndex] = tmp;
                parentIndex = leftChildIndex;
            }

            return frontItem;
        }

        public int Count => data.Count;

        public bool Contains(T item)
        {
            return data.Contains(item);
        }
    }
}
