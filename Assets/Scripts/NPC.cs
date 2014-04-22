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
	public bool clickedOnSomething = false;


	//Mouse icon information
	public string mouseOverIcon = "Speech_Icon";

	void Start(){
		if (maxDist < GameManager.Instance.maxDist)
			maxDist = GameManager.Instance.maxDist;
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		myConvo = GameManager.npcConversations[(int)enumName];
	}
	
	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	//enable conversation object if left mouse button is clicked.
	public void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			if (this.name.Equals("Shammy")){
				Animator a = GetComponent<Animator>();
				a.SetBool("active", false);
			}
			if (player.canWalk == true)	
				clickedOnSomething = true;
		}
	}

	public NPCNames getEnumName(){
		return enumName;
	}
	
	//switch the displaying order of the npc. 
	void Update () {

		if (Input.GetMouseButton (0)) {
			
			RaycastHit hit = new RaycastHit ();        
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit))
				if (hit.collider.gameObject != this.gameObject)
					clickedOnSomething = false;
		}

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (player.canWalk == true)
		if (Vector3.Distance(player.transform.position, transform.position) <= maxDist && clickedOnSomething){
			Dialoguer.StartDialogue(myConvo);
			player.setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
			player.canWalk = false;
			player.anim.SetFloat("distance", 0f);
			player.anim.SetBool("walking", false);
			OnMouseExit();
			clickedOnSomething = false;

			DialogueGUI_Test dGUI = GameManager.Instance.GetComponent<DialogueGUI_Test>();
			Debug.Log ("LEFT SPRITE: " + this.elementName);
			dGUI.setLeftSpriteName((this.elementName + "Sprite").Replace(" ", string.Empty));
		}

		SpriteRenderer r = GetComponent<SpriteRenderer> ();
		if (GameManager.firstTimeOffice && !this.name.Equals("Shammy")) {
						r.color = Color.black;		
				} else {
			r.color = Color.white;		
		}
	}
}
