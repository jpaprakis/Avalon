using UnityEngine;
using System.Collections;

public class Merlin : Card {
	
	public override void createCard() {
		
		this.setName("Merlin");

		this.setGood (true);

		this.setSpecial ("Knows who the evil people are");

		this.setImg ("");

		this.setOtherCards ("Minions of Mordred Visisble to You");
	}
}