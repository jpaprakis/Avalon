using UnityEngine;
using System.Collections;

public class Minion : Card {

	public override void createCard() {

		this.setName("Minion of Mordred");

		this.setGood (false);

		this.setSpecial ("");

		this.setImg ("");

		this.setOtherCards ("Other Minions of Mordred Visisble to You");
	}
}