using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    // Player class
    private ClassBase _class;
    public ClassBase Class { get { return _class; } }

    // Equipped spells
    private SpellBase _spell0;
    private SpellBase _spell1;
    private SpellBase _spell2;
    private SpellBase _spell3;

    public SpellBase Spell0 { get { return _spell0; } }
    public SpellBase Spell1 { get { return _spell1; } }
    public SpellBase Spell2 { get { return _spell2; } }
    public SpellBase Spell3 { get { return _spell3; } }

    public void LoadSpells()
    {
        _class = GetComponentInChildren<ClassBase>();

        _spell0 = _class.GetSpell(0);
        _spell1 = _class.GetSpell(1);
        _spell2 = _class.GetSpell(2);
        _spell3 = _class.GetSpell(3);
    }
}
