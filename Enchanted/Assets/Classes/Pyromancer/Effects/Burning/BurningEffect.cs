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

    private Material material;

    public BurningEffect(Material mat)
    {
        material = mat;
    }

    public override void OnBegin()
    {
        //receiver.AddMaterial(material);
    }

    public override void OnTick()
    {
        receiver.Damage(10, sender);
        _stacks -= 1;
    }

    public override void OnEnd()
    {
        //receiver.RemoveMaterial(material);
    }
}
