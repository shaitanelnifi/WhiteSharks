using UnityEngine;
using System.Collections;

public class MapClickable : Clickable {

	public Convo dialogueEnum;

	private bool doneTalking = false;

	/*public void Start() {
		//player = (playerScript)FindObjectOfType(typeof(playerScript));
		offset = GameManager.offset;
		//pDist = gameObject.GetComponent<distanceCheck>();
		mouseOverIcon = "Examine_Icon";
		defaultIcon = "Walk_Icon";
	}
	

	public void OnMouseEnter() {
		//Debug.LogWarning ("Stop? Go?: " + player.canWalk);
		if(!wall)
			//if(player.canWalk)
				GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	public void OnMouseExit() {
		GameManager.Instance.updateMouseIcon(defaultIcon);
		//Debug.Log ("Talking " + player.talking);
		/*if(player != null) {
			if(wall && !player.talking) {
				player.canWalk = true;
			}
		}
	}

	public void OnMouseDown() {
		if(Input.GetMouseButtonDown(0)) {
			/*if(player != null) {
				if(player.canWalk && !wall) {
					clickedOnSomething = true;
					player.setTarget(new Vector3(transform.position.x, transform.position.y, 0));
				}
				else if(!player.talking) {
					player.stopMove();
					player.canWalk = true;
				}
			}
			clickedOnSomething = true;
		}

	}

	public void onMouseMiss() {

		if(Input.GetMouseButton(0)) {
			//Debug.LogWarning("Checking click.");      
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null) {
				Debug.LogWarning("Ray hit object");
				if(hit.collider.gameObject != this.gameObject)
					clickedOnSomething = false;
			}
			else
				clickedOnSomething = false;
		}

	}

	public void startDialogue() {
		if(!wall) {
			Dialoguer.StartDialogue((int)diaNum + offset);
		}
		//player.stopMove();
		//SoundManager.Instance.StopWalk();
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
		clickedOnSomething = false;
		doneTalking = true;

		//player.talking = true;
	}

	IEnumerator DialogueDone() {
		while(!doneTalking) {
			yield return null;
		}
		Destroy(gameObject);
	}
	

	void Update() {


		//if(player == null)
		//	player = (playerScript)FindObjectOfType(typeof(playerScript));

		if(clickedOnSomething)
			//onMouseMiss();

		if(!wall) {
			//if(player.canWalk == true && clickedOnSomething)
			//	if(pDist.isCloseEnough(player.transform.position))
					startDialogue();
		}
		else if(wall && clickedOnSomething) {
			//player.stopMove();
			clickedOnSomething = false;
		}

	}
	void Start() {

	}*/

	void Update() {

	}

	public void OnMouseDown() {
		if(Input.GetMouseButtonDown(0)) {
			startDialogue();
		}

	}

	public void startDialogue() {
		if(!wall) {
			Dialoguer.StartDialogue((int)dialogueEnum);
		}
		StartCoroutine("DialogueDone");
		doneTalking = true;
	}


	

	IEnumerator DialogueDone() {
		while(!doneTalking || player.talking) {
			yield return null;
		}
		Destroy(gameObject);
	}


}
