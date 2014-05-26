using UnityEngine;
using System.Collections;

public class NetworkMap : MonoBehaviour {

	public Animator drone;
	public Animator jane;
	public GameObject cellDoor;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		drone.SetBool("DeactivatedDrone", Dialoguer.GetGlobalBoolean(4));
		if (Dialoguer.GetGlobalBoolean (3))
						Destroy (cellDoor);
		Debug.Log (Dialoguer.GetGlobalBoolean (3));
		if (Dialoguer.GetGlobalBoolean (3) && Dialoguer.GetGlobalBoolean(4)) {
			jane.SetBool("moveJane", true);
		}
	}
}
