using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public GameObject damageNumberPrefab;
    [SerializeField] private Transform damageNumberParent;

    private static AssetManager _instance;
    public static AssetManager Instance => _instance;

    private void Start()
    {
        _instance = this;
    }

    public void InstantiateDamageNumber(float value, Vector3 position)
    {
        GameObject inst = Instantiate(damageNumberPrefab, damageNumberParent);
        DamageNumber script = inst.GetComponent<DamageNumber>();
        script.SetData(value, position);
    }
}
