using UnityEngine;
using System.Collections;

public class BossSkeleton : MonoBehaviour
{
	// Variables
	public float startingHealth;
	public float invincibleDuration;

	public GameObject horsie;

	public AudioClip enemyHit;
	public AudioClip bossSong;
	
	private float currentHealth;
	private bool isInvincible;
	private bool alive;
	private GameObject horseSpawnPosition;
	private GameObject[] blockades;
	private GameObject blockadeOn;
	private GameObject gameController;
	private SpriteRenderer[] bodyPartsArray;
	private AudioClip originalSong;

	void Start()
	{
		// Sets the current health to the starting value;
		currentHealth = startingHealth;
		isInvincible = false;
		alive = true;

		// Finds the horses' spawn position.
		horseSpawnPosition = GameObject.FindGameObjectWithTag ("HorseSpawnPosition");

		// Finds the game controller and changes the audio to the boss themesong.
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		originalSong = gameController.audio.clip;
		gameController.audio.volume = 0.25f;
		gameController.audio.clip = bossSong;
		gameController.audio.Play ();

		// Finds all blockade objects.
		blockades = GameObject.FindGameObjectsWithTag ("Blockade");
		blockadeOn = GameObject.FindGameObjectWithTag ("BlockadeOn");
		
		// Gets the bodypart sprites from the enemy parent object.
		bodyPartsArray = GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(currentHealth < startingHealth)
		{
			ShadeByHealth ();
		}
		
		if(currentHealth <= 0)
		{
			if(alive)
			{
				// Enemy is now dead.
				alive = false;
				
				// Call the kill function.
				StartCoroutine(KillEnemy());
			}
		}
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
	
	IEnumerator KillEnemy()
	{
		// Turn of the enemy's collider.
		gameObject.collider2D.enabled = false;
		
		// Play the damage sound.
		audio.volume = 1.0f;
		audio.PlayOneShot(enemyHit);
		yield return new WaitForSeconds (enemyHit.length);

		// Spawns the horse at set position.
		Instantiate(horsie, horseSpawnPosition.transform.position, Quaternion.identity);

		foreach(GameObject blockade in blockades)
		{
			// Turn off the blockades.
			blockade.renderer.enabled = false;
			blockade.collider2D.enabled = false;
		}

		// Disables the blockade's On-switch.
		blockadeOn.collider2D.enabled = false;

		//Rests the gameController's sound.
		gameController.audio.volume = 0.5f;
		gameController.audio.clip = originalSong;
		gameController.audio.Play();
		
		// Destroys the enemy.
		DestroyObject(transform.gameObject);
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
				
				// Play the damage sound.
				audio.volume = 1.0f;
				audio.PlayOneShot(enemyHit);
				
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

