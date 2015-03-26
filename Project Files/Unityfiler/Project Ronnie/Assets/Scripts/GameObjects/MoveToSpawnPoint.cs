using UnityEngine;
using System.Collections;

public class MoveToSpawnPoint : MonoBehaviour
{
	private GameObject spawnPoint;

	void Update()
	{
		spawnPoint = GameObject.FindGameObjectWithTag ("SpawnPoint");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.transform.position = spawnPoint.transform.position;
		}
	}
}
