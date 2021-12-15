using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private float life = 1.5f;

    public Transform origTarget;
    private Transform pickTarget;
    public float damping = 5.0f;
    public Vector3 camOffset;

    public void SentTarget(Transform pickTrans)
    {
        StartCoroutine(BeginSentTarget(pickTrans));
    }

    private IEnumerator BeginSentTarget(Transform pickTrans)
    {
        //transform.LookAt(pickTrans);
        //yield return new WaitForSeconds(life);
        //ReturnTarget();

        pickTarget = pickTrans;

        var lookAtPosition = pickTarget.position;
        lookAtPosition.y = transform.position.y;
        lookAtPosition.z = transform.position.z;
        var rotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        
        yield return new WaitForSeconds(life);
        ReturnTarget();
    }

    private void ReturnTarget()
    {
        var origPosition = origTarget.position;
        origPosition.y = transform.position.y;
        origPosition.z = transform.position.z;
        var rotation = Quaternion.LookRotation(origPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        Destroy(transform.parent.gameObject);
    }
}
