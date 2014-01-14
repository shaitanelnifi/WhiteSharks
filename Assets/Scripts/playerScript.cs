/*
player class. controls the movement of the main character.

changes: change scene depends on which door the player collide with. -John Mai 1/12/2014
 */ 
using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public KeyCode moveLeft, moveRight, moveTop, moveBottom;
	public float speed = 8f;
	public Camera mainCam;
	public Transform mainChar;
	public float sceneX, sceneY, sceneZ;
	public float newX, newY;
	public float playerCurrentY = -2.083729f;

	////Move the camera to the next scene when collides with the door object.
	void OnTriggerEnter2D(Collider2D collider){
		doorScript doorObj = collider.gameObject.GetComponent<doorScript> ();
		if (doorObj != null) {

			newX = doorObj.newX;
			newY = doorObj.newY;
			sceneX = doorObj.sceneX;
			sceneY = doorObj.sceneY;
			sceneZ = doorObj.sceneZ;

			//move the player to the next scene.
			Vector3 temp = transform.position; 
			temp.x = newX;
			temp.y = newY;
			mainChar.position = temp;
			//move the camera to the next scene
			Vector3 temp2 = new Vector3(sceneX,sceneY,sceneZ);
			mainCam.transform.position = temp2;
		}
	}

	// Update is called once per frame
	void Update () {
		//inputs
		if(Input.GetKey(moveLeft))
			transform.Translate(new Vector3(1,0, 0)* -speed * Time.deltaTime);
		if(Input.GetKey(moveRight))
			transform.Translate(new Vector3(1,0, 0)* speed * Time.deltaTime);
		if(Input.GetKey(moveTop)){
			transform.Translate(new Vector3(0,1, 0)* speed * Time.deltaTime);
		}
		if(Input.GetKey(moveBottom)){
			transform.Translate(new Vector3(0,1, 0)* -speed * Time.deltaTime);
		}

	}
}
