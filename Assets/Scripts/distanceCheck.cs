using UnityEngine;
using System.Collections;

public class distanceCheck : MonoBehaviour {

	private playerScript player;
	public float maxDist;

	// Use this for initialization
	void Start () {
	
		player = (playerScript) FindObjectOfType(typeof(playerScript));

	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
			player = (playerScript)FindObjectOfType (typeof(playerScript));
	}

	public bool isCloseEnough(Vector3 toMe){

		return (Vector3.Distance(toMe, transform.position) <= maxDist);

	}
}
