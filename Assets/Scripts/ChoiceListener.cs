using UnityEngine;
using System.Collections;

public class ChoiceListener : MonoBehaviour {

	public GameObject choice1;
	public GameObject choice2;
	public GameObject choice3;
	public GameObject choice4;
	public GameObject choice5;
	public GameObject choice6;

	// Use this for initialization
	void Start () {
		UIEventListener.Get (choice1).onClick += this.onClick;
		UIEventListener.Get (choice2).onClick += this.onClick;
		UIEventListener.Get (choice3).onClick += this.onClick;
		UIEventListener.Get (choice4).onClick += this.onClick;
		UIEventListener.Get (choice5).onClick += this.onClick;
		UIEventListener.Get (choice6).onClick += this.onClick;
	}

	void onClick(GameObject button)
	{
		if (button == choice1) {
			Debug.Log ("Choice1");
			Dialoguer.ContinueDialogue(0);
		}
		if (button == choice2) {
			Debug.Log ("Choice2");
			Dialoguer.ContinueDialogue(1);
		}
		if (button == choice3) {
			Debug.Log ("Choice3");
			Dialoguer.ContinueDialogue(2);
		}
		if (button == choice4) {
			Debug.Log ("Choice4");
			Dialoguer.ContinueDialogue(3);
		}
		if (button == choice5) {
			Debug.Log ("Choice5");
			Dialoguer.ContinueDialogue(4);
		}
		if (button == choice6) {
			Debug.Log ("Choice6");
			Dialoguer.ContinueDialogue(5);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
