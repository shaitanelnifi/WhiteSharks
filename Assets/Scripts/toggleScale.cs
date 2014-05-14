using UnityEngine;
using System.Collections;

public class toggleScale : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		playerScript playerObj = collider.gameObject.GetComponent<playerScript> ();

		if (playerObj != null) {
			playerObj.canScale = false;		
		
		}
		
	}
	void OnTriggerExit2D(Collider collider) {
		playerScript playerObj = collider.gameObject.GetComponent<playerScript> ();
		if (playerObj != null) {
			playerObj.canScale = true;		
			
		}
	}
}
