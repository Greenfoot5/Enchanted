using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    public RoomShape roomShape;
    public GameObject room;

    void Awake()
    {
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        var hasGeneratedRoom = roomShape.GenerateRoom(transform);
    }
}
