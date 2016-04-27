using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameSettings : Photon.MonoBehaviour {

	public static int num_players;
	public static List <Card> player_cards = new List<Card> ();
	public static bool entered_players = false;
	public static PhotonPlayer[] allPlayers; 
	public static bool dealt = false;
	public static string[] known_cards;
	public static int playerRole;
	private static bool dealtOnce = false;

	//{# players, # good guys}
	private static Dictionary<int, int> playerConfig = new Dictionary<int, int>()
	{	{ 3, 2 },
		{ 5, 3 },
		{ 6, 4 },
		{ 7, 4 },
		{ 8, 5 },
		{ 9, 6 },
		{ 10, 6 }
	};

	/*
		CardConfig ints:

		1 - Merlin
		2 - Assassin
		3 - Servant of Arthur
		4 - Minion of Mordred
	*/

	private static List<int> RandomizePlayerOrder () {
		List<int> consecPlayers = new List<int>();
		List<int> playerPos = new List<int>();
		int randNum;
	
		System.Random rand = new System.Random ();

		for (int j = 0; j < num_players; j++) {
			consecPlayers.Add(j);
		}

		for (int i = num_players - 1; i >= 0; i--) {
			randNum = rand.Next (0, i+1);
			playerPos.Add (consecPlayers[randNum]);
			consecPlayers.Remove(consecPlayers[randNum]);
		}

		return playerPos;

	}

	public static void Deal(PhotonView photonView) {
		Debug.Log ("IN DEAL METHOD");

		//Make sure we only call the Deal method once
		if (dealtOnce)
			return;

		dealtOnce = true;
		int numGood = 0;
		int numBad;
		//TODO: Change this to be configurable based on amt of special cards
		int numSpecialGood = 1;
		int numSpecialBad = 1;

		List <int> playerOrder = RandomizePlayerOrder ();

		allPlayers = PhotonNetwork.playerList;		
	

		numGood = playerConfig[num_players];
		numBad = num_players - numGood;	


		int curPlayerNum;
		int orderIdx = 0;
	

		//GET BAD GUY ARRAY OF EVERY BAD GUY PPL WILL SEE
		string [] badGuys = new string[numBad];

		for (int b = 0; b < numBad; b++) {
			badGuys [b] = allPlayers[playerOrder [b]].name;
		}

		//Deal generic bad (4)
		for (int bg = 0; bg < numBad - numSpecialBad; bg++) {
			Debug.Log ("deal generic bad");
			curPlayerNum = playerOrder [orderIdx];
			orderIdx++;

			photonView.RPC ("SetDealSettings", allPlayers[curPlayerNum], 4, badGuys);
		}

		//Deal Assassin (2)
		curPlayerNum = playerOrder [orderIdx];
		orderIdx++;
		Debug.Log("deal assassin to player " + curPlayerNum+ " with ID " + allPlayers[curPlayerNum].ID + " and name " + allPlayers[curPlayerNum].name);
		photonView.RPC ("SetDealSettings", allPlayers[curPlayerNum], 2, badGuys);

		//TODO: Deal special bad guys

		//Deal generic good (3)
		for (int g = 0; g < numGood - numSpecialGood; g++) {
			Debug.Log ("deal generic good");
			curPlayerNum = playerOrder [orderIdx];
			orderIdx++;
			Debug.Log ("dealing to player " + curPlayerNum + " with ID " + allPlayers[curPlayerNum].ID+ " and name " + allPlayers[curPlayerNum].name);
			photonView.RPC ("SetDealSettings",  allPlayers[curPlayerNum], 3, null);
		}

		//Deal Merlin (1)
		curPlayerNum = playerOrder [orderIdx];
		orderIdx++;
		Debug.Log("deal merlin to player " + curPlayerNum + " with ID " + allPlayers[curPlayerNum].ID+ " and name " + allPlayers[curPlayerNum].name);
		photonView.RPC ("SetDealSettings", allPlayers[curPlayerNum], 1, badGuys);


		//TODO: Deal special good guys

	
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
