#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntReference))]
public class IntReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Find the properties inside FloatReference
        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
        SerializedProperty variable = property.FindPropertyRelative("variable");

        // Draw the main label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate the rects
        Rect lableRect = new Rect(position.x - 40, position.y, 50, position.height);
        Rect toggleRect = new Rect(position.x, position.y, 20, position.height);
        Rect fieldRect = new Rect(position.x + 25, position.y, position.width - 25, position.height);

        // Draw the toggle and field based on the state of useConstant
        EditorGUI.LabelField(lableRect, "Const");
        useConstant.boolValue = EditorGUI.Toggle(toggleRect, useConstant.boolValue);
        if (useConstant.boolValue)
        {
            EditorGUI.PropertyField(fieldRect, constantValue, GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(fieldRect, variable, GUIContent.none);
        }

        EditorGUI.EndProperty();
    }
}
#endif
