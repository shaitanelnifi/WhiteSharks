using UnityEngine;
using System.Collections;

public class JournalNotification : MonoBehaviour {

	public void disableNotification(){
		this.GetComponent<TweenAlpha>().enabled = false;
		this.GetComponent<UIButtonMessage>().enabled = false;
	}
}
