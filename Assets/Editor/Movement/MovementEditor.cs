using TopDown.Scripts.Movement;
using UnityEditor;
using UnityEngine;

namespace TopDown.Editor.Movement
{
    [CustomEditor(typeof(MovementScript))]
    internal class MovementEditor : UnityEditor.Editor
    {
        MovementScript script;

        private void Awake()
        {
            script = this.target as MovementScript;
        }

        public void OnSceneGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                Handles.color = Color.magenta;
                Handles.ArrowHandleCap(
                    0,
                    script.transform.position + Vector3.up * 3,
                    Quaternion.Euler(0, Vector2.SignedAngle(new Vector2(script.Direction.x, script.Direction.z), Vector2.up), 0),
                    script.Direction.magnitude * 2,
                    EventType.Repaint
                );
            }
        }
    }
}
