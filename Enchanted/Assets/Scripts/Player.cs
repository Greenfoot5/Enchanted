using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : EntityBase
{
    // Player stats
    [Header("Base Stats")]
    [SerializeField] private float maxHealth;

    public float GetMaxHealth => maxHealth;

    // ClassSpells
    private SpellBase[] _spells;

    // Equipped spells
    private SpellBase _spell0;
    private SpellBase _spell1;
    private SpellBase _spell2;
    private SpellBase _spell3;

    public SpellBase Spell0 => _spell0;
    public SpellBase Spell1 => _spell1;
    public SpellBase Spell2 => _spell2;
    public SpellBase Spell3 => _spell3;

    protected virtual void Awake()
    {
        _spells = GetComponentsInChildren<SpellBase>();
    }

    public virtual SpellBase GetSpell(int index) => _spells[index];

    public void LoadSpells()
    {
        _spell0 = GetSpell(0);
        _spell1 = GetSpell(1);
        _spell2 = GetSpell(2);
        _spell3 = GetSpell(3);
    }
}
