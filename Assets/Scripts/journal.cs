//Adrian Williams
//Journal
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class journal : MonoBehaviour {
	
	private static journal instance;
	private static journal j;
	
	public static journal Instance {
		get {
			if (instance == null) {
				Debug.Log("JOURNAL: Instance null, setting journal to self.");
				instance = j;
			}
			return instance;
		}
	}
	
	public List<NPC> personsOfInterest;
	
	//Defaults for non-visible NPC
	public static Sprite emptyPortrait;
	private string emptyName;
	
	public GameObject viewTab1;
	public GameObject viewTab2;
	public GameObject poiObjectView;
	public GameObject journalButton;
	public GameObject notificationPanel;
	public float notificationTime;
	private bool inMenu;
	private bool isNotifying;
	
	private List<GameObject> viewTabList;
	private List<GameObject> poiButtonList;
	private static List<GameObject> objectButtonList;
	private Queue<GameObject> notificationQ;
	
	public GameObject poiButtonGrid;
	public GameObject objectButtonGrid;
	public GameObject buttonPrefab;
	public UI2DSprite poiPortrait;

	private GameObject mainCam;
	public LayerMask cullingMask;
	
	public UILabel descriptionLabel, panelNameLabel, pcName;
	private GameObject pc;
	
	public static Inventory inventory = new Inventory();
	
	void Awake () {
		if(!j){
			j = this;
			DontDestroyOnLoad(gameObject.transform.parent.transform.parent.gameObject);
		}
		else {
			if (gameObject.transform.parent.gameObject != null)
				Destroy (gameObject.transform.parent.transform.parent.gameObject);
		}
	}
	
	void OnLevelWasLoaded(int level){
		//Sets camera culling to ignore this object's layer.
		mainCam = GameObject.Find ("Main Camera");
		mainCam.camera.cullingMask = cullingMask;

		if (level == 0) {
			Destroy (gameObject.transform.parent.transform.parent.gameObject);
		}
		resetObjectButtonList();
	}
	
	void resetObjectButtonList(){
		if (objectButtonList != null)
		{
			objectButtonList.Clear();
		}
		foreach (Transform child in objectButtonGrid.transform){
			objectButtonList.Add (child.gameObject);
		}
	}
	
	void Start () {
		////----    Journal Panel Init    ----//////////////////////////////////////////////////////
		
		emptyName = "?????";
		
		NPC nina = (NPC)Resources.Load ("NinaWalker", typeof(NPC));
		NPC josh = (NPC)Resources.Load ("JoshSusach", typeof(NPC));
		personsOfInterest = new List<NPC> ();
		personsOfInterest.Add (nina);
		personsOfInterest.Add (josh);
		
		//inventory = new Inventory();
		
		UIEventListener.Get (viewTab1).onClick += this.onClick;
		UIEventListener.Get (viewTab2).onClick += this.onClick;
		UIEventListener.Get (journalButton).onClick += this.journalAccusationPanelToggle;
		
		viewTabList = new List<GameObject>();
		viewTabList.Add(viewTab1);
		viewTabList.Add(viewTab2);
		
		poiButtonList = new List<GameObject>();
		objectButtonList = new List<GameObject>();
		notificationQ = new Queue<GameObject>();
		isNotifying = false;
		initPoIView();
		changeView(0);
		changePOI (0);

		string[] pcNameSplit = GameObject.FindGameObjectWithTag("Player").name.Split('(');
		pcName.text = pcNameSplit[0];
	}
	
	//Single onclick function for any button in the journal.
	void onClick(GameObject button){
		resetObjectButtonList();
		if(viewTabList != null && viewTabList.Contains(button)){
			changeView (viewTabList.IndexOf(button));
		}
		else if(poiButtonList.Contains(button)){
			changePOI(poiButtonList.IndexOf(button));
		}
		else if (objectButtonList.Contains (button)){
			changeObject(objectButtonList.IndexOf(button));
		}
	}
	
	//No longer toggles.
	void journalAccusationPanelToggle(GameObject button){
		if (button == journalButton){
			playerScript player = (playerScript) FindObjectOfType(typeof(playerScript));
			if (inMenu){
				player.stopMove();
				player.canWalk = true;
				player.talking = false;
				Time.timeScale = 1f;
				inMenu = false;
				Debug.LogWarning("Ding");
			}
			else {
				player.stopMove();
				player.talking = true;
				inMenu = true;	
				Debug.LogWarning("Dong");
				Time.timeScale = 0f;
			}
		}
	}
	
	//----- Button type functions
	//Changes view when view tab is clicked.
	void changeView(int viewNumber){
		clearLabels ();
		switch (viewNumber) {
		case 0://PoI
			poiObjectView.SetActive(true);
			objectButtonGrid.SetActive(false);
			poiButtonGrid.SetActive(true);
			changePOI(0);
			break;
		case 1://Object
			poiObjectView.SetActive(true);
			objectButtonGrid.SetActive(true);
			poiButtonGrid.SetActive(false);
			changeObject(0);
			break;
		}
	}
	
	//Changes PoI when a PoI portrait is clicked.
	void changePOI(int poiNumber){
		//Sprint 2 change POI code.
		if (personsOfInterest [poiNumber].isVisible ()) {
			descriptionLabel.text = personsOfInterest[poiNumber].getDescription();
		}
		else {
			descriptionLabel.text = emptyName;
		}
		poiPortrait.sprite2D = personsOfInterest[poiNumber].getProfileImage();
		panelNameLabel.text = personsOfInterest [poiNumber].getElementName ();
	}
	
	//Changes object/weapon being viewed when portrait is clicked.
	void changeObject(int objectNumber){
		if (inventory.Count > 0) {
			descriptionLabel.text = inventory.getDescription(objectNumber);
			poiPortrait.sprite2D = inventory.getIcon(objectNumber);
			panelNameLabel.text = inventory.getName(objectNumber);
		}
		else {
			descriptionLabel.text = emptyName;
			poiPortrait.sprite2D = emptyPortrait;
			panelNameLabel.text = emptyName;
		}
		
	}
	
	//Initialize PoI view. 
	//Code for sprint 2 journal.
	public void initPoIView(){
		Debug.LogWarning ("INIT JOURNAL");
		//Add buttons to poi button list and put them in UI event listener.
		foreach (Transform child in poiButtonGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			poiButtonList.Add(child.gameObject);
		}
		//Put suspect names on poi button labels.
		for (int i = 0; i < personsOfInterest.Count; i++) {
			if(personsOfInterest[i] != null){
				poiButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = personsOfInterest[i].getElementName();
			}
			else {
				poiButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = emptyName;
			}
		}
	}
	
	public void addObject(CaseObject newObject){
		changeView (1);
		GameObject tempButton = (GameObject)Instantiate (buttonPrefab, objectButtonGrid.transform.position, objectButtonGrid.transform.rotation);
		objectButtonList.Add (tempButton);
		notificationQ.Enqueue(objectButtonList[objectButtonList.Count - 1]);
		tempButton.transform.parent = objectButtonGrid.transform;
		tempButton.transform.GetComponentInChildren<UILabel>().text = inventory.getName (inventory.Count - 1);
		tempButton.transform.localScale = new Vector3(1,1,1);
		UIEventListener.Get (tempButton).onClick += this.onClick;
		objectButtonGrid.GetComponent<UIGrid>().Reposition();

		if(!isNotifying){
			isNotifying = true;
			StartCoroutine(showNotifications());
		}
	}

	IEnumerator showNotifications(){
		notificationPanel.GetComponent<TweenPosition>().Play();
		setNotifications(notificationQ.Dequeue ());
		yield return new WaitForSeconds(notificationTime);
		notificationPanel.GetComponent<TweenPosition>().PlayReverse();
		StartCoroutine(waitForPanelTween());
	}

	//Similar to changeObject. Should merge.
	void setNotifications(GameObject button){
		if(objectButtonList.Contains(button)){
			notificationPanel.GetComponentInChildren<UILabel>().text = inventory.getName(objectButtonList.IndexOf(button)) + " was added to the journal.";
			notificationPanel.GetComponentInChildren<UI2DSprite>().sprite2D = inventory.getIcon(objectButtonList.IndexOf(button));
		}
		//Might have to add else to show npcs that are added.
	}

	IEnumerator waitForPanelTween(){
		yield return new WaitForSeconds(1.0f);
		if(notificationQ.Count != 0){
			StartCoroutine(showNotifications());
		}
		else {
			isNotifying = false;
		}
	}
	
	//Clear description labels. Might rename and add obj/poi grid on/off.
	void clearLabels(){
		panelNameLabel.text = "";
		descriptionLabel.text = "";
	}

	//Method for setting PC's name on label.
	public void getPCName(){
		string[] pcNameSplit = GameObject.FindGameObjectWithTag("Player").name.Split('(');
		pcName.text = pcNameSplit[0];
	}

	void testNotificationQ(){
		for(int i = 0; i < 3; i++){
			GameObject newCaseObject = (GameObject)Resources.Load ("chapter1objects/brokenBracelet", typeof(GameObject));
			notificationQ.Enqueue(newCaseObject);
		}
		StartCoroutine(showNotifications());
	}
	
	//Keeps time.
/*	IEnumerator UpdateTime(){
		while (true) {
			DateTime currentTime = System.DateTime.Now;
			//timeLabel.text = currentTime.ToString("HH:mm");
			//timeLabel.text += "//";
			timeLabel.text = "";
			yield return new WaitForSeconds(0.2f);
		}
	}*/
	
	
	public bool isItemInInventory(GameObject item){
		
		bool matchFound = false;
		for (int i = 0; i < inventory.Count; i++) {
			
			string itemString = item.GetComponent<CaseObject>().elementName;
			string invenString = inventory.getName(i);
			if (itemString == invenString)
				matchFound = true;
			
		}
		
		return matchFound;
		
	}
}
