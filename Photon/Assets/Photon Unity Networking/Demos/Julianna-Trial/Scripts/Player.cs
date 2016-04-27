using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (photonView.isMine)
			ChangeMyColor ();

	}

	void ChangeMyColor() {

		string color_to_change;

		if (Input.GetKey (KeyCode.W))
			callRPCColor ("white");

		if (Input.GetKey (KeyCode.S))
			callRPCColor ("grey");

		if (Input.GetKey (KeyCode.D))
			callRPCColor ("red");

		if (Input.GetKey(KeyCode.A))
			callRPCColor ("blue");

	}
		

	void callRPCColor(string color_to_change) {

		Renderer cur_renderer = GetComponent<Renderer> ();

		if (color_to_change == cur_renderer.material.color.ToString()) 
			return;

		RendererToCol (cur_renderer, color_to_change);

		Debug.Log ("Just changed colour");

		photonView.RPC ("RPCColor", PhotonTargets.AllViaServer, color_to_change);
	}


	[PunRPC]
	void RPCColor(string color_to_change) {
		Debug.Log ("In RPC");
		Renderer cur_renderer = GetComponent<Renderer> ();

		RendererToCol (cur_renderer, color_to_change);
	}


	void RendererToCol(Renderer cur_renderer, string color_to_change) {

		if (color_to_change == "white")
			cur_renderer.material.color = Color.white;
		else if (color_to_change == "grey")
			cur_renderer.material.color = Color.grey;
		else if (color_to_change == "red")
			cur_renderer.material.color = Color.red;
		else if (color_to_change == "blue")
			cur_renderer.material.color = Color.blue;
		
	
	}
}
