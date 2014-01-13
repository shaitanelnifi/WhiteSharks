/*
npc class. opens conversation when player clicks on the npc

*/
using UnityEngine;
using System.Collections;

public class npcScript : MonoBehaviour {
	
	public GameObject conversation;


	//enable conversation object if left mouse button is clicked.
	void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			conversation.renderer.enabled = true;
			conversation.collider2D.enabled = true;
		}
	}

	// Update is called once per frame
	void update(){

	}
}
