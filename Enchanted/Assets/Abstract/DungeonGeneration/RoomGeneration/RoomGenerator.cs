using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    public RoomShape roomShape;
    public static int roomCount;

    void Awake()
    {
        if (roomCount <= 0)
            return;

        roomCount -= 1;
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        var hasGeneratedRoom = roomShape.GenerateRoom(transform);
    }
}
