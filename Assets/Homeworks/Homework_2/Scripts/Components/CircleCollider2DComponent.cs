using UnityEngine;

// Готово.
namespace ShootEmUp
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class CircleCollider2DComponent : MonoBehaviour
    {
        private CircleCollider2D _circleCollider2D;


        private void Awake() => _circleCollider2D = GetComponent<CircleCollider2D>();

        public void SetActiveCollider(bool value) => _circleCollider2D.enabled = value;
    }
}
