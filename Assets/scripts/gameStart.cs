// @author Anthony Lim
// Main menu of the game

using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {

	// Our Startscreen GUI
	void OnGUI ()
	{
		if (GUI.Button (new Rect (30, 30, 150, 30), "Start Game")) {
			startGame();
		}

		if (GUI.Button (new Rect (30, 70, 150, 30), "Exit Game")) {
			quitGame();
		}
	}
	
	private void startGame() {
		print ("Starting game");

		// Initialize various managers for the game
		// Single
		DontDestroyOnLoad(gameManager.Instance);
		DontDestroyOnLoad (inputManager.Instance);
		gameManager.Instance.startState();
	}

	private void quitGame() {
		print ("Quitting game");

		DontDestroyOnLoad(gameManager.Instance);
		gameManager.Instance.quitGame();  
	}
}
