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
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) {

			if (player.canWalk){
				//Debug.LogWarning ("Inventory Size Before: " + journal.Instance.inventory.Count);
				//Dialoguer.StartDialogue((int)myEnumName + offset);
				journal.Instance.inventory.Add(this);
				Destroy(this.gameObject);
				OnMouseExit();
				player.anim.SetBool("walking", false);
				player.anim.SetFloat("distance", 0f);
				player.setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
				//Debug.LogWarning ("Inventory: " + journal.Instance.inventory[0].name);
			}
			
		}
	}

}
