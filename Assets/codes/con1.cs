using UnityEngine;
using System.Collections;

public class con1 : MonoBehaviour {
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
		bool flag = npc1.Click;
		if (flag){
			npc1.Click = false;
			click = true;
		}
		if (click) {
			renderer.enabled = true;
			collider.enabled = true;
		}
	}
	void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			click = false;
			renderer.enabled = false;
			collider.enabled = true;
		}
	}
}
