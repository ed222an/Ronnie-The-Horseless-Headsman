using UnityEngine;
using System.Collections;

public class BossDemon : MonoBehaviour
{
	// Variables
	public float startingHealth;
	public float invincibleDuration;

	public GameObject horsie;
	
	public AudioClip[] enemyHurtClips;
	public AudioClip enemyDyingClip;

	private bool alive;
	private bool isInvincible;
	private float currentHealth;
	private GameObject horseSpawnPosition;
	private GameObject[] blockades;
	private GameObject blockadeOn;
	private SpriteRenderer[] bodyPartsArray;
	private Animator anim;
	private BossMeleeAI bossMeleeAI;
	private GameObject gameController;
	
	void Start()
	{
		// Sets the current health to the starting value;
		currentHealth = startingHealth;
		isInvincible = false;
		alive = true;

		// Finds the gamecontroller & lowers the pitch its audio.
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameController.audio.pitch = 0.8f;
		
		// Finds the horses' spawn position.
		horseSpawnPosition = GameObject.FindGameObjectWithTag ("HorseSpawnPosition");
		
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
	
	// Spawn a large wave of slimes, then die.
	IEnumerator Die()
	{
		// Turn of the enemy's collider & AI-script.
		gameObject.collider2D.enabled = false;
		bossMeleeAI.enabled = false;

		// Turns the audio pitch back up.
		gameController.audio.pitch = 1.0f;

		// Play the damage sound.
		audio.volume = 1.0f;
		audio.PlayOneShot (enemyDyingClip);

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
			blockade.renderer.enabled = false;
			blockade.collider2D.enabled = false;
		}
		
		// Disables the blockade's On-switch.
		blockadeOn.collider2D.enabled = false;
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
				audio.volume = 1.0f;
				if(!audio.isPlaying)
				{
					audio.PlayOneShot(enemyHurtClips[Random.Range(0, enemyHurtClips.Length)]);
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


