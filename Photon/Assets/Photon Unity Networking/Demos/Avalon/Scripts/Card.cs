﻿using UnityEngine;
using System.Collections;

public abstract class Card : MonoBehaviour {

	private string charName;
	private bool isGood;
	private string specialAttr = "";
	private string image = "";
	private string otherCards = "";

	// Use this for initialization
	public abstract void createCard();

	public void setName(string name) {
		this.charName = name;		
	}

	public void setGood(bool good) {
		this.isGood = good;		
	}

	public void setSpecial(string special) {
		this.specialAttr = special;		
	}

	public void setImg(string img) {
		this.image = img;		
	}

	public void setOtherCards(string otherCardstr) {
		this.otherCards = otherCardstr;		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
