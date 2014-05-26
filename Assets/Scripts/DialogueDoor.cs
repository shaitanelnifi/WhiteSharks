using UnityEngine;
using System.Collections;

public class DialogueDoor : DoorScript {

	public bool playDoorSound = false;
	public bool wall;
	public bool hasConvo = true;
	public clickableID diaNum;
	public int offset;
	private bool clickedOnSomething;
	private bool doneTalking = false;
	GameObject backEffect = null;

	public string whatCharacter;
	public string nextLevel;
	public bool isTherePlayer = false;
	public Vector2 spawnHereAfter;

	void Start() {

		player = (playerScript)FindObjectOfType(typeof(playerScript));
		orderCount++;
		order = orderCount;

	}

	void Update() {
		if(player == null)
			player = (playerScript)FindObjectOfType(typeof(playerScript));

	}

	public void useDoor() {
		GameManager.Instance.currRoom = id;
		SoundManager.Instance.StopWalk();
		GameManager.Instance.SetNextX(x);
		GameManager.Instance.SetNextY(y);
		orderCount = 0;
		if(playDoorSound) SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/FinDoor"), SoundManager.SoundType.Sfx, true);
		if (hasConvo) {
						startDialogue ();
						StartCoroutine ("DialogueDone");
				} else {
			backEffect = (GameObject)Instantiate(Resources.Load("blackScreen"));
			StartCoroutine("Fade");
		}
	}

	IEnumerator DialogueDone() {
		while(!doneTalking || player.talking) {
			yield return null;
		}
		backEffect = (GameObject)Instantiate(Resources.Load("blackScreen"));
		StartCoroutine("Fade");
	}

	IEnumerator Fade() {
		bool temp = false;
		while(!temp){
			temp = true;
			yield return new WaitForSeconds(0.3f);
		}

		GameManager.Instance.playerInScene = isTherePlayer;

		if(isTherePlayer) {

			//Debug.Log ("Setting nexts to " + spawnHereAfter.x + " and " + spawnHereAfter.y);
			GameManager.Instance.SetMainCharacter(whatCharacter);
			GameManager.Instance.SetNextX(spawnHereAfter.x);
			GameManager.Instance.SetNextY(spawnHereAfter.y);
		}
		GameManager.dialogueJustFinished = false;
		SoundManager.Instance.CantWalk();
		Application.LoadLevel(nextLevel);
	}

	public void startDialogue() {
		if(!wall) {
			Dialoguer.StartDialogue((int)diaNum + offset);
		}
		player.stopMove();
		SoundManager.Instance.StopWalk();
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		clickedOnSomething = false;
		player.talking = true;
		doneTalking = true;
	}
}
