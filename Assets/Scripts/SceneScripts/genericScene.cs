using UnityEngine;
using System.Collections;

public class genericScene : MonoBehaviour {
	//Implement when dialoguer/animation ends, go to first scene.

	public string debugMe;
	public AudioClip playMe;
	public float waitThisLong;
	public string whatCharacter;
	public string nextLevel;
	public bool isTherePlayer;
	public Vector2 spawnHereAfter;

	private bool done = false;
	public bool needGUI = false;

	
	// Use this for initialization
	void Start () {
		GameManager.dialogueJustFinished = false;
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ("wait");
		Debug.Log ("ALIVE");
	}

	void OnGUI() {
		if (needGUI) {
			GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "DIALOGUER CONVERSATION FOR BRAIN ROOM.");
			if (done) {
					GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "DONE.");
			}
		}
	}

	IEnumerator wait(){
		SoundManager.Instance.Play2DMusic(playMe);
		Debug.Log (debugMe);
		yield return new WaitForSeconds (waitThisLong);
		GameManager.Instance.playerInScene = isTherePlayer;
		done = true;
		if (isTherePlayer) {
			GameManager.Instance.SetMainCharacter (whatCharacter);
			GameManager.Instance.SetNextX (spawnHereAfter.x);
			GameManager.Instance.SetNextX (spawnHereAfter.y);
		}
		Application.LoadLevel(nextLevel);
	}
}
