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
    
    //public List<EnemyAIController> enemyControllers = new List<EnemyAIController>();
    public void TakeDamage(int damageAmount){
        
        HP -= damageAmount;
        if (HP <= 0) {
            //AudioManager.instance
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            
        }
        else {
             animator.SetTrigger("damage");
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = HP;
    }
}
