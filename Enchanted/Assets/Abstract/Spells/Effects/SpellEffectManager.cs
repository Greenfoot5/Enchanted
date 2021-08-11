using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>The spell effect manager.</para>
/// It keeps track of effects present on the entity and is responsible for any crowd control or multipliers.
/// </summary>
public class SpellEffectManager : MonoBehaviour
{
    // List of effects
    private readonly List<SpellEffectBase> _effects = new List<SpellEffectBase>();

    /// <summary>
    /// Adds a spell effect to the manager.
    /// </summary>
    /// <param name="effect">The effect instance.</param>
    public void AddEffect(SpellEffectBase effect)
    {
        _effects.Add(effect);
    }

    /// <summary>
    /// Runs ticks and checks if effects change.
    /// </summary>
    private void Update()
    {
        // To keep track of if there were any changes for stats recalculation.
        var effectsChanged = false;

        for (var i = 0; i < _effects.Count; i++)
        {
            // Loop through each effect in a MUTABLE list.
            var effect = _effects[i];

            // Run the Update function.
            effect.Update();

            // If effect needs to be removed.
            if (!effect.NeedsRemoval) continue;
            // Then end and remove it.
            effect.OnEnd();
            _effects.RemoveAt(i);

            // Offset the loop.
            i--;

            // Mark the effects as changed for stats recalculation.
            effectsChanged = true;
        }

        // Run stats recalculation if so.
        if (effectsChanged)
            RecalculateEffects();
    }

    /// <summary>
    /// <para>Resets the stats of the entity and recalculates the new ones by applying each spell on to the entity.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    private void RecalculateEffects()
    {

    }
}
