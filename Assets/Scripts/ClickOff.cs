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
	public Convo failConversation;

	private bool success = false;
	private bool playingConv = false;
	private bool started = false;
	private bool failure = false;

	void Start(){

		pos.x = Screen.width / 2 - progressBarMay.width / 2;
		if (failConversation == Convo.ch0none)
			started = true;
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

		if (started) {

			if (progress <= 0f){
				failure = true;
			} else if (progress < progressNeeded && !success) {
					if (Input.GetMouseButtonDown (0)) {
							progress++;
					}

					progress -= Time.deltaTime * 2;
			} else if (progress >= progressNeeded && !success){
					success = true;
			}
		} else if (GameManager.dialogueJustFinished) {
			started = true;
		}

		if (success && !playingConv) {
			if (waitThisLong <= 0){
				playingConv = true;
				Debug.LogWarning("YAY");
				if (nextScene != "")
				Application.LoadLevel(nextScene);
			} else {
				waitThisLong-= Time.deltaTime;
			}
		} else if (failure && !playingConv){
				playingConv = true;
				Debug.LogWarning("AWW");
				if (failConversation != Convo.ch0none)
					Dialoguer.StartDialogue((int)failConversation);
		} else if (playingConv && GameManager.dialogueJustFinished){
				Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
