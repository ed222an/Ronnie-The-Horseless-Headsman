using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{
	// Variables.
	private GUISkin guiSkin;
	private bool pauseIsActive;

	void Start()
	{
		pauseIsActive = false;
	}

	void Update ()
	{
		// Enables pause-functionality.
		if(Input.GetKey(KeyCode.P) && !pauseIsActive)
		{
			Time.timeScale = 0.0f;
			pauseIsActive = true;
		}
		else if(Input.GetKeyDown(KeyCode.P) && pauseIsActive)
		{
			Time.timeScale = 1.0f;
			pauseIsActive = false;
		}
	}

	void OnGUI ()
	{
		// Set up gui skin
		GUI.skin = guiSkin;

		if(pauseIsActive)
		{
			// Begins a GUI-group to help with organization.
			GUI.BeginGroup(new Rect((Screen.width/2) - 50, (Screen.height/2)- 60, 100, 200));
			
			// The win-text.
			GUI.Label(new Rect(20, 0, 100, 20), "PAUSED!");
			
			// The "Continue"-button.
			if(GUI.Button(new Rect(0, 20, 100, 50), "Continue" ))
			{
				// Disables the pause.
				Time.timeScale = 1.0f;
				pauseIsActive = false;  
			}

			// The "Restart"-button.
			if(GUI.Button (new Rect(0, 70, 100, 50), "Restart"))
			{
				// Restarts the level.
				Application.LoadLevel(Application.loadedLevel);
				Time.timeScale = 1.0f;
				pauseIsActive = false;
			}
			
			// The "Quit"-button.
			if(GUI.Button(new Rect(0, 120, 100, 50), "Quit"))
			{
				// Loads the startscreen.
				Application.LoadLevel(0);
			}
			
			// Ends the GUI-group.
			GUI.EndGroup();
		}
	}
}