using UnityEngine;
using System.Collections;

public class Disableable : MonoBehaviour {


	public GameObject[] dis;

	// Use this for initialization
	void Start () {
		dis[1].gameObject.renderer.enabled = false;
		dis[2].gameObject.renderer.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
