using UnityEngine;
using System.Collections;

public class TurnOffSmooth : MonoBehaviour {
	GameObject playerObj;
	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindWithTag ("Player");
		if(playerObj != null){
			playerScript.turnOffSmoothMod ();
		}
	
	}

	void Update(){
		if (playerObj == null) {
			playerObj = GameObject.FindWithTag ("Player");	
			playerScript.turnOffSmoothMod ();
		}
	}	

}
