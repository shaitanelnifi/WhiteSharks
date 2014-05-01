using UnityEngine;
using System.Collections;

public abstract class CaseElement : MonoBehaviour {

	//Every case element contains these bits of information
	public string elementName;	//Its name
	public string description;	//A text description of it
	public Sprite profileImage; //Its base image (square mug-shot)

	public bool visible=false;			//Is the element visible in the journal?

	protected playerScript player;
	protected distanceCheck pDist;
	public bool clickedOnSomething;
	public float maxDist = 1f;

	public Convo myConvo = Convo.none;

	void Start (){
		Init ();
	}

	protected void Init(){

		player = (playerScript) FindObjectOfType(typeof(playerScript));
		profileImage = gameObject.GetComponent<SpriteRenderer> ().sprite;
		pDist = gameObject.GetComponent<distanceCheck>();
		if (maxDist < GameManager.Instance.maxDist)
			maxDist = GameManager.Instance.maxDist;

	}

	public void onMouseMiss(){
		
		RaycastHit hit = new RaycastHit ();        
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit))
			if (hit.collider.gameObject != this.gameObject)
				clickedOnSomething = false;
		
	}

	void Update(){
		
		if (player == null)
			player = (playerScript) FindObjectOfType(typeof(playerScript));
		
	}

	//Setters for all Case Element fields
	public void setElementName(string someName){
		elementName = someName;
	}

	public void setDescription(string someDesc){
		description = someDesc;
	}

	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (GameManager.Instance.defaultIcon);
	}

	public void setProfileImage(Sprite someImage){
		profileImage = someImage;
	}


	//Getters for all Case Element fields
	public string getElementName( ){
		return elementName;
	}
	
	public string getDescription( ){
		return description;
	}


	public Sprite getProfileImage( ){
		return profileImage;
	}

	public bool isVisible(){
		return visible;
	}

	public void setVisible(bool v){
		visible = v;
	}
}
