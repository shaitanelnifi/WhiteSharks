using UnityEngine;
using System.Collections;

public class objectContainer : MonoBehaviour {

	public CaseObject iHoldThis;	//This is what is revealed when the container is clicked.

	//Mouse icon information
	private string mouseOverIcon = "Examine_Icon";	
	public int offset;
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (GameManager.Instance.defaultIcon);
	}

	public void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			Dialoguer.StartDialogue(offset);
			
			playerScript temp = (playerScript) FindObjectOfType(typeof(playerScript));
			temp.canWalk = false;
			temp.anim.SetBool("walking", false);

			Instantiate(iHoldThis, new Vector3(transform.localPosition.x, transform.localPosition.y, -1), Quaternion.identity);

			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		offset = GameManager.Instance.offset + 3;
	}
}
