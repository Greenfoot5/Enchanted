using UnityEngine;

public enum EffectTickType
{
    Disabled, Paused, Running
}

public abstract class SpellEffectBase
{
    // Effect data
    protected virtual bool CanStack => false;
    protected virtual int Stacks => 1;
    protected virtual bool IsBeneficial => false;

    // Ticks
    protected virtual EffectTickType TickType => EffectTickType.Disabled;
    protected virtual int TickRate => 1;  // per second

    protected readonly float _tickSize = 0;
    protected float _clock = 0;

    // Effect casting information
    protected EntityBase sender;
    protected EntityBase receiver;

    public SpellEffectBase()
    {
        if (TickType != EffectTickType.Disabled)
            _tickSize = (float)1 / TickRate;
    }

    public virtual void Initialize(EntityBase sender, EntityBase receiver)
    {
        this.sender = sender;
        this.receiver = receiver;
        OnBegin();
    }

    public virtual void Update()
    {
        _clock += Time.deltaTime;

        if (_clock < _tickSize)
            return;

        int times = Mathf.FloorToInt(_clock / _tickSize);
        for (int n = 0; n < times; n++)
            OnTick();

        _clock -= times * _tickSize;
    }

    public virtual bool NeedsRemoval => false;

    public virtual void OnBegin() { }
    public virtual void OnTick() { }
    public virtual void OnReapply() { }
    public virtual void OnEnd() { }
    public virtual void OnPlayerEvent() { }
}
