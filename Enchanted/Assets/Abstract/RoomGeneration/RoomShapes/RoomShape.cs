using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class RoomShape : ScriptableObject
{
    /// <summary>
    /// Generates the rooms using the selected prefabs.
    /// </summary>
    /// <param name="parent">The location to spawn the first door from</param>
    /// <param name="wallCount">The maximum amount of walls to surround the room.
    /// May decrease due to shape selected.</param>
    /// <returns>Returns if the room was placed</returns>
    public abstract bool GenerateRoom(Transform parent, int wallCount);
}
