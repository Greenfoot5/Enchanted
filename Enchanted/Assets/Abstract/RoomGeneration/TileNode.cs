using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(menuName = "Generation/Tiles/Tile")]
public class TileNode : ScriptableObject
{
    [System.Serializable]
    public class Edge
    {
        public TileNode tileNode;
        public float weight;
        [Range(0, 360)]
        public int rotation;
    }
    
    public GameObject tile;
    [Header("Connectors")]
    [SerializeField]
    private List<Edge> forwardNeighbours;
    public List<Edge> ForwardNeighbours => forwardNeighbours ??= new List<Edge>();
    [SerializeField]
    private List<Edge> rightNeighbours;
    public List<Edge> RightNeighbours => rightNeighbours ??= new List<Edge>();
    [SerializeField]
    private List<Edge> backNeighbours;
    public List<Edge> BackNeighbours => backNeighbours ??= new List<Edge>();
    [SerializeField]
    private List<Edge> leftNeighbours;
    public List<Edge> LeftNeighbours => leftNeighbours ??= new List<Edge>();

    public static T Create<T>(string name)
    where T : TileNode
    {
        var node = CreateInstance<T>();

        var path = $"Assets/{name}.asset";
        AssetDatabase.CreateAsset(node, path);

        return node;
    }
}
