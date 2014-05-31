using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	//Volume
	public static float sfxVolume = 1.0f; //overall volume control for sound effects
	public static float musicVolume = 1.0f; //overall volume control for music
	public static float gameVolume = 1.0f; //overall volume control for everything

	public enum SoundType { Sfx, Music }

	private AudioClip curClip = null; //music file that should be playing
	private GameObject musicObj; //object that will hold the audio source
	private GameObject walkNoise; //object that holds the walking noise

	bool fading = false;

	public bool isWalking = false; //bool which is tested to see if the walking noise is already playing

	//change this number to modulate how many increments sound can be broken into. aka if you want a 100pt slider, make this 100
	private int soundChangeIncrementNumber = 100;

	private float soundPercentageChange; //goes with the above variable. is late defined by 1/soundChangeIncrementNumber

	bool canWalk = true; //bool which is tested to see if the walking noise can be played
	bool fadeInFinish = false; //becomes true during a transition where the new music needs to rise from zero to its set volume

	public static SoundManager instance;

	private ArrayList longSounds = new ArrayList(); //any sounds that might play longer than a few seconds

	private int musicCounter = 0; //keeps track of difference between the overall volume and the music volume
	private int sfxCounter = 0; //keeps track of difference between the overall volume and the sound effect volume

	//From what I understand, this creates an instance of soundmanager once and then allows it to be called
	//private static SoundManager s_instance = null;
	public static SoundManager Instance {
		get {
			if(instance == null) {
				GameObject go = new GameObject("SoundManager");
				instance = go.AddComponent<SoundManager>();
			}
			return instance;
		}
	}
	void Start() {
		soundPercentageChange = 1.0f / soundChangeIncrementNumber;
	}


	/*Plays 2D Sound Effect by creating an empty object and attaching a sound clip to it. 
	Then it plays the clip and deletes the object after its done playing
	 * 
	 * 
	 * To call, simply run the code below with the soundfile in Resources/SoundEffects
	 * SoundManager.instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/SOUND_FILE_NAME"), SoundManager.SoundType.Sfx);
	 * 
	 * if its a door, you add a true and if its supposed to loop, you add false, true
	 */
	public void Play2DSound(AudioClip clip, SoundType type, bool isDoor = false, bool isLoop = false) {
		GameObject newObj = new GameObject("SoundPlayer");
		newObj.transform.position = Camera.main.transform.position;
		if(isDoor) {
			newObj.transform.parent = SoundManager.Instance.transform;
			longSounds.Clear();
		}
		else newObj.transform.parent = Camera.main.transform;
		AudioSource newSource = newObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		if(isLoop) {
			newSource.audio.loop = true;
			longSounds.Add(newObj);
		}
		newSource.audio.volume = sfxVolume;
		newSource.audio.Play();
		if(!isLoop) StartCoroutine("DeleteSource", newObj);
	}

	//makes a walking noise if canWalk is true
	public void WalkSound() {
		if(canWalk) {
			isWalking = true;
			if(walkNoise == null) {
				walkNoise = new GameObject("SoundPlayer");
				walkNoise.transform.position = Camera.main.transform.position;
				walkNoise.transform.parent = Camera.main.transform;
				AudioSource newSource = walkNoise.AddComponent<AudioSource>() as AudioSource;
				newSource.audio.clip = (AudioClip)Resources.Load("Sounds/SoundEffects/HeelsLoop");
				newSource.audio.loop = true;
			}
			walkNoise.audio.volume = sfxVolume;
			walkNoise.audio.Play();
		}
	}


	//When the player stops walking, this is called and makes a simple step noise to simulate planting feet
	public void StopWalk() {
		isWalking = false;
		if(walkNoise != null) {
			walkNoise.audio.Stop();
			Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/HeelsStep"), SoundType.Sfx);
		}
	}


	//Forces the walking noise to stop without planting the feet.
	public void CantWalk() {
		canWalk = false;
		if(walkNoise != null) {
			walkNoise.audio.Stop();
		}
	}

	//allows the player to begin walking again
	public void CanWalk() {
		canWalk = true;
		isWalking = false;
	}

	/*Plays 2D Music by selecting the empty object defined at the top and attaching a sound clip to it. 
	Then it plays the clip and loops it until another music is defined, or the KillMusic function is called.
	If something will try to play a song, it will ignore it unless it is a different song, in which case, it 
	will quiet the music and then transition into the new song.
	 * 
	 * 
	 * To call, simply run the code below with the soundfile in Resources/Music
	 * SoundManager.instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/SOUND_FILE_NAME"));
	 */
	public void Play2DMusic(AudioClip clip) {
		if(clip == curClip) return;
		else if(musicObj != null) {
			Debug.Log("Should play " + clip);
			StartCoroutine("FadeOut", clip);
			return;
		}
		musicObj = new GameObject("SoundPlayer");
		musicObj.transform.position = Camera.main.transform.position;
		musicObj.transform.parent = SoundManager.Instance.transform;
		curClip = clip;
		AudioSource newSource = musicObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.loop = true;
		newSource.audio.volume = musicVolume;
		newSource.audio.Play();
	}

	//sets the new music up and then starts to raise the volume until it reaches the set music volume
	public void TransitionMusic(AudioClip clip) {
		musicObj = new GameObject("SoundPlayer");
		musicObj.transform.position = Camera.main.transform.position;
		musicObj.transform.parent = SoundManager.Instance.transform;
		curClip = clip;
		AudioSource newSource = musicObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.loop = true;
		newSource.audio.volume = 0;
		newSource.audio.Play();
		fading = false;
		fadeInFinish = false;
		StartCoroutine("FadeIn");

	}

	//raises the volume until it reaches the set music volume
	IEnumerator FadeIn() {
		while(musicObj.audio.volume <= musicVolume && !fading && fadeInFinish == false) {
			musicObj.audio.volume += 0.01f;
			if(musicObj.audio.volume == musicVolume) fadeInFinish = true;
			yield return new WaitForSeconds(0.001f);
		}
	}


	//quiets the music until its volume reaches zero and then destroys it before calling for the new music to begin
	IEnumerator FadeOut(AudioClip clip) {
		while(musicObj.audio.volume > 0) {
			fading = true;
			musicObj.audio.volume -= 0.01f;
			yield return new WaitForSeconds(0.001f);
		}
		Destroy(musicObj);
		TransitionMusic(clip);
	}



	//Kills current music
	public void KillMusic() {
		Destroy(musicObj);
		curClip = null;
	}

	//Pauses current music
	public void Pause() {
		musicObj.audio.Pause();
		StopWalk();
		if(longSounds.Count != 0) {
			foreach(GameObject obj in longSounds) {
				obj.audio.Pause();
			}
		}
	}

	//Resumes current music
	public void Unpause() {
		musicObj.audio.Play();
		if(longSounds.Count != 0) {
			foreach(GameObject obj in longSounds) {
				obj.audio.Play();
			}
		}
	}



	//Coroutine for sound effects that gets rid of audio source when audio is done playing
	IEnumerator DeleteSource(GameObject obj) {
		float time = obj.audio.clip.length;
		while(time > 0) {
			time--;
			yield return new WaitForSeconds(1f);
		}
		Destroy(obj);
	}




	//Edits the sound effect volume. If 1 is entered, the sound effect increases one soundPercentage Change. If -1, it decreases
	public void EditSfx(int a) {
		if(sfxVolume < 0f) sfxVolume = 0f;
		if(sfxVolume > 1f) sfxVolume = 1f;
		if(a == -1) {
			if(sfxVolume > 0f) {
				sfxVolume -= soundPercentageChange;
				sfxCounter++;
				sfxVolume = (Mathf.Round(sfxVolume * 100)) / 100;
			}
			else if((sfxVolume != gameVolume || gameVolume == 0f) && sfxCounter < soundChangeIncrementNumber) sfxCounter++;
		}
		if(a == 1) {
			if(sfxCounter != 0) {
				if(Mathf.RoundToInt((sfxCounter * soundPercentageChange) * 100) != Mathf.RoundToInt((gameVolume - sfxVolume) * 100)) {
					sfxCounter--;
				}
				else {
					sfxCounter--;
					sfxVolume += soundPercentageChange;
					sfxVolume = (Mathf.Round(sfxVolume * 100)) / 100;
				}
			}
		}
		if(longSounds.Count != 0) {
			foreach(GameObject obj in longSounds) {
				obj.audio.volume = sfxVolume;
			}
		}
	}

	//Edits the music volume. If 1 is entered, the music increases one soundPercentage Change. If -1, it decreases
	public void EditMusic(int a) {
		if(musicVolume < 0f) musicVolume = 0f;
		if(musicVolume > 1f) musicVolume = 1f;
		if(a == -1) {
			if(musicVolume > 0f) {
				musicVolume -= soundPercentageChange;
				musicCounter++;
				musicVolume = (Mathf.Round(musicVolume * 100)) / 100;
			}
			else if((musicVolume != gameVolume || gameVolume == 0f) && musicCounter < soundChangeIncrementNumber) musicCounter++;
		}
		if(a == 1) {
			if(musicCounter != 0) {
				if(Mathf.RoundToInt((musicCounter * soundPercentageChange) * 100) != Mathf.RoundToInt((gameVolume - musicVolume) * 100)) {
					//Debug.Log("First One");
					musicCounter--; 
				}
				else {
					//Debug.Log("Second One");
					musicCounter--;
					musicVolume += soundPercentageChange;
					musicVolume = (Mathf.Round(musicVolume * 100)) / 100;
				}

			}
		}
		musicObj.audio.volume = musicVolume;
	}

	//Edits the sound volume. If 1 is entered, the sound increases one soundPercentage Change. If -1, it decreases
	public void EditAll(int a) {
		if(gameVolume < 0f) gameVolume = 0f;
		if(gameVolume > 1f) gameVolume = 1f;
		if(sfxVolume < 0f) sfxVolume = 0f;
		if(sfxVolume > 1f) sfxVolume = 1f;
		if(musicVolume < 0f) musicVolume = 0f;
		if(musicVolume > 1f) musicVolume = 1f;
		if(a == -1 && gameVolume > 0f) {
			gameVolume -= soundPercentageChange;
			if(musicVolume > 0f) {
				musicVolume -= soundPercentageChange;
			}

			if(sfxVolume > 0f) {
				sfxVolume -= soundPercentageChange;
			}
		}

		if(a == 1 && gameVolume != 1.0f) {
			if(musicCounter == 0) {
				musicVolume += soundPercentageChange;
			}
			else if(musicCounter * soundPercentageChange == gameVolume - musicVolume) {
				musicVolume += soundPercentageChange;
			}

			if(sfxCounter == 0) {
				sfxVolume += soundPercentageChange;
			}
			else if(sfxCounter * soundPercentageChange == gameVolume - sfxVolume) {
				sfxVolume += soundPercentageChange;
			}
			gameVolume += soundPercentageChange;
		}
		musicObj.audio.volume = musicVolume;
		if(longSounds.Count != 0) {
			foreach(GameObject obj in longSounds) {
				obj.audio.volume = sfxVolume;
			}
		}
	}

	//moves the sound to keep it with the camer so that it sounds like it is coming from all around you.
	public void MoveSpeaker(Camera mainCam) {
		musicObj.transform.position = mainCam.transform.position;
	}

