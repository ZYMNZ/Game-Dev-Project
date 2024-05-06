using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsScriptLite : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log($"speedUpTime: {speedUpTime}");
        //Debug.Log($"speedUpTime: {speedParameter}");
    }
    
    // I get nonsensical errors so i'm going to comment this out for now.
    /* private void OnTriggerEnter(Collider trigger)
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
            Arsh.Scripts.PlayerController.speedPowerUp = true;
            trigger.transform.parent.gameObject.SetActive(false);
        }
    }   */
}
