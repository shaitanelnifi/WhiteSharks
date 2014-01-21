using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {

	enum GuiltLevel
	{
		guilty=1,
		suspect,
		witness
	};

	enum Category
	{
		WeaponType1=1,
		WeaponType2,
		WeaponType3,
		WeaponType4,
		PersonalItem
	};
}
