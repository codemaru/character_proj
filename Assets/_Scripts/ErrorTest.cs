using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorTest : MonoBehaviour
{
    private void Start()
    {
        ErrorFunc();
    }

    private void ErrorFunc()
    {
        ErrorFunc2();
    }

    private void ErrorFunc2()
    {
        throw new NotImplementedException();
    }
}
