using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Generation/Rooms/Square")]
public class SquareRoom : RoomShape
{
    public RangedInt wallsPerSide;
    public RoomTheme roomTheme;

    private GameObject _wallParent;
    private GameObject _floorParent;
    private Transform _spawnerTransform;

    public override bool GenerateRoom(Transform parent)
    {
        // Room is not generated before
        if (randomSeed < 0)
        {
            // Generate the seed for this room
            randomSeed = Random.Range(0, (int) (Time.time * Random.Range(1, 1234567890)));
            cornerPos = new Vector3[4];
            GenerateNewRoom(parent);
            return true;
        }

        GenerateOldRoom(parent);
        return false;
    }
    private bool GenerateNewRoom(Transform parent)
    {
        var random = new System.Random(randomSeed);
        var wallsPerEdge = Random.Range(wallsPerSide.minValue, wallsPerSide.maxValue);
        // We can freely move this to manage the location to spawn the rooms components
        var spawner = new GameObject("Spawner");
        spawner.transform.SetPositionAndRotation(parent.position, parent.rotation);
        spawner.transform.parent = parent;
        _spawnerTransform = spawner.transform;
    
        // Use this to keep the Hierarchy clean
        _wallParent = Instantiate(spawner, parent.transform);
        _wallParent.name = "Walls";
        _floorParent = Instantiate(spawner, parent.transform);
        _floorParent.name = "Floors";
        
        //
        // Walls
        //
        SpawnWalls(wallsPerEdge, random);
        
        // Reset spawner transform
        _spawnerTransform.Translate(Vector3.forward);
        _spawnerTransform.Rotate(Vector3.up, 90);
        
        //
        // Floor
        //
        // GenerateFloor(wallsPerEdge);
        
        // Destroy the spawner as we don't need it anymore
        Destroy(spawner);
        
        return false;
    }

    private bool GenerateOldRoom(Transform parent)
    {
        var random = new System.Random(randomSeed);
        var wallsPerEdge = Random.Range(wallsPerSide.minValue, wallsPerSide.maxValue);
        
        // We can freely move this to manage the location to spawn the rooms components
        var spawner = new GameObject("Spawner");
        spawner.transform.SetPositionAndRotation(parent.position, parent.rotation);
        spawner.transform.parent = parent;
        _spawnerTransform = spawner.transform;
        
        // These already exist
        _wallParent = GameObject.Find("Walls");
        _floorParent = GameObject.Find("Floors");
        
        //
        // Walls
        //
        SpawnWalls(wallsPerEdge, random);
        
        // Reset spawner transform
        _spawnerTransform.Translate(Vector3.forward);
        _spawnerTransform.Rotate(Vector3.up, 90);
        
        //
        // Floor
        //
        // GenerateFloor(wallsPerEdge);

        return false;
    }

    private void SpawnWalls(int wallsPerEdge, System.Random random)
    {
        //
        // Door positions
        //
        
        // Setup the base for door decisions
        var doorAmount = random.Next(extraDoors.minValue, extraDoors.maxValue);
        var validPositions = Enumerable.Range(1, wallsPerEdge * 4 - 1).ToList();
        
        // Remove corners and walls
        for (var i = 0; i < 4; i++)
        {
            validPositions.Remove(i * wallsPerEdge);
            validPositions.Remove(i * wallsPerEdge - 1);
        }
        var doorPositions = new int[doorAmount + 1];
        // Set the entry position
        doorPositions[0] = Random.Range(1, wallsPerEdge - 1);
        validPositions.Remove(doorPositions[0]);
        validPositions.Remove(doorPositions[0] - 1);
        validPositions.Remove(doorPositions[0] + 1);
        // Add the door positions
        var index = 1;
        while (validPositions.Any() && index < doorAmount)
        {
            var doorPosition = validPositions[Random.Range(0, validPositions.Count)];
            doorPositions[index] = doorPosition;
            validPositions.Remove(doorPosition);
            validPositions.Remove(doorPosition - 1);
            validPositions.Remove(doorPosition + 1);
            index++;
        }

        //
        // Walls
        //
        _spawnerTransform.Translate(Vector3.back * doorPositions[0]);

        for (var i = 0; i < 4; i++)
        {
            // Add the corner
            var corner = Instantiate(roomTheme.GETCorner(), _spawnerTransform.position,
                _spawnerTransform.rotation, _wallParent.transform);
            corner.transform.Rotate(Vector3.up, 270f);
            cornerPos[i] = corner.transform.position;
            // Add all the walls
            for (var j = 1; j < wallsPerEdge - 1; j++)
            {
                Debug.Log(i * wallsPerEdge + j);
                _spawnerTransform.Translate(Vector3.forward);
                Instantiate(doorPositions.Contains(i * wallsPerEdge + j) ? roomTheme.door : roomTheme.GETWall(),
                    _spawnerTransform.position,
                    _spawnerTransform.rotation, _wallParent.transform);
            }
            _spawnerTransform.Translate(Vector3.forward);
            _spawnerTransform.Rotate(Vector3.up, 90);
        }
    }

    private void GenerateFloor(int wallsPerEdge)
    {
        for (var i = 0; i < wallsPerEdge; i++)
        {
            for (var j = 0; j < wallsPerEdge; j++)
            {
                Instantiate(roomTheme.GETFloor(), _spawnerTransform.position, _spawnerTransform.rotation,
                    _floorParent.transform);
                _spawnerTransform.Translate(Vector3.forward);
            }
            _spawnerTransform.Translate(Vector3.back * wallsPerEdge);
            _spawnerTransform.Translate(Vector3.right);
        }
    }
    
    public override bool HasCollision(Vector3[] corners)
    {
        if (cornerPos.Length < 1)
        {
            return false;
        }

        return true;
    }
}
