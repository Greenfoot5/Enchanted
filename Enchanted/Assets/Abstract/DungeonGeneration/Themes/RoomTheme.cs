using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Generation/RoomTheme")]
public class RoomTheme : ScriptableObject
{
    public WeightedPrefab[] walls;
    public WeightedPrefab[] corners;
    public WeightedPrefab[] floors;

    public GameObject GETWall()
    {
        var totalWeight = walls.Sum(t => t.weight);
        var choice = Random.Range(0f, totalWeight);
        foreach (var t in walls)
        {
            if (choice < t.weight)
                return t.prefab;
            choice -= t.weight;
        }
        
        Debug.LogError("Failed to get a wall", this);
        return null;
    }
}