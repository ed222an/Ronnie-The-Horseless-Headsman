using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	// Variables
	public int startingHealth;
	private int currentHealth;

	void Awake()
	{
		// Sets the current health to the starting value;
		currentHealth = startingHealth;
	}

	// Update is called once per frame
	void Update ()
	{
		Debug.Log (currentHealth);
		if(currentHealth <= 0)
		{
			DestroyObject(transform.gameObject);
		}
	}

	public void ModifyHealth(int amount)
	{
		currentHealth += amount;
	}

	// Destroys objects on collision with the weapon if the space-key is pressed.
	void OnTriggerEnter2D(Collider2D other) // FIXA DETTA, Kanske en timer?
	{
		if(other.gameObject.tag == "Weapon")
		{
			ModifyHealth(-1);
		}
	}
}
