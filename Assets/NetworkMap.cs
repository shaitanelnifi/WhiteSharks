using UnityEngine;
using System.Collections;

public class NetworkMap : MonoBehaviour {

	public Animator droneAnimator;
	public Animator janeAnimator;

	// Use this for initialization
	void Start () {
		janeAnimator.StopPlayback ();
		droneAnimator.StartPlayback ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Dialoguer.GetGlobalBoolean (3));
		if (Dialoguer.GetGlobalBoolean (3) && Dialoguer.GetGlobalBoolean(4)) {

			droneAnimator.StopPlayback();
			janeAnimator.StartPlayback();
		}
	}
}
