using UnityEngine;
using System.Collections;

public class cutscene : MonoBehaviour {

	public Animator a;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		a.SetFloat ("narrationBG", Dialoguer.GetGlobalFloat (2));
	}
}
