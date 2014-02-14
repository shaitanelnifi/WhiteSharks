using UnityEngine;
using System.Collections;

public class itemHider : MonoBehaviour {

	public CaseObject hiddenCaseObject;
	public string mouseOverIcon = "";
	public string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object
	public bool opened = false;


	public void OnMouseDown(){
		if (Input.GetMouseButton(0)){
			print ("\n");
			print ("You found it!\n");
			if (!opened) {

				opened = true;
				Instantiate(hiddenCaseObject, transform.position, transform.rotation);
				Destroy(this.gameObject);
			}
		}
	}

	public void OnMouseOver(){
		//GameManager.Instance.updateMouseIcon (mouseOverIcon);
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}
}
