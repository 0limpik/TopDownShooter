using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = 0.0f;

        totalHeight += EditorGUIUtility.singleLineHeight;

        if (property.objectReferenceValue == null)
            return totalHeight;

        if (!property.isExpanded)
            return totalHeight;

        SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

        if (targetObject == null)
            return totalHeight;

        SerializedProperty field = targetObject.GetIterator();

        field.NextVisible(true);

        while (field.NextVisible(false))
        {
            totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        return totalHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect fieldRect = new Rect(position);
        fieldRect.height = EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(fieldRect, property, label, true);

        if (property.objectReferenceValue == null)
            return;

        property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

        if (!property.isExpanded)
            return;

        SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

        if (targetObject == null)
            return;


        #region Format Field Rects
        List<Rect> propertyRects = new List<Rect>();
        Rect marchingRect = new Rect(fieldRect);

        Rect bodyRect = new Rect(fieldRect);
        bodyRect.xMin += EditorGUI.indentLevel * 14;
        bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        SerializedProperty field = targetObject.GetIterator();
        field.NextVisible(true);

        while (field.NextVisible(false))
        {
            marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
            marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
            propertyRects.Add(marchingRect);
        };

        bodyRect.yMax = marchingRect.yMax;
        #endregion

        #region Draw Fields
        EditorGUI.indentLevel++;

        int index = 0;
        field = targetObject.GetIterator();
        field.NextVisible(true);

        //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
        while (field.NextVisible(false))
        {
            try
            {
                EditorGUI.PropertyField(propertyRects[index], field, true);
            }
            catch (StackOverflowException)
            {
                field.objectReferenceValue = null;
                Debug.LogError("Detected self-nesting cauisng a StackOverflowException, avoid using the same " +
                    "object iside a nested structure.");
            }

            index++;
        }

        targetObject.ApplyModifiedProperties();

        EditorGUI.indentLevel--;
        #endregion
    }
}