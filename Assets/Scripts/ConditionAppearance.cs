using UnityEngine;
using System.Collections;

public class ConditionAppearance : MonoBehaviour {

	public int getBoolID;
	public bool getBoolCondition;
	public double getPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float oldX = (float)getPosition;
		float newX = 40.0f;

		if (Dialoguer.GetGlobalBoolean(getBoolID) == getBoolCondition) {
			//transform.position = new Vector3(oldX, transform.position.y);
			renderer.enabled = false;
		}
		else {
			//transform.position = new Vector3(newX, transform.position.y);
			renderer.enabled = true;
		}
	}
}
