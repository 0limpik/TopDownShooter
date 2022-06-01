using System;
using System.Collections.Generic;

namespace TopDown.Scripts.Weapon
{
    internal static class BulletRepository
    {
        public static event Action<BulletScript> OnAdd;
        public static event Action<BulletScript> OnRemove;

        public static IEnumerable<BulletScript> Bullets => _Bullets;
        private static List<BulletScript> _Bullets = new List<BulletScript>();

        public static void Add(BulletScript bullet)
        {
            _Bullets.Add(bullet);
            OnAdd?.Invoke(bullet);
        }

        public static void Remove(BulletScript bullet)
        {
            _Bullets.Remove(bullet);
            OnRemove?.Invoke(bullet);
        }
    }
}
