using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	// Variables
	public float speed;
	public float destroyTimer;
	
	private Vector3 moveDirection;
	private Vector3 currentDirection;
	private Vector3 playerPosition;
	private Vector3 currentPosition;

	private Transform player;
	
	void Awake()
	{
		// Gets the player object and his position.
		player = GameObject.FindWithTag ("Player").transform;
		playerPosition = player.position;

		// Destroys the projectile after set amount of time.
		StartCoroutine(WaitAndDestroy ());
	}
	
	void Update()
	{
		// Gets current position of the enemy.
		Vector3 currentPosition = transform.position;

		// Sets a direction to move towards.	
		moveDirection = playerPosition - currentPosition;
		moveDirection.z = 0;
		moveDirection.Normalize();
		
		// Fly towards set direction.
		Vector3 target = moveDirection * speed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
	}

	IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds (destroyTimer);

		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.tag == "Player" || other.transform.tag == "Wall")
		{
			Destroy (gameObject);
		}
	}
}
