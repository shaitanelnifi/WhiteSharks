using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

	public List<string> names = new List<string>();
	public List<Sprite> icons = new List<Sprite>();
	public List<string> descriptions =  new List<string>();

	public int Count = 0;


	// Use this for initialization
	void Start () {

	}

	public void Add(CaseObject addMe){
		//Debug.LogError ("What Am I? " + addMe);
		if (!names.Contains (addMe.elementName)) {
			names.Add (addMe.elementName);
			icons.Add (addMe.profileImage);
			descriptions.Add (addMe.description);
			Count++;
		} else
			Debug.Log ("Object already held: " + addMe.elementName);

	}

	public string getName(int which){
		Debug.LogWarning (names.Count + " " + which);
		return names[which];
	}

	public Sprite getIcon(int which){
		Debug.LogWarning (icons.Count + " " + which);
		return icons[which];
	}

	public string getDescription(int which){
		Debug.LogWarning (descriptions.Count + " " + which);
		return descriptions[which];
	}
}
