using System;
using TopDown.Scripts.Movement;
using UnityEngine;

namespace TopDown.Scripts.Unit
{
    public interface IUnit
    {
        event Action OnDrawGizmos;
        IMovement Movement { get; }
        IRotation Rotation { get; }
        Vector3 Center { get; }
        Collider Collider { get; }
    }
}
