using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class KidsScript2 : MonoBehaviour
    {
        private Animator _animator;
        public float moveSpeed = 5f; // Adjust the speed as needed

        void Start ()
        {
            _animator = GetComponent<Animator>();
        }
        void Update()
        {
            // Get input from arrow keys
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalInput -= 500000f;
                _animator.Play("move");
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontalInput += 500000f;
                _animator.Play("move");
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                verticalInput += 500000f;
                _animator.Play("move");
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                verticalInput -= 500000f;
                _animator.Play("move");
            }
            
            /*
            if (Input.GetKey(KeyCode.RightArrow))
                horizontalInput += 500000f;
            if (Input.GetKey(KeyCode.UpArrow))
                verticalInput += 50000000f;
            if (Input.GetKey(KeyCode.DownArrow))
                verticalInput -= 5000000f;*/

            // Calculate the movement direction based on input
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            // Move the GameObject
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        }

    }
}
