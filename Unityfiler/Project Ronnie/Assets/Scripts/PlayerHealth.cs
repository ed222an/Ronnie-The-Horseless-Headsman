using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	// Public variables
	public int startingHealth;					// How much health the player starts with.
	public int healthPerHeart;					// How much health a heart is worth.
	public float invincibleDuration;			// Invincibility duration.

	public GUITexture heartGUI;					// Reference to the heart texture.
	public Texture[] heartImages;				// Imagearray with the heart textures.

	public float resetAfterDeathTime;			// How much time from the player dying to the level resetting.
	public AudioClip deathClip;					// The sound effect of the player dying.

	// Private variables
	private bool isInvincible;					// Player is invincible while true.
	private int currentHealth;					// How much health the player has left.
	private int maxHealth;						// The maximum health value.

	private SpriteRenderer[] bodyPartsArray;	// SpriteRenderer-array containing the players bodyparts.

	private ArrayList hearts;	// How many hearts the player has.
	private int maxHeartsPerRow;				// Heart spacing.
	private float spacingX;						// Heart spacing.
	private float spacingY;						// Heart spacing.

	private Animator anim;						// Reference to the animator component.
	private PlayerMovement playerMovement;		// Reference to the PlayerMovement script.
	private SceneFadeInOut sceneFadeInOut;		// Reference to the SceneFadeInOut script.
	private float deadTimer;					// A timer for counting to the reset of the level once the player is dead.
	private bool playerDead;					// A bool to show if the player is dead or not.

	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag ("Fader").GetComponent<SceneFadeInOut> ();
	}

	void Start()
	{
		// Sets the players health to the starting value.
		hearts = new ArrayList();
		currentHealth = startingHealth;
		maxHealth = startingHealth;
		maxHeartsPerRow = 3;
		isInvincible = false;

		// Gets the bodypart sprites from the player parent object.
		bodyPartsArray = GetComponentsInChildren<SpriteRenderer>();

		// Sets the spacing between the hearts.
		spacingX = heartGUI.pixelInset.width;
		spacingY = heartGUI.pixelInset.height;

		// Adds a heart per health.
		AddHearts (startingHealth / healthPerHeart);
	}

	void Update()
	{
		// If the current health is less than or equal to 0...
		if(currentHealth <= 0)
		{
			// ... and if the player is not yet dead...
			if(!playerDead)
			{
				// ... call the PlayerDying function.
				PlayerDying();
			}
			else
			{
				// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
				PlayerDead();
				LevelReset();
			}
		}
	}

	void PlayerDying()
	{
		// The player is now dead.
		playerDead = true;

		// Set the animator's dead parameter to true also.
		anim.SetBool ("Dying", true);

		// Play the dying sound effect at the player's location.
		// AudioSource.PlayClipAtPoint (deathClip, transform.position);
	}

	void PlayerDead()
	{
		/*
		// If the player is in the dying state then reset the dead parameter.
		if(anim.GetCurrentAnimatorStateInfo(0).nameHash == Hashtable.dyingState)
		{
			anim.SetBool("Ronnie_Dying", false);
		}
		*/
		// Disable the movement.
		anim.SetFloat ("Speed", 0f);
		playerMovement.enabled = false;

		// Stop the footsteps playing.
		// audio.Stop ();
	}

	void LevelReset()
	{
		// Increase the timer.
		deadTimer += Time.deltaTime;

		//If the timer is greater than or equal to the time before the level resets...
		if(deadTimer >= resetAfterDeathTime)
		{
			// ... reset the level.
			sceneFadeInOut.EndScene();
		}

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

			// Adds the hearts to the arraylist.
			hearts.Add(newHeart);
		}

		// Calls the UpdateHearts function to re-render the hearts shown in game.
		UpdateHearts();
	}

	// Checks for collisions with object with certain tags.
	IEnumerator OnCollisionEnter2D(Collision2D collision)
	{
		// If the player can take damage, call the ModifyHealth function & set invincible to true for a small amount of time.
		if (!isInvincible)
		{
			if(collision.transform.tag == "Enemy")
			{
				// Variable for blinking time.
				float startBlinking = Time.time;

				// Decrement the player's health by 1
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

		if(collision.transform.tag == "Health")
		{
			ModifyHealth(1);
		}
	}
	

	public void ModifyHealth(int amount)
	{
		// Sets the players health to a new value.
		currentHealth += amount;

		// Limits the player from having less than 0 health or more than the max health.
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);

		// Calls the UpdateHearts function to re-render the hearts shown in game.
		UpdateHearts ();
	}

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
}
