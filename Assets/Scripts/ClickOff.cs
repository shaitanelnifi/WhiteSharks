using UnityEngine;
using System.Collections;

public class ClickOff : MonoBehaviour {

	public float progress = 50f;
	public float progressNeeded = 100f;
	public Vector2 pos = new Vector2(256, 96);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarMay;
	public Texture2D progressBarFounder;
	public float waitThisLong;

	public string nextScene;
	public Convo rewardConversation;

	private bool success = false;
	private bool playingConv = false;

	void Start(){

		pos.x = Screen.width / 2 - progressBarMay.width / 2;

	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, progressBarFounder.width, 
		                         progressBarFounder.height), progressBarFounder);

		GUI.DrawTexture(new Rect(pos.x, pos.y, progressBarMay.width * Mathf.Clamp01(progress / progressNeeded), 
		                         progressBarMay.height), progressBarMay);

		GUI.DrawTexture(new Rect(Screen.width / 2 - progressBarEmpty.width / 2, 0, progressBarEmpty.width, 
		                         progressBarEmpty.height), progressBarEmpty);
	} 

	void playReward(){
	}
	
	void Update()
	{

		if (progress < progressNeeded && !success) {
			if (Input.GetMouseButtonDown (0)) {
					progress++;
			}

			progress -= Time.deltaTime * 2;
		} else {
			success = true;
		}

		if (success && !playingConv) {
			playingConv = true;
			Dialoguer.StartDialogue ((int)rewardConversation);
		} else if (GameManager.dialogueJustFinished) {
			if (waitThisLong <= 0){
			GameManager.dialogueJustFinished = false;
			Application.LoadLevel(nextScene);
			} else {
				waitThisLong-= Time.deltaTime;
			}
		}
	}
}
