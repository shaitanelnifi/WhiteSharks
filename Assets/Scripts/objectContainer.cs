using UnityEngine;
using System.Collections;

public class objectContainer : MonoBehaviour {

	public CaseObject iHoldThis;	//This is what is revealed when the container is clicked.

	//Mouse icon information
	private string mouseOverIcon = "Examine_Icon";	
	public int offset;
	public float maxDist = 1f;
	private bool clickedOnSomething = false;
	private playerScript player;
	private distanceCheck pDist;

    private GameObject journal;
	
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
		offset = GameManager.Instance.offset + 3;
		pDist = gameObject.GetComponent<distanceCheck>();
        journal = GameObject.Find("UI Root");
	}

	public void revealItem(){

		player.talking = true;
		player.stopMove ();
		SoundManager.Instance.StopWalk();
		Dialoguer.StartDialogue(offset);
		clickedOnSomething = false;
		Instantiate(iHoldThis, new Vector3(transform.localPosition.x, transform.localPosition.y, -1), Quaternion.identity);
        journal.SendMessage("addObject", iHoldThis);
		Destroy(this.gameObject);
		GameManager.Instance.updateMouseIcon(mouseOverIcon);

	}

	public void onMouseMiss(){
		
		RaycastHit hit = new RaycastHit ();        
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit))
			if (hit.collider.gameObject != this.gameObject)
				clickedOnSomething = false;
		
	}

	void Update(){

		if (Input.GetMouseButton (0))
			onMouseMiss();

		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));

		if (player.canWalk == true  && clickedOnSomething)
		if (pDist.isCloseEnough(player.transform.position)) 
			revealItem();

	}
}
