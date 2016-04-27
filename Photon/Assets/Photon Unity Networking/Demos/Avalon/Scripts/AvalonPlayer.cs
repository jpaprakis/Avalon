using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AvalonPlayer : Photon.MonoBehaviour {
	private string numPlayers = "5";

	void Start() {
		
	}

	void Update() {
		
	}

	void OnGUI() {
		if ((!GameSettings.entered_players) && (photonView.owner.Equals (PhotonNetwork.masterClient))) {
			if (photonView.isMine) {
				numPlayers = GUILayout.TextArea (numPlayers, 2);
				if (GUILayout.Button ("Setup")) {
					GameSettings.entered_players = true;
					GameSettings.num_players = Int32.Parse (numPlayers);

					//Send over to the other players that game settings are set
					photonView.RPC ("SetGameSettings", PhotonTargets.AllBufferedViaServer, GameSettings.entered_players, GameSettings.num_players);
				}
			} else {
				GUILayout.Label ("The room owner is setting up the game, please wait");
			}
		} 
		//Entered the amt of players, room is full, cards not dealt
		else if ((GameSettings.entered_players) && (PhotonNetwork.playerList.Length == GameSettings.num_players) && (!GameSettings.dealt)) {
			GUILayout.Label ("Ready to begin!");

			//Master client deals
			if (photonView.owner.Equals (PhotonNetwork.masterClient) && (photonView.isMine)) {
				GameSettings.Deal (photonView);
			}
		
		//Ready to play the game!
		} else if ((GameSettings.entered_players) && (PhotonNetwork.playerList.Length == GameSettings.num_players) && (GameSettings.dealt)) {
			if (photonView.isMine) {
				string cardLabel = "Your Card: " + GameSettings.playerRole;
				GUILayout.Label (cardLabel);

				string otherLabel = "Other cards you can see: ";

				if (GameSettings.known_cards != null) {
					for (int i = 0; i < GameSettings.known_cards.Length; i++) {
						string cardstr = GameSettings.known_cards [i] + ",";
						otherLabel += cardstr;
					}
					GUILayout.Label (otherLabel);
				} else {
					GUILayout.Label ("no other known cards");
				}
			}
		}

		//Entered amt of players, waiting on all to join room
		else if (GameSettings.entered_players) {
			GUILayout.Label ("Waiting on players to join...");
		}

	}

	[PunRPC]
	void SetGameSettings(bool enteredPlayers, int numPlayers){
		GameSettings.entered_players = enteredPlayers;
		GameSettings.num_players = numPlayers;
	}


	[PunRPC]
	void SetDealSettings( int playerCard, string[] otherKnownCards){
		GameSettings.dealt = true;
		Debug.Log ("CARD NUM IS " + playerCard);
		GameSettings.playerRole = playerCard;
		GameSettings.known_cards = otherKnownCards;
	}

}
