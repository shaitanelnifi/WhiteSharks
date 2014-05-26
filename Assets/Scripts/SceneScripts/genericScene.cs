using UnityEngine;
using System.Collections;

public class genericScene : MonoBehaviour {
	//Implement when dialoguer/animation ends, go to first scene.

	//For use with dialogue
	public bool autoPlay;
	public AudioClip playMe;
	public float waitThisLong;
	public string whatCharacter;
	public string nextLevel;
	public bool isTherePlayer = false;
	public Vector2 spawnHereAfter;
	public bool loadNewXML = false;
	public string dialoguer = "chapter1";

	protected bool done = false;
	public bool needGUI = false;
	public Convo[] dialogue;
	protected int curDia = 0;

	public int setOffset;

	//For handling the player in the scene
	protected static GameObject backEffect;
	public playerScript player;
	public float maxY = 2f;
	public float minY = 1f;
	public float minScale = 1f;
	public float maxScale = 1f;
	public float baseSpeed = 1f;

	// For determining whether to place conversation bubble top or bottom
	public bool placeTop = false;

	// Use this for initialization
	void Start () {
		player = (playerScript) FindObjectOfType(typeof(playerScript));

		sceneInit ();

		if (loadNewXML || !Dialoguer.isInitialized ()) {
						Dialoguer.Initialize (dialoguer);
			GameManager.offset = 0;
			setOffset = 0;
				}

		GameManager.dialogueJustFinished = false;
		if (GameManager.offset == 0)
			GameManager.offset = setOffset;

		if (autoPlay) 
			playDialogue();
			
		
	}

	public bool getPlaceTop()
	{
		return placeTop;
	}

	void playDialogue(){

		if (dialogue.Length != 0)
			Dialoguer.StartDialogue ((int)dialogue[curDia]);

		if (player != null) {
			player.stopMove ();
			player.talking = true;
			//Debug.LogWarning("TALKING");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (autoPlay)
		StartCoroutine ("wait");
		//Debug.Log ("ALIVE");

	}

	void OnGUI() {
		if (needGUI) {
				//GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "DIALOGUER CONVERSATION FOR BRAIN ROOM.");
			if (done) {
				//GUI.Box (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "DONE.");
			}
		}
	}

	public void sceneInit(){
		if (FindObjectOfType(typeof(AstarPath)) != null)
			AstarPath.active.Scan ();
		backEffect = (GameObject)Instantiate(Resources.Load("effect"));
		if (GameManager.Instance.playerInScene) {
			string temp = (string)GameManager.Instance.GetMainCharacter ();
			var Player = (GameObject)Instantiate (Resources.Load ((temp)));
			Vector2 tempVec = transform.position;
			tempVec.x = GameManager.Instance.GetNextX ();
			tempVec.y = GameManager.Instance.GetNextY ();

			Player.transform.position = tempVec;
			player = Player.GetComponent<playerScript>();
			player.canScale = true;
			player.GetComponent<playerScript> ().scaleInfo = new float[4]{minScale, maxScale, minY, maxY};
			player.GetComponent<playerScript> ().baseSpeed = baseSpeed;
		}
		
		GameManager.Instance.updateMouseIcon ("Walk_Icon");
	}

	IEnumerator wait(){
		SoundManager.Instance.Play2DMusic (playMe);
		if (dialogue.Length != 0) {
			if (!GameManager.dialogueJustFinished) {
					player = (playerScript)FindObjectOfType (typeof(playerScript));
					if (player != null) {
							player.stopMove ();
							player.talking = true;
							//Debug.LogWarning("TALKING");
					}
			}

			if (GameManager.dialogueJustFinished && curDia < dialogue.Length - 1) {

					GameManager.dialogueJustFinished = false;
					curDia ++;
					Dialoguer.StartDialogue ((int)dialogue [curDia]);

			} else 
			if ((int)dialogue [curDia] >= 0 && GameManager.dialogueJustFinished && curDia == dialogue.Length - 1) {
					if (waitThisLong != -1) {
							yield return new WaitForSeconds (waitThisLong);
							done = true;
							GameManager.Instance.playerInScene = isTherePlayer;
	
							if (isTherePlayer) {
							
									//Debug.Log ("Setting nexts to " + spawnHereAfter.x + " and " + spawnHereAfter.y);
									GameManager.Instance.SetMainCharacter (whatCharacter);
									GameManager.Instance.SetNextX (spawnHereAfter.x);
									GameManager.Instance.SetNextY (spawnHereAfter.y);
							}
							GameManager.dialogueJustFinished = false;
							SoundManager.Instance.CantWalk ();
							Application.LoadLevel (nextLevel);
					}
			} else if (dialogue [curDia].Equals (Convo.ch0none)) {
					if (waitThisLong != -1) {
							yield return new WaitForSeconds (waitThisLong);
							GameManager.Instance.playerInScene = isTherePlayer;
							done = true;
							if (isTherePlayer) {
									if (!GameManager.Instance.playerInScene) {
											GameManager.Instance.playerInScene = true;
									}
									Debug.Log ("Setting nexts to " + spawnHereAfter.x + " and " + spawnHereAfter.y);
									GameManager.Instance.SetMainCharacter (whatCharacter);
									GameManager.Instance.SetNextX (spawnHereAfter.x);
									GameManager.Instance.SetNextY (spawnHereAfter.y);
							}
							GameManager.dialogueJustFinished = false;
							SoundManager.Instance.CantWalk ();
							Application.LoadLevel (nextLevel);
					}
			}
		} else if (waitThisLong != -1) {
			yield return new WaitForSeconds (waitThisLong);
			done = true;
			GameManager.Instance.playerInScene = isTherePlayer;
			
			if (isTherePlayer) {
				
				//Debug.Log ("Setting nexts to " + spawnHereAfter.x + " and " + spawnHereAfter.y);
				GameManager.Instance.SetMainCharacter (whatCharacter);
				GameManager.Instance.SetNextX (spawnHereAfter.x);
				GameManager.Instance.SetNextY (spawnHereAfter.y);
			}
			GameManager.dialogueJustFinished = false;
			SoundManager.Instance.CantWalk ();
			Application.LoadLevel (nextLevel);
		}
	}
}
