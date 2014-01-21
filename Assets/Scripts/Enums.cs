using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class Enums : MonoBehaviour {

	enum GuiltLevel
	{
		guilty,
		suspect,
		witness
	};

	enum Category
	{
		WeaponType1,
		WeaponType2,
		WeaponType3,
		WeaponType4,
		PersonalItem
	};



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
=======
public enum Category {
	WeaponType1 = 1,
	WeaponType2,
	WeaponType3,
	WeaponType4,
	PersonaItem
};

public enum GuiltLevel {
	Guilty = 1,
	Suspect,
	Witness
};
>>>>>>> 387d5b74d8b87077ff41f8feb9758fcca49ef57b
