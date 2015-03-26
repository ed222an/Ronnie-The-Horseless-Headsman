using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	// Variables
	public GameObject spawnObject;
	public GameObject spawnPosition;
	public int numberOfSpawns;
	public float spawnDelay;
	
	private bool canSpawn;

	void Start()
	{
		canSpawn = true;
	}

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if(canSpawn)
		{
			// If player collides with the spawner...
			if (other.transform.tag == "Player")
			{
				// Can't spawn again.
				canSpawn = false;

				// Spawn as many objects as numberOfSpawns.
				for(int i = 0; i < numberOfSpawns; i++)
				{
					Instantiate(spawnObject, spawnPosition.transform.position, spawnPosition.transform.rotation);
					yield return new WaitForSeconds(0.1f);
				}

				// Waits for the spawn delay to take effect.
				yield return new WaitForSeconds(spawnDelay);

				// Can spawn again.
				canSpawn = true;
			}

		}
	}
}
