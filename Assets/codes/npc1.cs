using UnityEngine;
using System.Collections;

public class npc1 : MonoBehaviour {

	private static bool click = false;
	public static bool Click{
		get{
			return click;
		}
		set{
			click = value;
		}

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(Input.GetMouseButton(0))
			click = true;	
	}
}
