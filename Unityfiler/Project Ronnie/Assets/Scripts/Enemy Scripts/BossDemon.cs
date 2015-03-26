using UnityEngine;
using System.Collections;

public class BossDemon : MonoBehaviour
{
	// Variables
	public float startingHealth;
	public float invincibleDuration;

	private int smallDemonAmount;

	public GameObject horsie;
	public GameObject smallDemon;
	
	public AudioClip[] enemyHurtClips;
	public AudioClip enemyDyingClip;
	public AudioClip bossSong;

	private bool alive;
	private bool isInvincible;
	private bool canSpawnSmallDemons;
	private float currentHealth;
	private GameObject horseSpawnPosition;
	private GameObject smallDemonSpawnPosition;
	private GameObject[] blockades;
	private GameObject blockadeOn;
	private SpriteRenderer[] bodyPartsArray;
	private Animator anim;
	private BossMeleeAI bossMeleeAI;
	private GameObject gameController;
	private AudioClip originalSong;
	
	void Start()
	{
		// Sets the current health to the starting value;
		currentHealth = startingHealth;
		isInvincible = false;
		alive = true;
		canSpawnSmallDemons = true;

		// Finds the gamecontroller & plays the boss song.
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		
		originalSong = gameController.GetComponent<AudioSource>().clip;
		gameController.GetComponent<AudioSource>().clip = bossSong;
		gameController.GetComponent<AudioSource>().Play ();
		
		// Finds the horses' spawn position.
		horseSpawnPosition = GameObject.FindGameObjectWithTag ("HorseSpawnPosition");

		// Find the smallDemonSpawnPosition.
		smallDemonSpawnPosition = GameObject.FindGameObjectWithTag ("DemonSpawnPosition");
		
		// Finds all blockade objects.
		blockades = GameObject.FindGameObjectsWithTag ("Blockade");
		blockadeOn = GameObject.FindGameObjectWithTag ("BlockadeOn");
		
		// Gets the bodypart sprites from the enemy parent object.
		bodyPartsArray = GetComponentsInChildren<SpriteRenderer>();
		anim = GetComponent<Animator> ();
		bossMeleeAI = GetComponent<BossMeleeAI> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(currentHealth < startingHealth)
		{
			ShadeByHealth ();
		}

		// At 5 damage, spawn 1 demon.
		if(currentHealth == startingHealth - 5)
		{
			smallDemonAmount = 1;

			if(canSpawnSmallDemons)
			{
				canSpawnSmallDemons = false;
				StartCoroutine(SpawnSmallDemons());
			}
		}

		// At 15 damage, spawn 2 demons.
		if(currentHealth == startingHealth - 15)
		{
			smallDemonAmount = 2;
			
			if(canSpawnSmallDemons)
			{
				canSpawnSmallDemons = false;
				StartCoroutine(SpawnSmallDemons());
			}
		}

		// At 5 damage, spawn 3 demons.
		if(currentHealth == startingHealth - 25)
		{
			smallDemonAmount = 3;
			
			if(canSpawnSmallDemons)
			{
				canSpawnSmallDemons = false;
				StartCoroutine(SpawnSmallDemons());
			}
		}
		
		if(currentHealth <= 0)
		{
			if(alive)
			{
				// Enemy is now dead.
				alive = false;

				// Call the Die function.
				StartCoroutine(Die());
			}
		}
	}

	// Spawns set amount of small demons at given location.
	IEnumerator SpawnSmallDemons()
	{
		// Spawns set amount of demons.
		for(int i = 0; i < smallDemonAmount; i++)
		{
			Instantiate (smallDemon, smallDemonSpawnPosition.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(0.2f);
		}

		yield return new WaitForSeconds (10.0f);

		// Can spawn another wave of demons after 10 seconds.
		canSpawnSmallDemons = true;
	}
	
	// Spawn a large wave of slimes, then die.
	IEnumerator Die()
	{
		// Turn of the enemy's collider.
		BoxCollider2D[] bossColliders = gameObject.GetComponents<BoxCollider2D>();
		foreach(BoxCollider2D bc in bossColliders)
		{
			bc.enabled = false;
		}

		// Turn off the AI-script.
		bossMeleeAI.enabled = false;

		//Rests the gameController's sound.
		gameController.GetComponent<AudioSource>().pitch = 1.0f;
		gameController.GetComponent<AudioSource>().volume = 0.5f;
		gameController.GetComponent<AudioSource>().clip = originalSong;
		gameController.GetComponent<AudioSource>().Play();

		// Play the damage sound.
		GetComponent<AudioSource>().volume = 1.0f;
		GetComponent<AudioSource>().PlayOneShot (enemyDyingClip);
		
		// Plays the dying animation.
		anim.Play("KingUrkel_Dying");

		// Waits for the soundclip to play.
		yield return new WaitForSeconds (enemyDyingClip.length);

		// Plays the dead animation.
		anim.SetBool("Dying", false);
		anim.SetBool ("Dead", true);
		
		// Spawns the horse at set position.
		Instantiate(horsie, horseSpawnPosition.transform.position, Quaternion.identity);
		
		foreach(GameObject blockade in blockades)
		{
			// Turn off the blockades.
			blockade.GetComponent<Renderer>().enabled = false;
			blockade.GetComponent<Collider2D>().enabled = false;
		}
		
		// Disables the blockade's On-switch.
		blockadeOn.GetComponent<Collider2D>().enabled = false;
	}
	
	// Shades the enemy sprites depending on how much health they have left.
	public void ShadeByHealth()
	{
		float difference = currentHealth / startingHealth;
		
		if(difference < 1 && difference > 0.5f)
		{
			for(int i = 0; i < bodyPartsArray.Length; i++)
			{
				bodyPartsArray[i].material.color = new Color32(193,193,193,255);
			}
		}
		
		if(difference <= 0.5f)
		{
			for(int i = 0; i < bodyPartsArray.Length; i++)
			{
				bodyPartsArray[i].material.color = new Color32(172,81,81,255);
			}
		}
	}
	
	// Modifies the enemy's current health.
	public void ModifyHealth(int amount)
	{
		currentHealth += amount;
	}
	
	// Decrements the enemys health by 1 if they weren't recently attacked.
	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		// If the enemy can take damage, call the ModifyHealth function & set invincible to true for a small amount of time.
		if (!isInvincible)
		{
			if(other.transform.tag == "Weapon")
			{
				// Variable for blinking time.
				float startBlinking = Time.time;

				// Play a random hurt sound if no one is currently playing.
				GetComponent<AudioSource>().volume = 1.0f;
				if(!GetComponent<AudioSource>().isPlaying)
				{
					GetComponent<AudioSource>().PlayOneShot(enemyHurtClips[Random.Range(0, enemyHurtClips.Length)]);
				}
				
				// Decrement the enemy's health by 1
				ModifyHealth(-1);
				
				// Make the player invincible
				isInvincible = true;
				
				// Blinks the player for as long as he is invincible.
				while ((Time.time - startBlinking) < invincibleDuration)
				{
					//Blink each bodypart for 2 Seconds
					for(int i = 0; i < bodyPartsArray.Length; i++)
					{
						bodyPartsArray[i].enabled = false;
					}
					yield return new WaitForSeconds(0.1f);
					
					// Bodyparts invisible for some time
					for(int i = 0; i < bodyPartsArray.Length; i++)
					{
						bodyPartsArray[i].enabled = true;
					}
					yield return new WaitForSeconds(0.1f);
				}
				
				// Player no longer invincible.
				isInvincible = false;
			}
		}
	}
}


