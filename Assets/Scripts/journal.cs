using UnityEngine;
using System.Collections;

public class journal : MonoBehaviour {
	//PoI buttons
	public GameObject poI2;

	private ArrayList personsOfInterest;

	//Test case elements
	public Sprite noelPortrait;
	public Sprite emptyPortrait;
	private CaseElement npc1;
	private CaseElement npc2;
	private CaseElement npc3;

	// Use this for initialization
	void Start () {
		npc1.setElementName("Noel");
		npc1.setDescription("Leader of the Young Advisors program/lobby, Noel has always been an outstanding member of our community. Graduated with honours in both human biology and psychology, Noel is the person to get in touch with if you ever feel in need of help.");
		npc1.setProfileImage(noelPortrait);
		npc1.setGuilt (GuiltLevel.guilty);
		npc1.setLocation(1);
		personsOfInterest.Add(npc1);
		//personsOfInterest.Add(npc2);
		//personsOfInterest.Add(npc3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setPoIView(){
	}
}
