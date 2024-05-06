using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    public Slider healthBar;
    public Animator animator;

   private bool isDying = false;

    // Update is called once per frame
    void Update()
    {
        healthBar.value = HP;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0 && !isDying)
        {
           
            isDying = true;
           
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            Invoke("DestroyAfterDelay", 5f);
        }
        else if (HP > 0)
        {
            animator.SetTrigger("damage");
        }
    }

    // This method will be invoked after 5 seconds
    private void DestroyAfterDelay()
    {
        // Destroy the game object
        gameObject.SetActive(false);
    }
}
