/*
 door object.

changes: added some variables.-John Mai 1/12/2014
*/ 
using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	//id = 0 means go back one room, 1 means go to next room
	public string id;
	public float x, y;

	//Mouse icon information
	public string mouseOverIcon = "Door_Icon";
	private playerScript player;

	void Start(){

		player = (playerScript) FindObjectOfType(typeof(playerScript));

	}

	void Update(){

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

	}

	public void useDoor(){

		GameManager.Instance.currRoom = id;
		SoundManager.Instance.StopWalk ();
		GameManager.Instance.SetNextX(x);
		GameManager.Instance.SetNextY(y);
		//DestoryPlayer();
		if((Application.loadedLevelName == "finbalcony" && id == "finplaza") || (Application.loadedLevelName == "finplaza" && id == "finbalcony"))
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/FinElevator"), SoundManager.SoundType.Sfx, true);
		else
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/FinDoor"), SoundManager.SoundType.Sfx, true);
		Application.LoadLevel (id);

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
