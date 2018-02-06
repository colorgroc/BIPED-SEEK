using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewControl : MonoBehaviour
{
   
    [SerializeField]
    public int numOfPlayers;
    public static bool objComplete;
    public static bool objKilledByGuard;
    [SerializeField]
    public float timeLeft;
    private float timeStartLeft;
    public static GameObject objective;
	public static GameObject parcialWinner;
	public static GameObject finalWinner;
	//public List<GameObject> finalWinners = new List<GameObject>();
    public GameObject showObjective;
    public static int random;
    public static List<GameObject> players = new List<GameObject>();
    public GameObject[] guards;
    public GameObject[] killers;
	private bool paused;
	[SerializeField]
	private GameObject pausa;
	public GameObject finalWinnerCanvas;
	private int topScore;
    // Use this for initialization
    void Start()
    {
		pausa.SetActive (false);
		timeLeft = UnityEngine.Random.Range(60, 3*60);
		//añadir jugadores activos
        if (numOfPlayers == 2)
        {
            players.Add(GameObject.FindGameObjectWithTag("Player 1"));
            players.Add(GameObject.FindGameObjectWithTag("Player 2"));
        }
        guards = GameObject.FindGameObjectsWithTag("Guard");
        killers = GameObject.FindGameObjectsWithTag("Killer Guards");

        timeStartLeft = timeLeft;

        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerControl>().timePast = 0;
            player.GetComponent<PlayerControl>().Respawn(player.gameObject);
        }
        //eleccio objectiu
        random = UnityEngine.Random.Range(0, numOfPlayers);
        if (random == 0)
            objective = GameObject.FindGameObjectWithTag("Player 1");
        else if (random == 1)
            objective = GameObject.FindGameObjectWithTag("Player 2");
        showObjective = objective;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
			pausa.SetActive (paused);
			if (paused)
				Time.timeScale = 0;
			else Time.timeScale = 1;
		}
		if (Input.GetKeyDown (KeyCode.Return) && paused) {
			pausa.SetActive (false);
			Time.timeScale = 1;
			SceneManager.LoadScene (0);

		}
        showObjective = objective;
        timeLeft -= Time.deltaTime;

		//asignar ganador final
		foreach (GameObject player in players)
		{
			if (player.GetComponent<PlayerControl> ().scoreGeneral > topScore) {
				topScore = player.GetComponent<PlayerControl> ().scoreGeneral;
				finalWinner = player;
				//finalWinners.Add (player);

			} else if (player.GetComponent<PlayerControl> ().scoreGeneral == topScore) {
				if (finalWinner != null) {
					int fin = UnityEngine.Random.Range (0, 2);
					if (fin == 1)
						finalWinner = player;
				} else {
					topScore = player.GetComponent<PlayerControl> ().scoreGeneral;
					finalWinner = player;
				}
				
			}
		}
		//asignar puntos ganador parcial
		if (objComplete && parcialWinner != null) //this.gameObject != objective && )
        {
			Debug.Log("Congratulations to " + this.gameObject.name);
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreKills += 1;
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 50;
			/*GameObject.Find ("Winner").GetComponent<Canvas> ().enabled = true;
			Time.timeScale = 0;*/

            //eleccio objectiu
            random = UnityEngine.Random.Range(0, numOfPlayers);
			if (random == 0) {
				objective = GameObject.FindGameObjectWithTag ("Player 1");
			} else if (random == 1) {
				objective = GameObject.FindGameObjectWithTag ("Player 2");
			}
            objComplete = false;
            foreach (GameObject player in players)
            {
				if(player != parcialWinner)
					player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
			parcialWinner = null;
        }
        if (timeLeft <= 0 && !objComplete)
        {
			parcialWinner = objective;
			Debug.Log("Congratulations to " + this.gameObject.name);
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreKills += 1;
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 50;

			/*GameObject.Find ("Winner").GetComponent<Canvas> ().enabled = true;
			Time.timeScale = 0;*/

            random = UnityEngine.Random.Range(0, numOfPlayers);
            if (random == 0)
                objective = GameObject.FindGameObjectWithTag("Player 1");
            else if (random == 1)
                objective = GameObject.FindGameObjectWithTag("Player 2");
            timeLeft = timeStartLeft;
            foreach (GameObject player in players)
            {
				if(player != parcialWinner)
					player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
			parcialWinner = null;
        }
        if (objKilledByGuard)
        {
            Debug.Log("U got killed noob!");
            //Pause (p);
            random = UnityEngine.Random.Range(0, numOfPlayers);
            if (random == 0)
                objective = GameObject.FindGameObjectWithTag("Player 1");
            else if (random == 1)
                objective = GameObject.FindGameObjectWithTag("Player 2");
            objKilledByGuard = false;
            foreach (GameObject player in players)
            {
				player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
        }
    }

  
}
