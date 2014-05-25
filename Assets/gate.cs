using UnityEngine;
using System.Collections;

public class gate : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Dialoguer.GetGlobalBoolean(0)) {
			Destroy(gameObject);
		}
	}
}
