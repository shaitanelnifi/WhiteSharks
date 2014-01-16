using UnityEngine;
using System.Collections;

public class NPC : CaseElement {

	public GameObject conversation, playerObj;
	public BoxCollider2D box;

	//enable conversation object if left mouse button is clicked.
	public override void onMouseDown(){
		if(Input.GetMouseButton(0)){
			conversation.renderer.enabled = true;
			conversation.collider2D.enabled = true;
		}
	}
	//switch the displaying order of the npc. 
	void Update () {
		if (transform.position.y < playerObj.transform.position.y) {
			renderer.sortingLayerName= "foreground";
			renderer.sortingOrder = 2;
			box.isTrigger = true;
		}
		else{
			renderer.sortingLayerName= "middleground";
			box.isTrigger = false;
		}
	}
}
