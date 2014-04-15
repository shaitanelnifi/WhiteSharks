/*
Get the main character and store it to GameManager
*/
using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

	public string character;

	public void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			GameManager.Instance.SetMainCharacter(character);
			//load level 1
			SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/" + GameManager.episodeStartLevels[GameManager.currentEpisode]));
			Application.LoadLevel (GameManager.episodeStartLevels[GameManager.currentEpisode]);
			//TEST
			//GameManager.Instance.playerInScene = true; 
			//Application.LoadLevel ("finroom");
			//END TEST
			

		}
	}
}
