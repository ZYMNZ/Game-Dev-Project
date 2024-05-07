using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Arsh.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public List<EnemyHealthBar> enemyHealthBars = new List<EnemyHealthBar>();
        public List<EnemyAIController> enemyControllers = new List<EnemyAIController>();
       
        public int health;
        public Slider healthSlider;

        [SerializeField] private float movementForce = 1f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private Camera playerCamera;
        public static bool speedPowerUp = false; // accesed by kidsscriptlite!
        private static int speedCount = 0;

        private Vector3 _forceDirection = Vector3.zero;
        private Animator _animator;

        // Input Fields
        private InputManager _inputManager;
        private InputAction _move;

        // Movement Fields
        private Rigidbody _rb;
        public bool _attackTrigger;

        // Attack trigger
        public GameObject wand;
        private Collider _wandCollider;

        public bool AttackTrigger
        {
            get => _attackTrigger;
            set => _attackTrigger = value;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _inputManager = new InputManager();
            _animator = GetComponent<Animator>();
            //_wandCollider = GetComponentInChildren<Collider>(); 
            _wandCollider = wand.GetComponent<Collider>();
            health = 100;
        }

        private void Update()
        {
            healthSlider.value = health;
            PowerUp_Speed();
            Debug.Log(speedCount);
        }

        private void Start()
        {
            _attackTrigger = false;
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
            
            _attackTrigger = false;
        }

        private void OnEnable()
        {
            _inputManager.Player.Jump.started += Jump;
            _inputManager.Player.Attack.started += Attack;
            _move = _inputManager.Player.Move;
            _inputManager.Player.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Player.Jump.started -= Jump;
            _inputManager.Player.Attack.started -= Attack;
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
            _wandCollider.enabled = true;
            _attackTrigger = true;
            _animator.SetTrigger("attack");
            
             // Apply damage to enemies within range
            foreach (EnemyHealthBar enemyHealthBar in enemyHealthBars)
            {
                if (Vector3.Distance(transform.position, enemyHealthBar.transform.position) < 5f)
                {
                    enemyHealthBar.TakeDamage(35);
                    
                }
            }

            Invoke(nameof(DisableWandCollider), 0.1f);

            /*
            // Apply damage only to enemy controllers within range
            foreach (EnemyAIController enemyController in enemyControllers)
            {
                if (Vector3.Distance(transform.position, enemyController.transform.position) < 5f)
                {
                    enemyController.TakeDamage(35);
                }
            }

            Invoke("DisableWandCollider", 0.1f);
            */
        }
        
        
        private void DisableWandCollider()
        {
            _wandCollider.enabled = false;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            healthSlider.value = health;
            Debug.Log(healthSlider.value);
            Canvas.ForceUpdateCanvases();
            // if (health <= 0)
            // {
            //     //end game?
            // }
        }

        /* CONDITION TO CHANGE SPEEDPOWERUP TO TRUE IS FOUND IN KIDSCRIPTLITE, MEGANE FOLDER */
        private void PowerUp_Speed()
        {
            if (speedPowerUp)
            {
                if (maxSpeed <= 5) // if the speed has not been applied yet
                    maxSpeed = 100;
                else
                    speedCount++;

                transform.Find("Particles")?.gameObject.SetActive(true);
            }
            
            if(speedCount == 300)
            {
                speedPowerUp = false; // reset the speed to normal
                speedCount = 0;
            }

            if (!speedPowerUp && maxSpeed >= 5)
            {
                maxSpeed = 5;
                transform.Find("Particles")?.gameObject.SetActive(false);
            }
        }
    }
}