/*
	//code for testing
	void Update() {
		if(Input.GetKeyDown(KeyCode.T)) {
			EditMusic(1);
			/*Debug.Log("music counter: " + musicCounter);
			Debug.Log("music volume: " + musicVolume);
			Debug.Log("soundPercentageChange: " + soundPercentageChange);
			Debug.Log("game volume: " + gameVolume);
			Debug.Log("left side: " + (musicCounter * soundPercentageChange));
			Debug.Log("right side: " + (gameVolume - musicVolume));
			Debug.Log("int value of left side: " + Mathf.RoundToInt((musicCounter * soundPercentageChange) * 100));
			Debug.Log("int value of right side: " + Mathf.RoundToInt((gameVolume - musicVolume) * 100));
			Debug.Log("Is left different than right?");
			if(Mathf.RoundToInt((musicCounter * soundPercentageChange) * 100) != Mathf.RoundToInt((gameVolume - musicVolume) * 100)) Debug.Log("true");
			Debug.Log("---------------------------------------------------------------------");
			Debug.Log("             ");
			Debug.Log("             ");
			Debug.Log("             ");
			Debug.Log("---------------------------------------------------------------------");
		}
		if(Input.GetKeyDown(KeyCode.Y)) {
			EditMusic(-1);
			/*Debug.Log("music counter: " + musicCounter);
			Debug.Log("music volume: " + musicVolume);
			Debug.Log("soundPercentageChange: " + soundPercentageChange);
			Debug.Log("game volume: " + gameVolume);
			Debug.Log("left side: " + (musicCounter * soundPercentageChange));
			Debug.Log("right side: " + (gameVolume - musicVolume));
			Debug.Log("int value of left side: " + Mathf.RoundToInt((musicCounter * soundPercentageChange) * 100));
			Debug.Log("int value of right side: " + Mathf.RoundToInt((gameVolume - musicVolume) * 100));
			Debug.Log("Is left different than right?");
			if(Mathf.RoundToInt((musicCounter * soundPercentageChange) * 100) != Mathf.RoundToInt((gameVolume - musicVolume) * 100)) Debug.Log("true");
			else Debug.Log("false");
			Debug.Log("---------------------------------------------------------------------");
			Debug.Log("             ");
			Debug.Log("             ");
			Debug.Log("---------------------------------------------------------------------");
		}
		if(Input.GetKeyDown(KeyCode.G)) {
			EditSfx(1);
		}
		if(Input.GetKeyDown(KeyCode.H)) {
			EditSfx(-1);
		}
		if(Input.GetKeyDown(KeyCode.P)) {
			Pause();
		}
		if(Input.GetKeyDown(KeyCode.O)) {
			Unpause();
		}
		if(Input.GetKeyDown(KeyCode.J)) {
			EditAll(1);
		}
		if(Input.GetKeyDown(KeyCode.K)) {
			EditAll(-1);
		}
	}*/
}