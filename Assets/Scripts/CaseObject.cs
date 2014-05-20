using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public int offset;
	private GameObject uiThing;
	public int[] conditions;

	public CaseObject[] associatedObjects;

	public string mouseOverIcon = "Grab_Icon";
	public bool autoPickupPostConvo = false;

	void Start(){
		Debug.Log ("asda");
		Debug.Log (this.elementName);
		if (journal.inventory.Contain (this)) {	
			Debug.Log ("inside");
			DestroyObject (gameObject);
		}
		base.Init ();
		uiThing = GameObject.Find("Journal");

	}

	public void OnMouseEnter(){
		if (player != null)
			if (player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) 
		if (player.canWalk){
			clickedOnSomething = true;
			player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
		}
	}

	bool checkCondis(){

		bool fulfilled = true;

		for (int i = 0; i < conditions.Length; i++) {

			Debug.LogWarning (conditions[i] + ": " + Dialoguer.GetGlobalBoolean(i));
			if (!Dialoguer.GetGlobalBoolean(conditions[i])){
				fulfilled = false;
			}
			Debug.LogWarning(fulfilled);

		}
		return fulfilled;

	}

	public void pickUpItem(){

		player.stopMove ();
		clickedOnSomething = false;

		//Debug.LogError ("Inventory: " + journal.inventory[0]);
		GameManager.Instance.updateMouseIcon(mouseOverIcon);

		if (checkCondis()) {
			handleAssociated();
			collectItems();
			Dialoguer.StartDialogue(GameManager.pickUpConvo);
		} else if (myConvo != Convo.ch0none) {
			Dialoguer.StartDialogue ((int)myConvo);
			player.talking = true;
			player.canWalk = false;
		}

	}


	public void handleAssociated(){

		for (int i = 0; i < associatedObjects.Length; i++) {
			var temp = Instantiate(associatedObjects[i]) as CaseObject;
			temp.collectItems();
		}

	}

	public void collectItems(){
		journal.inventory.Add(this);
		uiThing.SendMessage("addObject", this);
		DestroyObject(gameObject);
	}

	void Update(){

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (clickedOnSomething) 
			onMouseMiss ();

		if (clickedOnSomething)
			if (pDist.isCloseEnough (player.transform.position))
				pickUpItem ();

		if (autoPickupPostConvo)
		if (pDist.isCloseEnough (player.transform.position) && checkCondis () && !player.talking) {
			handleAssociated();
			collectItems();
			Dialoguer.StartDialogue(GameManager.pickUpConvo);
		}
	}

}
