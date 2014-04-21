using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public Category category;		//What kind of item is it? (such as Personal Items, blades, guns, etc)
	public ArrayList infoGuilty = new ArrayList();	//Store a list of strings that might be displayed if the item is related to the murder
	public ArrayList infoNotGuilty = new ArrayList(); //If the item is unrelated, you might display one or more of these strings
	public CaseObjectNames myEnumName;

	public int offset;

	public string mouseOverIcon = "Grab_Icon";

	public bool clickedOnSomething = false;

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
		if (Input.GetMouseButton (0)) 
			if (player.canWalk)
				clickedOnSomething = true;
	}

	void Update(){

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (Input.GetMouseButton (0)) {
			
			RaycastHit hit = new RaycastHit ();        
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit))
				if (hit.collider.gameObject != this.gameObject)
					clickedOnSomething = false;
		}

		if (Vector3.Distance (player.transform.position, transform.position) <= maxDist && clickedOnSomething) {

			player.setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
			player.canWalk = true;
			player.anim.SetFloat("distance", 0f);
			player.anim.SetBool("walking", false);
			OnMouseExit();
			clickedOnSomething = false;
			journal.Instance.inventory.Add(this);
			Destroy(this.gameObject);
		}

	}

}
