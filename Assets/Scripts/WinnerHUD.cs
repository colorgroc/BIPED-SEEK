using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerHUD : MonoBehaviour {

	public Text player;
    //public static GameObject[] winners;
    // Use this for initialization
    private void Awake()
    {
        //this.gameObject.SetActive(false);
    }
    void Start () {
		
		
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
		} else Time.timeScale = 0;
        if (NewControl.finalWinner != null)
			player.text = NewControl.finalWinner.name;
	}

}
