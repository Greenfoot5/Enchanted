using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Generation/RoomTheme")]
public class RoomTheme : ScriptableObject
{
    [SerializeField]
    public GameObject door;
    [SerializeField]
    private WeightedPrefab[] walls;
    [SerializeField]
    private WeightedPrefab[] corners;
    [SerializeField]
    private WeightedPrefab[] floors;

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
        
        Debug.LogError("Failed to get a wall.", this);
        return null;
    }
    
    public GameObject GETCorner()
    {
        var totalWeight = corners.Sum(t => t.weight);
        var choice = Random.Range(0f, totalWeight);
        foreach (var t in corners)
        {
            if (choice < t.weight)
            {
                return t.prefab;
            }

            choice -= t.weight;
        }
        
        Debug.LogError("Failed to get a corner.", this);
        return null;
    }
    
    public GameObject GETFloor()
    {
        var totalWeight = floors.Sum(t => t.weight);
        var choice = Random.Range(0f, totalWeight);
        foreach (var t in floors)
        {
            if (choice < t.weight)
                return t.prefab;
            choice -= t.weight;
        }
        
        Debug.LogError("Failed to get a floor.", this);
        return null;
    }
}