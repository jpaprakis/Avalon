using UnityEngine;
using System.Collections;

public class AvalonNetworkManager : MonoBehaviour {

	private RoomInfo[] roomsList;

	private string roomName = "";
	private string playerName = "";
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}

		else if (PhotonNetwork.room == null)
		{
			GUILayout.Label ("Name:");
			playerName = GUI.TextArea(new Rect (100, 50, 250, 50), playerName, 30);
			// Create Room
			GUILayout.Label ("Create a Room:");
			roomName = GUI.TextArea (new Rect (100, 150, 250, 50), roomName, 30);
			if (GUI.Button(new Rect(100, 200, 250, 100), "Create Room"))
				PhotonNetwork.CreateRoom(roomName);

			// Join Room
			GUILayout.Label ("Join a Room:");
			if (roomsList != null)
			{
				for (int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 350 + (110 * i), 250, 100), "Join " + roomsList[i].name))
						PhotonNetwork.JoinRoom(roomsList[i].name);
				}
			}
		}
	}

	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}
	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");

		PhotonNetwork.Instantiate (playerPrefab.name, new Vector3 (Random.Range (0, 5), Random.Range (0, 5), 0), Quaternion.identity, 0);
		PhotonNetwork.playerName = playerName;
	}

}

