using TopDown.Input;
using TopDown.Scripts.Weapon;
using UnityEngine;

namespace TopDown.Scripts.Input
{
    [RequireComponent(typeof(WeaponScript))]
    internal class WeaponInput : MonoBehaviour
    {
        private WeaponScript _weapon;

        void Awake()
        {
            _weapon = this.GetComponent<WeaponScript>();
        }

        void Update()
        {
            if (InputScript.Input.Weapon.Shoot.ReadValue<float>() > 0f)
            {
                _weapon.Shoot();
            }
        }
    }
}
