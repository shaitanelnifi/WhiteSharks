/*
player class. controls the movement of the main character.

 */ 
using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public KeyCode moveLeft;
	public KeyCode moveRight;
	public float speed = 8f;
	public Camera mainCam;
	public Transform mainChar;
	public float scene2X, scene2Y, scene2Z;
	public float newX, newY;

	////Move the camera to the next scene when collides with the door object.
	void OnTriggerEnter2D(Collider2D collider){
		doorScript doorObj = collider.gameObject.GetComponent<doorScript> ();
		if (doorObj != null) {
			//move the player to the next scene.
			Vector3 temp = transform.position; 
			temp.x = newX;
			temp.y = newY;
			mainChar.position = temp;
			//move the camera to the next scene
			Vector3 temp2 = new Vector3(scene2X,scene2Y,scene2Z);
			mainCam.transform.position = temp2;
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(moveLeft))
			transform.Translate(new Vector3(1,0, 0)* -speed * Time.deltaTime);
		if(Input.GetKey(moveRight))
			transform.Translate(new Vector3(1,0, 0)* speed * Time.deltaTime);
	}
}
