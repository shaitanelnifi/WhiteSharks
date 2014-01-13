// @author Anthony Lim
// Manages core gameplay elements such as scenes and states

using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
	// Declare properties
	private static gameManager instance;
	private gameStates currentState;
	private string currLevel;			// Current level
	private string name;				// Character name

	public static gameManager Instance {
		get {
			if (instance == null) {
				print("Instance null, creating new gameManager");
				instance = new GameObject("gameManager").AddComponent<gameManager>();
			}
			return instance;
		}
	}

	// Sets instance to null when the application quits
	public void OnApplicationQuit() {
		instance = null;
	}

	public void startState() {
		print ("Creating a new game state");

		// Set default properties
		currLevel = "Level 1";
		name = "My Character";
		currentState = gameStates.MAINMENU;

		// Load Level 1
		Application.LoadLevel ("stage1");

	}

	public void quitGame() {
		// Quit the game
		print ("Qutting the game");
		Application.Quit ();
	}

	public string getLevel() {
		if (currLevel != null)
			return currLevel;
		else
			return "currLevel is null!";
	}

	public void setLevel(string level) {
		currLevel = level;
	}

	public string getName() {
		if (name != null)
			return name;
		else
			return "name is null!";
	}

	public void setName(string newName) {
		name = newName;
	}

	void Update() {
		//print ("Test??");
	}
}

public enum gameStates {
	INTRO,
	MAINMENU,
	JOURNAL,
	CONVERSATION
}