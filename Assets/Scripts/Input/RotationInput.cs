using TopDown.Input;
using TopDown.Scripts.Unit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Scripts.Input
{
    [RequireComponent(typeof(UnitScript))]
    public class RotationInput : MonoBehaviour
    {
        private UnitScript _unit;
        private Camera _camera;

        private Plane plane = new Plane(Vector3.up, 0);

        private Vector2 mouse;

        void Awake()
        {
            _unit = this.GetComponent<UnitScript>();
            _camera = Camera.main;
        }

        void OnEnable()
        {
            InputScript.Input.Rotation.Position.performed += OnPosition;
        }

        void OnDisable()
        {
            InputScript.Input.Rotation.Position.performed -= OnPosition;
        }

        void Update()
        {
            var ray = _camera.ScreenPointToRay(mouse);

            if (plane.Raycast(ray, out float enter))
            {
                _unit.Rotation.LookAt = ray.GetPoint(enter);
            }
        }

        private void OnPosition(InputAction.CallbackContext context)
        {
            mouse = context.ReadValue<Vector2>();

            mouse = new Vector2
            (
                Mathf.Clamp(mouse.x, 0, Screen.width),
                Mathf.Clamp(mouse.y, 0, Screen.height)
            );
        }
    }
}
