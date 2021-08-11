using UnityEngine;
using TMPro;

/// <summary>
/// The damage number controller.
/// </summary>
public class DamageNumber : MonoBehaviour
{
    // A dumb fix for the position misalignments
    private Vector3? _newLoc;

    /// <summary>
    /// Gets the animation duration and uses that information to destroy itself later.
    /// </summary>
    void Start()
    {
        var animator = GetComponent<Animator>();
        var clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        Destroy(gameObject, clipInfo[0].clip.length);
    }

    /// <summary>
    /// Updates the damage number instance to have correct data.
    /// </summary>
    /// <param name="value">The value of the number.</param>
    /// <param name="worldPoint">The world location of the damage.</param>
    public void SetData(float value, Vector3 worldPoint)
    {
        // Gets the text component and updates to contain the new value.
        var text = GetComponentInChildren<TMP_Text>();
        text.text = value.ToString();

        // Prepares to update the location in LateUpdate.
        // For some reason positions are just really inaccurate and framerate based in physics updates.
        _newLoc = worldPoint;
    }

    private void LateUpdate()
    {
        // If it doesn't need to update its location
        if (_newLoc == null) return;
        
        // Then get the screen-space position.
        var screenPos = Camera.main.WorldToScreenPoint(_newLoc.GetValueOrDefault());

        // Update it for the object.
        transform.position = screenPos;

        // And reset the field so it doesn't calculate again.
        _newLoc = null;
    }
}
