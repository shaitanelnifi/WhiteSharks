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
	public List<NPCNames> relations;
	public int myConvo;


	//Mouse icon information
	public string mouseOverIcon = "Speech_Icon";

	void Start(){
		myConvo = GameManager.npcConversations[(int)enumName];
	}
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	//enable conversation object if left mouse button is clicked.
	public void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			if (this.name.Equals("Shammy")){
				Animator a = GetComponent<Animator>();
				a.SetBool("active", false);
			}
			playerScript temp = (playerScript) FindObjectOfType(typeof(playerScript));
			if (temp.canWalk == true){
				temp.canWalk = false;
				temp.anim.SetFloat("distance", 0f);
				temp.anim.SetBool("walking", false);
				temp.setTarget(new Vector2(temp.transform.position.x, temp.transform.position.y));
				Dialoguer.StartDialogue(myConvo);
				string npcResource = (this.elementName + "Sprite").Replace(" ", string.Empty);
	 			Texture2D npcTex = (Texture2D) Resources.Load (npcResource);

	 
	 			DialogueGUI dGUI = GameManager.Instance.GetComponent<DialogueGUI>();
	 			Debug.LogError ("dgui: " + dGUI.ToString());
	 			dGUI.setTargetTex(npcTex);
			}
 			//dGUI.tweenCam();
			//GameManager.npcList.Find(x => x.elementName.CompareTo(this.elementName) == 0).setVisible(true);
			
			/*
			//conversationObj.renderer.enabled = true;
			//conversationObj.collider2D.enabled = true;
			GameManager.npcList.Find(x => x.elementName.CompareTo(this.elementName) == 0).setVisible(true);

			convoBubble.GetComponentInChildren<UILabel>().text = convo;
			convoBubble.GetComponentInChildren<UI2DSprite>().sprite2D = this.getProfileImage();*/
		}
	}

	public NPCNames getEnumName(){
		return enumName;
	}
	
	public Category getWeaponProf(){
		return weaponProficiency;
	}
	
	public float getTrust(){
		return trust;
	}
	
	public string getAlibi(){

		if (alibi == "") {
			alibi = (string) GameManager.Instance.roomIDList[location];
		}

		return alibi;
	}
	
	//switch the displaying order of the npc. 
	void Update () {
		SpriteRenderer r = GetComponent<SpriteRenderer> ();
		if (GameManager.firstTimeOffice && !this.name.Equals("Shammy")) {
						r.color = Color.black;		
				} else {
			r.color = Color.white;		
		}
		/*if (transform.position.y < playerObj.transform.position.y) {
			renderer.sortingLayerName= "foreground";
			renderer.sortingOrder = 2;
			box.isTrigger = true;
		}
		else{
			renderer.sortingLayerName= "middleground";
			box.isTrigger = false;
		}*/
	}
}
