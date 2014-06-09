using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueGUI_Test : MonoBehaviour {
	
	private bool _dialogue;
	//private bool _ending;
	private bool _showDialogueBox;
	
	private string _nameText = string.Empty;
	
	private bool _isBranchedText;
	private string[] _branchedTextChoices;
	
	private string _windowTargetText = string.Empty;
	private string _windowCurrentText = string.Empty;
	
	private UILabel label;
	private bool runOnce = false;
	private GameObject convoBubble;
	private UIRoot uiroot;
	
	private List<UILabel> choices = new List<UILabel> ();
	private UISprite leftChar;
	//private UISprite rightChar;
	private UILabel nameLabel;
	
	private string leftSpriteName;
	//private string rightSpriteName;
	private string _metadata;
	private List<string[]> _parsedmetadata;
	
	private int choiceIndex;
	private float time;
	private float delay = 0.16f;

	private genericScene[] genericSceneObj;
	
	// Use this for initialization
	void Start () {
		addDialoguerEvents();
		StartCoroutine (resetRunOnce());
		_showDialogueBox = false;
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
			// SET color at 0 first
			Color col = convoBubble.GetComponent<UIWidget>().color;
			col.a = 0f;
			convoBubble.GetComponent<UIWidget>().color = col;
			
			convoBubble.transform.localPosition = new Vector3(0f, Screen.height / 16f, 1f);
			setScaleFull();
			SoundManager.Instance.CantWalk();
		}
	}
	
	private void onDialogueEndedHandler(){
		//_ending = true;
		
		if (convoBubble != null)
		{
			setScaleZero();
			SoundManager.Instance.CanWalk();
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
		
		// Parse the metadata
		_metadata = data.metadata;
		_parsedmetadata = parseString(data.metadata.Split(' '));
		
		if (uiroot != null)
		{
			uiroot.scalingStyle = UIRoot.Scaling.FixedSize;
			uiroot.manualHeight = 600;
			uiroot.transform.Find("Conversation Bubble").transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		}
	}
	
	private void onDialogueWindowCloseHandler(){
		// Resets the camera to default size
		_dialogue = false;
		_showDialogueBox = false;
	}
	
	private void onDialoguerMessageEvent(string message, string metadata){
		
	}
	#endregion
	
	List<string[]> parseString(string[] str)
	{
		List<string[]> list = new List<string[]>();
		for (int i = 0; i < str.Length; ++i)
		{
			list.Add (str[i].Split(':'));
		}
		return list;
	}
	
	private void resetChoiceColor()
	{
		if (GameObject.Find ("BranchedChoices"))
		{
			Component[] buttons = GameObject.Find ("BranchedChoices").GetComponentsInChildren(typeof(UIWidget));
			foreach (Component button in buttons)
			{
				button.GetComponent<UIWidget>().color = new Color(250f, 240f, 200f, 255f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!_dialogue || !_showDialogueBox)
		{
			setScaleZero ();
			return;
		}
		
		// Set position of conversation bubble
		if (convoBubble != null)
		{
			uiroot = GameObject.Find ("UI Root").GetComponent<UIRoot>();
			
			if (uiroot != null)
			{
				Transform convo = uiroot.transform.Find("Conversation Bubble");
				uiroot.scalingStyle = UIRoot.Scaling.FixedSize;
				uiroot.manualHeight = 600;
				convo.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
				
				// For determining whether convo displays top or bottom of screen
				if (runOnce == false)
				{
					genericSceneObj = FindObjectsOfType(typeof(genericScene)) as genericScene[];
				}
				Vector3 top = new Vector3 (0f, 100f, 1f);
				Vector3 bottom = new Vector3(0f, -800f, 1f);
				Vector3 centertop = new Vector3(-200f, -100f, 1f);
				Vector3 centerbottom = new Vector3(-200f, -800f, 1f);
				
				if (genericSceneObj != null && genericSceneObj.Length >= 1)
				{
					foreach (genericScene scene in genericSceneObj)
					{
						string sprName = convo.GetComponent<UISprite>().spriteName;
						// If want to place on top of screen, else...
						if (scene.getPlaceTop())
						{
							if (sprName == "conversation-template-new")
								convo.transform.localPosition = Vector3.Lerp (convo.transform.localPosition, top, Time.deltaTime * 8f);
							else if (sprName == "regulartext")
								convo.transform.localPosition = Vector3.Lerp (convo.transform.localPosition, centertop, Time.deltaTime * 8f);
						}
						else
						{
							if (sprName == "conversation-template-new")
								convo.transform.localPosition = Vector3.Lerp (convo.transform.localPosition, bottom, Time.deltaTime * 8f);
							else if (sprName == "regulartext")
								convo.transform.localPosition = Vector3.Lerp (convo.transform.localPosition, centerbottom, Time.deltaTime * 8f);
						}

						Color col = convo.GetComponent<UIWidget>().color;
						col.a = Mathf.Lerp (col.a, 1f, Time.deltaTime * 4f);
						convo.GetComponent<UIWidget>().color = col;
						
						// Used for switching between conversation template backgrounds
						// For text where there is no character speaking,
						// display a different background
						// HARD CODE LOLOL
						Transform convoText = convo.Find("Conversation Text");
						if ((Application.loadedLevelName == "chapter1intronarration" ||
						     Application.loadedLevelName == "chapter1introfinbalcony") &&
						    _nameText == "")
						{
							convo.GetComponent<UISprite>().spriteName = "regulartext";
							convo.GetComponent<UIWidget>().width = 8000;
							convoText.GetComponent<UIWidget>().pivot = UIWidget.Pivot.Top;
							convo.GetComponent<UISprite>().MarkAsChanged();
						}
						else
						{
							convo.GetComponent<UISprite>().spriteName = "conversation-template-new";
							convo.GetComponent<UIWidget>().width = 4618;
							convoText.GetComponent<UIWidget>().pivot = UIWidget.Pivot.TopLeft;
							convo.GetComponent<UISprite>().MarkAsChanged();
						}
					}
					runOnce = true;
				}
				else
				{
					// If there is no generic scene in the scene
					convo.transform.localPosition = Vector3.Lerp(convo.transform.localPosition, bottom, Time.deltaTime * 8f);
					
					Color col = convo.GetComponent<UIWidget>().color;
					col.a = Mathf.Lerp (col.a, 1f, Time.deltaTime * 4f);
					convo.GetComponent<UIWidget>().color = col;
				}
				
				
				if (_nameText == "")
				{
					runOnce = false;
				}
				
				convo.GetComponent<UISprite> ().enabled = true;
			}
		}
		
		// Setup
		if (!GameObject.Find ("Conversation Bubble"))
		{
			setup ();
			leftChar = GameObject.Find("LeftCharacter").GetComponent<UISprite>();
			nameLabel = GameObject.Find ("Name").GetComponent<UILabel>();
		}
		
		// Mouse click interval delay
		time += Time.deltaTime;
		
		// Branched Text
		if (_isBranchedText && _windowCurrentText == _windowTargetText && _branchedTextChoices != null)
		{
			enableColliders();
			repopulateChoices();
			label.text = "";	// Clear conversation text field
			if (_metadata == "")
			{
				for (int i = 0; i < _branchedTextChoices.Length; ++i)
				{
					choices[i].text = "- " + _branchedTextChoices[i];		
				}
			}
			else
			{
				/// For parsing metadata
				List<int> mylist = new List<int>();
				List<int> newlist = new List<int>();
				for (int i = 1; i <= _branchedTextChoices.Length; ++i)
				{
					mylist.Add(i);
					newlist.Add(i);
				}
				List<int> metalist = new List<int>();
				List<int> varlist = new List<int>();
				foreach (string[] ele in _parsedmetadata)
				{
					metalist.Add (int.Parse(ele[0].Trim(',')));
					varlist.Add (int.Parse (ele[2].Trim(',')));
				}
				foreach (int i in mylist)
				{
					foreach(int j in metalist)
					{
						if (i == j)
						{
							newlist.Remove(i);
						}
					}
				}
				foreach (int i in newlist)
				{
					choices[i-1].text = "- " + _branchedTextChoices[i-1];
					enableCollider(choices[i-1]);
				}
				int itor = 0;
				foreach (int i in metalist)
				{
					if (Dialoguer.GetGlobalBoolean(varlist[itor]))
					{
						choices[i-1].text = "- " + _branchedTextChoices[i-1];
						enableCollider(choices[i-1]);
					}
					else
					{
						choices[i-1].text = "";
						disableCollider(choices[i-1]);
					}
					++itor;
				}
			}
		}
		// Regular text
		else
		{
			clearBranchedText();
			label.text = _windowTargetText;
		}
		
		// Regular text progression
		if (!_isBranchedText)
		{
			disableColliders();
			if (Input.GetMouseButtonUp(0) && time > delay)
			{
				time = 0;
				Dialoguer.ContinueDialogue(0);
			}
		}
		
		updatePortraits();
		
		# region test things
		if (Input.GetKeyUp (KeyCode.F))
		{
			Dialoguer.EndDialogue();
		}
		#endregion
	}
	
	private void setScaleFull()
	{
		if (convoBubble != null)
		{
			convoBubble.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
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
				label.text = "";
			}
		}
	}
	
	private GameObject findCamera()
	{
		return GameObject.Find ("Camera");
	}
	
	private void updatePortraits()
	{
		nameLabel.text = _nameText;
		
		if (leftChar.spriteName.Equals ("") || ! leftChar.spriteName.Equals (_nameText.Replace(" ", string.Empty) + "Sprite")) {
			leftSpriteName = _nameText.Replace(" ", string.Empty) + "Sprite";
		}
		
		leftChar.spriteName = leftSpriteName;
		leftChar.MarkAsChanged();
	}
	
	// Enable single collider
	private void enableCollider(UILabel choice)
	{
		choice.GetComponent<BoxCollider> ().enabled = true;
	}
	
	// Disable single colldier
	private void disableCollider(UILabel choice)
	{
		choice.GetComponent<BoxCollider> ().enabled = false;
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
			if (obj.GetComponent<UILabel>().text == "")
			{
				obj.GetComponent<BoxCollider>().enabled = false;
			}
			else
			{
				obj.GetComponent<BoxCollider>().enabled = true;
			}
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
		convoBubble = Instantiate(Resources.Load ("Conversation Bubble")) as GameObject;
		convoBubble.name = "Conversation Bubble";
		
		label = GameObject.Find("Conversation Text").GetComponent<UILabel>();
		
		foreach (Transform child in GameObject.Find ("BranchedChoices").transform)
		{
			UILabel temp = child.GetComponent<UILabel>();
			choices.Add(temp);
		}
		
		// SET color at 0 first
		Color col = convoBubble.GetComponent<UIWidget>().color;
		col.a = 0f;
		convoBubble.GetComponent<UIWidget>().color = col;
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
	
	public string getMetadata()
	{
		if (_metadata == null)
		{
			Debug.LogError("Metadata is null!");
		}
		
		return _metadata;
	}
	
	IEnumerator resetRunOnce()
	{
		while(true)
		{
			runOnce = false;
			yield return new WaitForSeconds(1f);
		}
	}
}
