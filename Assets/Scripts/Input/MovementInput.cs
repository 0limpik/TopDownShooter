using TopDown.Input;
using TopDown.Scripts.Unit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Scripts.Input
{
    internal class MovementInput : MonoBehaviour
    {
        private UnitScript _unit;
        private Camera _camera;

        void Awake()
        {
            _unit = this.GetComponent<UnitScript>();
            _camera = Camera.main;
        }

        void OnEnable()
        {
            InputScript.Input.Movement.Direction.performed += OnDirection;
            InputScript.Input.Movement.Direction.canceled += OnDirection;
        }

        void OnDisable()
        {
            InputScript.Input.Movement.Direction.performed -= OnDirection;
            InputScript.Input.Movement.Direction.canceled -= OnDirection;
        }

        private void OnDirection(InputAction.CallbackContext context)
        {
            var read = context.ReadValue<Vector2>();

            var direction = new Vector3(read.x, 0, read.y);

            direction = Quaternion.AngleAxis(_camera.transform.eulerAngles.y, Vector3.up) * direction;

            _unit.Movement.Direction = direction;
        }
    }
}
