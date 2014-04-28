/*
player class. controls the movement of the main character.

changes: change scene depends on which door the player collide with. -John Mai 1/12/2014
 */ 

/*
note: add id 3 and another list for rooms


 */
using UnityEngine;
using System.Collections;
using Pathfinding;

public class playerScript : CaseElement {

	//public KeyCode moveLeft, moveRight, moveTop, moveBottom;
	public float baseSpeed;
	public float minSpeed;
	public Camera mainCam;
	public Transform mainChar;
	public bool facingLeft = true;
	public bool goingtLeft= true;
	public Animator anim;
	Vector3 targetPosition;
	Vector2 direction;
	Vector2 closestColl;
	public bool canWalk = true;
	public int walkWait = 0;
	public int currentRoom;
	private int counter = 0;
	public float[] scaleInfo = new float[4]{0f, 0f, 0f, 0f};
	public bool canScale = true;
	public bool talking = false;
	GameObject backEffect = null;

	int currentWayPoint;
	Seeker seeker;
	Path path;

	float maxWayPointDistance=0.01f;
	bool lastNode;



	void Start(){
		seeker = GetComponent<Seeker>();
		if(Application.loadedLevelName == "chapter1finbalcony"){
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/Birds"), SoundManager.SoundType.Sfx);
			SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		}
		if(Application.loadedLevelName == "chapter1finplaza") {
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/PlazaChatter"), SoundManager.SoundType.Sfx);
			SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		}
		if(Application.loadedLevelName == "chapter1finoffice" || Application.loadedLevelName == "chapter1finofficeend")
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/Music/Office"), SoundManager.SoundType.Sfx);
		if(Application.loadedLevelName == "chapter1finroom0" || Application.loadedLevelName == "chapter1finroom1" 
				|| Application.loadedLevelName == "chapter1finroom10")
			SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		anim = GetComponentInChildren<Animator>();
		Debug.Log (anim);
		//canWalk = true;
		canScale = true;

		float scale = calcScale ();
		transform.localScale = new Vector2 (scale, scale);
		//backEffect = GameObject.Find ("effect");
	}

/*	public void moveTarget(Vector2 adjust){
		targetPosition = targetPosition + adjust;
	}*/

	public void setTarget(Vector2 moveHere){
		targetPosition = moveHere;
		}

	public void OnPath(Path p){
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
		else{
			Debug.Log(p.error);
		}
	}


	void FixedUpdate(){	
		lastNode = false;
		float distance;
		//float modSpeed = Mathf.Sqrt(transform.localScale.y) * baseSpeed;
		float modSpeed = (Mathf.Log(transform.localScale.y) + 1) * baseSpeed;
		if (modSpeed < minSpeed){
			modSpeed = minSpeed;
		}
		if(Input.GetMouseButtonDown(0))
		if (canWalk){
			//Debug.Log("Pressed left click.");
			//get mouse clicked location and convert them to world point.
			targetPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0 );
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, camera.nearClipPlane));
			targetPosition.x = mousePosition.x;
			targetPosition.y = mousePosition.y;

			seeker.StartPath (transform.position,targetPosition, OnPath);
		}
		if(path == null||currentWayPoint> path.vectorPath.Count){
			return;
		}
		//Vector3 dir = (path.vectorPath[currentWayPoint]-transform.position).normalized *modSpeed*Time.deltaTime;
		distance = Vector2.Distance (transform.position, targetPosition);

		if(currentWayPoint < path.vectorPath.Count-1){
			//Debug.Log("count: " +path.vectorPath.Count);
			if(Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1.5f){
				currentWayPoint++;	
				//Debug.Log("current way point:"+currentWayPoint);
			}
		}
		//Debug.Log("Last way point222:"+currentWayPoint);
		if(distance >0.1f){
			Vector3 dir = (path.vectorPath[currentWayPoint]-transform.position).normalized *5f*Time.deltaTime;
			transform.position = transform.position + dir;
			if(Mathf.Abs(distance) > 1 && !SoundManager.Instance.isWalking) SoundManager.Instance.WalkSound();
			else if(Mathf.Abs(distance) <= 1 && SoundManager.Instance.isWalking) SoundManager.Instance.StopWalk();
		}
		if((targetPosition.x - transform.position.x)<0 ){
			goingtLeft = true;
		}
		else if((targetPosition.x - transform.position.x)>0 ) {	
			goingtLeft = false;
		}
		if(facingLeft!=goingtLeft&& Mathf.Abs(distance)>1){
			transform.Rotate(0,180,0);
			facingLeft = !facingLeft;
			//goingtLeft = !goingtLeft;
		}
		anim.SetFloat("distance", distance);
		if(canScale){
			float scale = calcScale ();
			transform.localScale = new Vector2 (scale, scale);
		}
		if (walkWait > 0)
			walkWait--;
	}

	//Calculate the proper scaling for the avatar using scene traits
	private float calcScale(){
		float currY = transform.position.y;
		//Debug.LogError (currentRoom);
		
		float scale = 0f;
		float slope = -1 * (scaleInfo [1] - scaleInfo [0]) / (scaleInfo [3] - scaleInfo [2]);
		
		scale = slope * (currY - scaleInfo[3]) + scaleInfo[0];

		//Ensures they don't go too small or too big.
		if (scale > scaleInfo[1])
			scale = scaleInfo[1];
		else if (scale < scaleInfo[0])
			scale = scaleInfo[0];

		return scale;

	}

	public void stopMove(){

		setTarget(new Vector2(player.transform.position.x, player.transform.position.y));
		canWalk = false;
		anim.SetFloat("distance", 0f);

	}

	//change scene when collide with door
	void OnTriggerEnter2D(Collider2D collider){

		//canScale = false;
		//int coolDown = 30;
		DoorScript doorObj = collider.gameObject.GetComponent<DoorScript> ();

		if (doorObj != null){ 
			gameObject.collider2D.enabled = false;
			renderer.enabled = false;
			if (backEffect == null){
				Debug.Log("Calling effect");
				backEffect = (GameObject)Instantiate(Resources.Load("blackScreen"));
				Debug.Log(backEffect);
			}
		}
		else {
			renderer.enabled = true;
		}
		string temp;
		int tempIndex;
		if (doorObj != null) {
			doorObj.useDoor();
		}
	}

	//flips the sprite or animation
	void Flip(){
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void DestoryPlayer(){
		Destroy (Scene.player);
	}
}
