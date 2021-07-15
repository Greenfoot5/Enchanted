using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellEffectsUpdate
{
    OnDamage,
    OnHeal = 1 << 1
}

public abstract class SpellEffect
{
    public int Stacks { get { return _stacks; } }

    private int _stacks;
}
