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
	private bool clickedOnSomething;
	private distanceCheck pDist;
	
	public void OnMouseEnter(){
		//Debug.LogWarning ("Stop? Go?: " + player.canWalk);
		if (player != null && !wall)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
		//Debug.Log ("Talking " + player.talking);
		if (player != null) {
				if (wall && !player.talking) {
					player.canWalk = true;
			}
		}
	}
	
	public void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) {
			if (player != null) {
				if (player.canWalk){
					clickedOnSomething = true;
					player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
				}
			}
		}
		
	}

	public void onMouseMiss(){
		
		if (Input.GetMouseButton (0)) {
			//Debug.LogWarning("Checking click.");      
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				Debug.LogWarning ("Ray hit object");
				if (hit.collider.gameObject != this.gameObject) 
					clickedOnSomething = false;
			} else 
				clickedOnSomething = false;
		}
		
	}

	public void startDialogue(){
		if(!wall){
			Dialoguer.StartDialogue((int)diaNum + offset);
		}
		player.stopMove();
		SoundManager.Instance.StopWalk();
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		clickedOnSomething = false;
		player.talking = true;
	}
	
	public void Start(){
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		offset = GameManager.offset;
		pDist = gameObject.GetComponent<distanceCheck>();
	}
	
	void Update(){
		
		
		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (clickedOnSomething) 
			onMouseMiss ();

		if (!wall) {
			if (player.canWalk == true && clickedOnSomething) 
				if (pDist.isCloseEnough (player.transform.position))
						startDialogue ();
		} else if (wall && clickedOnSomething){
			player.stopMove();
			clickedOnSomething = false;
		}

	}
	
}
