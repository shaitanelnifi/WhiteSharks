using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	//Volume
	public static float sfxVolume = 1.0f;
	public static float musicVolume = 1.0f;
	public static float dialogueVolume = 1.0f;
	private static float gameVolume = 1.0f;

	public enum SoundType { Sfx, Music, Dialogue }

	private AudioClip curClip = null;
	private GameObject musicObj;
	private GameObject walkNoise;

	bool fading = false;

	public bool isWalking = false;

	//change this number to modulate how many increments sound can be broken into. aka if you want a 100pt slider, make this 100
	private int soundChangeIncrementNumber = 4;

	private float soundPercentageChange;

	bool canWalk = true;
	bool fadeInFinish = false;

	public static SoundManager instance;

	private ArrayList longSounds = new ArrayList();

	private int musicCounter = 0;
	private int sfxCounter = 0;

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

	public void StopWalk() {
		isWalking = false;
		if(walkNoise != null) {
			walkNoise.audio.Stop();
			Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/HeelsStep"), SoundType.Sfx);
		}
	}

	public void CantWalk() {
		canWalk = false;
		if(walkNoise != null) {
			walkNoise.audio.Stop();
		}
	}

	public void CanWalk() {
		canWalk = true;
		isWalking = false;
	}

	/*Plays 2D Music by selecting the empty object defined at the top and attaching a sound clip to it. 
	Then it plays the clip and loops it until another music is defined, or the KillMusic function is called.
	If something will try to play a song, it will ignore it unless it is a different song, in which case, it 
	will kill the music and play the new song.
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

	public void TransitionMusic(AudioClip clip) {
		musicObj = new GameObject("SoundPlayer");
		musicObj.transform.position = Camera.main.transform.position;
		musicObj.transform.parent = SoundManager.Instance.transform;
		curClip = clip;
		AudioSource newSource = musicObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.loop = true;
		newSource.audio.volume = 0;
		Debug.Log("playing");
		newSource.audio.Play();
		fading = false;
		fadeInFinish = false;
		StartCoroutine("FadeIn");

	}

	IEnumerator FadeIn() {
		while(musicObj.audio.volume <= musicVolume && !fading && fadeInFinish == false) {
			musicObj.audio.volume += 0.01f;
			if(musicObj.audio.volume == musicVolume) fadeInFinish = true;
			yield return new WaitForSeconds(0.001f);
		}
	}

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



	public void MoveSpeaker(Camera mainCam) {
		musicObj.transform.position = mainCam.transform.position;
	}

	public void EditSfx(int a) {
		if(sfxVolume < 0) sfxVolume = 0;
		if(sfxVolume > 1) sfxVolume = 1;
		if(a == -1) {
			if(sfxVolume > 0) {
				sfxVolume -= soundPercentageChange;
				sfxCounter++;
			}
			else if((sfxVolume != gameVolume || gameVolume == 0) && sfxCounter < soundChangeIncrementNumber) sfxCounter++;
		}
		if(a == 1) {
			if(sfxCounter != 0) {
				if(sfxCounter * soundPercentageChange != gameVolume - sfxVolume) sfxCounter--;
				else {
					sfxCounter--;
					sfxVolume += soundPercentageChange;
				}
			}
		}
		if(longSounds.Count != 0) {
			foreach(GameObject obj in longSounds) {
				obj.audio.volume = sfxVolume;
			}
		}
	}


	public void EditMusic(int a) {
		if(musicVolume < 0) musicVolume = 0;
		if(musicVolume > 1) musicVolume = 1;
		if(a == -1) {
			if(musicVolume > 0) {
				musicVolume -= soundPercentageChange;
				musicCounter++;
			}
			else if((musicVolume != gameVolume || gameVolume == 0) && musicCounter < soundChangeIncrementNumber) musicCounter++;
		}
		if(a == 1) {
			if(musicCounter != 0) {
				if(musicCounter * soundPercentageChange != gameVolume - musicVolume) musicCounter--;
				else {
					musicCounter--;
					musicVolume += soundPercentageChange;
				}

			}
		}
		musicObj.audio.volume = musicVolume;
	}


	public void EditAll(int a) {
		if(gameVolume < 0) gameVolume = 0;
		if(gameVolume > 1) gameVolume = 1;
		if(sfxVolume < 0) sfxVolume = 0;
		if(sfxVolume > 1) sfxVolume = 1;
		if(musicVolume < 0) musicVolume = 0;
		if(musicVolume > 1) musicVolume = 1;
		if(a == -1 && gameVolume > 0) {
			gameVolume -= soundPercentageChange;
			if(musicVolume > 0) {
				musicVolume -= soundPercentageChange;
			}

			if(sfxVolume > 0) {
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




	void Update() {
		if(Input.GetKeyDown(KeyCode.T)) {
			EditMusic(1);
		}
		if(Input.GetKeyDown(KeyCode.Y)) {
			EditMusic(-1);
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
	}
}
