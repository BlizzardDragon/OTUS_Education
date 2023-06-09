using Architecture.DI;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private CharacterController _characterController;
        private float _horizontalDirection;


        [Inject]
        public void Construct(MoveComponent moveComponent, CharacterController characterController)
        {
            _moveComponent = moveComponent;
            _characterController = characterController;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterController._fireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _horizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _horizontalDirection = 1;
            }
            else
            {
                _horizontalDirection = 0;
            }
        }

        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(_horizontalDirection, 0) * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}