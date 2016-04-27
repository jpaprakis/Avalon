using UnityEngine;
using System.Collections;

public class Assassin : Card {

	public override void createCard() {

		this.setName("Assassin");

		this.setGood (false);

		this.setSpecial ("Assassinates one player who they think is Merlin at the end of the game");

		this.setImg ("");
	}
}