using UnityEngine;
using System.Collections;

public class objectContainer : MonoBehaviour {

	public CaseObject iHoldThis;	//This is what is revealed when the container is clicked.

	//Mouse icon information
	private string mouseOverIcon = "Examine_Icon";	
	public float maxDist = 1f;
	public Convo convoID;
	private bool clickedOnSomething = false;
	private playerScript player;
	private distanceCheck pDist;
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (GameManager.Instance.defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) 
		if (player.canWalk){
			clickedOnSomething = true;
			player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
		}
	}

	// Use this for initialization
	void Start () {
		if (maxDist < GameManager.Instance.maxDist)
			maxDist = GameManager.Instance.maxDist;
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		pDist = gameObject.GetComponent<distanceCheck>();
        

		if (journal.inventory.Contain (iHoldThis)) {	
			Debug.Log ("inside");
			DestroyObject (gameObject);
		}
	}

	public void revealItem(){

		player.talking = true;
		player.stopMove ();
		SoundManager.Instance.StopWalk();
		Dialoguer.StartDialogue((int)convoID);
		clickedOnSomething = false;
		Instantiate(iHoldThis, new Vector3(transform.localPosition.x, transform.localPosition.y, -1), Quaternion.identity);
        //journal.SendMessage("addObject", iHoldThis);
		Destroy(this.gameObject);
		GameManager.Instance.updateMouseIcon(mouseOverIcon);

	}

	public void onMouseMiss(){
		
		if (Input.GetMouseButton (0)) {
			//Debug.LogWarning("Checking click.");      
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				Debug.LogWarning ("Ray hit object");
				if (hit.collider.gameObject != this.gameObject) 
					clickedOnSomething = false;
			} else 
				clickedOnSomething = false;
		}
		
	}

	void Update(){

		if (clickedOnSomething) 
			onMouseMiss ();

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (player.canWalk == true  && clickedOnSomething)
		if (pDist.isCloseEnough(player.transform.position)) 
			revealItem();

	}
}
