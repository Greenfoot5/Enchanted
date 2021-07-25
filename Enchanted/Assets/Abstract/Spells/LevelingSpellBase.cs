using UnityEngine;

/// <summary>
/// The base of a spell.<br/>
/// It contains the static information that doesn't change through levels
/// and the levels of the spells themselves.
/// </summary>
[CreateAssetMenu(menuName = "Spells/Base")]
public class LevelingSpellBase : ScriptableObject
{
    public string title;
    public SpellBase[] levels;
}
