﻿// @author Anthony Lim
// Main menu of the game

using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {
	// Declare properties
	private float _buttonWidth = 150;
	private bool _mainMenu = true;
	private bool test_Mode = false;

	public gameStart(int a){
		test_Mode = true;
	}

	public bool getTestMode(){
		return test_Mode;
	}

	// Our Startscreen GUI
	void OnGUI ()
	{
		if (_mainMenu) {
			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2, _buttonWidth, 30), "Start Game")) {
				startGame();
			}

			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2 + 40, _buttonWidth, 30), "Options")) {
				optionsMenu();
			}

			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2 + 80, _buttonWidth, 30), "Exit Game")) {
				quitGame();
			}
		} else {
			//GUI.TextField(new Rect(50, 50, 50, 50), "He", 44);
			GUI.Label(new Rect(150, 150, 50, 50), "Testing");

			if (GUI.Button (new Rect(100, 100, 100, 100), "Go Back")) {
				_mainMenu = true;
			}
		}
	}

	void Update() {

	}
	
	public void startGame() {
		//Debug.Log("Starting gamedasadasDASAdssad");
		//Debug.Log (GameManager.episodeDialogues [GameManager.currentEpisode]); 
		//Dialoguer.Initialize (GameManager.episodeDialogues[GameManager.currentEpisode]);

		// Initialize various managers for the game
		// Singleton pattern
		DontDestroyOnLoad(GameManager.Instance);
		GameManager.Instance.startState(test_Mode);

	}

	private void optionsMenu() {
		print ("Entering Options menu");

		_mainMenu = false;
	}

	private void quitGame() {
		print ("Quitting game");

		DontDestroyOnLoad(GameManager.Instance);
		GameManager.Instance.quitGame();  
	}
}
