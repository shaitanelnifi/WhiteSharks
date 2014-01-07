using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	// Use this for initialization
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public float speed = 0.5f;
	public Camera mainCam;
	public Transform player;


	void OnTriggerEnter2D(Collider2D collider){
		activate act = collider.gameObject.GetComponent<activate> ();
		if (act != null) {
			Vector3 temp = transform.position; 
			temp.x = -10.86f;
			temp.y = -1.217f;
			player.position = temp;
			//Vector3 temp2 = transform.position; 
			Vector3 temp2 = new Vector3(-17.873f,-0.9213791f,-10f);
			mainCam.transform.position = temp2;

			//mainCam.camera.transform.position.x = -17.873;

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
