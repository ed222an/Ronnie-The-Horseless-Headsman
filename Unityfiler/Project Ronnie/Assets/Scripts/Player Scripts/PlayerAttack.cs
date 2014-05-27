using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	// Variables
	private bool canThrow;
	private Animator anim;
	
	void Start ()
	{
		canThrow = true;

		// Gets the animator on the player
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{	
		// Calls the attack function if mousebutton is pressed & released.
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
