using UnityEngine;
using System.Collections;

public class cutscene : MonoBehaviour {

	public Animator a;
	public string varName;
	public bool isBool;

	// Use this for initialization
	void Start () {
		if (string.IsNullOrEmpty (varName)) {
			varName = "narrationBG";		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isBool)
			a.SetBool (varName, Dialoguer.GetGlobalBoolean (6));
		else
			a.SetFloat (varName, Dialoguer.GetGlobalFloat (2));
	}
}
