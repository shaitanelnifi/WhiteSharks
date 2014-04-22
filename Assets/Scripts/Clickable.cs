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
		if (player != null && !wall)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
		if (wall && !player.talking){
			player.canWalk = true;
		}
	}
	
	public void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) 
		if (player.canWalk){
			clickedOnSomething = true;
			player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
		}
		
	}

	public void onMouseMiss(){
		
		RaycastHit hit = new RaycastHit ();        
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit))
			if (hit.collider.gameObject != this.gameObject)
				clickedOnSomething = false;
	}

	public void startDialogue(){
		if(!wall){
			Dialoguer.StartDialogue((int)diaNum + offset);
		}
		player.stopMove();
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		clickedOnSomething = false;
		player.talking = true;
	}
	
	public void Start(){
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		offset = GameManager.Instance.offset;
		pDist = gameObject.GetComponent<distanceCheck>();
	}
	
	void Update(){
		
		
		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (Input.GetMouseButtonDown (0))
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
