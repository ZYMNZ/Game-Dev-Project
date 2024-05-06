using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    private double health;
    private bool canHeal;

    private void Awake()
    {
        health = 1000;
        canHeal = false;
    }

    private void Update()
    {
        Debug.Log($"health: {health}");
        Heal();
    }

    private void Damage()
    {
        this.health--;
        if (health <= 0)
            Die();
    }

    private void Heal()
    {
        if (canHeal)
            this.health += 0.5;

        if (health > 1000)
            canHeal = false;
    }

    private void OnTriggerStay(Collider trigger)
    {
        // and some other condition saying the player is ATTACKING
        if (trigger.transform.parent.tag == "Player")
        {
            Damage();
            canHeal = true;
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        // so the enemy doesn't heal too quickly
        if (trigger.transform.parent.tag == "Player" && canHeal == true)
        {
            canHeal = true;
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
