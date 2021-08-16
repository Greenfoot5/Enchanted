using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Generation/Rooms/Square")]
public class SquareRoom : RoomShape
{
    public RangedInt wallsPerSide;
    public RoomTheme roomTheme;
    public override bool GenerateRoom(Transform parent)
    {
        var wallsPerEdge = Random.Range(wallsPerSide.minValue, wallsPerSide.maxValue);
        // We can freely move this to manage the location to spawn the rooms components
        var spawner = new GameObject("Spawner");
        spawner.transform.SetPositionAndRotation(parent.position, parent.rotation);
        spawner.transform.parent = parent;
        var spawnerTransform = spawner.transform;
    
        // Use this to keep the Hierarchy clean
        var wallParent = Instantiate(spawner, parent.transform);
        wallParent.name = "Walls";
        var floorParent = Instantiate(spawner, parent.transform);
        floorParent.name = "Floors";
        var decorParent = Instantiate(spawner, parent.transform);
        decorParent.name = "Decorations";
        
        //
        // Door positions
        //
        
        // Decide where the entry door goes
        var entryPosition = Random.Range(1, wallsPerEdge);
        var doorAmount = Random.Range(extraDoors.minValue, extraDoors.maxValue);
        var validPositions = Enumerable.Range(0, (wallsPerEdge - 2) * 4).ToList();
        var doorPositions = new int[doorAmount];
        while (validPositions.Any() || doorPositions[doorAmount - 1] != 0)
        {
            
        }

        //
        // Walls
        //
        
        // Spawn the first side
        spawnerTransform.Translate(Vector3.back * entryPosition);
        var corner = Instantiate(roomTheme.GETCorner(), spawnerTransform.position,
            spawnerTransform.rotation, wallParent.transform);
        corner.transform.Rotate(Vector3.up, 270f);
        // Spawn the walls
        for (var i = 1; i < wallsPerEdge - 1; i++)
        {
            spawnerTransform.Translate(Vector3.forward);
            Instantiate(i == entryPosition ? roomTheme.door : roomTheme.GETWall(), spawnerTransform.position,
                spawnerTransform.rotation, wallParent.transform);
        }
        
        // Other 3 sides
        for (var side = 0; side < 3; side++)
        {
            // Add the corner
            spawnerTransform.Translate(Vector3.forward);
            spawnerTransform.Rotate(Vector3.up, 90);
            corner = Instantiate(roomTheme.GETCorner(), spawnerTransform.position, spawnerTransform.rotation,
                wallParent.transform);
            corner.transform.Rotate(Vector3.up, 270f);
            // Add all the walls
            for (var i = 1; i < wallsPerEdge - 1; i++)
            {
                spawnerTransform.Translate(Vector3.forward);
                Instantiate(roomTheme.GETWall(), spawnerTransform.position, spawnerTransform.rotation,
                    wallParent.transform);
            }
        }
        
        // Reset spawner transform
        spawnerTransform.Translate(Vector3.forward);
        spawnerTransform.Rotate(Vector3.up, 90);
        
        //
        // Floor
        //
        for (var i = 0; i < wallsPerEdge; i++)
        {
            for (var j = 0; j < wallsPerEdge; j++)
            {
                Instantiate(roomTheme.GETFloor(), spawnerTransform.position, spawnerTransform.rotation,
                    floorParent.transform);
                spawnerTransform.Translate(Vector3.forward);
            }
            spawnerTransform.Translate(Vector3.back * wallsPerEdge);
            spawnerTransform.Translate(Vector3.right);
        }

        return false;
    }
}
