using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// The spell joystick.
/// A custom child of the Joystick package to support for casting, cooldowns, levels and more.
/// </summary>
public class SpellJoystick : Joystick
{
    /// <summary>
    /// <para>Event when the joystick is pressed or sent to cast a spell.</para>
    /// </summary>
    /// <param name="spell">The joystick's assigned spell.</param>
    /// <param name="direction">The cast direction of the spell, can be null if tapped (auto targeting).<br/></param>
    public delegate void OnCast(SpellBase spell, Vector2? direction, SpellJoystick joystick);
    public OnCast onCast;

    /// <summary>
    /// <para>Event when the joystick is dragged out of the dead zone and aiming has begun.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    /// <param name="direction">The current aiming direction.</param>
    public delegate void OnBeginAim(Vector2? direction);
    public OnBeginAim onBeginAim;

    /// <summary>
    /// <para>Event when the joystick targeting is cancelled.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    public delegate void OnStopAim();
    public OnStopAim onStopAim;

    // Cancelling
    protected float cancelRange = .1f;

    // State
    public bool isHeld;
    public bool targeting;

    // Reversion
    private float _deadZoneReset;

    private Transform _handle;

    // The assigned spell
    private SpellBase _spell;

    // Cooldown
    private float _cooldownMax;
    private float _cooldownLeft;

    protected override void Start()
    {
        // Run parent code.
        base.Start();

        // Prepare for reversion of values.
        _deadZoneReset = DeadZone;
        _handle = transform.GetChild(1);
    }
    
    /// <summary>
    /// Called every frame, lowers the cooldown of the spell.
    /// Enables the joystick once it reaches 0
    /// </summary>
    protected void Update()
    {
        if (!(_cooldownLeft > 0)) return;
        
        _cooldownLeft -= Time.deltaTime;
        if (!(_cooldownLeft < 0)) return;
        
        HandleRange = 1f;
        _handle.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // Update state.
        isHeld = true;

        // TEMP Visual feedback (shrink the handle).
        _handle.localScale = Vector3.one * .9f;

        // Run parent code.
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // Update state.
        isHeld = false;

        // TEMP Visual feedback (reset handle size).
        _handle.localScale = Vector3.one;

        // Cast or cancel check.
        if (Direction.magnitude < cancelRange)
            StopAiming();
        else
            CastSpell();

        // Run parent code.
        base.OnPointerUp(eventData);
    }

    /// <summary>
    /// Begins aiming.
    /// </summary>
    public virtual void BeginAiming()
    {
        // TEMP Visual feedback (display the ring).
        background.GetComponent<Image>().color = new Color(1, 1, 1, .3f);

        // Update state.
        targeting = true;

        // Disable dead zone snapping.
        DeadZone = 0;
    }

    /// <summary>
    /// Cancels aiming.
    /// </summary>
    public virtual void StopAiming()
    {
        // TEMP Visual feedback (hide the ring).
        background.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        // Update state.
        targeting = false;

        // Re-enable the dead zone snapping.
        DeadZone = _deadZoneReset;

        // TEMP Visual feedback (make handle white).
        _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }

    /// <summary>
    /// Casts the spell in the joystick direction.
    /// </summary>
    public virtual void CastSpell()
    {
        // Don't cast while on cooldown
        if (_cooldownLeft > 0) return;
        
        // TEMP Visual feedback (hide the ring).
        background.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        // Update state.
        targeting = true;

        // Re-enable the dead zone snapping.
        DeadZone = _deadZoneReset;

        // TEMP Visual feedback (make handle white).
        _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        // Send event: cast the spell.
        onCast(_spell, Direction, this);
    }

    /// <summary>
    /// <para>Updates the cooldown of the joystick.</para>
    /// <b>Warning: Still allows players to use joystick, just not move it/cast while on cooldown.</b>
    /// </summary>
    /// <param name="cooldown">New cooldown.</param>
    public void SetCooldown(float cooldown)
    {
        _cooldownMax = cooldown;
        _cooldownLeft = cooldown;
        HandleRange = 0f;
        _handle.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        // Begin aiming if wasn't before and above the dead zone.
        if (!targeting && magnitude > DeadZone)
            BeginAiming();

        // Tint the handle slightly red if in cancel range.
        if (targeting && magnitude < cancelRange)
            _handle.gameObject.GetComponent<Image>().color = new Color(1, .9f, .9f);
        else
            _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        // Run parent code.
        base.HandleInput(magnitude, normalised, radius, cam);
    }

    /// <summary>
    /// Assigns the joystick to a specific spell and the system's events.
    /// </summary>
    /// <param name="system">The spell system. Used to assign events related to the spell.</param>
    /// <param name="spell">The spell itself.</param>
    public void RegisterSystem(SpellSystem system, SpellBase spell)
    {
        // Events
        onBeginAim = system.StartAim;
        onStopAim = system.CancelAim;
        onCast = system.CastSpell;

        // Spell
        _spell = spell;
    }
}
