using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerHUD : MonoBehaviour {

	public Text player;
	//public static GameObject[] winners;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		this.gameObject.SetActive (false);
		//player = GetComponent<GameObject>();
		if(NewControl.finalWinner != null)
			player.text = NewControl.finalWinner.name;
	}
	void Update(){
		if (Input.GetButtonDown("Submit")) {
			this.gameObject.SetActive (false);
			Time.timeScale = 1;
			SceneManager.LoadScene (0);
			//SceneManager.LoadScene ("menu", LoadSceneMode.Single);
		}
		if(NewControl.finalWinner != null)
			player.text = NewControl.finalWinner.name;
	}

}
