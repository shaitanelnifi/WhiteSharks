using UnityEngine;
using System.Collections;

public class victoryCondition : genericScene {

	public GameObject[] getTheseToProceed;
	public int talkToThisManyToProceed;
	public int talkedToThisMany;

	private bool startEnd = false;

	// Use this for initialization
	void Start () {
		GameManager.dialogueJustFinished = false;
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ("wait");
		Debug.Log ("ALIVE");	
	}

	private bool gotThem(){

		//Check the journal inventory for these things
		for (int i = 0; i < getTheseToProceed.Length; i++) {
			
			GameObject current = getTheseToProceed[i];

			if (!journal.Instance.isItemInInventory(current))
				return false;
		}

		return true;

	}

	IEnumerator wait(){

		if (!startEnd) 
			if (gotThem()){
			GameManager.dialogueJustFinished = false;
			Dialoguer.StartDialogue((int)dialogue[curDia]);
			playerScript player = FindObjectOfType(typeof(playerScript)) as playerScript;
			player.stopMove();
			startEnd = !startEnd;
		}

		SoundManager.Instance.Play2DMusic(playMe);
		//Debug.Log (debugMe);
		if (GameManager.dialogueJustFinished && curDia < dialogue.Length - 1) {
			
			GameManager.dialogueJustFinished = false;
			curDia ++;
			Dialoguer.StartDialogue((int)dialogue[curDia]);
			
		} else 
		if ((int)dialogue[curDia] >= 0 && GameManager.dialogueJustFinished && curDia == dialogue.Length - 1) {
			yield return new WaitForSeconds (waitThisLong);
			GameManager.Instance.playerInScene = isTherePlayer;
			done = true;
			if (isTherePlayer) {
				GameManager.Instance.SetMainCharacter (whatCharacter);
				GameManager.Instance.SetNextX (spawnHereAfter.x);
				GameManager.Instance.SetNextX (spawnHereAfter.y);
			}
			GameManager.dialogueJustFinished = false;
			Application.LoadLevel (nextLevel);
		}
	}

}
