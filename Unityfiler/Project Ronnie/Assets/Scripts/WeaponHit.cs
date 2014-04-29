using UnityEngine;
using System.Collections;

public class WeaponHit : MonoBehaviour {
	
	// Destroys objects on collision with the weapon if the space-key is pressed.
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Weapon")
		{
			DestroyObject(transform.gameObject);
		}
	}
}
