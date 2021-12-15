using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieReady : MonoBehaviour
{
    public Animator theAnimator;

    public Transform target;
    public bool alerted;
    public AudioClip snarlSound;
    private bool soundReady = true;

    private float turnSpeed = 60.0f;
    private bool turning = false;
    private float angle;

    private const float kAngleMax = 2.0f;

    private float walkSpeed = 2.0f;
    private CharacterController charControl;

    private void Start()
    {
        charControl = GetComponent<CharacterController>();
        theAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        theAnimator.SetBool("IsTurning", turning);

        if (Input.GetButton("Fire1") && !alerted)
        {
            alerted = true;
            //soundReady = true;
        }

        if (alerted)
        {
            //StartCoroutine(TurnToPlayer());        
            TurnToPlayer();

            if (angle > kAngleMax || angle < -kAngleMax)
            {
                turning = true;
            }
            else if(angle < kAngleMax && angle > -kAngleMax)
            {
                if (soundReady)
                {
                    Snarl();
                }
                else
                {
                    turning = false;
                    WalkTowards();
                }
            }
        }
    }



    private float TurnToPlayer()
    {
        /***
        transform.LookAt(target);
        yield return new WaitForSeconds(2f);

        if (soundReady)
        {
            //theAnimator.ResetTrigger("IsSnarling");
            theAnimator.SetTrigger("IsSnarling");
            GetComponent<AudioSource>().PlayOneShot(snarlSound);
            soundReady = false;
        }

        Walks();
        ***/

        var localRotate = transform.InverseTransformPoint(target.position);
        angle = Mathf.Atan2(localRotate.x, localRotate.z) * Mathf.Rad2Deg;
        var maxRotation = turnSpeed * Time.deltaTime;
        var turnAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);
        transform.Rotate(0, turnAngle, 0);
        return angle;

    }

    private void Walks()
    {
        theAnimator.SetBool("IsWalking", true);
    }

    private void SnarlSound()
    {
        GetComponent<AudioSource>().PlayOneShot(snarlSound);
        theAnimator.SetBool("IsSnarling", false);
    }

    private void Snarl()
    {
        GetComponent<AudioSource>().PlayOneShot(snarlSound);
        theAnimator.SetTrigger("IsSnarling");
        soundReady = false;
    }

    private void WalkTowards()
    {
        var direction = transform.TransformDirection(Vector3.forward * walkSpeed);
        charControl.SimpleMove(direction);
        theAnimator.SetBool("IsWalking", true);
    }

        

}
