using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	SpriteRenderer r;
	Color a;
	bool started = false;
	bool theEnd = false;
	public Sprite next;
	// Use this for initialization
	void Start () {
		r = GetComponent<SpriteRenderer> ();
		a = r.color;
		a.a = 0;
		r.color = a;
	}
	
	// Update is called once per frame
	void Update () {
		if (r.color.a < 1) {
						a.a += 0.01f;
						r.color = a;
				} else if (!started) {
						started = true;
						initDialogue();
		}

		if (GameManager.dialogueJustFinished && !theEnd) {
			GameManager.dialogueJustFinished = false;
			if (!theEnd){
				r.sprite = 	next;
				Dialoguer.StartDialogue (0);
				theEnd = true;
			} else {
				Application.LoadLevel("scene1");
			}
		}

	}

	void initDialogue(){
		Dialoguer.StartDialogue(0);
	}

}
