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
	

	//scaling variables
	public float[] scaleInfo = new float[4]{0f, 0f, 0f, 0f};
	public bool canScale = true;
	//background effect
	GameObject backEffect = null;

	//animation variables
	public Animator anim;
	public bool canWalk = true;
	public int walkWait = 0;
	public bool facingLeft = true;
	public bool goingtLeft= true;
	public bool talking =false;

	//path finding variables
	float nodeTriggerDistace = 0.5f;
	int currentWayPoint;
	Seeker seeker;
	Path path;
	Vector3 targetPosition;
	public float baseSpeed;


	//sets up scale, path, and animation stuff
	void Start(){

		seeker = GetComponent<Seeker>();
		SoundManager.Instance.CanWalk();
		if(Application.loadedLevelName == "chapter1finbalcony") {
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/Birds"), SoundManager.SoundType.Sfx);
			SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		}
		if(Application.loadedLevelName == "chapter1finplaza") {
			SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/PlazaChatter"), SoundManager.SoundType.Sfx);
		}

		anim = GetComponentInChildren<Animator>();
		canScale = true;

		float scale = calcScale ();
		transform.localScale = new Vector2 (scale, scale);

	}


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

		Debug.DrawLine (transform.position, targetPosition, Color.red);

		//varaible for distance between target location and where the player currently is
		float distance;

		//if left mouse is pressed
		if (Input.GetMouseButtonDown (0)) {
			//if within camera screen
			if((Input.mousePosition.x <= Screen.width)  && (Input.mousePosition.x >= 0) 
			   &&(Input.mousePosition.y <= Screen.height) && (Input.mousePosition.y >= 0)){
				   //if player is allow to walk, then convert mouse position
				   // and search for path
				   if(canWalk) {
					   
					   targetPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
					   Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, camera.nearClipPlane));
					   targetPosition.x = mousePosition.x;
					   targetPosition.y = mousePosition.y;
					   SoundManager.Instance.CanWalk();
					   seeker.StartPath(transform.position, targetPosition, OnPath);
				   }
			}
		}

		//if theres no path or already at the last node of path, break
		if(path == null||currentWayPoint> path.vectorPath.Count){
			SoundManager.Instance.StopWalk();
			anim.SetFloat("distance", 0f);
			return;
		}

		distance = Vector2.Distance (transform.position, targetPosition);

		//if the current node is not he last node and 
		//if the distance bewteen the current node and the player is very small
		//then go to the next node
		if(currentWayPoint < path.vectorPath.Count-1){
			if(Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < nodeTriggerDistace){
				currentWayPoint++;	
			}
		}

		//move towards next node
		if(distance >0.1f){
			Vector3 dir = (path.vectorPath[currentWayPoint]-transform.position).normalized *baseSpeed*Time.deltaTime;
			Vector3 oldPosition = transform.position;
			transform.position = transform.position + dir;

		
			if(Mathf.Abs(distance) > nodeTriggerDistace && !SoundManager.Instance.isWalking) SoundManager.Instance.WalkSound();
			else if(Mathf.Abs(distance) <= nodeTriggerDistace && SoundManager.Instance.isWalking) SoundManager.Instance.StopWalk();
		}

		//check if the player is going to the left or right
		if((targetPosition.x - transform.position.x)<0 ){
			goingtLeft = true;
		}
		else if((targetPosition.x - transform.position.x)>0 ) {	
			goingtLeft = false;
		}

		//determines which direction the player is facing
		if(facingLeft!=goingtLeft&& Mathf.Abs(distance)>.1f){
			transform.Rotate(0,180,0);
			facingLeft = !facingLeft;
			//goingtLeft = !goingtLeft;
		}

		//play the animation
		anim.SetFloat("distance", distance);

		//scales as the character walks
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
		//Debug.LogWarning ("STOP");
		setTarget(new Vector2(transform.position.x, transform.position.y));
		canWalk = false;
		SoundManager.Instance.CantWalk();
		if (anim != null)
		anim.SetFloat("distance", 0f);

	}

	//change scene when collide with door
	void OnTriggerEnter2D(Collider2D collider){

		DoorTrigger (collider);
	
	}

	void scaleTrigger(Collider2D collider){
		toggleScale scaleToggleObj = collider.gameObject.GetComponent<toggleScale> ();
		if (scaleToggleObj != null) {
			canScale = !canScale;
		}
	}


	void DoorTrigger(Collider2D collider ){

		DoorScript doorObj = collider.gameObject.GetComponent<DoorScript> ();
		
		if (doorObj != null){ 
			canScale = false;
			gameObject.collider2D.enabled = false;
			renderer.enabled = false;
			if (backEffect == null){
				//Debug.Log("Calling effect");
				backEffect = (GameObject)Instantiate(Resources.Load("blackScreen"));
				//Debug.Log(backEffect);
			}
		}
		else {
			renderer.enabled = true;
		}
		
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
