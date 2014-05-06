using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour {

	public string toolText;
	public int sizeOfFont = 24;

	private static Font font;
	private string currText;
	private GUIStyle guiStyleFore;
	private GUIStyle guiStyleBack;

	private playerScript player;

	// Use this for initialization
	void Start(){

		if (font != null)
			font = Resources.Load ("Fonts/KoushikiSans") as Font;

		guiStyleFore = new GUIStyle();
		guiStyleFore.normal.textColor = Color.white;  
		guiStyleFore.alignment = TextAnchor.UpperCenter ;
		guiStyleFore.wordWrap = true;
		guiStyleFore.fontSize = sizeOfFont;
		guiStyleFore.font = font;
		guiStyleBack = new GUIStyle();
		guiStyleBack.normal.textColor = Color.black;  
		guiStyleBack.alignment = TextAnchor.UpperCenter ;
		guiStyleBack.wordWrap = true;
		guiStyleBack.font = font;
		guiStyleBack.fontSize = sizeOfFont;
	}
	
	void OnMouseEnter (){

		if (player == null)
			player = FindObjectOfType (typeof(playerScript)) as playerScript;

		if (player.canWalk)
			currText = toolText;
	}
	
	void OnMouseExit (){
		currText = "";
	}

	void OnGUI(){
		if (currText != ""){
			var x = Input.mousePosition.x;
			var y = Input.mousePosition.y;

			GUI.Label (new Rect (x - 150, Screen.height - y - 30, 300, 60), currText, guiStyleBack);
			GUI.Label (new Rect (x - 151, Screen.height - y - 29, 300, 60), currText, guiStyleFore);
		}
	}

}
