using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Base;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    [Info("Предсказывает позицию через заданное время")]
    internal class PredictionPositionBehaviour : BaseBehaviour, IFuturePosition
    {
        [SerializeField] public float positionsCacheTime = 3f;

        private Func<Vector3> GetPosition;

        private List<PositionCache> positions = new List<PositionCache>();

        public void Init(Func<Vector3> GetPosition)
        {
            this.GetPosition = GetPosition;
        }

        protected override Task OnRun()
        {
            return Task.CompletedTask;
        }

        protected override void OnStop()
        {
            positions.Clear();
        }

        private float time;
        private Vector3 lastPosition;

        public override void Update(float deltaTime)
        {
            time += deltaTime;

            var delta = GetPosition() - lastPosition;

            positions.Add(new PositionCache(deltaTime, time, delta));

            lastPosition = GetPosition();

            var calcTime = time - positionsCacheTime;

            var toDel = new List<PositionCache>();

            foreach (var cache in positions)
            {
                if (cache.Time > calcTime)
                {
                    break;
                }
                toDel.Add(cache);
            }

            foreach (var cache in toDel)
            {
                positions.Remove(cache);
            }
        }

        public Vector3 GetFuturePosition(float time)
        {
            return GetFuturePosition(_ => time);
        }

        public Vector3 GetFuturePosition(GetTime GetTime)
        {
            IEnumerable<PositionCache> enumerable = positions;

            var deltaSum = Vector3.zero;
            var expTime = 0f;

            var delta = GetPosition();

            foreach (var cache in enumerable.Reverse())
            {
                delta = GetPosition() + deltaSum;

                if (expTime > GetTime(delta))
                {
                    return delta;
                }

                expTime += cache.DeltaTime;

                deltaSum += cache.DeltaPosition;
            }

            return delta;
        }

        private class PositionCache
        {
            public float Time { get; private set; }
            public float DeltaTime { get; private set; }
            public Vector3 DeltaPosition { get; private set; }

            public PositionCache(float deltaTime, float time, Vector3 position)
            {
                DeltaTime = deltaTime;
                Time = time;
                DeltaPosition = position;
            }
        }
    }
}
