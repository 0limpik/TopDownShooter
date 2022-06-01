using UnityEngine;

namespace TopDown.Scripts.Movement
{
    public interface IMovement
    {
        Vector3 Direction { get; set; }
        Vector3 Position { get; }
        float Speed { get; }
    }
}
