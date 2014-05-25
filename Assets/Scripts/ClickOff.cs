using UnityEngine;
using System.Collections;

public class ClickOff : MonoBehaviour {

	float progress = 0;
	Vector2 pos = new Vector2(20,40);
	Vector2 size = new Vector2(60,20);
	Texture2D progressBarEmpty;
	Texture2D progressBarFull;
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progressBarEmpty);
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(progress), size.y), progressBarFull);
	} 
	
	void Update()
	{
		progress = Time.time * 0.05f;
	}
}
