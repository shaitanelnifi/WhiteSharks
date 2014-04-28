using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public int offset;
	private GameObject journal;

	public string mouseOverIcon = "Grab_Icon";

	void Start(){
	
		base.Init ();
		journal = GameObject.Find("UI Root");
	
	}

	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) 
		if (player.canWalk){
			clickedOnSomething = true;
			player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
		}
	}

	public void pickUpItem(){

		//player.talking = true;
		Debug.LogError ("GOT IT: " + elementName);
		player.stopMove ();
		//SoundManager.Instance.StopWalk();
		clickedOnSomething = false;
		//journal.inventory.Add(this);
		journal.SendMessage("addObject", this.gameObject);
		//Debug.LogError ("Inventory: " + journal.inventory[0]);
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		player.canWalk = true;
		Destroy(this.gameObject);

	}


	void Update(){

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (Input.GetMouseButton (0)) 
			onMouseMiss ();

		if (player.canWalk == true && clickedOnSomething)
			if (pDist.isCloseEnough (player.transform.position))
				pickUpItem ();

	}

}
