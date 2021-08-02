using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Displays the RangedFloat in the editor in a way easy to understand.
/// </summary>
[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class RangedFloatDrawer : PropertyDrawer {
    /// <summary>
    /// Called for rendering and handling the GUI events for RangedFLoat
    /// </summary>
    /// <param name="position">The position in the Inspector</param>
    /// <param name="property">The property we're displaying</param>
    /// <param name="label">The GUI label for the property</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Setup the position and label
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);
        
        // Get a reference to the min and max variables of the RangedFloat
        var minProp = property.FindPropertyRelative("minValue");
        var maxProp = property.FindPropertyRelative("maxValue");
        
        // Obtain the actual values for the variables
        float[] range = {minProp.floatValue, maxProp.floatValue};
        
        // Check if we have the MinMaxRangeAttribute on the variable
        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            // If we do, get the Min/Max or the attribute
            var rangeMin = ranges[0].Min;
            var rangeMax = ranges[0].Max;
            
            // Set the width of the label
            const float rangeBoundsLabelWidth = 40f;
            
            // Display the min value label to the left of the slider
            var rangeBoundsLabel1Rect = new Rect(position) {width = rangeBoundsLabelWidth};
            GUI.Label(rangeBoundsLabel1Rect, new GUIContent(range[0].ToString("F2")));
            position.xMin += rangeBoundsLabelWidth;
            
            // Display the max value label to the right of the slider
            var rangeBoundsLabel2Rect = new Rect(position);
            rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
            GUI.Label(rangeBoundsLabel2Rect, new GUIContent(range[1].ToString("F2")));
            position.xMax -= rangeBoundsLabelWidth;
            
            EditorGUI.BeginChangeCheck();
            // Create the slider
            EditorGUI.MinMaxSlider(position, ref range[0], ref range[1],
                rangeMin, rangeMax);
            // If the variables are changed in the editor, set them.
            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = range[0];
                maxProp.floatValue = range[1];
            }
        }
        else
        {
            // We require subLabels for each box, but we don't want to display anything
            GUIContent[] subLabels = {GUIContent.none, GUIContent.none};
            
            EditorGUI.BeginChangeCheck();
            // Create the input fields
            EditorGUI.MultiFloatField(position, subLabels, range);
            // If the variables are changed in the editor, round them to 5 d.p. and set them.
            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = (float) Math.Round(range[0], 5);
                maxProp.floatValue = (float) Math.Round(range[1], 5);
            }
        }

        EditorGUI.EndProperty();
    }
}