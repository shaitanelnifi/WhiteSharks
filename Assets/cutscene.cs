using UnityEngine;
using System.Collections;

public class cutscene : MonoBehaviour {

	SpriteRenderer s;
	public Sprite s0;
	public Sprite s1;
	public Sprite s2;
	public Sprite s3;
	public Sprite s4;
	public Sprite s5;
	public Sprite s6;
	public Sprite s7;


	// Use this for initialization
	void Start () {
		s = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		switch ((int)Dialoguer.GetGlobalFloat (2)) {
		case 0:
			s.sprite = s0;
			break;
		case 1:
			s.sprite = s1;
			break;
		case 2:
			s.sprite = s2;
			break;
		case 3:
			s.sprite = s3;
			break;
		case 4:
			s.sprite = s4;
			break;
		case 5:
			s.sprite = s5;
			break;
		case 6:
			s.sprite = s6;
			break;
		case 7:
			s.sprite = s7;
			break;
		}
	}
}
