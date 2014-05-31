using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Main Menu
	public GameObject start;
	public GameObject credits;
	public GameObject options;
	
	// Options Menu
	public GameObject options_goBack;
	public GameObject options_apply;
	public GameObject volume_percent;
	public GameObject sfx_percent;
	public GameObject music_percent;

	// Credits
	public GameObject creditsLabel;
	public GameObject credits_goBack;

	public Camera cam;
	public float smooth;

	private string _wo;
	private bool test_Mode = false;
	private string sceneName;

	private Vector3 camPosition;
	private Vector3 creditsStart;
	private Vector3 creditsPos;
	private bool isCredits = false;

	// Use this for initialization
	public MainMenu(string name){
		test_Mode = true;
		sceneName = name;
	}

	void Awake() {
		camPosition = cam.transform.localPosition;
		creditsStart = new Vector3 (-4205f, -1959f, camPosition.z);
		creditsPos = new Vector3(-4320f, 5000f, camPosition.z);
		smooth = 4.0f;
	}

	void Start () {
		UIEventListener.Get (start).onClick += this.onClick;
		UIEventListener.Get (credits).onClick += this.onClick;
		UIEventListener.Get (options).onClick += this.onClick;
		UIEventListener.Get (options_goBack).onClick += this.onClick;
		UIEventListener.Get (options_apply).onClick += this.onClick;
		UIEventListener.Get (credits_goBack).onClick += this.onClick;
		SoundManager.Instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/MainMenu"));
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("SFX: " + SoundManager.sfxVolume);
//		Debug.Log ("MUS: " + SoundManager.musicVolume);
		// Reset credits after they're mostly out of frame
		if (cam.transform.localPosition.x > -50f && !isCredits)
		{
			creditsLabel.transform.localPosition = creditsStart;
		}

		// Start game
		if (cam.transform.localPosition.y > 2449f)
		{
			startGame();
		}

		if (cam.transform.localPosition.x < -4319f && isCredits)
		{
			creditsLabel.transform.localPosition = Vector3.Lerp(creditsLabel.transform.localPosition, creditsPos, Time.deltaTime / 50f);
		}
		else
		{
			ChangeScreen ();
		}
	}

	void ChangeScreen()
	{
		cam.transform.localPosition = Vector3.Lerp (cam.transform.localPosition, camPosition, Time.deltaTime * smooth);
	}
	
	void onClick(GameObject button){
		if (button == start) {
			camPosition = new Vector3(camPosition.x, 2450f, camPosition.z);
		}

		if (button == credits) {
			camPosition = new Vector3(-4320f, camPosition.y, camPosition.z);
			isCredits = true;
		}

		if (button == options) {
			camPosition = new Vector3(4320f, camPosition.y, camPosition.z);
		}

		if (button == options_goBack) {
			camPosition = new Vector3 (0f, camPosition.y, camPosition.z);
		}

		if (button == options_apply) {
			int settingVol = int.Parse(volume_percent.GetComponent<UILabel>().text.TrimEnd ('%'));
			int numTimesVol = (int)Mathf.Abs (settingVol - (SoundManager.gameVolume * 100f));
			if (settingVol > (int)(SoundManager.gameVolume * 100))
			{
				changeGameVolume(1, numTimesVol);
			}
			else if (settingVol < (int)(SoundManager.sfxVolume * 100))
			{
				changeGameVolume(-1, numTimesVol);
			}

			int settingSFX = int.Parse(sfx_percent.GetComponent<UILabel>().text.TrimEnd('%'));
			int numTimesSFX = (int)Mathf.Abs(settingSFX - (SoundManager.sfxVolume * 100f));
			if (settingSFX > (int)(SoundManager.sfxVolume * 100))
			{
				changeSfxVolume(1, numTimesSFX);
			}
			else if (settingSFX < (int)(SoundManager.sfxVolume * 100))
			{
				changeSfxVolume(-1, numTimesSFX);
			}

			int settingMusic = int.Parse(music_percent.GetComponent<UILabel>().text.TrimEnd('%'));
			int numTimesMusic = (int)Mathf.Abs(settingMusic - (SoundManager.musicVolume * 100f));
			if (settingMusic > (int)(SoundManager.musicVolume * 100))
			{
				changeMusicVolume(1, numTimesMusic);
			}
			else if (settingMusic < (int)(SoundManager.musicVolume * 100))
			{
				changeMusicVolume(-1, numTimesMusic);
			}
		}

		if (button == credits_goBack) {;
			camPosition = new Vector3 (0f, camPosition.y, camPosition.z);
			isCredits = false;
		}
	}

	// 1 = positive, -1 = negative
	private void changeSfxVolume(int direction, int numTimes) {
		for (int i = 0; i < numTimes; ++i)
		{
			SoundManager.Instance.EditSfx (direction);
		}
	}

	private void changeMusicVolume(int direction, int numTimes) {
		for (int i = 0; i < numTimes; ++i)
		{
			SoundManager.Instance.EditMusic (direction);
		}
	}

	private void changeGameVolume(int direction, int numTimes) {
		for (int i = 0; i < numTimes; ++i)
		{
			SoundManager.Instance.EditAll (direction);
		}
	}

	public void startGame() {
		Dialoguer.Initialize ("chapter2");
		// Initialize various managers for the game
		// Singleton pattern

		DontDestroyOnLoad(GameManager.Instance);
		DontDestroyOnLoad(SoundManager.Instance);
		GameManager.Instance.startState(test_Mode, sceneName);

		switch (GameManager.currentEpisode) {
		case 0:
			//GameManager.npcConversations[(int)NPCNames.Shammy] = 1;
			//GameManager.npcConversations[(int)NPCNames.CarlosFranco] = 4;
			//GameManager.npcConversations[(int)NPCNames.NoelAlt] = 3;
			//GameManager.offset = 8;
			break;
		}
	}


}
