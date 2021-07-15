using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    //[Header("Basic spell stats")]
    private float effect;
    private float cost;
    private float scaling;

    [Header("MOBA stats")]
    [SerializeField] private float cooldown;

    public float CooldownStat { get { return cooldown; } }
    public float Cooldown { get { return _cooldown; } }

    private float _cooldown;

    protected Player _caster;

    public void RegisterOwner(Player owner)
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

