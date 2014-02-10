using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/*
 * This class is meant to hold game state as the NPC or Player knows it
 * When you talk to certain NPCs, they update the players Dictionary with
 * their own entries.  Entries are the key to unlocking different dialogue
 * options (they act as preconditions).
*/
public class DictEntry : MonoBehaviour {
	
	public string index;			//Look up npcs through a string index (their name)
	public GuiltLevel guilt;		//Their guilt level
	public Category weapon;			//Their weapon
	public string location;			//Their location
	public List<string> relations;	//If the NPC is connected to another somehow, store the other npcs indices here
	public float trust;				//Their trust

	public string getIndex(){
		return index;
	}

	public GuiltLevel getGuilt(){
		return guilt;
	}

	public Category getWeapon(){
		return weapon;
	}

	public string getLocation(){
		return location;
	}

	public float getTrust(){
		return trust;
	}

	//Print entry to console, useful for testing/debugging
	//Does not print relations
	public void printEntry(){
		print ("[ " + index + "-> Guilt: " + guilt.ToString () + ", Weapon: " + weapon.ToString () + ", Loc: " + location + 
						", Trust: " + trust + " ]\n");
		}

	//When given an entry whose index matches, update values to the new entry, otherwise print error statement
	public void updateEntry(DictEntry newEntry){

		if (index == newEntry.getIndex ()) {
			guilt = newEntry.getGuilt ();
			weapon = newEntry.getWeapon ();
			location = newEntry.getLocation ();
			trust = newEntry.getTrust ();
		}
	}

	//Alter the Equals() to allow DictEntry comparison, handy for comparing a preCondition to the current player knowledge
	public override bool Equals (object o){
		DictEntry otherEntry = (DictEntry)o;

		if (otherEntry == null)
			return false;

		if (index == otherEntry.getIndex () && 
		    guilt == otherEntry.getGuilt ()  && 
		    weapon == otherEntry.getWeapon ()  && 
		    location == otherEntry.getLocation ()  && 
		    trust == otherEntry.getTrust () ) {
			return true;
		}
		
		return false;
	}

	//Alter the == to allow DictEntry comparison, handy for comparing a preCondition to the current player knowledge	
	public static bool operator ==(DictEntry oldEntry, DictEntry otherEntry){

		return oldEntry.Equals (otherEntry);

	}

	//Alter the != to allow DictEntry comparison, handy for comparing a preCondition to the current player knowledge	
	public static bool operator !=(DictEntry oldEntry, DictEntry newEntry){
		
		if (oldEntry.getIndex() != newEntry.getIndex () && 
		    oldEntry.getGuilt() != newEntry.getGuilt ()  && 
		    oldEntry.getWeapon() != newEntry.getWeapon ()  && 
		    oldEntry.getLocation() != newEntry.getLocation ()  && 
		    oldEntry.getTrust() != newEntry.getTrust () ) {
			return true;
		}
		
		return false;
	}

}
