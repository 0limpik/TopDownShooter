using System;
using System.Threading.Tasks;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Unit;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Common
{
    [Serializable]
    public abstract class UnitBehaviour : BaseBehaviour
    {
        [field: SerializeField, HideInInspector] public bool DrawGizmos { get; private set; }

        protected IUnit unit;

        private bool isInit;

        protected virtual void Init(IUnit unit)
        {
            this.unit = unit;

            unit.OnDrawGizmos += () =>
            {
                if (DrawGizmos)
                    OnDrawGizmos();
            };

            isInit = true;
        }

        protected override Task OnRun()
        {
            if (!isInit) throw new InvalidOperationException("init before run");

            return Task.CompletedTask;
        }

        protected virtual void OnDrawGizmos() { }
    }
}
