using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour
{
	// Variables.
	public GUIStyle menuHeaderStyle;
	public GUIStyle menuInstructionsHeaderStyle;
	public GUIStyle menuInstructionsContentStyle;

	private GUISkin guiSkin;
	private bool showMenu;
	private bool showInstructions;

	void Start ()
	{
		guiSkin = null;
		showMenu = true;
		showInstructions = false;
	}

	void OnGUI ()
	{
		// Set up gui skin
		GUI.skin = guiSkin;

		if(showMenu)
		{
			// Begins a GUI-group to help with organization.
			GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

			// The "Play"-button.
			if(GUI.Button(new Rect((Screen.width/2) - 50, (Screen.height/2) + 50, 100, 50), "Play!"))
			{
				// Loads the next scene.
				Application.LoadLevel(Application.loadedLevel + 1);
			}

			// The "How To Play"-button.
			if(GUI.Button(new Rect((Screen.width/2) - 50, (Screen.height/2) + 100, 100, 50), "How To Play"))
			{
				showMenu = false;
				showInstructions = true;
			}
			
			// Ends the GUI-group.
			GUI.EndGroup();
		}
		
		if(showInstructions)
		{
			// Begins a GUI-group to help with organization.
			GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
			
			// Instructiontext.
			GUI.Label(new Rect((Screen.width/2 - 85), 275, 100, 20), "Instructions!", menuInstructionsHeaderStyle);
			GUI.Label(new Rect((Screen.width/2 - 50), 325, 100, 20), "Use the W,A,S,D- or arrow keys to move Ronnie around.\nUse the mouse cursor to rotate Ronnie.\nUse the left mouse button to attack.\n(Holding the attack button allows continuous attacking!)\n\nKeep it safe kids and be careful not to die!\nI hear that's dangerous!", menuInstructionsContentStyle);
			
			// Return to pause screen.
			if(GUI.Button(new Rect((Screen.width/2 - 50),(Screen.height/2) + 200, 100,50), "Back"))
			{
				showInstructions = false;
				showMenu = true;
			}
			
			// Ends the GUI-group.
			GUI.EndGroup();
		}
	}
}
