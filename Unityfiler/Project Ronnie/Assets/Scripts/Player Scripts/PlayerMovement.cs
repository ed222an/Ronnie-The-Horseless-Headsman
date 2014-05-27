using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	// Player variables
	public float speed;

	// Animator variable
	private Animator anim;

	void Start()
	{
		// Gets the animator on the player
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

		// Makes the player face the mouse position.
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.0f; 															// The distance from the camera to the player object
		Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
		lookPos = lookPos - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
	}
}
