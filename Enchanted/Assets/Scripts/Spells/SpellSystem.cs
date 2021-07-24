using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    [Header("Joysticks")]
    [SerializeField] private SpellJoystick joy1;

    private SpellBase _spell1;

    private PlayerBase _player;

    public Vector3 _forward, _right;

    void Start()
    {
        _player = gameObject.GetComponent<PlayerBase>();
        _player.LoadSpells();

        PlayerMovement movement = gameObject.GetComponent<PlayerMovement>();
        _forward = movement.Forward;
        _right = movement.Right;

        _spell1 = _player.Spell0.levels[0];

        joy1.RegisterSystem(this, _spell1);
        _spell1.RegisterOwner(_player);
    }

    public void StartAim(Vector2? direction)
    {

    }

    public void CancelAim()
    {

    }

    public void CastSpell(Vector2? direction, SpellBase spell)
    {
        var dir = direction.GetValueOrDefault(new Vector2(1, 0));
        //Debug.Log(dir);
        //dir = _right * dir.x + _forward * dir.y;
        //Debug.Log(dir);
        spell.CastSpell(_right * dir.x + _forward * dir.y, _player);
    }
}
