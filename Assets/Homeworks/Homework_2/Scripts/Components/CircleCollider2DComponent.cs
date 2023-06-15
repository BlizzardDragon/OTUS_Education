using UnityEngine;

// Готово.
namespace ShootEmUp
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class CircleCollider2DComponent : MonoBehaviour
    {
        private CircleCollider2D _circleCollider2D;


        private void Awake() => _circleCollider2D = GetComponent<CircleCollider2D>();

        public void EnableCollider() => _circleCollider2D.enabled = true;
        public void DisableCollider() => _circleCollider2D.enabled = false;
    }
}
