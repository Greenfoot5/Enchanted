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
        
        // Decide where the door goes
        var doorPosition = Random.Range(1, wallsPerEdge);
        
        // Spawn the first side
        spawnerTransform.Translate(Vector3.back * doorPosition);
        var item = Instantiate(roomTheme.GETCorner(), spawnerTransform.position,
            spawnerTransform.rotation, wallParent.transform);
        item.transform.Rotate(Vector3.up, 270f);
        for (var i = 1; i < wallsPerEdge - 1; i++)
        {
            spawnerTransform.Translate(Vector3.forward);
            Instantiate(i == doorPosition ? roomTheme.door : roomTheme.GETWall(), spawnerTransform.position,
                spawnerTransform.rotation, wallParent.transform);
        }

        for (var side = 0; side < 3; side++)
        {
            spawnerTransform.Translate(Vector3.forward);
            spawnerTransform.Rotate(Vector3.up, 90);
            item = Instantiate(roomTheme.GETCorner(), spawnerTransform.position, spawnerTransform.rotation,
                wallParent.transform);
            item.transform.Rotate(Vector3.up, 270f);
            for (var i = 1; i < wallsPerEdge - 1; i++)
            {
                spawnerTransform.Translate(Vector3.forward);
                Instantiate(roomTheme.GETWall(), spawnerTransform.position, spawnerTransform.rotation,
                    wallParent.transform);
            }
        }
        

        return false;
    }
}
