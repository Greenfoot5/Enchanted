using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : ScriptableObject
{
    //[Header("Basic spell stats")]
    private float effect;
    private float cost;
    private float scaling;

    [Header("MOBA stats")]
    [SerializeField] private float cooldown;

    public float CooldownStat => cooldown;
    public float Cooldown => _cooldown;

    private float _cooldown;

    protected PlayerBase _caster;

    public void RegisterOwner(PlayerBase owner)
    {
        _caster = owner;
    }

    protected virtual void BeginAim()
    {

    }

    protected virtual void EndAim()
    {

    }

    public abstract void CastSpell(Vector3 direction, EntityBase caster);
}

