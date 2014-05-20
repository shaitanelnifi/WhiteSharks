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
	//chapter 1
	ch1sharkLightPoster = 0,
	ch1shammySwag = 1,
	ch1frankBracelet = 2,
	ch1frankBlood = 3,
	ch1partnerBlood = 4,
	ch1noelPicture = 5,
	ch1janeShammyRooms = 6,
	ch1hideoutBed = 7,
	ch1ninaBed = 8,
	ch1liamBed = 9,
	ch1hideoutWindow = 10,
	ch1ninaWindow = 11,
	ch1liamWindow = 12,
	ch1officeExitBlockedStart = 13,
	ch1cafeExitBlockedStart = 14,
	ch1bellyExitBlockedStart = 15,
	ch1leftExitBlockedStart = 16,
	ch1projector = 17,

	//chapter 2
	ch2MayWalkwayStartWrongway = 2,
	ch2MayPlazaStartWrongway = 5,
	ch2MayBarPreFrankWrongway = 8,
	ch2MayMarketPreFrankWrongway = 12,
	ch2MayAlleyPreFrankWrongway = 15,
	ch2MayFranksOpenJournal = 18,
	ch2MayFranksWrongway = 21,


};


public enum Convo {
	ch0none = -1,
	//chapter 1
	ch1openingNarration = 0,
	ch1frankPartnerBRoom = 1,
	ch1frankPartnerBalcony = 2,
	ch1frankPartnerBedRoom = 3,
	ch1janeShammyPreOffice = 4,
	ch1noelAlexiaOffice = 5,
	ch1janeAlexiaOfficeStart = 6,
	ch1janeNoelPlaza = 7,
	ch1janeNinaRoom = 8,
	ch1janeLiamRoom = 9,
	ch1janeNoelDoor = 10,
	ch1janeNoelBalconyEnd = 11,
	ch1janeMayOffice = 12,
	ch1janeAlexiaOfficeEnd = 13,
	ch1janeNoelEnd = 14,
	ch1janeShammyBracelet = 15,
	ch1janeCuteBox = 16,
	ch1janeNinasPhone = 17,
	ch1janeShammyCase = 24,

	//chapter 2
	ch2MayWalkwayPreFrankWaiting = 1,
	ch2MayPlazaPreFrankWaiting = 4,
	ch2MayBarPreFrankWaiting = 6,
	ch2RobotBarPreFrankClosed = 9,
	ch2MayMarketPreFrankWaiting = 11,
	ch2MayAlleyPreFrankWaiting = 14,
	ch2MayFranksPreJournalWaiting = 17,
	ch2MayFranksPostJournalWaiting = 20,


};

public enum FadeType {
	fadeIn = 0,
	fadeOut = 1,
	cycle = 2,
};

public enum GlobalBools {



};