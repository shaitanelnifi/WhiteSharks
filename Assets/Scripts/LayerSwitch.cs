using UnityEngine;
using System.Collections;

public class LayerSwitch : MonoBehaviour {
	GameObject playerObj;
	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (playerObj == null) {
			playerObj = GameObject.FindWithTag ("Player");
		}
		else{
			if(transform.position.y > playerObj.transform.position.y){
				renderer.sortingLayerID = playerObj.renderer.sortingLayerID -1;
			}
			else{
				renderer.sortingLayerID = playerObj.renderer.sortingLayerID +1;
			}

		}

	}
}
