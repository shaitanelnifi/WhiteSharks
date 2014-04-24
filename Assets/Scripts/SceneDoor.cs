using UnityEngine;
using System.Collections;

public class SceneDoor : MonoBehaviour {
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
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (GameManager.Instance.defaultIcon);
	}

	public void switchScene(){
		SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/FinDoor"), SoundManager.SoundType.Sfx, true);
		GameManager.Instance.SetNextX(x);
		GameManager.Instance.SetNextY(y);
		GameManager.Instance.currRoom = id;
		Application.LoadLevel (id);
	}
	
	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
}
