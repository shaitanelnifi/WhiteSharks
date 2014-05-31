using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public int pickUpConvo;
	private GameObject uiThing;
	private bool tookItem = false;
	public int[] conditions;

	public CaseObject[] associatedObjects;

	public string mouseOverIcon = "Grab_Icon";
	public bool autoPickupPostConvo = false;

	void Start(){


		if (journal.inventory.Contain (this)) {	
			DestroyObject (gameObject);
		}
		base.Init ();
		uiThing = GameObject.Find("Journal");

		if (GameManager.pickUpConvo != pickUpConvo)
			GameManager.pickUpConvo = pickUpConvo;

		if (profileImage == null)
			profileImage = GetComponent<SpriteRenderer> ().sprite;
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
			//player.stopMove ();
			player.talking = true;
			Dialoguer.StartDialogue((int)myConvo);
		} else if (myConvo != Convo.ch0none) {
			player.talking = true;
			//player.stopMove ();
			Dialoguer.StartDialogue ((int)myConvo);
			tookItem = true;
		}

	}


	public void handleAssociated(){

		for (int i = 0; i < associatedObjects.Length; i++) {

			var temp = Instantiate(associatedObjects[i]) as CaseObject;
			Debug.LogError(temp.elementName + i.ToString());
			journal.inventory.Add(temp);
			uiThing.SendMessage("addObject", temp);
			Destroy(temp.gameObject);
		}

	}

	public void collectItems(){
		journal.inventory.Add(this);
		uiThing.SendMessage("addObject", this);
		Destroy(this.gameObject);
	}

	void Update(){

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (clickedOnSomething) 
			onMouseMiss ();

		if (clickedOnSomething)
			if (pDist.isCloseEnough (player.transform.position))
				pickUpItem ();

		if (autoPickupPostConvo && GameManager.dialogueJustFinished && checkCondis() && !clickedOnSomething)
		if (pDist.isCloseEnough (player.transform.position) && !player.talking && tookItem) {
			handleAssociated();
			collectItems();
			player.stopMove ();
			player.talking = true;
			Dialoguer.StartDialogue(pickUpConvo);
			autoPickupPostConvo = false;
		}
	}

}
