
/// <summary>
/// <para>Effect tick types.</para>
/// <para>This changes how the effect treats ticking.</para>
/// <b>Paused</b> - halted. Useful for freezing effects or tick-less effects.<br/>
/// <b>Running</b> - running and ticking down. Useful for "every X ms" effects.
/// </summary>
public enum EffectTickType
{
    /// <summary>
    /// Halted. Useful for freezing effects or tick-less effects.
    /// </summary>
    Paused,

    /// <summary>
    /// Running and ticking down. Useful for "every X ms" effects.
    /// </summary>
    Running
}

/// <summary>
/// <para>The interface to store spell effect data.</para>
/// The class must inherit this interface in order to be able to create effects.
/// </summary>
public interface IEffectData
{
    /// <summary>
    /// If enabled, the effect will render with a number in the corner displaying stacks.
    /// </summary>
    public bool CanStack { get; }

    /// <summary>
    /// The amount of stacks on the effect, can be used in any way, just a data tracker.
    /// </summary>
    public int Stacks { get; }

    /// <summary>
    /// Categorizes if the effect should be rendered as if it were a good or bad effect for the affected.
    /// </summary>
    public bool IsBeneficial { get; }

    /// <summary>
    /// Changes the ticking type.
    /// Refer to the enum documentation for explanation.
    /// </summary>
    public EffectTickType TickType { get; }

    /// <summary>
    /// The amount of time (in seconds) that will pass before running a "tick".
    /// </summary>
    public float TickSize { get; }
}
