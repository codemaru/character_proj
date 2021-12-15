using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class LunchBox : MonoBehaviour
    {
        [SerializeField] 
        private AudioClip sound;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);

            if (other.gameObject.CompareTag("Player"))
            {
                if (sound)
                {
                    AudioSource.PlayClipAtPoint(sound, transform.position);
                }

                Remove();
            }      
        }

         private void Remove()
        {
            Destroy(gameObject);
        }
    }
}
