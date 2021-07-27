using UnityEngine;

[CreateAssetMenu(menuName = "Generation/Rooms/Rectangle")]
public class RectangleRoom : RoomShape
{
    public GameObject doorPrefab;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public int minimumWallLength;
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

        var wallsOnWidth = Random.Range(minimumWallLength, wallCount / 2);
        var wallsOnLength = wallCount / 2 - wallsOnWidth;
        // TODO - Add a door


        // Loops through and creates our walls
        for (var side = 0; side < 4; side++)
        {
            for (var i = 0; i < wallsOnWidth; i++)
            {
                Instantiate(wallPrefab, spawnerTransform.position, spawnerTransform.rotation, wallParent.transform);
                spawnerTransform.Translate(Vector3.forward);
            }
            spawnerTransform.Translate(Vector3.back);
            spawnerTransform.Rotate(Vector3.up, 90);
            for (var i = 0; i < wallsOnLength; i++)
            {
                Instantiate(wallPrefab, spawnerTransform.position, spawnerTransform.rotation, wallParent.transform);
                spawnerTransform.Translate(Vector3.forward);
            }
            spawnerTransform.Translate(Vector3.back);
            spawnerTransform.Rotate(Vector3.up, 90);
        }
        
        // Places the floors
        for (int i = 0; i < wallsOnLength; i++)
        {
            for (int j = 0; j < wallsOnWidth; j++)
            {
                Instantiate(floorPrefab, spawnerTransform.position, spawnerTransform.rotation, floorParent.transform);
                spawnerTransform.Translate(Vector3.forward);
            }

            spawnerTransform.Translate(Vector3.back * wallsOnWidth);
            spawnerTransform.Translate(Vector3.right);
        }
        return false;
    }
}
