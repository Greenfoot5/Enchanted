using UnityEngine;

public enum SpellType
{
    Damage, Heal, Mana, Drain
}

[CreateAssetMenu (fileName="New Spell", menuName="Spell")]
public class Spell : ScriptableObject
{
    [Header("Base Info")]
    public SpellClass spellClass;
    public SpellType spellType;
    [Header("Stats")]
    public float effect;
    public float cost;
    public float scaling;
}