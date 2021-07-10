using UnityEngine;

public class Boss : Player
{
    //public Player opponent;
    public Spell[] spellBook = new Spell[6];

    public void PickSpell()
    {
        Spell chosenSpell = spellBook[Random.Range(0, 6)];
        
        opponent.DealDamage(chosenSpell.effect * chosenSpell.scaling);
    }
}
