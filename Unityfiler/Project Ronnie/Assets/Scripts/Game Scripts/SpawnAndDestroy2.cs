using UnityEngine;
using System.Collections;

public class SpawnAndDestroy2 : MonoBehaviour
{
	// Variables
	public GameObject healthPotion;
	public GameObject slime;
	public GameObject skeleton;
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
				if(randomNumber == 1 || randomNumber == 2 || randomNumber == 3 || randomNumber == 4)
				{
					Instantiate(healthPotion, transform.position, Quaternion.identity);
				}
				
				// Spawn a slime if conditions are met.
				if(randomNumber == 5 || randomNumber == 6)
				{
					Instantiate(slime, transform.position, Quaternion.identity);
				}

				// Spawn a skeleton if conditions are met.
				if(randomNumber == 7 || randomNumber == 8)
				{
					Instantiate(skeleton, transform.position, Quaternion.identity);
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
