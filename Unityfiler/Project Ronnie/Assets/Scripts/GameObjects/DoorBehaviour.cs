using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour
{
	// Variables.
	public AudioClip damage1Clip;
	public AudioClip damage2Clip;
	public AudioClip damage3Clip;
	public AudioClip damage4Clip;
	public AudioClip breakingClip;

	private int counter;
	private bool isInvincible;
	private Animator anim;
	
	void Start ()
	{
		counter = 0;
		isInvincible = false;
		anim = GetComponent<Animator> ();
	}

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		// If not invincible...
		if(!isInvincible)
		{
			// and the colliding object is the players weapon...
			if(other.transform.tag == "Weapon")
			{
				// become invincible.
				isInvincible = true;

				// Increment counter.
				counter++;

				if(counter == 1)
				{
					anim.SetBool("Damage1", true);
					GetComponent<AudioSource>().PlayOneShot(damage1Clip);
				}
				
				if(counter == 2)
				{
					anim.SetBool("Damage2", true);
					GetComponent<AudioSource>().PlayOneShot(damage2Clip);
				}
				
				if(counter == 3)
				{
					anim.SetBool("Damage3", true);
					GetComponent<AudioSource>().PlayOneShot(damage3Clip);
				}
				
				if(counter == 4)
				{
					anim.SetBool("Damage4", true);
					GetComponent<AudioSource>().PlayOneShot(damage4Clip);
				}
				
				if(counter == 5)
				{
					anim.SetBool("Breaking", true);
					GetComponent<AudioSource>().PlayOneShot(breakingClip);
					transform.GetComponent<Collider2D>().enabled = false;
				}

				// wait for some time.
				yield return new WaitForSeconds(0.2f);

				// become vulnerable again.
				isInvincible = false;
			}
		}
	}
}
