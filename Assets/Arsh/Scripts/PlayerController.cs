using UnityEngine;
using UnityEngine.InputSystem;

namespace Arsh.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movementForce = 1f;

        [SerializeField] private float jumpForce = 5f;

        [SerializeField] private float maxSpeed = 5f;

        [SerializeField] private Camera playerCamera;

        private Vector3 _forceDirection = Vector3.zero;
        private Animator _animator;

        // Input Fields
        private InputManager _inputManager;
        private InputAction _move;

        // Movement Fields
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _inputManager = new InputManager();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            _forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            _forceDirection += _move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

            _rb.AddForce(_forceDirection, ForceMode.Impulse);
            _forceDirection = Vector3.zero;

            if (_rb.velocity.y < 0f) _rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

            Vector3 horizontalVelocity = _rb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
                _rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * _rb.velocity.y;
        
            LookAt();
        }

        private void OnEnable()
        {
            _inputManager.Player.Jump.started += Jump;
            // _inputManager.Player.Attack.started += Attack;
            _move = _inputManager.Player.Move;
            _inputManager.Player.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Player.Jump.started -= Jump;
            // _inputManager.Player.Attack.started -= Attack;
            _inputManager.Player.Disable();
        }

        private void LookAt()
        {
            Vector3 direction = _rb.velocity;
            direction.y = 0f;

            if (_move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.01f)
            {
                _rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            else
            {
                _rb.angularVelocity = Vector3.zero;
            }
        }
    
        private Vector3 GetCameraForward(Camera playerCamera)
        {
            Vector3 forward = playerCamera.transform.forward;
            forward.y = 0;
            return forward.normalized;
        }

        private Vector3 GetCameraRight(Camera playerCamera)
        {
            Vector3 right = playerCamera.transform.right;
            right.y = 0;
            return right.normalized;
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            if (IsGrounded()) _forceDirection += Vector3.up * jumpForce;
        }

        private bool IsGrounded()
        {
            Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
            return Physics.Raycast(ray, out RaycastHit hit, 0.3f);
        }
    
        private void Attack(InputAction.CallbackContext obj)
        {
            _animator.SetTrigger("attack");
        }
    }
}