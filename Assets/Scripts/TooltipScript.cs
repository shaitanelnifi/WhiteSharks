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

	private static bool revealAll = false;
	private Vector3 point;

	public Vector2 textAdjusts = new Vector2(157, 30);

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
		if (currText != "") {
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;

			GUI.Label (new Rect (x - textAdjusts.x, Screen.height - y - 30, 300, 60), currText, guiStyleBack);
			GUI.Label (new Rect (x - textAdjusts.x + 1, Screen.height - y - 29, 300, 60), currText, guiStyleFore);
		} else if (revealAll) {
			float x = point.x;
			float y = point.y; // bottom left corner set to the 3D point

			//Debug.LogWarning("SPACE2");

			GUI.Label (new Rect (x - textAdjusts.x , Screen.height - y - textAdjusts.y * transform.localScale.y + 30, 300, 60), toolText, guiStyleBack);
			GUI.Label (new Rect (x - textAdjusts.x + 1 , Screen.height - y - textAdjusts.y * transform.localScale.y + 31, 300, 60), toolText, guiStyleFore);
		}
	}

	void Update(){

			if (Input.GetKey (KeyCode.Space)) {
				revealAll = true;
				point = Camera.main.WorldToScreenPoint(transform.position);
			} else {
				revealAll = false;
			}

	}

}
