using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Animator animator;
    EnemyHealthBar enemyHealth; 

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.transform.tag == "enemy")
        {
            Arsh.Scripts.PlayerController.projectileMovement = false;

            enemyHealth = otherCollider.gameObject.GetComponent<EnemyHealthBar>();
            gameObject.SetActive(false);
            enemyHealth.TakeDamage(20);
            Debug.Log(enemyHealth.HP);
        }
    }
}
