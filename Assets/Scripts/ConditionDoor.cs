using UnityEngine;
using System.Collections;

public class ConditionDoor : DoorScript {

	public GameObject[] getTheseToProceed;
	public int[] getTheseGlobalBools;
	public clickableID dontGotIt;

	void Start(){
		
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		
	}
	
	void Update(){
		
		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));
		
	}



	public bool gotThem(){
		
		//Check the journal inventory for these things
		for (int i = 0; i < getTheseToProceed.Length; i++) {
			
			GameObject current = getTheseToProceed[i];
			
			if (!journal.Instance.isItemInInventory(current))
				return false;
		}
		
		for (int i = 0; i < getTheseGlobalBools.Length; i++) {
			
			bool current = Dialoguer.GetGlobalBoolean(getTheseGlobalBools[i]);
			
			if (!current)
				return false;
			
		}
		
		return true;
		
	}

	public void useDoor(){

		if (gotThem()) {
			GameManager.Instance.currRoom = id;
			SoundManager.Instance.StopWalk ();
			GameManager.Instance.SetNextX (x);
			GameManager.Instance.SetNextY (y);
			//DestoryPlayer();
			if ((Application.loadedLevelName == "finbalcony" && id == "finplaza") || (Application.loadedLevelName == "finplaza" && id == "finbalcony"))
					SoundManager.Instance.Play2DSound ((AudioClip)Resources.Load ("Sounds/SoundEffects/FinElevator"), SoundManager.SoundType.Sfx, true);
			else
					SoundManager.Instance.Play2DSound ((AudioClip)Resources.Load ("Sounds/SoundEffects/FinDoor"), SoundManager.SoundType.Sfx, true);
			Application.LoadLevel (id);
		}
		
	}

	void OnMouseDown(){
		if (!gotThem ()) {
			GameManager.dialogueJustFinished = false;
			Dialoguer.StartDialogue ((int)dontGotIt);
		}
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (GameManager.Instance.defaultIcon);
	}
	
	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}


}
