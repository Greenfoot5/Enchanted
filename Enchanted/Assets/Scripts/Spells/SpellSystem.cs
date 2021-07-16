using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    [Header("Joysticks")]
    [SerializeField] private SpellJoystick joy1;

    private SpellBase _spell1;

    private PlayerBase _player;

    void Start()
    {
        _player = gameObject.GetComponent<PlayerBase>();
        _player.LoadSpells();

        _spell1 = _player.Spell0;

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
        spell.CastSpell(new Vector3(dir.x, 0, dir.y), _player);
    }
}
