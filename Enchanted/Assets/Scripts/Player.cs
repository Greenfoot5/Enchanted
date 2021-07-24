using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : EntityBase
{
    // ClassSpells
    [SerializeField] private LevelingSpellBase[] _spells;

    // Equipped spells
    private LevelingSpellBase _spell0;
    private LevelingSpellBase _spell1;
    private LevelingSpellBase _spell2;
    private LevelingSpellBase _spell3;

    public LevelingSpellBase Spell0 => _spell0;
    public LevelingSpellBase Spell1 => _spell1;
    public LevelingSpellBase Spell2 => _spell2;
    public LevelingSpellBase Spell3 => _spell3;

    public virtual LevelingSpellBase GetSpell(int index) => _spells[index];
    public virtual SpellBase GetSpell(int index, int level) => _spells[index].levels[level];

    public void LoadSpells()
    {
        _spell0 = GetSpell(0);
        _spell1 = GetSpell(1);
        _spell2 = GetSpell(2);
        _spell3 = GetSpell(3);
    }
}
