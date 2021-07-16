using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellJoystick : Joystick
{
    public delegate void OnCast(Vector2? direction, SpellBase _spell);
    public OnCast onCast;

    public delegate void OnBeginAim(Vector2? direction);
    public OnBeginAim onBeginAim;

    public delegate void OnStopAim();
    public OnStopAim onStopAim;

    protected float cancelRange = .1f;

    public bool isHeld = false;
    public bool targetting = false;

    private float _deadZoneReset;

    private Transform _handle;

    private SpellBase _spell;

    private float _cooldownMax = 0;
    private float _cooldownLeft;

    protected override void Start()
    {
        base.Start();

        _deadZoneReset = DeadZone;
        _handle = transform.GetChild(1);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        _handle.localScale = Vector3.one * .9f;

        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        _handle.localScale = Vector3.one;

        if (Direction.magnitude < cancelRange)
            StopAiming();
        else
            CastSpell();

        base.OnPointerUp(eventData);
    }

    public virtual void BeginAiming()
    {
        background.GetComponent<Image>().color = new Color(1, 1, 1, .3f);
        targetting = true;
        DeadZone = 0;
    }

    public virtual void StopAiming()
    {
        background.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        targetting = false;
        DeadZone = _deadZoneReset;
        _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
    }

    public virtual void CastSpell()
    {
        targetting = true;
        DeadZone = _deadZoneReset;
        _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        onCast(Direction, _spell);
    }

    public void SetCooldown(float cooldown)
    {
        _cooldownMax = cooldown;
        _cooldownLeft = cooldown;
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (!targetting && magnitude > DeadZone)
            BeginAiming();

        if (targetting && magnitude < cancelRange)
            _handle.gameObject.GetComponent<Image>().color = new Color(1, .9f, .9f);
        else
            _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        base.HandleInput(magnitude, normalised, radius, cam);
    }

    public void RegisterSystem(SpellSystem system, SpellBase spell)
    {
        onBeginAim = system.StartAim;
        onStopAim = system.CancelAim;
        onCast = system.CastSpell;
        _spell = spell;
    }
}
