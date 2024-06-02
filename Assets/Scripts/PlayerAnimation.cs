using UnityEngine;

namespace Arsh.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody _rigidBody;
        private float _maxSpeed = 5f;

        private static readonly int Speed = Animator.StringToHash("speed");

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody>();
        
        }

        // Update is called once per frame
        void Update()
        {
            // Changed Animation speed of run in editor
            _animator.SetFloat(Speed, _rigidBody.velocity.magnitude / _maxSpeed);
        }
    }
}
