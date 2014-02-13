using UnityEngine;
using System.Collections;

public class itemHider : MonoBehaviour {

	public CaseObject hiddenCaseObject;
	public string mouseOverIcon = "";
	public string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object
	public bool opened;

	public itemHider(CaseObject whatToHide){

		hiddenCaseObject = whatToHide;
		opened = false;

	}

	public void OnMouseDown(){
		if (!opened) {
			opened = true;
			Instantiate(hiddenCaseObject);
		}
	}

	public void OnMouseOver(){
		//GameManager.Instance.updateMouseIcon (mouseOverIcon);
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}
}
