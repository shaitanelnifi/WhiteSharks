using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

	public clickableID diaNum;
	public int offset;

	//Mouse icon information
	private string mouseOverIcon = "Examine_Icon";	
	private string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object

	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			Dialoguer.StartDialogue((int)diaNum + offset);

			playerScript temp = (playerScript) FindObjectOfType(typeof(playerScript));
			temp.canWalk = false;
			temp.anim.SetBool("walking", false);
		}
	}

	public void Start(){
		offset = GameManager.Instance.offset;
	}

}
