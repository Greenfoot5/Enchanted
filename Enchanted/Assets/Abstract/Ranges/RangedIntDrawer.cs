using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedInt), true)]
public class RangedIntDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        var minProp = property.FindPropertyRelative("minValue");
        var maxProp = property.FindPropertyRelative("maxValue");

        int[] range = {minProp.intValue, maxProp.intValue};

        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            var rangeMin = (int) ranges[0].Min;
            var rangeMax = (int) ranges[0].Max;

            const float rangeBoundsLabelWidth = 40f;

            var rangeBoundsLabel1Rect = new Rect(position) {width = rangeBoundsLabelWidth};
            GUI.Label(rangeBoundsLabel1Rect, new GUIContent(rangeMin.ToString()));
            position.xMin += rangeBoundsLabelWidth;

            var rangeBoundsLabel2Rect = new Rect(position);
            rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
            GUI.Label(rangeBoundsLabel2Rect, new GUIContent(rangeMax.ToString()));
            position.xMax -= rangeBoundsLabelWidth;
            
            GUIContent[] subLabels = {GUIContent.none, GUIContent.none};

            EditorGUI.BeginChangeCheck();
            EditorGUI.MultiIntField(position, subLabels, range);
            if (EditorGUI.EndChangeCheck())
            {
                // Make sure both values stay between the range
                minProp.intValue = range[0] < rangeMin || range[0] > rangeMax ? rangeMin : range[0];
                maxProp.intValue = range[1] < rangeMin || range[1] > rangeMax ? rangeMax : range[1];
            }
        }
        else
        {
            GUIContent[] subLabels = {GUIContent.none, GUIContent.none};
            
            EditorGUI.BeginChangeCheck();
            EditorGUI.MultiIntField(position, subLabels, range);
            if (EditorGUI.EndChangeCheck())
            {
                minProp.intValue = range[0];
                maxProp.intValue = range[1];
            }
        }

        EditorGUI.EndProperty();
    }
}