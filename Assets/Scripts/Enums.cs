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
	projector = 17,

	//chapter 2
	maychapter2finofficewalkwaywrongway = 2,
	maychapter2finplazawrongway = 5,
	maychapter2bellybarfirstarrivalwrongway = 8,
	maychapter2bellymarketwrongway = 12,
	maychapter2bellyalleyprefranksroomwrongway = 15,
	maychapter2franksroomwrongway = 21,
	maychapter2bellyalleypremoewrongway = 22,
	maychapter2fincafeterminal = 35,
	janechapter2fincafealarmprehack = 36,
	janechapter2fincafealarmmidhack = 37,
	janechapter2fincafealarmposthack = 38,
	janechapter2fincafespeakersprehack = 39,
	janechapter2fincafespeakersposthack = 40,
	janechapter2fincafecoat = 41,
	//maychapter2finplazawrongway = 46,
	maychapter2bellybarbyemay = 54,
	maychapter2bellyalleypassingthroughwrongway = 56,

};


public enum Convo {
	ch0none = -1,
	//chapter 1
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
	janeShammyCase = 24,

	//chapter 2
	maychapter2finofficewalkwaystart = 0,
	maychapter2finofficewalkwaywaiting = 1,
	maychapter2finplazafirstarrivalstart = 3,
	maychapter2finplazafirstarrivalwaiting = 4,
	maychapter2bellybarfirstarrivalstart = 6,
	maychapter2bellybarfirstarrivalwaiting = 7,
	robotchapter2bellybarclosed = 9,
	maychapter2bellymarketemptystart = 10,
	maychapter2bellymarketemptywaiting = 11,
	maychapter2bellyalleyprefranksroomstart = 13,
	maychapter2bellyalleyprefranksroomwaiting = 14,
	maychapter2franksroomstart = 16,
	maychapter2franksroomprejournalwaiting = 17,
	maychapter2franksroomopenjournal = 18,
	maychapter2franksroomjournal = 19,
	maychapter2franksroompostjournalwaiting = 20,
	chapter2bellyalleypremoewaiting = 23,
	maychapter2moesroomstart = 24,
	maychapter2moesroomprepicturewaiting = 25,
	maychapter2moesroompicture = 26,
	maychapter2moesroompostpicturewaiting = 27,
	maychapter2bellymarketpeijunstart = 28,
	maychapter2bellymarketpeijunprepeijuntalkwaiting = 29,
	maychapter2bellymarketpeijunpostpeijuntalkwaiting = 30,
	peijunchapter2bellymarketpassword = 31,
	peijunchapter2bellymarketwaiting = 32,
	joshchapter2fincafeprecluestart = 33,
	maychapter2fincafeclue = 34,
	maychapter2fincafepostcluewaiting = 42,
	joshchapter2fincafeprecluewaiting = 43,
	noelchapter2brainroomcutscene = 44,
	maychapter2finplazapassingthrough = 45,
	maychapter2bellybarpassingthroughwaiting = 49,
	maychapter2bellybarpostcafewaiting = 52,
	robotchapter2bellybarpostcafe = 53,
	maychapter2bellyalleypassingthroughwaiting = 55,
	maychapter2bellymarketpassingthroughwaiting = 59,
	//peijunchapter2bellymarketwaiting = 60,
	maychapter2fincafewaiting = 61,

};

public enum FadeType {
	fadeIn = 0,
	fadeOut = 1,
	cycle = 2,
};

public enum GlobalBools {



};