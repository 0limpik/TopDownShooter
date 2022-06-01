using TopDown.Scripts.Enemy.Behaviors.Common;
using UnityEditor;
using UnityEngine;

namespace TopDown.Editor.Behaviour
{
    [CustomPropertyDrawer(typeof(WaitBehaviour), true)]
    internal class WaitBehaviourDrawer : PropertyDrawer
    {
        private BaseBehaviourDrawer behaviourDrawer = new BaseBehaviourDrawer();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            behaviourDrawer.OnGUI((after) =>
            {
                after.x += 10f;

                var prefix = new GUIContent("Time");
                EditorGUI.LabelField(after, prefix);

                after.x += 10f + GUIStyle.none.CalcSize(prefix).x;
                var time = property.FindPropertyRelative("pastTime").floatValue;
                var timeLabel = new GUIContent(time.ToString("F2"), "Time left");
                EditorGUI.LabelField(after, timeLabel);
            },
            position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return behaviourDrawer.GetPropertyHeight(property, label);
        }
    }
}
