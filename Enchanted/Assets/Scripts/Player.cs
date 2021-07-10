using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Stats
    private int _maxHealth = 100;
    private float _curHealth = 100f;
    private int _maxMana = 100;
    private float _curMana = 100f;

    // UI
    public TMP_Text healthText;
    public TMP_Text manaText;
    
    // Other
    public Player opponent;

    public void DamageSpell(Spell spell)
    {
        TakeMana(spell.cost);
        opponent.DealDamage(spell.effect * spell.scaling);
    }

    public void DealDamage(float damage)
    {
        _curHealth -= damage;
        healthText.text = _curHealth.ToString();
    }

    private void TakeMana(float amount)
    {
        _curMana -= amount;
        manaText.text = _curMana.ToString();
    }
}
