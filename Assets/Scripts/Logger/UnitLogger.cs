using TopDown.Scripts.Extensions;
using TopDown.Scripts.UI;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Weapon;
using UnityEngine;

namespace TopDown.Scripts.Logger
{
    internal class UnitLogger : MonoBehaviour
    {
        [SerializeField] private UnitScript unit;
        [SerializeField] private WeaponScript weapon;
        [SerializeField] private EventsUI events;

        [SerializeField] private bool logMovement;
        [SerializeField] private bool logRicochet;
        private Camera _camera;

        private Vector3 direction;

        void Awake()
        {
            _camera = Camera.main;

            weapon.OnShoot += OnShoot;
        }

        private void OnShoot(BulletScript bullet)
        {
            events.AddEvent($"Cтреляет");
            bullet.OnRicochet += OnRicochet;
        }

        private void OnRicochet(BulletScript arg1, GameObject arg2)
        {
            if (!logRicochet)
                return;

            events.AddEvent($"Рикошет от {arg2.name}");
        }

        void Update()
        {
            if (!logMovement)
                return;

            var direction = Quaternion.AngleAxis(-_camera.transform.eulerAngles.y, Vector3.up) * unit.Movement.Direction;

            if (this.direction != direction)
            {
                if (direction == Vector3.zero)
                    events.AddEvent($"Cтоит");
                else
                    events.AddEvent($"Движется {direction.ToDirection()}");

                this.direction = direction;
            }
        }

    }
}
