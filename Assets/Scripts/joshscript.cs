using UnityEngine;
using System.Collections;

public class joshscript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		float oldX = -3.158945f;
		float newX = 40.0f;

		if (!Dialoguer.GetGlobalBoolean(12)) {
			//transform.position.x = -3.158945f;
			transform.position = new Vector3(oldX, transform.position.y);
		}
		else {
			//transform.position.x = 40.0f;
			transform.position = new Vector3(newX, transform.position.y);
		}
	}
}
