using UnityEngine;
using System.Collections;

public class ControlOrb : MonoBehaviour
{
	// Variables.
	public GameObject doorBlockade;
	public GameObject shatteredVersion;

	private bool isBreaking;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
		isBreaking = false;
	}

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if(!isBreaking)
		{
			// If the player's weapon hits the orb...
			if(other.transform.tag == "Weapon")
			{
				// Orb is breaking.
				isBreaking = true;

				// Plays the shatter animation.
				anim.SetTrigger("Shattering");
				
				// Plays the breaking sound.
				GetComponent<AudioSource>().Play();
				
				// Waits for the animation to finish.
				yield return new WaitForSeconds(0.2f);

				Instantiate(shatteredVersion, transform.position, Quaternion.identity);

				// Destroy the associated doorBlockade.
				Destroy(doorBlockade.gameObject);

				// Destroy the control orb.
				Destroy(gameObject);
			}
		}
	}
}
