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
						initDialogue(0);
		}

		if (GameManager.dialogueJustFinished) {
			GameManager.dialogueJustFinished = false;
			change ();
		}

	}

	IEnumerator test(){
		yield return new WaitForSeconds (3);
		initDialogue (6);
	}

	void initDialogue(int num){
		Debug.Log ("aloha");
		Dialoguer.StartDialogue(num);
	}

	void change(){
		if (!theEnd){
			Debug.Log ("emagherd");
			r.sprite = 	next;
			StartCoroutine("test");
			theEnd = true;
		} else {
			Application.LoadLevel("stage1");
		}	
	}
	
}
