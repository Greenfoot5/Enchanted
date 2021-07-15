using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float maxHealth;

    public float GetMaxHealth { get { return maxHealth; } }

    private SpellBase[] _spells;

    protected virtual void Awake()
    {
        _spells = GetComponents<SpellBase>();
    }

    public virtual SpellBase GetSpell(int index) => _spells[index];
}
