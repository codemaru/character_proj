using System;
using UnityEngine;
using System.Collections;
using Zombie;
using Random = UnityEngine.Random;

public class zombie_chapter9_Start : MonoBehaviour 
{
	public Animator theAnimator;
	public CharacterController charControl;
	public float walkSpeed  = 2f;
	
	public Transform target;
	public bool alerted = false;
	public AudioClip snarlSound;
	public bool soundReady = true;
	
	public float turnSpeed  = 60f;
	public bool turning;
	public float angle;

	private float distance;
	private float awareRange = 4.0f;

	[SerializeField]
	private Transform[] patrolPts;
	private int currPt;
	private Transform targetedPt;
	private float ptDistance;
	private float changeDistance = 0.5f;
	private float turnTime = 5.0f;
	private bool moving = true;
	private float speed;
	private float alertDistance = 3.0f;
	private float attackRange = 1.5f;
	private bool attacking = false;
	private float attackDuration = 2.0f;
	private float attackTimer;
	private int damage = 2;

	private int zombieHealth = 5;
	[SerializeField] private Transform deadPrefab;

	private const string IS_RUNNING_ANIMPARAM = "IsTurning";
	private const string SPEED_ANIMPARAM = "Speed";
	private const string IS_WALKING_ANIMPARAM = "IsWalking";
	private const string IS_SNARING_ANIMPARAM = "IsSnarling";
	private const string FORWARD_MOVEMENT_ANIMPARAM = "ForwardMovement";
	private const string IS_ATTACKING_ANIMPARAM = "IsAttacking";
	
	void Start ()
	{
		target = GameObject.FindWithTag("Player").transform;
		
		theAnimator = GetComponent<Animator>();
		charControl = GetComponent<CharacterController>();

		Invoke ("Walks", Random.Range (3f, 5f));
	}

	void Update ()
	{
		var speedFactor = theAnimator.GetFloat(FORWARD_MOVEMENT_ANIMPARAM);
		speed = walkSpeed * speedFactor;
		
		targetedPt = patrolPts[currPt];
		var patrolPtDistance = Vector3.Distance(targetedPt.position, transform.position);
		var playerDistance = Vector3.Distance(target.position, transform.position);
		if (playerDistance <= alertDistance)
		{
			alerted = true;
		}

		theAnimator.SetBool (IS_RUNNING_ANIMPARAM, turning);

		/*
		if(Input.GetButton("Fire1") && alerted == false)
		{
			alerted = true;
		}
		*/

		distance = Vector3.Distance(target.position, transform.position);
		if (distance <= awareRange)
		{
			alerted = true;
		}
		
		if(alerted)
		{
			targetedPt = target;
			
			//TurnToPlayer();
			TurnToPoint();
			if (playerDistance <= attackRange && !attacking)
			{
				Attack();
			}

			if (attacking)
			{
				attackTimer -= Time.deltaTime;
				if (attackTimer <= 0f)
				{
					attacking = false;
				}
			}
			
			if(angle > 5f ||angle < -5f)
			{
				turning = true;
				theAnimator.SetFloat(SPEED_ANIMPARAM,0f);
			}
			else if(angle < 5f && angle > -5f)
			{
				if(soundReady)
				{
					Snarl();
				}
				else
				{
					turning = false;
					WalkTowards();
				}
			}
		}
		else
		{
			if (patrolPtDistance <= changeDistance)
			{
				ChangePt();
				turning = true;
			}
		}
		if (turning)
		{
			TurnToPoint();
			if (angle < 2 && angle > -2)
			{
				WalkTowards();
			}
		}
	}

	private void Attack()
	{
		theAnimator.SetTrigger(IS_ATTACKING_ANIMPARAM);
		attackTimer = attackDuration;
		attacking = true;

		/*
		var playerStatus = target.Find("FirstPersonCharacter/player_m").GetComponent<PlayerStatus>();
		if (playerStatus) playerStatus.AddDamage(damage);		
		*/
		
		var playerStatus = target.Find("FirstPersonCharacter/player_m");
		playerStatus.SendMessage("AddDamage", damage);
		
	}

	void ChangePt()
	{
		currPt++;
		if (currPt > patrolPts.Length - 1)
		{
			currPt = 0;
		}
	}

	void Walks()
	{
		theAnimator.SetBool(IS_WALKING_ANIMPARAM,true);
	}

	//void TurnToPlayer()
	private void TurnToPoint()
	{
		theAnimator.SetBool(IS_WALKING_ANIMPARAM,false);
		//Vector3 localRotate = transform.InverseTransformPoint(target.position);
		var localRotate = transform.InverseTransformPoint(targetedPt.position);
		angle = Mathf.Atan2 (localRotate.x, localRotate.z) * Mathf.Rad2Deg;
		float maxRotation = turnSpeed * Time.deltaTime;
		float turnAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);
		transform.Rotate(0, turnAngle, 0);
	}

	void WalkTowards()
	{
		turning = false;
		//Vector3 direction = transform.TransformDirection(Vector3.forward * 	walkSpeed);
		var direction = transform.TransformDirection(Vector3.forward * speed);
		charControl.SimpleMove(direction);
		theAnimator.SetBool(IS_WALKING_ANIMPARAM, true);
	}

	void Snarl()
	{
		GetComponent<AudioSource>().PlayOneShot(snarlSound);
		
		theAnimator.SetTrigger(IS_SNARING_ANIMPARAM);
		soundReady = false;
	}

	void AddDamage(int damage)
	{
		zombieHealth -= damage;
		if (zombieHealth <= 0)
		{
			zombieHealth = 0;
			Dead();
		}
	}

	private void Dead()
	{
		Destroy(this.gameObject);
		if (deadPrefab)
		{
			Transform dead = Instantiate(deadPrefab,
				transform.position, transform.rotation);
			CopyTransformRecurse(transform, dead);
		}
	}

	private void CopyTransformRecurse(Transform src, Transform dst)
	{
		dst.position = src.position;
		dst.rotation = src.rotation;
		foreach (Transform child in dst)
		{
			var curSrc = src.Find(child.name);
			if (curSrc)
				CopyTransformRecurse(curSrc, child);
		}
	}
}
