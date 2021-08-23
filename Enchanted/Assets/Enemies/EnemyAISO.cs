using UnityEngine;

/// <summary>
/// Some base AI variables for an enemy.
/// Contains the genes as well as various other statistics.
/// </summary>
[CreateAssetMenu(menuName = "Enemies/AI")]
public class EnemyAISO : ScriptableObject
{
    [Tooltip("How often is should check for a new target")]
    public float updateTargetFrequency = 0.5f;
    [Tooltip("How close players have to be for the enemy to target them")]
    public float viewRange = 3;
    [Tooltip("How far away a player has to move before the enemy loses track of them")]
    public float followRange = 5;
    
    [Header("Attacks")]
    [Tooltip("The target's distance must be within this range for it to attack")]
    public RangedFloat attackRange;
    [Tooltip("If it actually uses a ranged attack")]
    public bool usesRangedAttack;
    [Tooltip("The spell to use when making an attack")]
    public SpellBase attackSpell;

    [Header("Genes")]
    [Tooltip("Movement speed is a generated random between the maximum and minimum")]
    public RangedFloat moveSpeed;
    [Tooltip("How long to wait between each attack, randomised value.")]
    public RangedFloat attackSpeed;
    
    /// <summary>
    /// Makes the attack at an enemy. Could be overwritten to do more.
    /// </summary>
    /// <param name="ai">Used to locate the caster of the spell</param>
    /// <param name="direction">The direction to cast the spell in</param>
    public void MakeAttack(EnemyAI ai, Vector3 direction)
    {
        attackSpell.CastSpell(ai.gameObject.GetComponent<EntityBase>(), direction);
    }
}
