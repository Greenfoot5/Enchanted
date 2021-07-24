using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Base")]
public class LevelingSpellBase : ScriptableObject
{
    public string title;
    public SpellBase[] levels;
}
