using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Player
{
    public class MovementHandler : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float Gravity = -9.81f;
        public Transform cameraTransform => Camera.main.transform;

        private CharacterController _characterController;
        private Vector3 _moveDirection;
        private Vector3 _velocity;
        private bool _isGrounded;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            _isGrounded = _characterController.isGrounded;

            ProcessInputs();
            ApplyGravity();
            Move();
            RotatePlayer();
        }

        void ProcessInputs()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveZ = Input.GetAxisRaw("Vertical");

            var forward = cameraTransform.forward;
            var right = cameraTransform.right;
        
            // We only want the movement on the XZ plane, so we set Y to 0
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            _moveDirection = (forward * moveZ + right * moveX).normalized;
        }

        private void ApplyGravity()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f; // A small negative value to keep the player grounded
            }
            else
            {
                _velocity.y += Gravity * Time.deltaTime;
            }
        }

        private void Move()
        {
            var movement = _moveDirection * MoveSpeed;
            movement.y = _velocity.y;
            _characterController.Move(movement * Time.deltaTime);
        }

        private void RotatePlayer()
        {
            if (_moveDirection == Vector3.zero) return;
            var toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
        }
    }
}
