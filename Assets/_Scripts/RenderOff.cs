using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOff : MonoBehaviour
{
    private void Awake()
    {
        var rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
}
