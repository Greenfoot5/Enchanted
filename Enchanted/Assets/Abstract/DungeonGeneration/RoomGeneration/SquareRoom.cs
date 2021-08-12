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
    
        var doorPosition = Random.Range(2, wallsPerEdge);
        var wall = Instantiate(roomTheme.GETWall(), spawnerTransform.position, spawnerTransform.rotation, wallParent.transform);

        return false;
    }
}
