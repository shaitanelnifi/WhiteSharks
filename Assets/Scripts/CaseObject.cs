using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public Category category;		//What kind of item is it? (such as Personal Items, blades, guns, etc)
	public ArrayList infoGuilty = new ArrayList();	//Store a list of strings that might be displayed if the item is related to the murder
	public ArrayList infoNotGuilty = new ArrayList(); //If the item is unrelated, you might display one or more of these strings
	public CaseObjectNames myEnumName;

	public int offset;

	public string mouseOverIcon = "Grab_Icon";

	public void addInfoGuilty(string newInfo){
		infoGuilty.Add(newInfo);
	}

	public void addInfoNotGuilty(string newInfo){
		infoNotGuilty.Add(newInfo);
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
		player.stopMove ();
		clickedOnSomething = false;
		journal.Instance.inventory.Add(this);
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
