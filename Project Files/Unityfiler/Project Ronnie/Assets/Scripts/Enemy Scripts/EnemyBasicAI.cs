using UnityEngine;
using System.Collections;

public class EnemyBasicAI : MonoBehaviour
{
	// Variables
	public float moveSpeed;
	public float maxDist;
	public float minDist;
	public float turnSpeed;
	
	private Vector3 moveDirection;
	private Vector3 currentDirection;
	private Vector3 playerPosition;
	private Vector3 currentPosition;
	
	private Transform player;
	
	void Start()
	{
		moveDirection = Vector3.right;
	}
	
	void Update()
	{
		// Gets current position of the enemy.
		Vector3 currentPosition = transform.position;

		// Gets the transform of the player object.
		player = GameObject.FindWithTag ("Player").transform;
		
		// If the enemy is farther away from the player than the maximum distance allows, move the enemy in the players direction...
		if (Vector3.Distance(player.position, currentPosition) > maxDist)
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
