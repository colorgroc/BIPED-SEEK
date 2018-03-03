using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class NewControl : MonoBehaviour
{
   
    [SerializeField]
    public static int numOfPlayers;
    public static bool objComplete;
    public static bool objKilledByGuard;
    public static float timeLeft;
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
	private GameObject pausa, objectiveCanvas, finalWinnerCanvas;
	private int topScore;
    [SerializeField]
    public static int characterPlayer_1, characterPlayer_2, characterPlayer_3, characterPlayer_4;
    [SerializeField]
    private Sprite SpriteTipo_1, SpriteTipo_2, SpriteTipo_3, SpriteTipo_4;
    [SerializeField]
    private Text textHUD;
    private int fin;
    // Use this for initialization
    void Awake()
    {
        numOfPlayers = PlayerPrefs.GetInt("NumPlayers");
        
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        MapaRandom();
        
        fin = UnityEngine.Random.Range(0, 2);
        //creacion jugadores
        for (int i = 1; i <= numOfPlayers; i++)
        {
            int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            //Debug.Log(PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()));
            //es crea player desde la seleccio escollida (es crida prefab)
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()).ToString());
            GameObject player = (GameObject)Instantiate(prefab, allMyRespawnPoints[random].transform.position, allMyRespawnPoints[random].transform.rotation);
            player.transform.parent = GameObject.Find("Players").transform;
            player.gameObject.name = "Player " + i.ToString();
            player.gameObject.tag = "Player " + i.ToString();
            if(PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 1)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_1;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 2)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_2;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 3)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_3;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 4)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_4;
            }
            //Sprite icon = (Sprite)Resources.Load("Icons/Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()).ToString());
            //GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = icon;
            player.gameObject.layer = 8;

            //creacion de guards x jugador 
            for (int y = 0; y < 10; y++)
            {  
                int rand = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
                GameObject prefabG = (GameObject)Resources.Load("Prefabs/Guard_Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + i.ToString()).ToString());
               // GameObject prefabG = (GameObject)Resources.Load("Prefabs/Tipo_3");
                GameObject guard = (GameObject)Instantiate(prefabG, allMyRespawnPoints[rand].transform.position, allMyRespawnPoints[rand].transform.rotation);
                guard.transform.parent = GameObject.Find("Guards").transform;
                guard.gameObject.name = "Guard_Tipo_" + i.ToString();
                guard.gameObject.tag = "Guard";
            }
        }
        //lo q te a veure amb els guards d moment, al no haverhi prefab, no va i per tant, el q hi ha a continuació no es fa

        //añadir jugadores activos a una lista de control
        for(int i = 1; i <= numOfPlayers; i++){
            players.Add(GameObject.Find("Player " + i.ToString()));
        }
        
        guards = GameObject.FindGameObjectsWithTag("Guard");
        killers = GameObject.FindGameObjectsWithTag("Killer Guards");

        
       

    }
    private void Start()
    {
        objKilledByGuard = false;
        parcialWinner = null;
        objComplete = false;
        finalWinnerCanvas.SetActive(false);
        paused = false;
        pausa.SetActive(false);
        timeStartLeft = timeLeft;
        RespawnNPCS();
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerControl>().timePast = 0;
            player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            player.GetComponent<FieldOfView>().Start();
        }
        //eleccio objectiu
        RecalculaObjetivo();
        timeLeft = UnityEngine.Random.Range(60, 3 * 60);
        textHUD.text = GetMinutes(timeLeft);
        // RecalculaObjetivo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Start();
        if(!finalWinnerCanvas.activeInHierarchy)
            Pausa();

        //showObjective = objective;
        timeLeft -= Time.deltaTime;
        textHUD.text = GetMinutes(timeLeft);
        //asignar ganador final
        foreach (GameObject player in players)
		{
			if (player.GetComponent<PlayerControl> ().scoreGeneral > topScore) {
				topScore = player.GetComponent<PlayerControl> ().scoreGeneral;
				finalWinner = player;
				//finalWinners.Add (player);

			} else if (player.GetComponent<PlayerControl> ().scoreGeneral == topScore) {
				if (finalWinner != null) {
					
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
			//Debug.Log("Congratulations to " + this.gameObject.name);
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreSurvived += 1;
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 10;
            Rondes.timesPlayed++;
            //PlayerPrefs.SetInt("Rondes", Rondes.timesPlayed);
            Start();
           
        }
        if (timeLeft <= 0 && !objComplete)
        {
			parcialWinner = objective;
			//Debug.Log("Congratulations to " + this.gameObject.name);
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreSurvived += 1;
			parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 10;
            Rondes.timesPlayed++;
            //PlayerPrefs.SetInt("Rondes", Rondes.timesPlayed);
            Start();
            
        }
        if (objKilledByGuard)
        {
            Rondes.timesPlayed++;
            //PlayerPrefs.SetInt("Rondes", Rondes.timesPlayed);
            Start();

        }

        if (Rondes.timesPlayed == Rondes.rondas)
        {
            finalWinnerCanvas.SetActive(true);
        }
    }
    private void MapaRandom()
    {
        int mapa = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("NumMapas"));
        if (mapa + 1 == 1) GameObject.Find("Map_2").SetActive(false);
        if (mapa + 1 == 2) GameObject.Find("Map_1").SetActive(false);

    }

    void RespawnNPCS()
    {
        foreach (GameObject guard in guards)
        {
            if (guard != null)
                guard.GetComponent<NPCConnectedPatrol>().Respawn(guard.gameObject);
        }
        foreach (GameObject killer in killers)
        {
            if(killer != null)
                killer.GetComponent<NPCConnectedPatrol>().Respawn(killer.gameObject);
        }
    }

    void RecalculaObjetivo()
    {
        random = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("NumPlayers"));
        objective = GameObject.Find("Player " + (random + 1).ToString());
        showObjective = objective;
        ShowObjectiveCanvas();

    }
    void ShowObjectiveCanvas()
    {
        objectiveCanvas.SetActive(true);
        objectiveCanvas.GetComponent<ObjectiveCanvas>().Start();
    }
    private void Pausa()
    {
        if (Input.GetButtonDown("Start"))
        {
            paused = !paused;
            pausa.SetActive(paused);
        }
        if (Input.GetButtonDown("Submit") && paused)
        {
            pausa.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene("menu");
            paused = false;
        }
        if (paused)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    private string GetMinutes(float timeLeft)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
        return string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
