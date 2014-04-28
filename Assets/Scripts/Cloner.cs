using UnityEngine;
using System.Collections;

public class Cloner : MonoBehaviour {

	public GameObject toClone;

	// Use this for initialization
	void Start () {
		Instantiate (toClone, transform.position, transform.rotation);
		Destroy (gameObject);
	}

}
