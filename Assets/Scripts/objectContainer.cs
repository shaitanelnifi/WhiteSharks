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
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (GameManager.Instance.defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) 
			if (player.canWalk == true)	
				clickedOnSomething = true;
	}

	// Use this for initialization
	void Start () {
		if (maxDist < GameManager.Instance.maxDist)
			maxDist = GameManager.Instance.maxDist;
		player = (playerScript) FindObjectOfType(typeof(playerScript));
		offset = GameManager.Instance.offset + 3;

	}

	void Update(){

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
		if (Vector3.Distance (player.transform.position, transform.position) <= maxDist && clickedOnSomething) {
			Dialoguer.StartDialogue(offset);
			player.setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
			player.canWalk = false;
			player.anim.SetFloat("distance", 0f);
			player.anim.SetBool("walking", false);
			OnMouseExit();
			clickedOnSomething = false;
			Instantiate(iHoldThis, new Vector3(transform.localPosition.x, transform.localPosition.y, -1), Quaternion.identity);
			Destroy(this.gameObject);
		}

	}
}
