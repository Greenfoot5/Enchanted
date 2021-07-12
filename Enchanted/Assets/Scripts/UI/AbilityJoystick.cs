using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityJoystick : Joystick
{
    public delegate void OnCast(Vector2? direction);
    public OnCast onCast;

    protected float cancelRange = .1f;

    public bool isHeld = false;
    public bool targetting = false;

    private float deadZoneReset;

    private Transform _handle;

    private float cooldown = 0;
    private float cooldownLeft;

    protected override void Start()
    {
        base.Start();

        deadZoneReset = DeadZone;
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

        base.OnPointerUp(eventData);

        if (Direction.magnitude < cancelRange)
            StopAiming();
        else
            CastSpell();
    }

    public virtual void BeginAiming()
    {
        background.GetComponent<Image>().color = new Color(1, 1, 1, .3f);
        targetting = true;
        DeadZone = 0;
        Debug.Log("SPELL: BEGIN AIM");
    }

    public virtual void StopAiming()
    {
        background.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        targetting = false;
        DeadZone = deadZoneReset;
        _handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
        Debug.Log("SPELL: CANCEL");
    }

    public virtual void CastSpell()
    {
        //targetting = true;
        //DeadZone = deadZoneReset;
        //_handle.gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
        StopAiming();  // We can have a different animation sequence for casting, this is just placeholder.

        //onCast(Direction);
        Debug.Log("WIND BLAAAADEEEEEEEEEEE");
    }

    public void SetCooldown(float cooldown)
    {
        this.cooldown = cooldown;
        cooldownLeft = cooldown;
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
}
