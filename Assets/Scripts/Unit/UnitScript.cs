using System;
using TopDown.Scripts.Movement;
using UnityEngine;

namespace TopDown.Scripts.Unit
{
    [ExecuteAlways]
    [RequireComponent(typeof(MovementScript))]
    [RequireComponent(typeof(RotationScript))]
    [RequireComponent(typeof(Collider))]
    public class UnitScript : MonoBehaviour, IUnit
    {
        public event Action OnDrawGizmos;

        public MovementScript Movement { get; private set; }
        public RotationScript Rotation { get; private set; }
        public Vector3 Center => Collider.bounds.center;
        public Collider Collider { get; private set; }

        IMovement IUnit.Movement => Movement;

        IRotation IUnit.Rotation => Rotation;

        void Awake()
        {
            Movement = this.GetComponent<MovementScript>();
            Rotation = this.GetComponent<RotationScript>();
            Collider = this.GetComponent<Collider>();
        }

        public void OnDrawGizmosInvoke()
        {
            OnDrawGizmos?.Invoke();
        }
    }
}
