using UnityEngine;
using System.Collections;

public class trapdoorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("test");
		this.renderer.enabled = Dialoguer.GetGlobalBoolean(6);
	}
}
