using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
	// Variables.
	public GameObject transportLocation;
	public float resetTimer;
	public float waitTime;

	private bool isEnabled;

	void Start()
	{
		isEnabled = true;
	}

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.tag == "Player")
		{
			if(isEnabled)
			{
				// Call the disablefunction.
				StartCoroutine(PortalDisabled());

				// Wait for a bit...
				yield return new WaitForSeconds(waitTime);

				// Move the player to the set location.
				other.transform.position = transportLocation.transform.position;
			}
		}
	}

	// Disables and enables the portal after set amount of time.
	IEnumerator PortalDisabled()
	{
		isEnabled = false;

		yield return new WaitForSeconds (resetTimer);

		isEnabled = true;
	}
}
