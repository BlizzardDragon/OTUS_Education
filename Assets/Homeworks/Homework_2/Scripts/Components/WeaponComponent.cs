using UnityEngine;

// Готово.
namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;

        public Vector2 Position
        {
            get { return firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return firePoint.rotation; }
        }
    }
}