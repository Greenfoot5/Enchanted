using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    public RoomShape roomShape;

    void Awake()
    {
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        bool hasGeneratedRoom = roomShape.GenerateRoom(transform);
    }
}
