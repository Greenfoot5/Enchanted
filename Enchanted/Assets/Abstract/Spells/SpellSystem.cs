using UnityEngine;

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
    public Vector3 _forward, _right;

    void Start()
    {
        // Get the player and load all spells.
        _player = gameObject.GetComponent<PlayerBase>();
        _player.LoadSpells();

        // Get the movement script and the camera local axis.
        PlayerMovement movement = gameObject.GetComponent<PlayerMovement>();
        _forward = movement.Forward;
        _right = movement.Right;

        // Assign the first spell of level 1.
        _spell1 = _player.Spell0.levels[0];

        // Register the spell to the joystick.
        joy1.RegisterSystem(this, _spell1);
    }

    /// <summary>
    /// <para>Function ran when the joystick is dragged out of the deadzone and aiming has begun.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    /// <param name="direction">The current aiming direction.</param>
    public void StartAim(Vector2? direction)
    {

    }
    
    /// <summary>
    /// <para>Function ran when the joystick targetting is cancelled.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    public void CancelAim()
    {

    }

    /// <summary>
    /// <para>Function ran when the joystick is pressed or sent to cast a spell.</para>
    /// </summary>
    /// <param name="spell">The joystick's assigned spell.</param>
    /// <param name="direction">The cast direction of the spell, can be null if tapped (auto targetting).<br/>
    /// <b>Not rotated to match camera</b></param>
    public void CastSpell(SpellBase spell, Vector2? direction)
    {
        // Default direction.
        // TODO: Auto targetting.
        var dir = direction.GetValueOrDefault(new Vector2(1, 0));

        // Cast the spell in the corrected direction.
        spell.CastSpell(_player, _right * dir.x + _forward * dir.y);
    }
}
