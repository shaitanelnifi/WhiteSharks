using UnityEngine;
using System.Collections;

public class ConditionAppearance : MonoBehaviour {

	public int getBoolID;
	public bool getBoolCondition;
	public double getPosition;
	public bool reverseType = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float oldX = (float)getPosition;
		float newX = 40.0f;

		if (Dialoguer.GetGlobalBoolean(getBoolID) == getBoolCondition) {
			if (!reverseType){
			renderer.enabled = false;
			} else {
				renderer.enabled = true;
			}
		}
		else {
			if (!reverseType){
				renderer.enabled = true;
			} else {
				renderer.enabled = false;
			}
		}
	}
}
