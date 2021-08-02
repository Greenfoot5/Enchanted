using UnityEngine;
using UnityEditor;

/// <summary>
/// Displays the RangedInt in the editor in a way easy to understand.
/// </summary>
[CustomPropertyDrawer(typeof(RangedInt), true)]
public class RangedIntDrawer : PropertyDrawer {
    /// <summary>
    /// Called for rendering and handling the GUI events for RangedFLoat
    /// </summary>
    /// <param name="position">The position in the Inspector</param>
    /// <param name="property">The property we're displaying</param>
    /// <param name="label">The GUI label for the property</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Setup the position and the label
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);
        
        // Get a reference to the min and max variables of the RangedFloat
        var minProp = property.FindPropertyRelative("minValue");
        var maxProp = property.FindPropertyRelative("maxValue");
        
        // Obtain the actual values for the variables
        int[] range = {minProp.intValue, maxProp.intValue};
        
        // Check if we have the MinMaxRangeAttribute on the variable
        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            // If we do, get the Min/Max or the attribute
            var rangeMin = (int) ranges[0].Min;
            var rangeMax = (int) ranges[0].Max;
            
            // Set the width of the label
            const float rangeBoundsLabelWidth = 40f;

            // Display the min value label to the left of the input fields
            var rangeBoundsLabel1Rect = new Rect(position) {width = rangeBoundsLabelWidth};
            GUI.Label(rangeBoundsLabel1Rect, new GUIContent(rangeMin.ToString()));
            position.xMin += rangeBoundsLabelWidth;

            // Display the max value label to the right of the input fields
            var rangeBoundsLabel2Rect = new Rect(position);
            rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
            GUI.Label(rangeBoundsLabel2Rect, new GUIContent(rangeMax.ToString()));
            position.xMax -= rangeBoundsLabelWidth;
            
            // We require subLabels for each box, but we don't want to display anything as our range is above
            // Displaying the range above allows us to set our own width and customise it more
            GUIContent[] subLabels = {GUIContent.none, GUIContent.none};

            EditorGUI.BeginChangeCheck();
            // Create the input fields
            EditorGUI.MultiIntField(position, subLabels, range);
            // If the variables are changed in the editor, set them.
            if (EditorGUI.EndChangeCheck())
            {
                // Make sure both values stay between the range
                minProp.intValue = range[0] < rangeMin || range[0] > rangeMax ? rangeMin : range[0];
                maxProp.intValue = range[1] < rangeMin || range[1] > rangeMax ? rangeMax : range[1];
            }
        }
        else
        {
            // We require subLabels for each box, but we don't want to display anything
            GUIContent[] subLabels = {GUIContent.none, GUIContent.none};
            
            EditorGUI.BeginChangeCheck();
            // Create the input fields
            EditorGUI.MultiIntField(position, subLabels, range);
            // If the variables are changed in the editor, set them.
            if (EditorGUI.EndChangeCheck())
            {
                minProp.intValue = range[0];
                maxProp.intValue = range[1];
            }
        }

        EditorGUI.EndProperty();
    }
}