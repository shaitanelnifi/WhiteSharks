using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	
	public clickableID diaNum;
	public int offset;
	
	//Mouse icon information
	private string mouseOverIcon = "Examine_Icon";	
	private string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object
	private playerScript player;
	public bool wall;
	
	public void OnMouseEnter(){
		Debug.Log("1111111111111111111111111111111111111111111111");
		if (player != null && !wall)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
		if (wall){
			player.canWalk = true;
		}
	}
	
	public void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("solo");
			if (player.canWalk == true){
				if(!wall){
					Dialoguer.StartDialogue((int)diaNum + offset);
					OnMouseExit();
				}
				player.setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
				player.canWalk = false;
				player.anim.SetFloat("distance", 0f);
				player.anim.SetBool("walking", false);
				
			}
		}
	}
	
	public void Start(){
		Debug.Log("wrwerewrwerwerwerwerew");
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		offset = GameManager.Instance.offset;
	}
	
	void Update(){
		
		
		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));
		
	}
	
}
