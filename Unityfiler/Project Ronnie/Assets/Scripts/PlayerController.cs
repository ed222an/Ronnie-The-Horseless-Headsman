using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Player variables
	public float speed;
	public float rotationSpeed;

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
	}

	// For physics stuff
	void FixedUpdate()
	{
		// Enables movement
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float totalSpeed = moveVertical + moveHorizontal;

		// Sets the animators speed parameter to match the movement.
		anim.SetFloat ("Speed", Mathf.Abs (totalSpeed));

		// Sets the move velocity.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * speed;

		// Enables rotation with the Q and E key.
		if(Input.GetKey(KeyCode.Q)) {
			// Clockwise
			transform.Rotate(0, 0, -rotationSpeed);
		}

		if(Input.GetKey(KeyCode.E)) {
			// Counter-clockwise
			transform.Rotate(0, 0, rotationSpeed);
		}
	}
}
