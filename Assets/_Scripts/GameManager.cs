using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(1024, 768, false);
        }
    }
}
