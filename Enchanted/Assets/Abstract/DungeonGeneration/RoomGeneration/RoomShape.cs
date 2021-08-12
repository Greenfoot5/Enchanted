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
    /// <returns>Returns if the room was placed</returns>
    public abstract bool GenerateRoom(Transform parent);
}
