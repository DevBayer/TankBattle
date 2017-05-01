using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	// Use this for initialization
	public void loadPlay(){
		SceneManager.LoadScene("Lobby");
	}
}
