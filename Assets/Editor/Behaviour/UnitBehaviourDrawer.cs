using TopDown.Scripts.Enemy.Behaviors.Common;
using UnityEditor;
using UnityEngine;

namespace TopDown.Editor
{
    [CustomPropertyDrawer(typeof(UnitBehaviour), true)]
    public class UnitBehaviourDrawer : PropertyDrawer
    {
        private BaseBehaviourDrawer behaviourDrawer = new BaseBehaviourDrawer();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            behaviourDrawer.OnGUI((after) =>
            {
                var debug = property.FindPropertyRelative($"<{nameof(UnitBehaviour.DrawGizmos)}>k__BackingField");

                after.x += after.width - 20;
                after.width = 20;

                var label = new GUIContent(string.Empty, "DrawGizmos is enabled");

                debug.boolValue = GUI.Toggle(after, debug.boolValue, label);
                property.serializedObject.ApplyModifiedProperties();
            },
            position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return behaviourDrawer.GetPropertyHeight(property, label);
        }
    }
}
