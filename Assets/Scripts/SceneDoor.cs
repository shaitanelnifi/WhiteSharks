using UnityEngine;
using System.Collections;

public class SceneDoor : MonoBehaviour {
	public int id;
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
	
	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
}
