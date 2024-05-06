using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Arsh.Scripts; 

public class EnemyAIController : MonoBehaviour
{
    public PlayerController playerController;
    public float health;
    public Slider healthSlider;

    

    private void Awake()
    {
        health = 100;
    }

    private void Update()
    {
        healthSlider.value = health;
       
        
    }
    //when enemy attacks
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reduce player's health
            playerController.TakeDamage(20); 
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);   
    }
}


