using System;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class RangedFloatDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        var minProp = property.FindPropertyRelative("minValue");
        var maxProp = property.FindPropertyRelative("maxValue");

        float[] range = {minProp.floatValue, maxProp.floatValue};

        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            var rangeMin = ranges[0].Min;
            var rangeMax = ranges[0].Max;

            const float rangeBoundsLabelWidth = 40f;

            var rangeBoundsLabel1Rect = new Rect(position) {width = rangeBoundsLabelWidth};
            GUI.Label(rangeBoundsLabel1Rect, new GUIContent(range[0].ToString("F2")));
            position.xMin += rangeBoundsLabelWidth;

            var rangeBoundsLabel2Rect = new Rect(position);
            rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
            GUI.Label(rangeBoundsLabel2Rect, new GUIContent(range[1].ToString("F2")));
            position.xMax -= rangeBoundsLabelWidth;

            EditorGUI.BeginChangeCheck();
            EditorGUI.MinMaxSlider(position, ref range[0], ref range[1], rangeMin, rangeMax);
            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = range[0];
                maxProp.floatValue = range[1];
            }
        }
        else
        {
            GUIContent[] subLabels = {GUIContent.none, GUIContent.none};
            
            EditorGUI.BeginChangeCheck();
            EditorGUI.MultiFloatField(position, subLabels, range);
            if (EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = (float) Math.Round(range[0], 5);
                maxProp.floatValue = (float) Math.Round(range[1], 5);
            }
        }

        EditorGUI.EndProperty();
    }
}