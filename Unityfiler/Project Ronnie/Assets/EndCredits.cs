using UnityEngine;
using System.Collections;

public class EndCredits : MonoBehaviour
{
	// Variables.
	public GameObject camera;
	public GameObject spawnPoint;
	public float speed;

	private GameObject[] enemies;

	void Start()
	{
		// Finds all monsters.
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}

	void Update()
	{
		// Checks how many monsters there are left alive.
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		// Moves the camera and spawnpoint down.
		spawnPoint.transform.Translate (Vector3.down * Time.deltaTime * speed);
		camera.transform.Translate (Vector3.down * Time.deltaTime * speed);

		// Loads the startscreen when all monsters are killed.
		if(enemies.Length <= 0)
		{
			StartCoroutine(RestartGame());
		}
	}

	// Restarts the game.
	IEnumerator RestartGame()
	{
		yield return new WaitForSeconds (5);
		Application.LoadLevel(0);
	}
}
