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
        if (NewControl.finalWinner != null)
        {
            player.text = NewControl.finalWinner.name;
            //Default();
        }

        if (Input.GetButtonDown("Submit")) {
			this.gameObject.SetActive (false);
			Time.timeScale = 1;
            Default();
			SceneManager.LoadScene ("menu");
			//SceneManager.LoadScene ("menu", LoadSceneMode.Single);
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
    /*
    public static int numOfPlayers;
    public static bool objComplete;
    public static bool objKilledByGuard;
    public static float timeLeft;
    private float timeStartLeft;
    public static GameObject objective;
	public static GameObject parcialWinner;
	public static GameObject finalWinner;
     */
}
