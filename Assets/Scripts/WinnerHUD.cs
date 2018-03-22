using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerHUD : MonoBehaviour {

	public Text player;

    private void Awake()
    {
    }
    void Start () {
		if(NewControl.finalWinner != null)
			player.text = NewControl.finalWinner.name;
	}
	void Update()
    {
        if (NewControl.finalWinner != null)
        {
            player.text = NewControl.finalWinner.name;
 
        }

        if (Input.GetButtonDown("Submit")) {
			this.gameObject.SetActive (false);
			Time.timeScale = 1;
 
			SceneManager.LoadScene ("Menu");

		} else Time.timeScale = 0;
        
	}

    void Default()
    {
        NewControl.killers = null;
        NewControl.players = null;
        NewControl.guards = null;
        NewControl.numOfPlayers = 0;
        NewControl.objComplete = false;
        NewControl.objKilledByGuard = false;
        NewControl.timeLeft = 0;
        NewControl.objective = null;
        NewControl.finalWinner = null;
        NewControl.parcialWinner = null;
        NumCanvasSeleccionJugadores.ready_P1 = false;
        NumCanvasSeleccionJugadores.ready_P2 = false;
        NumCanvasSeleccionJugadores.ready_P3 = false;
        NumCanvasSeleccionJugadores.ready_P4 = false;
    }
  
}
