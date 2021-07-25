using UnityEngine;

/// <summary>
/// <para>The base of a spell effect.</para>
/// Keeps track of every single effect-related event.
/// Such as ticks, on apply, on remove, etc.
/// </summary>
public abstract class SpellEffectBase
{
    // Effect data
    protected IEffectData data;
    protected int stacks;

    protected float _clock = 0;

    // Effect casting information
    protected EntityBase sender;
    protected EntityBase receiver;

    /// <summary>
    /// <para>Begins the effect.</para>
    /// This is important to keep track of damage as well as show the information about the effect.
    /// </summary>
    /// <param name="sender">The entity creating the effect.</param>
    /// <param name="receiver">The entity affected by the effect.</param>
    public virtual void Initialize(EntityBase sender, IEffectData data, EntityBase receiver)
    {
        // Set up basic static data.
        this.data = data;

        // Set up active data.
        this.sender = sender;
        this.receiver = receiver;
        stacks = data.Stacks;

        // Run OnBegin of effect.
        OnBegin();
    }

    /// <summary>
    /// <para>Basic ticking code.</para>
    /// If you plan to override, then don't forget to run the parent function.
    /// </summary>
    public virtual void Update()
    {
        // Cancel ticking code if they're not running.
        if (data.TickType != EffectTickType.Running)
            return;

        // Keep track of time passed.
        _clock += Time.deltaTime;

        // If time is below the tick size, then stop.
        if (_clock < data.TickSize)
            return;

        // If not, check how many did it tick, and run the tick code that many times. (Lag compensation)
        int times = Mathf.FloorToInt(_clock / data.TickSize);
        for (int n = 0; n < times; n++)
            OnTick();

        // Reset the clock with amount of ticks ran. (Lag compensation)
        _clock -= times * data.TickSize;
    }

    /// <summary>
    /// Helps keep track of spells that need to be removed from the affected entity.
    /// </summary>
    public virtual bool NeedsRemoval => false;
    
    /// <summary>
    /// The event when the effect is applied.
    /// </summary>
    public virtual void OnBegin() { }

    /// <summary>
    /// The event when the effect ticks once.
    /// </summary>
    public virtual void OnTick() { }

    /// <summary>
    /// <para>The event when the effect is re-applied.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    public virtual void OnReapply() { }

    /// <summary>
    /// The event when the effect is removed.
    /// </summary>
    public virtual void OnEnd() { }

    /// <summary>
    /// <para>A custom player event.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    public virtual void OnPlayerEvent() { }
}
