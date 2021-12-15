using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class PlayerStatus : MonoBehaviour
    {
        private int health = 10;
        private int healthLimit = 10;

        public void AddHealth(int increase)
        {
            health += increase;
            if (health > healthLimit)
            {
                health = healthLimit;
            }
        }

        public void AddDamage(int damage)
        {
            Debug.Log($"{this.gameObject.name} : get damage = {damage}");
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("You Died");
        }
    }
}