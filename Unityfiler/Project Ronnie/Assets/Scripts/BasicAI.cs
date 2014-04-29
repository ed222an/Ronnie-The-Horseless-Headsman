using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour
{
	// Variables
	public float moveSpeed;
	public float maxDist;
	public float minDist;
	public float turnSpeed = 20;

	private Vector3 moveDirection;
	private Vector3 currentDirection;
	private Vector3 playerPosition;
	private Vector3 currentPosition;

	private Transform player;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
		moveDirection = Vector3.right;
	}

	void Awake()
	{
		player = GameObject.FindWithTag ("Player").transform;
	}

	void Update()
	{
		// Gets current position of the enemy.
		Vector3 currentPosition = transform.position;

		// If the enemy is farther away from the player than the maximum distance allows, move the enemy in the players direction...
		if (Vector3.Distance(player.position, transform.position) > maxDist)
		{
			// Sets a direction to move towards.
			Vector3 moveToward = player.position;
			// 4
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();

			// Walking.
			Vector3 target = moveDirection * moveSpeed + currentPosition;
			transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );

			// Rotates toward the player
			float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = 
				Quaternion.Slerp( transform.rotation, 
				                 Quaternion.Euler( 0, 0, targetAngle ), 
				                 turnSpeed * Time.deltaTime );
		}
		else //... rotate the enemy facing the player.
		{
			Vector3 dir = player.position - transform.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
}
