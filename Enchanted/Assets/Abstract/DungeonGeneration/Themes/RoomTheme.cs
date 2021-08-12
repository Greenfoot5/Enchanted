using UnityEngine;

[System.Serializable]
public class WeightedPrefab
{
    public GameObject prefab;
    public float weight = 1;
}

[CreateAssetMenu(menuName = "Generation/RoomTheme")]
public class RoomTheme : ScriptableObject
{
    public WeightedPrefab[] walls = new WeightedPrefab[1];
    public WeightedPrefab[] corners = new WeightedPrefab[1];
    public WeightedPrefab[] floors = new WeightedPrefab[1];
}
