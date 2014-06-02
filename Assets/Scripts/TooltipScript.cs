using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour {

	public string toolText;
	public int sizeOfFont = 24;

	private static Font font;
	private string currText;
	private GUIStyle guiStyleFore;
	private GUIStyle guiStyleBack;

	private static playerScript player;
	private DoorScript doorscript;

	private static bool revealAll = false;
	private Vector3 point;
	private int iconSize = 64;
	public Texture2D ClickRevealer;
	public Vector2 iconMod = new Vector2 (0, 0);
	public Vector2 scale = new Vector2 (0, 0);

	//public Vector2 textAdjusts = new Vector2(157, 30);

	// Use this for initialization
	void Start(){

		if (font != null)
			font = Resources.Load ("Fonts/KoushikiSans") as Font;

		if (ClickRevealer == null)
			ClickRevealer = Resources.Load ("Textures/IndicatorRing") as Texture2D;

		if (scale.x == 0f) {
			scale.x = transform.localScale.x;
			scale.y = transform.localScale.y;
		}

		if (player == null)
			player = FindObjectOfType (typeof(playerScript)) as playerScript;

		doorscript = GetComponent<DoorScript>();

		//iconMod.x *= transform.localScale.x;
		//iconMod.x *= transform.localScale.y;

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
		else
		if (player.canWalk)
			currText = toolText;
	}
	
	void OnMouseExit (){
		currText = "";
	}

	void OnGUI(){
		if (currText != "" && !revealAll) {
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;

			GUI.Label (new Rect (x - 150, Screen.height - y - 30, 300, 60), currText, guiStyleBack);
			GUI.Label (new Rect (x - 151, Screen.height - y - 29, 300, 60), currText, guiStyleFore);
		} else if (revealAll && toolText != "") {
			float x = point.x;
			float y = point.y; // bottom left corner set to the 3D point

			//Debug.LogWarning("SPACE2");

			GUI.DrawTexture(new Rect(x - (32 + iconMod.x) * scale.x, Screen.height - y - (32 + iconMod.y) * scale.y, 
			                         iconSize * scale.x, iconSize * scale.y), ClickRevealer);

			if (doorscript != null){
				GUI.Label (new Rect(x - (32 + iconMod.x) * scale.x, Screen.height - y - (32 + iconMod.y) * scale.y, 20, 20), doorscript.getOrder ().ToString(), guiStyleFore);
				GUI.Label (new Rect(x - (32 + iconMod.x) * scale.x, Screen.height - y - (31 + iconMod.y) * scale.y, 20, 20), doorscript.getOrder ().ToString(), guiStyleBack);
			}
			//GUI.Label (new Rect (x - iconMod.x - 150, Screen.height - y - iconMod.y * transform.localScale.y -30, 300, 60), toolText, guiStyleBack);
			//GUI.Label (new Rect (x - textAdjusts.x + 1 , Screen.height - y - textAdjusts.y * transform.localScale.y + 31, 300, 60), toolText, guiStyleFore);
		}
	}

	void Update(){

			if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton(1)) {
				revealAll = true;
				point = Camera.main.WorldToScreenPoint(transform.position);
			} else {
				revealAll = false;
			}

	}

}
