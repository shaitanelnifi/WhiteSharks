// @author Anthony Lim
// Manager for the game input

using UnityEngine;
using System.Collections;

public class inputManager : MonoBehaviour {
	// Declare properties
	private static inputManager instance;
	private KeyCode prevKeyPressed;
	private gameStates currentState;
	
	public static inputManager Instance {
		get {
			if (instance == null) {
				print("Instance null, creating new inputManager");
				instance = new GameObject("inputManager").AddComponent<inputManager>();
			}
			return instance;
		}
	}

	KeyCode FetchKey() {
		// Gets the current singular key pressed
		int e = System.Enum.GetNames (typeof(KeyCode)).Length;
		for (int i = 0; i < e; ++i) {
			if (Input.GetKey ((KeyCode)i)) {
				return (KeyCode)i;
			}
		}
		return KeyCode.None;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Gets the last key pressed
		if (Input.anyKeyDown) {
			prevKeyPressed = FetchKey ();
		}
	}
}

public enum PlayerMovement {
	MOVELEFT,
	MOVERIGHT,
	MOVEDOWN,
	MOVEUP,
	OPENJOURNAL,
	OPENMENU
}
