using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class MyFPSAnimation : MonoBehaviour
    {
        private Animator thisAnimator;
        private float horizontalSpeed = 0.0f;
        private float itemWeight;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform muzzle;

        private const string SHOOTING_ANIM = "Shooting";
        
        private void Start()
        {
            thisAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetButton("Fire1") && thisAnimator.GetBool(SHOOTING_ANIM) == false)
            {
                Shoots();
                thisAnimator.SetBool(SHOOTING_ANIM, true);
                StartCoroutine(ReleaseShootingAfter());
            }

            horizontalSpeed = Input.GetAxis("Horizontal");
            thisAnimator.SetFloat("HSpeed", horizontalSpeed);
        }

        private WaitForSeconds halfSecond = new WaitForSeconds(0.5f);

        private IEnumerator ReleaseShootingAfter()
        {
            yield return halfSecond;
            this.thisAnimator.SetBool(SHOOTING_ANIM, false);

            //yield return new WaitForEndOfFrame(); // 프레임 끝날때까지 대기(프레임 끝나면 탈출)
            //yield return new WaitUntil(() => playerHP == 0); // hp가 0되면 탈출
            //yield return new WaitWhile(() => playerHP > 0); // hp가 0보다 큰 동안 대기
        }

        Vector3 prevPosition;
        Vector3 prevRotation;

        public void Pick(float objectWeight)
        {
            thisAnimator.SetTrigger("Picking");
            itemWeight = objectWeight;
            thisAnimator.SetFloat("Weighting", objectWeight);
        }

        public void Shoots()
        {
            Debug.Log("You Fired");
            GameObject theBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            theBullet.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 3000);   
        }
    }
}

