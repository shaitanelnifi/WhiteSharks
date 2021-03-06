﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : CaseElement {

	enum AnimationState //enum to avoid magic numbers in the animation ArrayList
	{
		idle=0,
		conversation
	};
	
	public GameObject playerObj;
	public BoxCollider2D box;
	protected string defaultIcon = "Walk_Icon";

	//Mouse icon information
	public string mouseOverIcon = "Speech_Icon";

	void Start(){
		base.Init ();
	}
	
	public void OnMouseEnter(){
		if (player != null&& !player.talking)
			GameManager.Instance.updateMouseIcon(mouseOverIcon);

		if (Input.GetMouseButton (0)) {
			player.stopMove();
			
		}
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
		//Debug.Log ("Talking " + player.talking);
		
		if (player != null) {
			//player.canWalk = true;
			if (!player.talking) {
				player.canWalk = true;
			}
		}
	}

	//enable conversation object if left mouse button is clicked.
	public void OnMouseDown(){
		if(Input.GetMouseButtonDown(0)){
			if (this.name.Equals("Shammy")){
				Animator a = GetComponent<Animator>();
				a.SetBool("active", false);
			}		 
			if (player.canWalk){
				clickedOnSomething = true;
				player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
			}
		}
	}

	public void startDialogue(){

		var temp = gameObject.GetComponent<victoryCondition> ();
		if (temp != null)
			//temp.curDia -= 1;

		Debug.Log (Dialoguer.GetGlobalBoolean (1));
		Dialoguer.StartDialogue((int)myConvo);
		player.stopMove();
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		clickedOnSomething = false;
		player.talking = true;
		
		DialogueGUI_Test dGUI = GameManager.Instance.GetComponent<DialogueGUI_Test>();
		//Debug.Log ("LEFT SPRITE: " + this.elementName);
		dGUI.setLeftSpriteName((this.elementName + "Sprite").Replace(" ", string.Empty));

	}
	
	//switch the displaying order of the npc. 
	void Update () {

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (Input.GetMouseButtonDown (0))
			onMouseMiss ();

		if (player != null)
		if (player.canWalk == true && clickedOnSomething)
				if (pDist.isCloseEnough (player.transform.position))
						startDialogue ();
	}
}
