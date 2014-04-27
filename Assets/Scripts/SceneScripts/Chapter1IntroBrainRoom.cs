using UnityEngine;
using System.Collections;

public class Chapter1IntroBrainRoom : MonoBehaviour {

	bool done;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("ALIVE");
		StartCoroutine ("wait");
	}

	void OnGUI() {
		GUI.Box(new Rect(0,0,Screen.width/2,Screen.height/2),"DIALOGUER CONVERSATION FOR BRAIN ROOM.");
		if (done) {
			GUI.Box(new Rect(0,0,Screen.width/2,Screen.height/2),"DONE.");
		}
	}

	IEnumerator wait(){
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/" + GameManager.episodeStartLevels[GameManager.currentEpisode]));
		//Application.LoadLevel (GameManager.episodeStartLevels[GameManager.currentEpisode]);

		yield return new WaitForSeconds (3f);
		done = true;
		GameManager.Instance.playerInScene = true;
		Application.LoadLevel("chapter1introfinbalcony");
	}
}
