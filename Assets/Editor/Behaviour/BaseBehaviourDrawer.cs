using System;
using System.Linq;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Base;
using TopDown.Scripts.Enemy.Behaviors.Base;
using UnityEditor;
using UnityEngine;

namespace TopDown.Editor
{
    [CustomPropertyDrawer(typeof(BaseBehaviour), true)]
    public class BaseBehaviourDrawer : PropertyDrawer
    {
        public void OnGUI(Action<Rect> OnGUI, Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var value = property.GetValue<BaseBehaviour>();

            var running = value?.running;

            var tooltip = "Green -> Running, Red -> Stop";

            if (value != null)
            {
                var attrs = value.GetType()
                    .GetCustomAttributes(false)
                    .Where(x => x.GetType() == typeof(InfoAttribute))
                    .Cast<InfoAttribute>();

                tooltip = string.Join("\n", attrs.Select(x => x.description));
            }

            var prefix = new GUIContent(property.displayName, tooltip);
            var brackets = new GUIContent(" (Behaviour)");

            var x = GUIStyle.none.CalcSize(prefix).x;
            var x2 = x + GUIStyle.none.CalcSize(brackets).x;

            OnGUI?.Invoke(new Rect(position.x + x2, position.y - 1, position.width - x2, 20));

            EditorGUI.PropertyField(position, property, label, true);

            var color = GUI.color;
            GUI.color = running.HasValue && Application.isPlaying ? running.Value ? Color.green : Color.red : Color.white;
            EditorGUI.LabelField(new Rect(position.x, position.y - 1, position.width, 20), prefix);
            GUI.color = color;

            EditorGUI.LabelField(new Rect(position.x + x, position.y - 1, position.width, 20), brackets);

            EditorGUI.EndProperty();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            OnGUI(null, position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
}
