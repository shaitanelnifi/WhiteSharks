using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/*
	 * createConversation (ArrayList alibi)
	 * This function works in the following way:
	 * Since the main premise of what makes a NPC guilty, suspect or witness is the number of arguments his alibi has,
	 * it sets up a three way switch/case so that when it's guilty (it contains one element) it only uses that element and
	 * gets a random room from the game manager (so it could be a lie or not).
	 * If it's suspect or witness, it uses all the elements in his/her alibi (and returns them as string).
	 */
	string createConversation (ArrayList alibi){
		switch (alibi.Count) {
		case GuiltLevel.guilty:
			int roomListSize = gameManager.Instance.rooms.Count;
			Random r = new Random();
			int rInt = r.Next(0, roomListSize);
			return alibi[0] + ", I was in " + gameManager.Instance.rooms[rInt] + ".";
		case GuiltLevel.suspect:
			//getRoomName()
			return alibi[0] + ", I was in " + gameManager.Instance.alibi[1] + ".";
		case GuiltLevel.witness:
			//getRoomName()
			NPC n = alibi[2];
			return alibi[0] + ", I was in " + alibi[1] + " with " + n.getElementName + ".";
			
		}
		return "";
	}


	string createInformation (int guilt){
		if (guilt == GuiltLevel.guilty) {
			return "This weapon was used.";		
		} else {
			return "This weapon wasn't used.";				
		}
	}
}
