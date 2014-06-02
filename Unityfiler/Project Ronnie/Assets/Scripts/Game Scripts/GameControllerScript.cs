using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{
	// Variables.
	public GUIStyle menuHeaderStyle;
	public GUIStyle menuInstructionsHeaderStyle;
	public GUIStyle menuInstructionsContentStyle;
	public AudioClip pauseMenuClip;

	private GUISkin guiSkin;
	private AudioClip originalSong;
	private bool pauseIsActive;
	private bool showInstructions;

	void Start()
	{
		guiSkin = null;
		pauseIsActive = false;
		showInstructions = false;

		originalSong = audio.clip;
	}

	void Update ()
	{
		// Enables pause-functionality.
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale == 1.0f)
			{
				audio.clip = pauseMenuClip;
				audio.volume = 1.0f;
				audio.Play();
				Time.timeScale = 0.0f;
				pauseIsActive = true;
			}
			else
			{
				Time.timeScale = 1.0f;
				pauseIsActive = false;
				showInstructions = false;
				audio.clip = originalSong;
				audio.volume = 0.25f;
				audio.Play();
			}
		}
	}
	
	// The pause menu.
	void OnGUI ()
	{
		// Set up gui skin
		GUI.skin = guiSkin;

		// Pause prompt.
		GUI.Label(new Rect(Screen.width - 120, 0, 200, 20), "Pause: \"Esc\" || \"P\"");

		if(pauseIsActive)
		{
			// Begins a GUI-group to help with organization.
			GUI.BeginGroup(new Rect((Screen.width/2) - 50, (Screen.height/2) - 250, 150, 200));
			
			// The Pause-text.
			GUI.Label(new Rect(20, 0, 100, 24), "PAUSED!", menuHeaderStyle);
			
			// The "How To Play"-button.
			if(GUI.Button(new Rect(0, 20, 100, 50), "How To Play"))
			{
				pauseIsActive = false;
				showInstructions = true;
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
				Time.timeScale = 1.0f;
				pauseIsActive = false;
			}
			
			// Ends the GUI-group.
			GUI.EndGroup();
		}

		if(showInstructions)
		{
			// Begins a GUI-group to help with organization.
			GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

			// Instructiontext.
			GUI.Label(new Rect((Screen.width/2 - 100),20,100,20), "Instructions!", menuInstructionsHeaderStyle);
			GUI.Label(new Rect((Screen.width/2 - 50),75,100,20), "Use the W,A,S,D- or arrow keys to move Ronnie around.\nUse the mouse cursor to rotate Ronnie.\nUse the left mouse button to attack.\n(Holding the attack button allows continuous attacking!)\n\nKeep it safe kids and be careful not to die!\nI hear that's dangerous!", menuInstructionsContentStyle);

			// Return to pause screen.
			if(GUI.Button(new Rect((Screen.width/2 - 100),300,100,50), "Back"))
			{
				showInstructions = false;
				pauseIsActive = true;
			}

			// Resumes the game.
			if(GUI.Button(new Rect((Screen.width/2),300,100,50), "Continue"))
			{
				showInstructions = false;
				pauseIsActive = false;
				Time.timeScale = 1.0f;
				audio.clip = originalSong;
				audio.volume = 0.25f;
				audio.Play();
			}

			// Ends the GUI-group.
			GUI.EndGroup();
		}
	}
}