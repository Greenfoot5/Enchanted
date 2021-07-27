using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    public int wallCount = 42;
    public RoomShape roomShape;

    void Awake()
    {
        GenerateRoom();
    }

    public void GenerateRoom()
    {
        bool hasGeneratedRoom = roomShape.GenerateRoom(transform, wallCount);
    }
}
