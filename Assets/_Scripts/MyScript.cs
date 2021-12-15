using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class MyScript : MonoBehaviour
    {
        private void Awake()
        {

        }

        private void OnEnable()
        {

        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        private void FixedUpdate()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(Vector3.up, 10f * Time.deltaTime);
        }

        private void LateUpdate()
        {

        }

        private void OnDisable()
        {

        }

        private void OnTriggerEnter(Collider other) // XXX : Enter, Stay, Exit
        {

        }

        private void OnCollisionEnter(Collision collision) // XXX : Enter, Stay, Exit
        {

        }

        private void OnDestroy()
        {

        }

        private void OnApplicationQuit()
        {

        }
    }
}