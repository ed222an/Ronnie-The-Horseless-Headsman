using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	// Variables
	private Animator anim;
	
	void Start ()
	{
		// Gets the animator on the player
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		// Enables attack when key is held down & attack animations
		if(Input.GetMouseButtonDown(0))
		{
			anim.SetBool ("Attack", true);
		}
		// Resets the attack animation.
		if(Input.GetMouseButtonUp(0))
		{
			anim.SetBool ("Attack", false);
		}
	}
}
