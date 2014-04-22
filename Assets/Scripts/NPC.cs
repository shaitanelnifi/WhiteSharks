using UnityEngine;
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

	//NPC specific data fields
	public Category weaponProficiency;	//What kinds of weapons is the NPC skilled with
	public bool highClass;				//Does the NPC belong to the higher class society (top floors) or not?
	public string alibi;			//A set of info that represents an alibi, requires another npc, location
	public ArrayList animations;		//An array list of sprites representing the animation
	public string scene;

	public float trust;
	public NPCNames enumName;
	public int myConvo;

	//Mouse icon information
	public string mouseOverIcon = "Speech_Icon";

	void Start(){
		base.Init ();
		myConvo = GameManager.npcConversations[(int)enumName];
	}
	
	public void OnMouseEnter(){
			if (player != null && player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
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

	public NPCNames getEnumName(){
		return enumName;
	}

	public void startDialogue(){

		Dialoguer.StartDialogue(myConvo);
		player.stopMove();
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		clickedOnSomething = false;
		player.talking = true;
		
		DialogueGUI_Test dGUI = GameManager.Instance.GetComponent<DialogueGUI_Test>();
		Debug.Log ("LEFT SPRITE: " + this.elementName);
		dGUI.setLeftSpriteName((this.elementName + "Sprite").Replace(" ", string.Empty));

	}
	
	//switch the displaying order of the npc. 
	void Update () {

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (Input.GetMouseButtonDown (0))
			onMouseMiss ();

		if (player.canWalk == true && clickedOnSomething)
				if (pDist.isCloseEnough (player.transform.position))
						startDialogue ();

		SpriteRenderer r = GetComponent<SpriteRenderer> ();
		if (GameManager.firstTimeOffice && !this.name.Equals("Shammy")) {
						r.color = Color.black;		
				} else {
			r.color = Color.white;		
		}
	}
}
