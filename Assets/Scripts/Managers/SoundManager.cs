using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	//Volume
	public static float sfxVolume = 1.0f;
	public static float musicVolume = 1.0f;
	public static float dialogueVolume = 1.0f;

	public enum SoundType { Sfx, Music, Dialogue }

	private AudioClip curClip = null;
	private GameObject musicObj;
	private GameObject walkNoise;

	private float gameVolumeMax;

	bool fading = false;

	public bool isWalking = false;

	public static SoundManager instance;

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




	/*Plays 2D Sound Effect by creating an empty object and attaching a sound clip to it. 
	Then it plays the clip and deletes the object after its done playing
	 * 
	 * 
	 * To call, simply run the code below with the soundfile in Resources/SoundEffects
	 * SoundManager.instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/SOUND_FILE_NAME"), SoundManager.SoundType.Sfx);
	 */
	public void Play2DSound(AudioClip clip, SoundType type, bool isDoor = false) {
		//AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
		GameObject newObj = new GameObject("SoundPlayer");
		newObj.transform.position = Camera.main.transform.position;
		if(isDoor) newObj.transform.parent = SoundManager.Instance.transform;
		else newObj.transform.parent = Camera.main.transform;
		AudioSource newSource = newObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.volume = Volume(newSource, type);
		newSource.audio.Play();
		StartCoroutine("DeleteSource", newObj);
	}

	public void WalkSound() {
		isWalking = true;
		//if(walkNoise != null) {
			walkNoise = new GameObject("SoundPlayer");
			walkNoise.transform.position = Camera.main.transform.position;
			walkNoise.transform.parent = Camera.main.transform;
			AudioSource newSource = walkNoise.AddComponent<AudioSource>() as AudioSource;
			newSource.audio.clip = (AudioClip)Resources.Load("Sounds/SoundEffects/HeelsLoop");
			newSource.audio.volume = Volume(newSource, SoundType.Sfx);
			newSource.audio.loop = true;
		//}
		walkNoise.audio.Play();
	}

	public void StopWalk() {
		isWalking = false;
		walkNoise.audio.Stop();
		Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/HeelsStep"), SoundType.Sfx);
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
			//KillMusic();
		}
		musicObj = new GameObject("SoundPlayer");
		musicObj.transform.position = Camera.main.transform.position;
		musicObj.transform.parent = SoundManager.Instance.transform;
		curClip = clip;
		AudioSource newSource = musicObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.loop = true;
		newSource.audio.volume = Volume(newSource, SoundType.Music); //needs some work if volume sliders are added
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
		gameVolumeMax = Volume(newSource, SoundType.Music);
		newSource.audio.volume = 0;
		Debug.Log("playing");
		newSource.audio.Play();
		fading = false;
		StartCoroutine("FadeIn");

	}

	IEnumerator FadeIn() {
		while(musicObj.audio.volume <= gameVolumeMax && !fading) {
			musicObj.audio.volume += 0.005f;
			yield return new WaitForSeconds(0.001f);
		}
	}

	IEnumerator FadeOut(AudioClip clip) {
		while(musicObj.audio.volume > 0) {
			fading = true;
			musicObj.audio.volume -= 0.005f;
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
	public void PauseMusic() {
		musicObj.audio.Pause();
	}

	//Resumes current music
	public void ResumeMusic() {
		musicObj.audio.Play();
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


	//Controls volume of sounds
	float Volume(AudioSource audio, SoundType type) {
		if(type == SoundType.Sfx) {
			return audio.volume * sfxVolume;
		}
		else if(type == SoundType.Music) {
			return audio.volume * musicVolume;
		}
		else if(type == SoundType.Dialogue) {
			return audio.volume * dialogueVolume;
		}
		return -1f;
	}

	public void MoveSpeaker(Camera mainCam) {
		musicObj.transform.position = mainCam.transform.position;
	}

	void Update() {
		if(Input.GetKey(KeyCode.T)) {
			//SoundManager.Instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/Journal"), SoundManager.SoundType.Sfx);
		}
	}
}
