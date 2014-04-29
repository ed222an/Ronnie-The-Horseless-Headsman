using UnityEngine;
using System.Collections;

public class HorseBehaviour : MonoBehaviour
{
	// Variables
	private Animator anim;
	
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
}
