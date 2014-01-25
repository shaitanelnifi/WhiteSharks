// ------------------------------------------------------------------------------
// CaseGenerator
// This class initializes the case and takes care of the random generation.
// It's only purpose is to determine: Murderer, Weapon, Location and to set the alibies of the different NPCs.
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseGenerator : MonoBehaviour {

	private List<NPC> npcs = new List<NPC>();
	private List<NPC> activeNpcs;
	private NPC guilty;
	private NPC weapon;

	public CaseGenerator(){
		foreach(NPC n in GameManager.npcList){
			npcs.Add(n);
		}
		int rg = Random.Range(0, npcs.Count - 1);
		guilty = makeGuilty (npcs[rg]);
	}


	private NPC makeGuilty(NPC n){
		n.setGuilt (GuiltLevel.guilty);
		return n;
	}	



	private void setAlibi(){

	
	}

	void Start(){


	}
					
					//NPC npc1 = new NPC(Noel Alt, Description, Whatever);
					
					
				
}

