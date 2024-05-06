using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsScriptLite : MonoBehaviour
{
    private int speedUpTime;
    public static int speedParameter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedUpTime = speedParameter = animator.GetInteger("speed"); // should be 10
        Debug.Log(speedParameter);
    }

    private void Update()
    {
        //Debug.Log(speedUpTime);
        //Debug.Log(speedParameter);
        SPEED_POWERUP();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        //Debug.Log(trigger.gameObject.name);
        if (trigger.transform.parent.tag == "doorWay1")
        {
            Animator doorAnimator = trigger.transform.parent.Find("door").GetComponent<Animator>();
            // check if no animation is already playing
            if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("door_idle"))
            {
                doorAnimator.Play("door_open");
            }
        }

        // check if no animation is already playing
        if (trigger.transform.parent.tag == "doorWay2")
        {
            Animator doorAnimator = trigger.transform.parent.GetComponent<Animator>();
            // check if no animation is already playing
            if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("door_idle_2"))
            {
                doorAnimator.Play("door_open_2");
            }
        }

        if (trigger.transform.parent.tag == "powerup")
        {
            trigger.transform.parent.gameObject.SetActive(false);
            // start the speed up, should be 11
            speedUpTime += 1;
        }
    }  

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void SPEED_POWERUP()
    {
        // if speed powerup is enabled
        if (speedUpTime > 10 && speedUpTime < 20)
        {
            animator.SetInteger(speedParameter, speedUpTime);
            speedUpTime++;
        }

        // reset speedParameter to its original value
        if (speedUpTime == 20)
        {
            speedUpTime = 10;
            animator.SetInteger(speedParameter, speedUpTime);
        }
    }
}
