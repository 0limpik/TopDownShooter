using UnityEngine;

namespace TopDown.Input
{
    internal class InputScript : MonoBehaviour
    {
        public static PlayerInput Input => _Input ??= new PlayerInput();
        private static PlayerInput _Input;

        void OnEnable()
        {
            Input.Enable();
        }

        void OnDisable()
        {
            Input.Disable();
        }
    }
}
