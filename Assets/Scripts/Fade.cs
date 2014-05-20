using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public FadeType typeOfFade = FadeType.fadeIn;
	public float time = 1f;
	private bool isFading = false;
	SpriteRenderer r;
	Color a;
	// Use this for initialization
	void Start () {
		r = GetComponent<SpriteRenderer> ();
		a = r.color;
		a.a = 0;
		r.color = a;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (typeOfFade.Equals(FadeType.fadeIn));
		//Debug.Log (typeOfFade.Equals(FadeType.fadeOut));
		if (typeOfFade.Equals(FadeType.fadeIn)) {
			//Debug.Log(r.color.a);
			if (r.color.a < 1) {
				a.a += 0.01f;
				//Debug.Log (a.a);
				r.color = a;
				//Debug.Log (r.color.a);
						}
				} 
		else if (typeOfFade.Equals(FadeType.cycle)){
			StartCoroutine("activateFadeout");
		} else if (typeOfFade.Equals(FadeType.fadeOut)) {
			if (r.color.a > 0) {
				a.a -= 0.01f;
				//Debug.Log (a.a);
				r.color = a;
				//Debug.Log (r.color.a);
			}
		
		}
	}

	IEnumerator activateFadeout(){
		typeOfFade = FadeType.fadeIn;
		yield return new WaitForSeconds (time);
		typeOfFade = FadeType.fadeOut;
	}
}
