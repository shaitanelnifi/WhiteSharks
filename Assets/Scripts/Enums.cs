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
	maychapter2finplazawrongway = 5,
	maychapter2bellybarfirstarrivalwrongway = 8,
	maychapter2bellymarketwrongway = 12,
	maychapter2bellyalleyprefranksroomwrongway = 15,
	maychapter2franksroomwrongway = 21,
	maychapter2bellyalleypremoewrongway = 22,
	janechapter2fincafealarmprehack = 36,
	janechapter2fincafealarmmidhack = 37,
	janechapter2fincafealarmposthack = 38,
	janechapter2fincafespeakersprehack = 39,
	janechapter2fincafespeakersposthack = 40,
	maychapter2finplazapassingthroughwrongway = 46,
	maychapter2bellybarbyemay = 54,
	maychapter2bellyalleypassingthroughwrongway = 56,
	janechapter2ExamineTrapDoor = 63,
	maychapter2marketpeijunwrongway = 64,
	maychapter2bellybarpostcafewrongway = 65,

	//chapter 3
	noelchapter3finofficenote = 2,
	peijunchapter3bellymarketbeforepeijun = 7,
	joshchapter3fincafepostpeijunpreorder = 13,
	//14, //merged with 13
	maychapter3finofficepostpeijun = 15,
	cantenter = 19,
	maychapter3finlobbywrongway = 21,
	maychapter3finofficenonote = 22,

	//chapter 4
	janechapter4networkmapcelldoor = 4,
	janechapter4mapbots = 5,
	janechapter4bellybarwrongway = 7,
	janechapter4bellybrainroomwrongway = 9,
	janechapter4bellybrainroomterminal = 10,
	maychapter4Networkofficetable = 24,

};


public enum Convo {
	ch0none = -1,
	//chapter 1
	openingNarration = 0,
	frankPartnerBRoom = 1,
	frankPartnerBalcony = 2,
	frankPartnerBedRoom = 3,
	frankBalconyIntro = 37,
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
	frankBracelet = 20,
	frankBlood = 21,
	partnerBlood = 22,
	janeShammyCase = 24,
	
	//chapter 2
	maychapter2finofficewalkwaystart = 0,
	maychapter2finofficewalkwaywaiting = 1,
	maychapter2finofficewalkwaywrongway = 2,
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
	maychapter2fincafeterminal = 35,
	janechapter2fincafecoat = 41,
	maychapter2fincafepostcluewaiting = 42,
	joshchapter2fincafeprecluewaiting = 43,
	noelchapter2brainroomcutscene = 44,
	maychapter2finplazapassingthrough = 45,
	maychapter2bellybarpassingthroughwaiting = 49,
	maychapter2bellypostcafestart = 51,
	maychapter2bellybarpostcafewaiting = 52,
	robotchapter2bellybarpostcafe = 53,
	maychapter2bellyalleypassingthroughwaiting = 55,
	maychapter2bellymarketpassingthroughwaiting = 59,
	//peijunchapter2bellymarketwaiting = 60,
	maychapter2fincafewaiting = 61,

	//chapter 3
	maychapter3finofficelobbystart = 0,
	maychapter3finofficestart = 1,
	noelchapter3finofficenote = 2,
	liamchapter3finplazabeforepeijun = 3,
	ninachapter3finbalconybeforepeijun = 4,
	joshchapter3fincafebeforepeijun = 5,
	robotchapter3bellybarbeforepeijun = 6,
	peijunchapter3bellymarketbeforepeijun = 7,
	peijunchapter3bellypeijunsroom = 8,
	ninachapter3bellymarketpostpeijun = 9,
	robotchapter3bellybarpostpeijun = 10,
	liamchapter3finplazapostpeijun = 11,
	joshchapter3fincafepostpeijun = 12,
	shammychapter3finofficeliamsroom = 16,
	peijunchapter3bellypeijunroomendcutscene = 17,
	alexiachapter3bellypeijunroomnetworkendcutscene = 18,

	//chapter4
	alexiachapter4networkpeijunroomstart = 0,
	maychapter4networkofficestart = 1,
	maychapter4networkofficewrongway = 2,
	maychapter4networkmapstart = 3,
	janechapter4networkmapcelldoor = 4,
	janechapter4mapbots = 5,
	robotchapter4bellybar = 6,
	janechapter4bellybarwrongway = 7,
	janechapter4bellybrainroomstart = 8,
	janechapter4bellybrainroomwrongway = 9,
	janechapter4bellybrainroomterminal = 10,
	shammychapter4networkbrainroomstart = 11,
	janechapter4bellybrainroomshowdown = 12,
	founderchapter4networkbrainroomrip = 13,
	janechapter4endcutscene = 14,
	founderchapter4lostbuttonmash = 15,
	alexiachapter4endnarration = 23,
	
};

public enum FadeType {
	fadeIn = 0,
	fadeOut = 1,
	cycle = 2,
};

public enum GlobalBools {



};