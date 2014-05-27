using UnityEngine;
using System.Collections;

public class BossDoor : MonoBehaviour
{
	// Variables
	private int controlOrbAmount;

	void Update ()
	{
		controlOrbAmount = GameObject.FindGameObjectsWithTag ("ControlOrb").Length;

		// If there are no control orbs left...
		if(controlOrbAmount <= 0)
		{
			// Destroy the boss door.
			Destroy(gameObject);
		}
	}
}
