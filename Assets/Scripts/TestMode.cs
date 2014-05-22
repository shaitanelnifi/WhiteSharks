using UnityEngine;
using System.Collections;

public class TestMode : MonoBehaviour {

	private MainMenu debugMode;
	private bool ready = false;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Alpha2)) {
			debugMode = new MainMenu("chapter2introtransition");
			ready = true;
		}
		else if (Input.GetKey (KeyCode.Alpha1)) {
			debugMode = new MainMenu("chapter1introtransition");
			ready = true;
		}


		if (ready == true) {
			debugMode.startGame();		
		}
	
	}

}
