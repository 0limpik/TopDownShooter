using System.Collections.Generic;
using System.Linq;
using TopDown.Scripts.Unit;
using UnityEngine;

namespace TopDown.Scripts.Visibility
{
    [ExecuteAlways]
    internal class UnitVisibility : MonoBehaviour
    {
        public static UnitVisibility Instance { get; private set; }

        private List<UnitVisible> units = new List<UnitVisible>();

        [SerializeField] private int count;

        void Awake()
        {
            Instance = this;

            var scripts = GameObject.FindObjectsOfType<UnitScript>().Cast<IUnit>().ToArray();

            for (int i = 0; i < scripts.Length; i++)
            {
                for (int j = i + 1; j < scripts.Length; j++)
                {
                    units.Add(new UnitVisible
                    {
                        first = scripts[i],
                        second = scripts[j],
                        isVisible = IsVisible(scripts[i].Center, scripts[j].Center)
                    });
                }
            }

            count = units.Count;
        }

        void FixedUpdate()
        {
            foreach (var visible in units)
            {
                visible.isVisible = IsVisible(visible.first.Center, visible.second.Center);
            }
        }

        public bool IsVisible(IUnit first, IUnit second)
        {
            var visible = GetOrAdd(first, second).isVisible;
            count = units.Count;
            return visible;
        }

        public bool IsVisible(Vector3 start, Vector3 end)
        {
            return !Physics.Linecast(start, end, 1 << LayerMask.NameToLayer("Obstacle"));
        }

        private UnitVisible GetOrAdd(IUnit first, IUnit second)
        {
            var visible = units.FirstOrDefault(x => x.Any(first) || x.Any(second));

            if (visible != null)
            {
                return visible;
            }

            visible = new UnitVisible
            {
                first = first,
                second = second,
                isVisible = IsVisible(first.Center, second.Center)
            };

            units.Add(visible);

            return visible;
        }

        private class UnitVisible
        {
            public IUnit first;
            public IUnit second;

            public bool isVisible;

            public bool Any(IUnit unit)
                => first == unit || second == unit;
        }
    }
}
