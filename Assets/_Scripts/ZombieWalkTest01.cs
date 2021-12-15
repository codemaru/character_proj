using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWalkTest01 : MonoBehaviour
{
    private Animator zombieControl;
    private bool walkBool;
    private const string kWalkAnimName = "SwitchWalk";

    private void Start()
    {
        zombieControl = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            walkBool = !walkBool;
            zombieControl.SetBool(kWalkAnimName, walkBool);          
        }
    }
}
