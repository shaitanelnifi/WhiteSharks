using UnityEngine;
using System.Collections;

public class NetworkMap : MonoBehaviour {

	public Animator drone;
	public Animator jane;
	public GameObject cellDoor;


	protected playerScript player;

	// Use this for initialization
	void Start () {
		player = (playerScript)FindObjectOfType(typeof(playerScript));
	}
	
	// Update is called once per frame
	void Update () {
		drone.SetBool("DeactivatedDrone", Dialoguer.GetGlobalBoolean(4));
		if (Dialoguer.GetGlobalBoolean (3))
						Destroy (cellDoor);
		jane.SetBool("moveJane", Dialoguer.GetGlobalBoolean (5));
		if(GameManager.dialogueJustFinished) player.talking = false;
	}
}
