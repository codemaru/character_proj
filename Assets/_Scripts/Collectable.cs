using UnityEngine;
using System.Collections;
using Zombie;
using System;

public class Collectable : MonoBehaviour 
{
	public AudioClip sound;

	public int increase; // 아이템 획득시 추가되는 체력의 양
	public GameObject playerObj;
	public GameObject pickingCamera;
	public Transform playerCamera;
	public Transform headBone; // 픽업 카메라의 부모
	public bool triggered = false;
	[SerializeField]
	private float objectWeight = 0.25f;


	void OnTriggerEnter (Collider other)
	{
		if (this.sound)
		{
			AudioSource.PlayClipAtPoint(this.sound, transform.position);
		}
		
		if (!triggered) //  triggered == false
		{
			triggered = true;

			var playerState = playerObj.GetComponent<PlayerStatus>();
			var playerAnim = playerObj.GetComponent<MyFPSAnimation>();

			playerState.AddHealth(increase);
			playerAnim.Pick(objectWeight);

			StartCoroutine(RunCam());
        }
	}

    private IEnumerator RunCam()
    {
		var pickTrans = transform;
		var addCamObj = 
			Instantiate(pickingCamera, headBone.position, playerCamera.rotation);
		addCamObj.transform.parent = headBone;		
		addCamObj.transform.GetChild(0).SendMessage("SentTarget", pickTrans); //
		yield return new WaitForSeconds(0.5f);
		Remove();
    }

    private void Remove()
    {
		Debug.Log("You got it");
		triggered = false;
		Destroy(gameObject);
    }
}
