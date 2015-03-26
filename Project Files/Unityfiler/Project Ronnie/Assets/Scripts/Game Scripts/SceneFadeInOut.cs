using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
	// Variables
	public float fadeSpeed = 1.5f;
	private bool sceneStarting = true;

	void Awake()
	{
		// Stretches the guiTexture so that it covers the screen.
		GetComponent<GUITexture>().pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
	}

	void Update()
	{
		if(sceneStarting)
		{
			StartScene();
		}
	}
	
	void FadeToClear()
	{
		GetComponent<GUITexture>().color = Color.Lerp (GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack()
	{
		GetComponent<GUITexture>().color = Color.Lerp (GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}

	// At the start of the scene, the blackness fades away.
	void StartScene()
	{
		FadeToClear ();
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			sceneStarting = false;
		}
	}

	// At the end of the scene, the blackness fades in.
	public void EndScene()
	{
		GetComponent<GUITexture>().enabled = true;
		FadeToBlack ();

		if(GetComponent<GUITexture>().color.a >= 0.95f)
		{
			Application.LoadLevel(1);
		}
	}
}
