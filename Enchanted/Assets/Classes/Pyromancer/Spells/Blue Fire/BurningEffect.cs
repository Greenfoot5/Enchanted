using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : SpellEffectBase
{
    private int _stacks = 3;

    protected override bool CanStack => true;
    protected override int Stacks => _stacks;

    protected override EffectTickType TickType => EffectTickType.Running;
    protected override int TickRate => 2;

    public override bool NeedsRemoval => _stacks == 0;

    public override void OnTick()
    {
        receiver.Damage(10, sender);
        _stacks -= 1;
    }
}
