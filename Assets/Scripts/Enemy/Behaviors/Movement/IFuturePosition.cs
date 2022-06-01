using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    internal delegate float GetTime(Vector3 futurePosition);

    internal interface IFuturePosition
    {
        Vector3 GetFuturePosition(float time);
        Vector3 GetFuturePosition(GetTime GetTime);
    }
}
