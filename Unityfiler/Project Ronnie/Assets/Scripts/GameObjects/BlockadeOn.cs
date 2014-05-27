using UnityEngine;
using System.Collections;

public class BlockadeOn : MonoBehaviour
{
	// Variables.
	private GameObject[] blockades;
	
	void Start ()
	{
		// Finds all blockade objects.
		blockades = GameObject.FindGameObjectsWithTag ("Blockade");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.tag == "Player")
		{
			foreach(GameObject blockade in blockades)
			{
				// Turns on the blockades.
				blockade.renderer.enabled = true;
				blockade.collider2D.enabled = true;
			}
		}
	}
}
