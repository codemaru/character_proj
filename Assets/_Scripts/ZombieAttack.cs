using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class ZombieAttack : MonoBehaviour
    {
        [SerializeField]
        private Animator thisAnimator;

        //private void Start()
        //{
        //    thisAnimator = GetComponent<Animator>();
        //}

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))//GetMouseButton(0)
            {
                Debug.Log("Fire1 Pressing");
                thisAnimator.SetTrigger("Hits");
            }
        }
    }
}