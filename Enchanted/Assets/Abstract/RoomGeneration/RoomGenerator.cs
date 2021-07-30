using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    public int tileCount = 42;
    public OriginTileNode origin;

    private Dictionary<Vector3, TileNode> _tilePositions;

    void Awake()
    {
        _tilePositions = new Dictionary<Vector3, TileNode>();
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        // Use this to spawn the tiles
        var spawner = new GameObject("Tile Spawn Point");
        var spawnerTransform = spawner.transform;
        spawnerTransform.SetParent(transform);
        spawnerTransform.SetPositionAndRotation(transform.position, transform.rotation);

        // Instantiate the origin of the room
        var originPosition = spawnerTransform.position;
        Instantiate(origin.tile, originPosition, spawnerTransform.rotation, transform);
        _tilePositions[originPosition] = origin;
        
        // Loop through and create the rest of the room
        while (_tilePositions.Count < tileCount)
        {
            
        }
    }
}
