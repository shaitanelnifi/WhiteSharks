using UnityEngine;
using System.Collections;

public class ExitConversation : MonoBehaviour {

	public GameObject exitButton;

	// Use this for initialization
	void Start () {
		UIEventListener.Get (exitButton).onClick += this.onClick;
	}

	void onClick(GameObject button)
	{
		if (button == exitButton)
		{
			Debug.Log ("Exit Dialogue");
			Dialoguer.EndDialogue();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
