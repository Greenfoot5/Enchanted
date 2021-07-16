using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectManager : MonoBehaviour
{
    private readonly List<SpellEffectBase> effects = new List<SpellEffectBase>();

    public void AddEffect(SpellEffectBase effect)
    {
        effects.Add(effect);
    }

    private void Update()
    {
        bool effectsChanged = false;

        for (int i = 0; i < effects.Count; i++)
        {
            var effect = effects[i];

            effect.Update();

            if (effect.NeedsRemoval)
            {
                effects.RemoveAt(i);
                i--;
                effectsChanged = true;
            }
        }

        if (effectsChanged)
            RecalculateEffects();
    }

    private void RecalculateEffects()
    {

    }
}
