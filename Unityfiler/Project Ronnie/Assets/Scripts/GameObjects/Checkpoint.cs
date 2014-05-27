using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	// Variables.
	private GameObject spawnPoint;
	
	void Start ()
	{
		// Finds the spawnpoint.
		spawnPoint = GameObject.FindGameObjectWithTag ("SpawnPoint");
	}

	void Update()
	{
		// Finds the spawnpoint.
		spawnPoint = GameObject.FindGameObjectWithTag ("SpawnPoint");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// If the player enters the checkpoint...
		if(other.tag == "Player")
		{
			// ...move the spawnpoint to the checkpoint's position...
			spawnPoint.transform.position = transform.position;

			//...and destroy the checkpoint.
			Destroy (gameObject);
		}
	}
}
