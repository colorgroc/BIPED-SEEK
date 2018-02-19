using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject showObjective;
    public static int random;
    public static List<GameObject> players = new List<GameObject>();
    public GameObject[] guards;
    public GameObject[] killers;
	private bool paused;
	[SerializeField]
	private GameObject pausa, objectiveCanvas;
	public GameObject finalWinnerCanvas;
	private int topScore;
    [SerializeField]
    public static int characterPlayer_1, characterPlayer_2, characterPlayer_3, characterPlayer_4;
    private int timesPlayed;
    [SerializeField]
    private int rondas = 3;
    // Use this for initialization
    void Awake()
    {
        numOfPlayers = PlayerPrefs.GetInt("NumPlayers");
        paused = false;
		pausa.SetActive (false);
		timeLeft = UnityEngine.Random.Range(60, 3*60);
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        
        MapaRandom();

        //creacion jugadores
        for(int i = 1; i <= numOfPlayers; i++)
        {
            int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            
            //es crea player desde la seleccio escollida (es crida prefab)
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()).ToString());
            GameObject player = (GameObject)Instantiate(prefab, allMyRespawnPoints[random].transform.position, allMyRespawnPoints[random].transform.rotation);
            player.transform.parent = GameObject.Find("Players").transform;
            player.gameObject.name = "Player_" + i.ToString();
            player.gameObject.tag = "Player " + i.ToString();
            player.gameObject.layer = 8;

            //creacion de guards x jugador 
           /* for (int y = 0; y < 10; y++)
            {  
                int rand = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
                GameObject prefabG = (GameObject)Resources.Load("Prefabs/Tipo_Guard_" + PlayerPrefs.GetInt("characterPlayer_" + i.ToString()).ToString());
               // GameObject prefabG = (GameObject)Resources.Load("Prefabs/Tipo_3");
                GameObject guard = (GameObject)Instantiate(prefabG, allMyRespawnPoints[rand].transform.position, allMyRespawnPoints[rand].transform.rotation);
                guard.transform.parent = GameObject.Find("Guards").transform;
                guard.gameObject.name = "Guard_Tipo_" + i.ToString();
                guard.gameObject.tag = "Guard";
            }*/
        }
        //lo q te a veure amb els guards d moment, al no haverhi prefab, no va i per tant, el q hi ha a continuació no es fa

        //añadir jugadores activos a una lista de control
        for(int i = 1; i <= numOfPlayers; i++){
            players.Add(GameObject.Find("Player_" + i.ToString()));
        }
        
        //guards = GameObject.FindGameObjectsWithTag("Guard");
        killers = GameObject.FindGameObjectsWithTag("Killer Guards");

        timeStartLeft = timeLeft;
        RespawnNPCS();
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerControl>().timePast = 0;
            player.GetComponent<PlayerControl>().Respawn(player.gameObject);
        }
        //eleccio objectiu
        RecalculaObjetivo();
        //showObjective = objective;
    }
    private void Start()
    {
       // RecalculaObjetivo();
    }

    // Update is called once per frame
    void Update()
    {
      
        Pausa();

        //showObjective = objective;
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
            timesPlayed++;
            objComplete = false;
            foreach (GameObject player in players)
            {
				if(player != parcialWinner)
					player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
            RespawnNPCS();
           
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
            timesPlayed++;
            timeLeft = timeStartLeft;
            foreach (GameObject player in players)
            {
				if(player != parcialWinner)
					player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
            RespawnNPCS();
			parcialWinner = null;
        }
        if (objKilledByGuard)
        {
            Debug.Log("U got killed noob!");
            //Pause (p);
            RecalculaObjetivo();
            timesPlayed++;
            objKilledByGuard = false;
            foreach (GameObject player in players)
            {
				player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            }
            RespawnNPCS();
        }

        if (timesPlayed == rondas)
        {
            finalWinnerCanvas.SetActive(true);
        }
    }
    private void MapaRandom()
    {
        int mapa = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("NumMapas"));
        if (mapa + 1 == 1) GameObject.Find("Plaza_2").SetActive(false);
        if (mapa + 1 == 2) GameObject.Find("Plaza").SetActive(false);

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
        random = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("NumPlayers"));
        objective = GameObject.Find("Player_" + (random + 1).ToString());
        showObjective = objective;
        ShowObjectiveCanvas();

    }
    void ShowObjectiveCanvas()
    {
        objectiveCanvas.SetActive(true);
    }
    private void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            paused = !paused;
            pausa.SetActive(paused);
        }
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit")) && paused)
        {
            pausa.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
            paused = false;
        }
        if (paused)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }

}
