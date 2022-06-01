using UnityEngine;

namespace TopDown.Scripts.Movement
{
    public interface IRotation
    {
        Vector3 LookAt { get; set; }
        Quaternion Quaternion { get; }
    }
}
