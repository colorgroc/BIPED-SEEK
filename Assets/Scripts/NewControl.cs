using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewControl : MonoBehaviour
{
   
    [SerializeField]
    public static int numOfPlayers;
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
    [SerializeField]
    public static int characterPlayer_1, characterPlayer_2, characterPlayer_3, characterPlayer_4;
    // Use this for initialization
    void Start()
    {
        numOfPlayers = PlayerPrefs.GetInt("NumPlayers");
        paused = false;
		pausa.SetActive (false);
		timeLeft = UnityEngine.Random.Range(60, 3*60);
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);

        for(int i = 1; i <= numOfPlayers; i++)
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + i.ToString()).ToString());
            GameObject player = (GameObject)Instantiate(prefab, allMyRespawnPoints[random].transform.position, allMyRespawnPoints[random].transform.rotation);
            player.transform.parent = GameObject.Find("Players").transform;
            player.gameObject.name = "Player_" + i.ToString(); 
        }
        /*GameObject prefab_1 = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_1").ToString());
        
        GameObject Player_1 = (GameObject)Instantiate(prefab_1, allMyRespawnPoints[random].transform.position, allMyRespawnPoints[random].transform.rotation);
        Player_1.transform.parent = GameObject.Find("Players").transform;

        GameObject prefab_2 = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_2").ToString());

        GameObject Player_2 = (GameObject)Instantiate(prefab_1, allMyRespawnPoints[random].transform.position, allMyRespawnPoints[random].transform.rotation);
        Player_2.transform.parent = GameObject.Find("Players").transform;*/

        //añadir jugadores activos
        for(int i = 1; i <= numOfPlayers; i++){
            players.Add(GameObject.Find("Player_" + i.ToString()));
        }
        /*if (numOfPlayers == 2)
        {
            //players.Add(GameObject.Find("Player_" + i.toString()));
            players.Add(GameObject.FindGameObjectWithTag("Player 1"));
            players.Add(GameObject.FindGameObjectWithTag("Player 2"));
        }*/
        guards = GameObject.FindGameObjectsWithTag("Guard");
        killers = GameObject.FindGameObjectsWithTag("Killer Guards");

        timeStartLeft = timeLeft;

        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerControl>().timePast = 0;
            player.GetComponent<PlayerControl>().Respawn(player.gameObject);
        }
        //eleccio objectiu
        RecalculaObjetivo();
        showObjective = objective;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            RespawnNPCS();
        }
        if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
			pausa.SetActive (paused);
		}
		if (Input.GetKeyDown (KeyCode.Return) && paused) {
			pausa.SetActive (false);
			Time.timeScale = 1;
			SceneManager.LoadScene (0);
			paused = false;
		}
		if (paused)
			Time.timeScale = 0;
		else Time.timeScale = 1;

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

            RecalculaObjetivo();
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

            RecalculaObjetivo();
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
            RecalculaObjetivo();
            objKilledByGuard = false;
            foreach (GameObject player in players)
            {
				player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
        }
    }

    void RespawnNPCS()
    {
        foreach (GameObject guard in guards)
        {
            guard.GetComponent<NPCConnectedPatrol>().Respawn(guard.gameObject);
        }
        foreach (GameObject killer in killers)
        {
            killer.GetComponent<NPCConnectedPatrol>().Respawn(killer.gameObject);
        }
    }

    void RecalculaObjetivo()
    {
        random = UnityEngine.Random.Range(0, numOfPlayers);
        objective = GameObject.FindGameObjectWithTag("Player " + (random + 1).ToString());
    }

  
}
