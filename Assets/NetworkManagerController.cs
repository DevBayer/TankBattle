using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerController : NetworkManager {

	public void StartUpHost(){
		NetworkManager.singleton.StartHost();
	}
	public void JoinGame(){
		SetIPAddress();
		SetPort();
		NetworkManager.singleton.StartClient();
	} 

	void SetIPAddress(){
		string ipAddress = GameObject.Find("inputAddress").transform.FindChild("Text").GetComponent<Text>().text;
	}	

	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}


}
