using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public Category category;		//What kind of item is it? (such as Personal Items, blades, guns, etc)
	public ArrayList infoGuilty = new ArrayList();	//Store a list of strings that might be displayed if the item is related to the murder
	public ArrayList infoNotGuilty = new ArrayList(); //If the item is unrelated, you might display one or more of these strings
	public CaseObjectNames myEnumName;

	public int offset;

	public string mouseOverIcon = "Grab_Icon";
	private string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object

	public void addInfoGuilty(string newInfo){
		infoGuilty.Add(newInfo);
	}

	public void addInfoNotGuilty(string newInfo){
		infoNotGuilty.Add(newInfo);
	}

	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) {

			playerScript temp = (playerScript) FindObjectOfType(typeof(playerScript));
			if (temp.canWalk == true){
				Debug.LogWarning ("Inventory Size Before: " + journal.Instance.inventory.Count);
				//Dialoguer.StartDialogue((int)myEnumName + offset);
				journal.Instance.inventory.Add(this);
				Destroy(this.gameObject);
				//temp.canWalk = false;
				temp.anim.SetBool("walking", false);
				temp.anim.SetFloat("distance", 0f);
				temp.setTarget(new Vector2(temp.transform.position.x, temp.transform.position.y));
				Debug.LogWarning ("Inventory: " + journal.Instance.inventory[0].name);
			}
			
		}
	}


	//Right now it just inputs preset sentences, replace with data driven stuff
	void Start(){
		this.addInfoGuilty ("There are fresh fingerprints in this weapon.");
		this.addInfoGuilty ("I really can tell that someone used this...I think.");
		this.addInfoGuilty ("I'm pretty confident that this is a trace.");
		this.addInfoNotGuilty ("God, this has been here for ages.");
		this.addInfoNotGuilty ("It's incredible how easily people tend to forget their stuff for ages.");
		this.addInfoNotGuilty ("I'm sure this has been cold for a long while.");
	}
}
