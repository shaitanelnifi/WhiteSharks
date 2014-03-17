using UnityEngine;
using System.Collections;


/*
 * This class controls the behaviour of the office for the tutorial.
 * We'll probably want to change the way it's implemented later.
 */
public class Office : MonoBehaviour {

	private bool firstTime = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (firstTime) {
			StartCoroutine("");
		}
		if (GameManager.dialogueJustFinished) {
			GameManager.dialogueJustFinished = false;
		}
	}
}
