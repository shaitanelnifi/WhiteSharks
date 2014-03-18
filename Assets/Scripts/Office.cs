using UnityEngine;
using System.Collections;


/*
 * This class controls the behaviour of the office for the tutorial.
 * We'll probably want to change the way it's implemented later.
 */
public class Office : MonoBehaviour {
	
	public bool correctAnswer = false;
	private bool started = false;
	public bool may = true;
	public Texture2D maySprite;
	public Texture2D alexiaSprite;

	// Use this for initialization
	void Start () {
		GameManager.Instance.playerInScene = false;
		GameManager.dialogueJustFinished = false;
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/Office"));
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.firstTimeOffice && !started) {
			Debug.Log("Hey");
			StartCoroutine ("firstDialogue");
		} else if (!GameManager.firstTimeOffice && !started) {
			Debug.Log("NO Hey");
			StartCoroutine("secondDialogue");
		}

		if (GameManager.dialogueJustFinished) {
			GameManager.dialogueJustFinished = false;
			if (GameManager.may){
				GameManager.firstTimeOffice = false;
				GameManager.may = false;
				StartCoroutine ("firstDialogueB");
			}
			else{
				StartCoroutine("goToPlaza");
			}
		} //else if (GameManager.dialogueJustFinished && !firstTime){ 
			//We have to add something that checks if you've answered correctly
			//if (correctAnswer){
			//	GameManager.Instance.finishEpisode();
			//} else {
			//	StartCoroutine("goToPlaza");
			//}
		//}
	}

	public IEnumerator firstDialogue(){
		started = true;
		DialogueGUI dGUI = GameManager.Instance.GetComponent<DialogueGUI>();
		dGUI.setTargetTex(alexiaSprite);
		dGUI.setMainTex (maySprite);
		yield return new WaitForSeconds (1.5f);
		Dialoguer.StartDialogue(7);
	}

	public IEnumerator firstDialogueB(){
		started = true;
		DialogueGUI dGUI = GameManager.Instance.GetComponent<DialogueGUI>();
		dGUI.setTargetTex(alexiaSprite);
		yield return new WaitForSeconds (1.5f);
		dGUI.setMainTex ((Texture2D)Resources.Load ("JaneSprite"));
		Dialoguer.StartDialogue(2);
	}

	public IEnumerator secondDialogue(){
		started = true;
		yield return new WaitForSeconds (1.5f);
		Dialoguer.StartDialogue(5);
	}

	public IEnumerator goToPlaza(){
		yield return new WaitForSeconds (1.5f);
		GameManager.Instance.playerInScene = true;
		Application.LoadLevel("finplaza");
	}
}
