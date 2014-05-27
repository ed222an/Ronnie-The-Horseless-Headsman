using UnityEngine;
using System.Collections;

public class HorseBehaviour : MonoBehaviour
{
	// Variables
	private bool win;
	
	void Start ()
	{
		// Win-condition is not met.
		win = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// If the player touches the horse the win-conditions are met.
		if(other.transform.tag == "Player")
		{
			// Loads the next level.
			if(Application.loadedLevel == 1 || Application.loadedLevel == 2)
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}

			// Shows the win screen.
			if(Application.loadedLevel == 3)
			{
				// Win-condition is met.
				win = true;
			}
		}
	}

	void OnGUI()
	{
		// If win-conditions are met a menu is displayed.
		if(win)
		{
			// Begins a GUI-group to help with organization.
			GUI.BeginGroup(new Rect((Screen.width/2) - 50, (Screen.height/2)- 60, 100, 120));

			// The win-text.
			GUI.Label(new Rect(0, 0, 100, 20), "You win!");

			// The "Play again"-button.
			if(GUI.Button(new Rect(0, 20, 100, 50), "Play Again" ))
			{
				// Restarts the game from the first screen.
				Application.LoadLevel(0);
				//ResetLevel();
			}

			// The "Credits"-button.
			if(GUI.Button(new Rect(0, 70, 100, 50), "Credits"))
			{
				Application.LoadLevel(4);
			}

			// Ends the GUI-group.
			GUI.EndGroup();
		}
	}

	// Resets the level.
	void ResetLevel()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

}
