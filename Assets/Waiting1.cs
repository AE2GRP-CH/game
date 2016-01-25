using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Waiting1 : NetworkBehaviour {



	void Awake () {
		if (isLocalPlayer) {
			Debug.Log (string.Format("Client"));
		}

		if (isClient) {
			Debug.Log (string.Format("ASFS"));
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
