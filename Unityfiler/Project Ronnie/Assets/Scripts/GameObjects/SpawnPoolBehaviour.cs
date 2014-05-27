using UnityEngine;
using System.Collections;

public class SpawnPoolBehaviour : MonoBehaviour
{
	// Variables.
	private bool isActive;
	private Animator anim;
	
	void Start ()
	{
		isActive = false;
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!isActive)
		{
			if(other.transform.tag == "Player")
			{
				// Activates the spawnpool animation.
				isActive = true;
				anim.SetBool("Activated", true);
			}
		}
	}
}
