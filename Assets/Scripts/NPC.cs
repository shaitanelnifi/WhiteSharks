using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : CaseElement {

	enum AnimationState //enum to avoid magic numbers in the animation ArrayList
	{
		idle=0,
		conversation
	};
	
	public GameObject conversationObj, playerObj;
	public BoxCollider2D box;

	//NPC specific data fields
	public Category weaponProficiency;	//What kinds of weapons is the NPC skilled with
	public bool highClass;				//Does the NPC belong to the higher class society (top floors) or not?
	public string alibi;				//A set of info that represents an alibi, eg a location they were at
	public ArrayList animations;		//An array list of sprites representing the animation
	public string scene;
	public string personalSentence;
	public string convo;
	public GameObject convoBubble;
	public float trust;
	public NPCNames enumName;

	public Dictionary npcKnowledge;
	private Conversation convSetup;

	//Mouse icon information
	public string mouseOverIcon = "Speech_Icon";

	void Start(){
		convoBubble = GameObject.Find ("Conversation Bubble");

		npcKnowledge = new Dictionary ();
		npcKnowledge.addNewEntry (new DictEntry(enumName, guilt, weaponProficiency, scene, trust));

		List<NPCNames> indexSet = new List<NPCNames>();

		for (int i = 0; i < npcKnowledge.size (); i++){
			DictEntry temp = npcKnowledge[i];
			indexSet.Add (temp.getIndex ());
		}

		indexSet.Insert (0, enumName);

		convSetup = new Conversation (GameManager.Instance.GetMainCharacter(), npcKnowledge, indexSet);
	}
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	//enable conversation object if left mouse button is clicked.
	public void OnMouseDown(){
		if(Input.GetMouseButton(0)){

			GameManager.npcList.Find(x => x.elementName.CompareTo(this.elementName) == 0).setVisible(true);

			convSetup.generateDialoguer();

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
		return alibi;
	}


	//switch the displaying order of the npc. 
	void Update () {
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
