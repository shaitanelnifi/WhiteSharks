using UnityEngine;
using System.Collections;

public enum GuiltLevel
{
	guilty=1,
	suspect=2,
	witness=3,
	unrelated = -1
};

public enum Category
{
	None = 0,			//Implies that the npc has no ability to use weapons
	SmallProjectiles=1,	//Like darts and other small thrown objects
	BluntWeapons =2, 		//Melee weapon
	SharpWeapons=3,		//Melee weapon
	Poisons=4,		//Objects not normally associated with weaponry
	Firearms=5,			//Guns are dangerous
	
	PersonalItem=-1,		//An object with this category is not used for killing but is associated with a character
	unrelated = -2,
	Miscellaneous = -3

};

public enum CaseObjectNames{
	
	unrelated = -1,
	HallPass = 1,	
	Picture = 2,
	Bracelet = 3
	
};

public enum NPCNames{
	
	LiamOShea = 0,
	NinaWalker = 1,
	JoshSusach = 2,
	NoelAlt = 3,
	CarlosFranco = 4,
	PeijunShi = 5,
	Shammy = 6,
	
	unrelated = -2
};

public enum clickableID {
	sharkLightPoster = 0,
	shammySwag = 1,
	frankBracelet = 2,
	frankBlood = 3,
	partnerBlood = 4,
	noelPicture = 5,
	janeShammyRooms = 6,
	hideoutBed = 7,
	ninaBed = 8,
	liamBed = 9,
	hideoutWindow = 10,
	ninaWindow = 11,
	liamWindow = 12,
	officeExitBlockedStart = 13,
	cafeExitBlockedStart = 14,
	bellyExitBlockedStart = 15,
	leftExitBlockedStart = 16,

};


public enum Convo {
	none = -1,
	openingNarration = 0,
	frankPartnerBRoom = 1,
	frankPartnerBalcony = 2,
	frankPartnerBedRoom = 3,
	janeShammyPreOffice = 4,
	noelAlexiaOffice = 5,
	janeAlexiaOfficeStart = 6,
	janeNoelPlaza = 7,
	janeNinaRoom = 8,
	janeLiamRoom = 9,
	janeNoelDoor = 10,
	janeNoelBalconyEnd = 11,
	janeMayOffice = 12,
	janeAlexiaOfficeEnd = 13,
	janeNoelEnd = 14,
	janeShammyBracelet = 15,
	janeCuteBox = 16,
	janeNinasPhone = 17,


};

public enum GlobalBools {



};