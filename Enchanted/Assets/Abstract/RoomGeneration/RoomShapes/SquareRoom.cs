using UnityEngine;

[CreateAssetMenu(menuName = "Generation/Rooms/Square")]
public class SquareRoom : RoomShape
{
    public GameObject doorPrefab;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public override bool GenerateRoom(Transform parent, int wallCount)
    {
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
        
        var wallsPerSide = wallCount / 4;
        var doorPosition = Random.Range(2, wallCount / 4);

        // Loops through and creates our walls
        for (var side = 1; side < 4; side++)
        {
            for (var i = 0; i < wallsPerSide; i++)
            {
                Instantiate(wallPrefab, spawnerTransform.position, spawnerTransform.rotation, wallParent.transform);
                spawnerTransform.Translate(Vector3.forward);
            }

            spawnerTransform.Translate(Vector3.back);
            spawnerTransform.Rotate(Vector3.up, 90);
        }
        
        // Places the floors
        for (var i = 0; i < wallsPerSide; i++)
        {
            for (var j = 0; j < wallsPerSide; j++)
            {
                Instantiate(floorPrefab, spawnerTransform.position, spawnerTransform.rotation, floorParent.transform);
                spawnerTransform.Translate(Vector3.forward);
            }

            spawnerTransform.Translate(Vector3.back * wallsPerSide);
            spawnerTransform.Translate(Vector3.right);
        }
        return false;
    }
}
