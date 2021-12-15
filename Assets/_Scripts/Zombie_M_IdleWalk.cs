using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_M_IdleWalk : MonoBehaviour
{
    private Animator theAnimator;
    private float walkTime = 5.0f;
    private float walkTimer;
    private bool walking;
    private bool changeWalk = false;

    private void Start()
    {
        theAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        theAnimator.SetBool("AltWalking", changeWalk);
        if (Input.GetKeyDown("b"))
        {
            changeWalk = !changeWalk;
        }

        walkTimer += Time.deltaTime;
        if (walkTimer >= walkTime)
        {
            ResetTime();
            walking = !walking;
        }

        theAnimator.SetBool("IsWalking", walking);
    }

    private void ResetTime()
    {
        walkTimer = 0.0f;
    }
}
