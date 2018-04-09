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
    public static bool startGame;
    private float timeStartLeft;
    public static GameObject objective;
	public static GameObject parcialWinner;
	public static GameObject finalWinner;

    
    //public List<GameObject> finalWinners = new List<GameObject>();
    [SerializeField]
    private GameObject showObjective;
    //public static int random;

    public static List<GameObject> players;// = new List<GameObject>();
    private List<GameObject> listPlayers;// = new List<GameObject>();
    public static GameObject[] guards;
    public static GameObject[] killers;
	private bool paused;
	[SerializeField]
	private GameObject pausa, objectiveCanvas, finalWinnerCanvas;
	private int topScore;
    [SerializeField]
    public static int characterPlayer_1, characterPlayer_2, characterPlayer_3, characterPlayer_4;
    [SerializeField]
    private Sprite SpriteTipo_1, SpriteTipo_2, SpriteTipo_3, SpriteTipo_4;
    [SerializeField]
    private Text textTiempo, countDown;
    private int fin;
    [SerializeField]
    private int numGuardsPerType = 10, numRondesPerJugador = 2, time = 90;//maxMinutes = 3, minMinutes = 1;
    [SerializeField]
    public static int numKillers = 7;
    private List<int> listPos;
    private List<int> listPosGuards;
    [SerializeField]
    private List<GameObject> scorePlayers;
    private float timeBack = 4;
    private Vector4 gold_Color = new Vector4(255, 215, 0, 255);
    // [Serializable]

    // Use this for initialization
    public void Awake()
    {
        startGame = false;
        numOfPlayers = PlayerPrefs.GetInt("NumPlayers");
        fin = UnityEngine.Random.Range(0, 2);
        listPos = new List<int>();
        listPlayers = new List<GameObject>();
        players = new List<GameObject>();
        scorePlayers = new List<GameObject>();
        //creacion jugadores
        PlayersAndGuardsCreation();
        //lista adicional para establecer las rondas de cada jugador
        int passades = 0;
        while (passades < numRondesPerJugador)
        {
            for (int i = 0; i < players.Count; i++)
            {
                listPlayers.Add(players[i]);
                Debug.Log(players[i]);
            }
            passades++;
        }

        countDown.gameObject.SetActive(true);
        //foreach (GameObject player in players)
        //{
        //    player.SetActive(false);
        //    //player.GetComponent<PlayerControl>().enabled = false;
        //}
        //foreach (GameObject guard in guards)
        //{
        //    guard.SetActive(false);
        //    //guard.GetComponent<NPCConnectedPatrol>().enabled = false;
        //    //guard.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //}
    }
    void Start()
    {
        //StartCoroutine(Countdown(3));
        Time.timeScale = 0;
        StartGame();
    }
    private void StartGame()
    {

        VariablesOnDefault();
        RecalculaObjetivo();
        RespawnNPCS();
        foreach (GameObject player in players)
        {
            //player.GetComponent<PlayerControl>().timePast = 0;
            player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            player.GetComponent<FieldOfView>().Start();
        }
        //eleccio objectiu
        
        timeLeft = time;//UnityEngine.Random.Range(minMinutes*60, maxMinutes * 60);
        textTiempo.text = GetMinutes(timeLeft);
        
    }
    //private void FixedUpdate()
    //{
    //    if (!startGame)
    //    {
    //        timeBack -= Time.fixedUnscaledDeltaTime;
    //        Debug.Log(timeBack);
    //        contrarreloj.text = ((int)timeBack).ToString();
    //        if (timeBack <= 0)
    //        {
    //            contrarreloj.gameObject.SetActive(false);
    //            Time.timeScale = 1;
    //            startGame = true;
    //        }
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        if (!startGame)
        {
            timeBack -= Time.fixedUnscaledDeltaTime;
           // Debug.Log(timeBack);
            if ((int)timeBack == 0)
            {
                countDown.color = gold_Color;
                countDown.text = "GO!";
            }
            else
                countDown.text = ((int)timeBack).ToString();
            if (timeBack <= 0)
            {
                countDown.gameObject.SetActive(false);
                Time.timeScale = 1;
                startGame = true;
            }
        }
        else
        {
            //if (startGame) { 
            if(!finalWinnerCanvas.activeInHierarchy)
                Pausa();

            timeLeft -= Time.deltaTime;
            textTiempo.text = GetMinutes(timeLeft);

		    //asignar puntos ganador parcial
		    if (objComplete && parcialWinner != null) 
            {
			    parcialWinner.gameObject.GetComponent<PlayerControl>().scoreWins += 1;
			    parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 10;
                StartGame();
           
            }
            if (timeLeft <= 0 && !objComplete)
            {
			    parcialWinner = objective;
			    parcialWinner.gameObject.GetComponent<PlayerControl>().scoreWins += 1;
			    parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 10;
                Rondes.timesPlayed++;
                StartGame();
            
            }
            if (objKilledByGuard)
            {
                Rondes.timesPlayed++;
                StartGame();

            }

            if (Rondes.timesPlayed == Rondes.rondas)
            {
                Winner();
                finalWinnerCanvas.SetActive(true);
            }
        }
    }
    private void VariablesOnDefault()
    {
        objKilledByGuard = false;
        //parcialWinner = null;
        objComplete = false;
        finalWinnerCanvas.SetActive(false);
        paused = false;
        pausa.SetActive(false);
        timeStartLeft = timeLeft;
        showObjective = null;
        objective = null;
    }
    private void PlayersAndGuardsCreation()
    {
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        for (int i = 1; i <= numOfPlayers; i++)
        {
            listPosGuards = new List<int>();
            int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            if (listPos.Contains(random))
            {
                do { random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length); } while (listPos.Contains(random));
            }
            listPos.Add(random);
            //es crea player desde la seleccio escollida (es crida prefab)
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()).ToString());
            Vector3 pos = new Vector3(allMyRespawnPoints[random].transform.position.x, 10.14516f, allMyRespawnPoints[random].transform.position.z);
            GameObject player = (GameObject)Instantiate(prefab, pos, allMyRespawnPoints[random].transform.rotation);
            player.transform.parent = GameObject.Find("Players").transform;
            player.gameObject.name = "Player " + i.ToString();
            player.gameObject.tag = "Player " + i.ToString();
            player.gameObject.layer = 8;
            AddScript(player);
            //player.gameObject.GetComponent<NPCConnectedPatrol>().enabled = false;
            

            if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 1)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_1;
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Bear");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 2)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_2;
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Bunny");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 3)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_3;
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Penguin");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + (i).ToString()) == 4)
            {
                GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_4;
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Fox");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }

            
            //creacion de guards x jugador 
            for (int y = 0; y < numGuardsPerType; y++)
            {
                    
                int rand = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
                if (listPosGuards.Contains(rand))
                {
                    do { random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length); } while (listPosGuards.Contains(random));
                }
                listPosGuards.Add(rand);
                //GameObject prefabG = (GameObject)Resources.Load("Prefabs/Guard_Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + i.ToString()).ToString());
                GameObject prefabG = (GameObject)Resources.Load("Prefabs/Tipo_" + PlayerPrefs.GetInt("characterPlayer_" + i.ToString()).ToString());
                Vector3 posG = new Vector3(allMyRespawnPoints[rand].transform.position.x, 10.14516f, allMyRespawnPoints[rand].transform.position.z);
                GameObject guard = (GameObject)Instantiate(prefabG, posG, allMyRespawnPoints[rand].transform.rotation);
                guard.transform.parent = GameObject.Find("Guards").transform;
                guard.gameObject.name = "Guard_Tipo " + i.ToString();
                guard.gameObject.tag = "Guard";
                guard.gameObject.layer = 9;
                guard.gameObject.GetComponentInChildren<Renderer>().material = player.gameObject.GetComponentInChildren<Renderer>().material;
                AddScript(guard);
                //guard.gameObject.AddComponent<NPCConnectedPatrol>();
                

                //guard.gameObject.GetComponent<PlayerControl>().enabled = false;
            }
        }

        //añadir jugadores activos a una lista de control
        for (int i = 1; i <= numOfPlayers; i++)
        {
            if(GameObject.Find("Player " + i.ToString()) != null)
                players.Add(GameObject.Find("Player " + i.ToString()));
        }

        guards = GameObject.FindGameObjectsWithTag("Guard");
    }
    public static void AddScript(GameObject tipo)
    {
        if (tipo.CompareTag("Guard"))
        {
            tipo.gameObject.GetComponent<PlayerControl>().enabled = false;
            tipo.gameObject.GetComponent<FieldOfView>().enabled = false;
            tipo.gameObject.GetComponent<NPCConnectedPatrol>().enabled = true;
        }
        else
        {
            tipo.gameObject.GetComponent<NPCConnectedPatrol>().enabled = false;
            tipo.gameObject.GetComponent<PlayerControl>().enabled = true;
            tipo.gameObject.GetComponent<FieldOfView>().enabled = true;
        }
    }

    /*public static void ConvertType(GameObject gO, string type)
    {
        if (type.Equals("Guard"))
        {
            gO.gameObject.GetComponent<PlayerControl>().enabled = false;
            gO.gameObject.GetComponent<FieldOfView>().enabled = false;
            gO.gameObject.GetComponent<NPCConnectedPatrol>().enabled = true;
        }
        else
        {
            gO.gameObject.GetComponent<NPCConnectedPatrol>().enabled = false;
            gO.gameObject.GetComponent<PlayerControl>().enabled = true;
            gO.gameObject.GetComponent<FieldOfView>().enabled = true;
        }
    }*/

    private void Winner()
    {
        players.Sort(SortByScore);
        scorePlayers = players;
        if(players[players.Count-1] != null)
            finalWinner = players[players.Count-1];
    }
    private static int SortByScore(GameObject o1, GameObject o2)
    {
        if (o1.GetComponent<PlayerControl>().scoreGeneral.CompareTo(o2.GetComponent<PlayerControl>().scoreGeneral) == 0) {
            if (o1.GetComponent<PlayerControl>().scoreWins.CompareTo(o2.GetComponent<PlayerControl>().scoreWins) == 0)
            {
                if(o1.GetComponent<PlayerControl>().scoreKills.CompareTo(o2.GetComponent<PlayerControl>().scoreKills)== 0)
                {
                    return o1.GetComponent<PlayerControl>().scoreGeneral;
                }

                 else return o1.GetComponent<PlayerControl>().scoreKills.CompareTo(o2.GetComponent<PlayerControl>().scoreKills);
                //return o1.GetComponent<PlayerControl>().scoreKills.CompareTo(o2.GetComponent<PlayerControl>().scoreKills);
            }
            else return o1.GetComponent<PlayerControl>().scoreWins.CompareTo(o2.GetComponent<PlayerControl>().scoreWins);
        }
        else return o1.GetComponent<PlayerControl>().scoreGeneral.CompareTo(o2.GetComponent<PlayerControl>().scoreGeneral);


    }

 

    void RespawnNPCS()
    {
        foreach (GameObject guard in guards)
        {
            if (guard != null)
                guard.GetComponent<NPCConnectedPatrol>().Respawn(guard.gameObject);
        }
        if (killers != null)
        {
            foreach (GameObject killer in killers)
            {
                if (killer != null)
                    killer.GetComponent<NPCConnectedPatrol>().Respawn(killer.gameObject);
            }
        }
    }

    void RecalculaObjetivo()
    {
        if (listPlayers.Count > 0)
        {
            int random = UnityEngine.Random.Range(0, listPlayers.Count);
            objective = GameObject.Find(listPlayers[random].name);
            listPlayers.RemoveAt(random);
            showObjective = objective;
            ShowObjectiveCanvas();
        }

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
           // Default();
            paused = false;
            SceneManager.LoadScene("Menu");
        }
        if (paused)
            Time.timeScale = 0;
        else Time.timeScale = 1;
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
    private string GetMinutes(float timeLeft)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
        return string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
   
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        
        while (count > 0)
        {
            countDown.text = count.ToString();
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        countDown.gameObject.SetActive(false);
        startGame = true;
        
        foreach(GameObject player in players)
        {
            player.SetActive(true);
            //player.GetComponent<PlayerControl>().enabled = true;
        }
        foreach (GameObject guard in guards)
        {
            guard.SetActive(true);
            //guard.GetComponent<NPCConnectedPatrol>().enabled = true;
            //guard.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        
        StartGame();
    }
}
