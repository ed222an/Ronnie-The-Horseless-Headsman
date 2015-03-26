using UnityEngine;
using System.Collections;

public class SpawnAndDestroy : MonoBehaviour
{
	// Variables
	public GameObject healthPotion;
	public GameObject enemy;
	public GameObject shatteredVersion;

	private int randomNumber;
	private bool isBreaking;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
		isBreaking = false;
	}

	void Update()
	{
		randomNumber = Random.Range (1, 11);
	}

	// Destroys objects on collision with the weapon if the space-key is pressed.
	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if(!isBreaking)
		{
			if(other.transform.tag == "Weapon" || other.transform.tag == "EnemyWeapon")
			{
				// Object is breaking.
				isBreaking = true;

				// Spawn a health potion if conditions are met.
				if(randomNumber == 1)
				{
					Instantiate(healthPotion, transform.position, Quaternion.identity);
				}

				// Spawn an enemy if conditions are met.
				if(randomNumber == 2)
				{
					Instantiate(enemy, transform.position, Quaternion.identity);
				}

				// Plays the shatter animation.
				anim.SetBool("Shatter", true);

				// Plays the breaking sound.
				GetComponent<AudioSource>().Play();

				// Waits for the animation to finish.
				yield return new WaitForSeconds(0.2f);

				// Spawns a shattered version of the object.
				Instantiate(shatteredVersion, transform.position, Quaternion.identity);

				// Destroy the container.
				DestroyObject(transform.gameObject);
			}
		}
	}
}
