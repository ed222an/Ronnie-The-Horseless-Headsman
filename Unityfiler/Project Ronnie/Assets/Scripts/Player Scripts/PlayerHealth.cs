using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	// Public variables
	public int startingHealth;					// How much health the player starts with.
	public int currentHealth;					// How much health the player has left.
	public int maxHealth;						// The maximum health value.
	public int healthPerHeart;					// How much health a heart is worth.
	public float invincibleDuration;			// Invincibility duration.

	public int maxHeartsPerRow;					// Heart spacing.
	public GUITexture heartGUI;					// Reference to the heart texture.
	public Texture[] heartImages;				// Imagearray with the heart textures.
	public SpriteRenderer bloodPool;			// Image for the players blood.
	
	public float resetAfterDeathTime;			// How much time from the player dying to the level resetting.
	public AudioClip deathClip;					// The sound effect of the player dying.
	public AudioClip damagedClip;				// The sound of taking damage.
	public AudioClip healingClip;				// The sound of healing.
	
	// Private variables
	private bool isInvincible;					// Player is invincible while true.
	private bool bloodSpawn;					// Shows the bloodpool if true.

	private GameObject spawnPoint;				// The spawnpoint, used for respawning.
	
	private SpriteRenderer[] bodyPartsArray;	// SpriteRenderer-array containing the players bodyparts.
	
	private ArrayList hearts;					// How many hearts the player has.
	private float spacingX;						// Heart spacing.
	private float spacingY;						// Heart spacing.
	
	private Animator anim;						// Reference to the animator component.
	private PlayerMovement playerMovement;		// Reference to the PlayerMovement script.
	private PlayerAttack playerAttack;			// Reference to the PlayerAttack script.
	private float deadTimer;					// A timer for counting to the reset of the level once the player is dead.
	private bool playerDead;					// A bool to show if the player is dead or not.
	
	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		playerAttack = GetComponent<PlayerAttack> ();
		spawnPoint = GameObject.FindGameObjectWithTag ("SpawnPoint");
	}
	
	void Start()
	{
		// Sets the players health to the starting value.
		hearts = new ArrayList();
		currentHealth = startingHealth;
		isInvincible = false;
		playerDead = false;
		bloodSpawn = true;
		
		// Gets the bodypart sprites from the player parent object.
		bodyPartsArray = GetComponentsInChildren<SpriteRenderer>();
		
		// Sets the spacing between the hearts.
		spacingX = heartGUI.pixelInset.width;
		spacingY = -heartGUI.pixelInset.height;
		
		// Adds a heart per health.
		AddHearts (startingHealth / healthPerHeart);
	}
	
	void Update()
	{
		// If the current health is less than or equal to 0...
		if(currentHealth <= 0)
		{
			// Call the PlayerDead function, followed by the ResetPlayer function.
			PlayerDead();
			ResetPlayer();
		}
	}
	
	void PlayerDead()
	{
		// The player is now dead.
		playerDead = true;
		
		// Set the animator's dead parameter to true also.
		anim.Play("Ronnie_Dying");
		
		if(bloodSpawn)
		{
			Instantiate (bloodPool, transform.position, transform.rotation);
			bloodSpawn = false;
		}
		
		// Disable the move & attack scripts.
		anim.SetFloat ("Speed", 0f);
		playerMovement.enabled = false;
		playerAttack.enabled = false;
	}
	
	void ResetPlayer()
	{
		// Increase the timer.
		deadTimer += Time.deltaTime;

		if(deadTimer >= 2)
		{
			// Moves the player to the spawnpoint.
			transform.position = spawnPoint.transform.position;
		}

		//If the timer is greater than or equal to the time before the level resets...
		if(deadTimer >= resetAfterDeathTime)
		{
			// Finds the current location of the spawnpoint.
			spawnPoint = GameObject.FindGameObjectWithTag ("SpawnPoint");
			
			// The player is alive.
			playerDead = false;
			
			// Player can bleed again.
			bloodSpawn = true;
			
			ModifyHealth(startingHealth);
			
			// Starts the idle animation.
			anim.SetBool("Dead", false);
			anim.SetBool("Attack", false);
			
			// Enables the move & attack scripts.
			playerMovement.enabled = true;
			playerAttack.enabled = true;
			
			deadTimer = 0;
		}
	}
	
	// Checks for collisions with object with certain tags.
	IEnumerator OnCollisionEnter2D(Collision2D collision)
	{
		// If the player can take damage, call the ModifyHealth function & set invincible to true for a small amount of time.
		if (!isInvincible && playerDead == false)
		{
			if(collision.transform.tag == "Enemy")
			{
				// Decrement the player's health by 1
				ModifyHealth(-1);
				
				// Make the player invincible
				isInvincible = true;
				
				// Calls the method for flashing.
				StartCoroutine(Flashing(true));
				yield return new WaitForSeconds(invincibleDuration);
				
				// Player no longer invincible.
				isInvincible = false;
			}
		}
	}
	
	IEnumerator OnTriggerEnter2D (Collider2D other)
	{
		if(other.transform.tag == "Health")
		{
			if(currentHealth != maxHealth)
			{
				// Healthpotion is consumed.
				DestroyObject(other.gameObject);
				
				// Increment health by one.
				ModifyHealth(1);
				
				// Calls the method for flashing.
				StartCoroutine(Flashing(false));
			}
		}
		
		if(other.transform.tag == "EnemyWeapon" && !isInvincible && playerDead == false)
		{
			// Decrement the player's health by 1
			ModifyHealth(-1);
			
			// Make the player invincible
			isInvincible = true;
			
			// Calls the method for flashing.
			StartCoroutine(Flashing(true));
			yield return new WaitForSeconds(invincibleDuration);
			
			// Player no longer invincible.
			isInvincible = false;
		}
	}
	
	IEnumerator Flashing(bool damage)
	{
		float startFlashing = Time.time;
		
		if(damage == false)
		{
			// Blinks the player for as long as the set duration
			while ((Time.time - startFlashing) < 0.3f)
			{
				//Blink each bodypart for 2 Seconds
				for(int i = 0; i < bodyPartsArray.Length; i++)
				{
					bodyPartsArray[i].material.color = Color.red;
				}
				yield return new WaitForSeconds(0.1f);
				
				// Bodyparts invisible for some time
				for(int i = 0; i < bodyPartsArray.Length; i++)
				{
					bodyPartsArray[i].material.color = Color.white;
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
		else
		{
			// Blinks the player for as long as he is invincible.
			while ((Time.time - startFlashing) < invincibleDuration)
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
		}
	}

	public void ModifyHealth(int amount)
	{
		bool negative = amount < 0;

		// Sets the players health to a new value.
		currentHealth += amount;
		
		// Limits the player from having less than 0 health or more than the max health.
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);

		// Plays the death clip if the player health drops to/below 0.
		if(currentHealth <= 0)
		{
			audio.PlayOneShot (deathClip);
		}

		// Plays the damage clip if amount is negative.
		if(negative)
		{
			audio.PlayOneShot (damagedClip);
		}

		// Plays the healing clip if amount is positive.
		if(!negative)
		{
			audio.PlayOneShot (healingClip);
		}
		
		// Calls the UpdateHearts function to re-render the hearts shown in game.
		UpdateHearts ();
	}
	
	public void AddHearts(int n)
	{
		// For each health, add a heart as a child to the player object.
		for(int i = 0; i < n; i++)
		{
			Transform newHeart = ((GameObject)Instantiate(heartGUI.gameObject, this.transform.position, Quaternion.identity)).transform;
			newHeart.parent = this.transform.parent;
			
			int y = (int)(Mathf.FloorToInt(hearts.Count / maxHeartsPerRow));
			int x = (int)(hearts.Count - y * maxHeartsPerRow);
			
			newHeart.GetComponent<GUITexture>().pixelInset = new Rect(x * spacingX,y * spacingY,58,58);
			newHeart.GetComponent<GUITexture>().texture = heartImages[0];

			newHeart.transform.position = new Vector3(0.005f,0.9f,-5);

			// Adds the hearts to the arraylist.
			hearts.Add(newHeart);
		}
		
		// Calls the UpdateHearts function to re-render the hearts shown in game.
		UpdateHearts();
	}

	// Re-renders the visible hearts to match the players current health.
	private void UpdateHearts()
	{
		bool restAreEmpty = false;
		int i = 0;
		
		foreach(Transform heart in hearts)
		{
			if(restAreEmpty)
			{
				heart.guiTexture.texture = heartImages[0];
			}
			else
			{
				i += 1;
				if(currentHealth >= i * healthPerHeart)
				{
					heart.guiTexture.texture = heartImages[heartImages.Length - 1]; // Current heart is full.
				}
				else
				{
					int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currentHealth));
					int imageIndex = currentHeartHealth / healthPerHeart;
					
					if (imageIndex == 0 && currentHeartHealth > 0)
					{
						imageIndex = 1;
					}
					
					heart.guiTexture.texture = heartImages[imageIndex];
					restAreEmpty = true;
				}
			}
		}
	}
	
	// Removes the hearts if the player respawns.
	public void RemoveHearts()
	{
		foreach(Transform heart in hearts)
		{
			Destroy(heart.gameObject);
		}
	}
}
