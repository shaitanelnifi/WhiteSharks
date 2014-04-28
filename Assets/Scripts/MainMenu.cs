﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject start;
	public GameObject options;
	public GameObject quit;
	private float _buttonWidth = 150;
	private bool _mainMenu = true;
	private string _wo;
	private bool test_Mode = false;

	// Use this for initialization
	public MainMenu(int a){
		test_Mode = true;
	}
	void Start () {
		UIEventListener.Get (start).onClick += this.onClick;
		UIEventListener.Get (options).onClick += this.onClick;
		UIEventListener.Get (quit).onClick += this.onClick;
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/MainMenu"));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void onClick(GameObject button){
		if (button == start) {
			Debug.Log ("START");	
			startGame();
		}
		if (button == options) {
			Debug.Log ("OPTIONS");		
			optionsMenu();
		}
		if (button == quit) {
			Debug.Log ("QUIT");		
			quitGame();
		}
	}

	public void startGame() {
		Dialoguer.Initialize ("chapter1");
		// Initialize various managers for the game
		// Singleton pattern

		DontDestroyOnLoad(GameManager.Instance);
		DontDestroyOnLoad(InputManager.Instance);
		DontDestroyOnLoad(SoundManager.Instance);
		GameManager.Instance.startState(test_Mode);

		switch (GameManager.currentEpisode) {
		case 0:
			//GameManager.npcConversations[(int)NPCNames.Shammy] = 1;
			//GameManager.npcConversations[(int)NPCNames.CarlosFranco] = 4;
			//GameManager.npcConversations[(int)NPCNames.NoelAlt] = 3;
			//GameManager.offset = 8;
			break;
		}

	}
	
	private void optionsMenu() {
		print ("Entering Options menu");
		
		//_mainMenu = false;
		DontDestroyOnLoad (InputManager.Instance);
	}
	
	private void quitGame() {
		print ("Quitting game");
		
		DontDestroyOnLoad(GameManager.Instance);
		GameManager.Instance.quitGame();  
	}
}
