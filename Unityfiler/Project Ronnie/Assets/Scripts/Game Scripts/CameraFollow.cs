using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public int cameraDistance;
	public float yDisposition;

	void Update ()
	{
		Vector3 PlayerPOS = GameObject.FindGameObjectWithTag("Player").transform.transform.position;
		GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(PlayerPOS.x, PlayerPOS.y + yDisposition, PlayerPOS.z - cameraDistance);
	}
}
