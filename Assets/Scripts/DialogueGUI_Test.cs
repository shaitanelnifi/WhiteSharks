using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueGUI_Test : MonoBehaviour {

	private bool _dialogue;
	private bool _ending;
	private bool _showDialogueBox;

	private string _nameText = string.Empty;
	
	private bool _isBranchedText;
	private string[] _branchedTextChoices;
	private int _currentChoice;

	private string _windowTargetText = string.Empty;
	private string _windowCurrentText = string.Empty;

	private UILabel label;
	private bool runOnce = false;
	private GameObject convoBubble;

	private int fullScale = 1;
	private int noScale = 0;

	private List<UILabel> choices = new List<UILabel> ();
	private UISprite leftChar;
	private UISprite rightChar;
	private UILabel nameLabel;

	private string leftSpriteName;
	private string rightSpriteName;
	private string name;

	private int choiceIndex;
	private float time;
	private float delay = 0.33f;

	// Use this for initialization
	void Start () {
		addDialoguerEvents();

		_showDialogueBox = false;

//		UISprite spr = GameObject.Find ("LeftCharacter").GetComponent<UISprite> ();
//		spr.spriteName = "LiamO'SheaSprite";
//		spr.MarkAsChanged ();
	}

	#region Dialoguer
	public void addDialoguerEvents(){
		Dialoguer.events.onStarted += onDialogueStartedHandler;
		Dialoguer.events.onEnded += onDialogueEndedHandler;
		Dialoguer.events.onInstantlyEnded += onDialogueInstantlyEndedHandler;
		Dialoguer.events.onTextPhase += onDialogueTextPhaseHandler;
		Dialoguer.events.onWindowClose += onDialogueWindowCloseHandler;
		Dialoguer.events.onMessageEvent += onDialoguerMessageEvent;
	}

	private void onDialogueStartedHandler(){
		_dialogue = true;

		time = 0;

		if (convoBubble != null)
		{
			setScaleFull ();
		}
	}
	
	private void onDialogueEndedHandler(){
		Debug.Log ("onDialogueEndedHandler()");
		_ending = true;

		if (convoBubble != null)
		{
			setScaleZero();
		}
	}
	
	private void onDialogueInstantlyEndedHandler(){
		_dialogue = false;
		_showDialogueBox = false;
	}
	
	private void onDialogueTextPhaseHandler(DialoguerTextData data){
		_windowCurrentText = string.Empty;
		_windowTargetText = data.text;
		
		_nameText = data.name;
		
		_showDialogueBox = true;
		
		_isBranchedText = data.windowType == DialoguerTextPhaseType.BranchedText;
		_branchedTextChoices = data.choices;
		_currentChoice = 0;
	}
	
	private void onDialogueWindowCloseHandler(){
		// Resets the camera to default size
		_dialogue = false;
		_showDialogueBox = false;
		Camera.main.orthographicSize = 6.0f;
	}
	
	private void onDialoguerMessageEvent(string message, string metadata){

	}
	#endregion
	
	// Update is called once per frame
	void Update () {
		if (!_dialogue || !_showDialogueBox)
		{
			setScaleZero ();
			return;
		}

		// Setup
		if (!GameObject.Find ("Conversation Bubble"))
		{
			setup ();
			leftChar = GameObject.Find("LeftCharacter").GetComponent<UISprite>();
			rightChar = GameObject.Find("RightCharacter").GetComponent<UISprite>();
			nameLabel = GameObject.Find ("Name").GetComponent<UILabel>();
		}

		time += Time.deltaTime;

		if (_isBranchedText && _windowCurrentText == _windowTargetText && _branchedTextChoices != null)
		{
			enableColliders();
			repopulateChoices();
			for (int i = 0; i < _branchedTextChoices.Length; ++i)
			{
				label.text = "";	// Clear conversation text field
				choices[i].text = _branchedTextChoices[i];
			}
		}
		else
		{
			clearBranchedText();
			label.text = _windowTargetText;
		}

		if (!_isBranchedText)
		{
			disableColliders();
			if (Input.GetMouseButtonDown(0) && time > delay)
			{
				time = 0;
				Dialoguer.ContinueDialogue(0);
			}
		}

		updatePortraits();

		# region test things
		if (Input.GetKeyDown(KeyCode.G)) 
		{
			Debug.Log ("DISABLE~~~~~~~~~~~~");
			setScaleZero();
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			Debug.Log ("ENABLE~~~~~~~~~~~~~~~");
			setScaleFull();
		}
		#endregion
	}

	private void setScaleFull()
	{
		if (convoBubble != null)
		{
			convoBubble.transform.localScale = new Vector3(1,1,1);
		}
	}

	private void setScaleZero()
	{
		if (convoBubble != null)
		{
			convoBubble.transform.localScale = new Vector3(0,0,0);
		}
	}

	private void clearBranchedText()
	{
		if (!_isBranchedText)
		{
			foreach (UILabel label in choices)
			{
				Debug.Log ("hello pls");
				label.text = "";
			}
		}
	}

	private void updatePortraits()
	{
		nameLabel.text = _nameText;

		Debug.Log ("leftSprite: " + leftSpriteName);
		leftChar.spriteName = leftSpriteName;
		leftChar.MarkAsChanged();

		Debug.Log ("rightSpriteName: " + rightSpriteName);
		Debug.Log ("_nameText: " + _nameText);
		if (_nameText.Equals("Jane Doe"))
		{
			rightSpriteName = "JaneSprite";
		}

		rightChar.spriteName = rightSpriteName;
		rightChar.MarkAsChanged();
	}

	// Enable and Disable Colliders are used to keep
	// the player from clicking on the branched text choices
	// when only the regular label is visible
	// This is because of the way the NGUI prefab is set up
	private void enableColliders()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("choice");
		foreach (GameObject obj in objs)
		{
			obj.GetComponent<BoxCollider>().enabled = true;
		}
	}

	private void disableColliders()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("choice");
		foreach (GameObject obj in objs)
		{
			obj.GetComponent<BoxCollider>().enabled = false;
		}
	}

	private void repopulateChoices()
	{
		choices.Clear();
		foreach (Transform child in GameObject.Find ("BranchedChoices").transform)
		{
			UILabel temp = child.GetComponent<UILabel>();
			choices.Add(temp);
		}
	}

	private void setup()
	{
		convoBubble = (GameObject)Instantiate(Resources.Load ("Conversation Bubble"));
		convoBubble.name = "Conversation Bubble";
		label = GameObject.Find("Conversation Text").GetComponent<UILabel>();
		runOnce = true;

		foreach (Transform child in GameObject.Find ("BranchedChoices").transform)
		{
			UILabel temp = child.GetComponent<UILabel>();
			choices.Add(temp);
		}
	}

	public void setLeftSpriteName(string leftSpriteName)
	{
		if (leftSpriteName == null)
		{
			Debug.LogError ("Null value error for string leftSpriteName in setLeftSpriteName in DialogueGUI.cs");
		}
		else
		{
			this.leftSpriteName = leftSpriteName;
		}
	}

	public void setRightSpriteName(string rightSpriteName)
	{
		if (rightSpriteName == null)
		{
			Debug.LogError ("Null value error for string rightSpriteName in setRightSpriteName in DialogueGUI.cs");
		}
		else
		{
			this.rightSpriteName = rightSpriteName;
		}
	}
}
