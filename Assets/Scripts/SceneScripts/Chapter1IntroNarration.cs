using UnityEngine;
using System.Collections;

public class Chapter1IntroNarration : MonoBehaviour {
	//Implement when dialoguer/animation ends, go to first scene.


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ("wait");
		Debug.Log ("ALIVE");
	}

	IEnumerator wait(){
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Fin"));
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/" + GameManager.episodeStartLevels[GameManager.currentEpisode]));
		//Application.LoadLevel (GameManager.episodeStartLevels[GameManager.currentEpisode]);
		Debug.Log ("HEY");
		yield return new WaitForSeconds (3f);
		GameManager.Instance.playerInScene = false;
		GameManager.Instance.SetMainCharacter("Frank");
		Application.LoadLevel("chapter1introbrainroom");
	}
}
