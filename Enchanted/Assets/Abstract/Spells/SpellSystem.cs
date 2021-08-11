using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The system controlling spells and assigning joysticks to them.
/// </summary>
public class SpellSystem : MonoBehaviour
{
    // List of joysticks.
    [Header("Joysticks")]
    [SerializeField] private SpellJoystick joy1;

    // List of assigned spells.
    private SpellBase _spell1;

    // The controlling player.
    private PlayerBase _player;

    // The player's camera local axis (for calculating the casting direction).
    [FormerlySerializedAs("_forward")] public Vector3 forward;
    [FormerlySerializedAs("_right")] public Vector3 right;

    void Start()
    {
        // Get the player and load all spells.
        _player = gameObject.GetComponent<PlayerBase>();
        _player.LoadSpells();

        // Get the movement script and the camera local axis.
        PlayerMovement movement = gameObject.GetComponent<PlayerMovement>();
        forward = movement.Forward;
        right = movement.Right;

        // Assign the first spell of level 1.
        _spell1 = _player.Spell0.levels[0];

        // Register the spell to the joystick.
        joy1.RegisterSystem(this, _spell1);
    }

    /// <summary>
    /// <para>Function ran when the joystick is dragged out of the dead zone and aiming has begun.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    /// <param name="direction">The current aiming direction.</param>
    public void StartAim(Vector2? direction)
    {

    }
    
    /// <summary>
    /// <para>Function ran when the joystick targeting is cancelled.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    public void CancelAim()
    {

    }

    /// <summary>
    /// <para>Function ran when the joystick is pressed or sent to cast a spell.</para>
    /// </summary>
    /// <param name="spell">The joystick's assigned spell.</param>
    /// <param name="direction">The cast direction of the spell, can be null if tapped (auto targeting).<br/>
    /// <b>Not rotated to match camera</b></param>
    /// <param name="joystick">The joystick casting the spell</param>
    public void CastSpell(SpellBase spell, Vector2? direction, SpellJoystick joystick)
    {
        // Default direction.
        // TODO: Auto targeting.
        var dir = direction.GetValueOrDefault(new Vector2(1, 0));
        
        // Set the cooldown
        joystick.SetCooldown(spell.Cooldown);

        // Cast the spell in the corrected direction.
        spell.CastSpell(_player, right * dir.x + forward * dir.y);
    }
}
