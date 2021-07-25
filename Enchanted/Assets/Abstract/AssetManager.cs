using UnityEngine;

/// <summary>
/// <para>The scene's static asset manager</para>
/// Helpful for keeping track of assets that are global for the scene and havign access to them from any script.
/// </summary>
public class AssetManager : MonoBehaviour
{
    // Damage numbers
    public GameObject damageNumberPrefab;
    [SerializeField] private Transform damageNumberParent;

    // Static assignment, so you can reference from anywhere.
    private static AssetManager _instance;
    public static AssetManager Instance => _instance;

    private void Start()
    {
        // Make global.
        _instance = this;
    }

    /// <summary>
    /// <para>Creates a damage number object.</para>
    /// Currently color is not supported.
    /// </summary>
    /// <param name="value">The value of the number itself.</param>
    /// <param name="position">The <b>world</b> position of the number.</param>
    public void InstantiateDamageNumber(float value, Vector3 position)
    {
        // Make instance.
        GameObject inst = Instantiate(damageNumberPrefab, damageNumberParent);

        // Get the script attached to the object.
        DamageNumber script = inst.GetComponent<DamageNumber>();

        // Update it to match data.
        script.SetData(value, position);
    }
}
