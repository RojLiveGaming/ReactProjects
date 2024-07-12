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