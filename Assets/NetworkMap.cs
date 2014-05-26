using UnityEngine;
using System.Collections;

public class NetworkMap : MonoBehaviour {

	public Animator drone;
	public Animator jane;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Dialoguer.GetGlobalBoolean (3));
		if (Dialoguer.GetGlobalBoolean (3) && Dialoguer.GetGlobalBoolean(4)) {
			jane.SetBool("moveJane", true);
			drone.SetBool("DeactivatedDrone", true);
		}
	}
}
