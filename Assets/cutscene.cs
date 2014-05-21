using UnityEngine;
using System.Collections;

public class cutscene : MonoBehaviour {

	public Animator a;
	public string varName;

	// Use this for initialization
	void Start () {
		if (string.IsNullOrEmpty (varName)) {
			varName = "narrationBG";		
		}
	}
	
	// Update is called once per frame
	void Update () {
		a.SetFloat (varName, Dialoguer.GetGlobalFloat (2));
	}
}
