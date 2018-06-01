using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using FMODUnity;

public class NewControl : MonoBehaviour
{
    public static int numOfPlayers;
    public static bool objComplete;
    public static bool objKilledByGuard;
    public static float timeLeft;
    public static bool startGame;
    public static GameObject objective;
    public static GameObject parcialWinner;
    public static GameObject finalWinner;

    //[SerializeField]
    //private AudioClip pauseSound, backSound, menuSound, startNumbers;
    //private AudioSource soundSource;

    public static List<GameObject> players;// = new List<GameObject>();
    private List<GameObject> listPlayers;// = new List<GameObject>();
    [HideInInspector]
    public static GameObject[] guards;
    [HideInInspector]
    public static GameObject[] killers;
    public static bool paused;
    [SerializeField]
    private GameObject pausa, objectiveCanvas;
    public GameObject rankingCanvas;
    public GameObject finalWinnerCanvas;
    public static int characterPlayer_1, characterPlayer_2, characterPlayer_3, characterPlayer_4;
    [SerializeField]
    private Sprite SpriteTipo_1_Blue, SpriteTipo_2_Blue, SpriteTipo_3_Blue, SpriteTipo_4_Blue, SpriteTipo_1_Red, SpriteTipo_2_Red, SpriteTipo_3_Red, SpriteTipo_4_Red, SpriteTipo_1_Green, SpriteTipo_2_Green, SpriteTipo_3_Green, SpriteTipo_4_Green, SpriteTipo_1_Yellow, SpriteTipo_2_Yellow, SpriteTipo_3_Yellow, SpriteTipo_4_Yellow;
    [SerializeField]
    private Text textTiempo, countDown;
    private int topScore, habilitat_1, habilitat_2;//, fin;
    [SerializeField]
    private int numGuardsPerType = 10, time = 120;//maxMinutes = 3, minMinutes = 1;
    public static int numRondesPerJugador = 2;
    public static int numKillers = 7;
    private List<int> listPos;
    private List<int> listPosGuards;
    // [SerializeField]
    //private static List<GameObject> scorePlayers;
    private float timeBack = 4;//, timeStartLeft;
    private Vector4 gold_Color = new Vector4(255, 215, 0, 255);
    Resolution res;
    public FMOD.Studio.EventInstance backgroudSound;
    public FMOD.Studio.EventInstance backgroudMusic;
    //private FMOD.Studio.ParameterInstance loop;
    private bool once = false;

    public enum Abilities
    {
        INVISIBLITY, IMMOBILIZER, SPRINT, TELEPORT, CONTROL, SMOKE//, REPEL
    };

    // Use this for initialization
    public void Awake()
    {
        startGame = once = false;
        numOfPlayers = PlayerPrefs.GetInt("NumPlayers");

        listPos = new List<int>();
        listPlayers = new List<GameObject>();
        players = new List<GameObject>();
        Rondes.timesPlayed = 0;
        habilitat_1 = PlayerPrefs.GetInt("Ability 1");
        habilitat_2 = PlayerPrefs.GetInt("Ability 2");
        rankingCanvas.SetActive(true);

        //creacion jugadores
        PlayersAndGuardsCreation();
        //lista adicional para establecer las rondas de cada jugador
        int passades = 0;
        while (passades < numRondesPerJugador)
        {
            for (int i = 0; i < players.Count; i++)
            {
                listPlayers.Add(players[i]);
            }
            passades++;
        }

        countDown.gameObject.SetActive(true);
        
        if (SceneManager.GetActiveScene().name == "Mapa_1")
        {
            backgroudMusic = RuntimeManager.CreateInstance("event:/BipedSeek/Music/Mapa 1");
            backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Ambient/Wind");
            backgroudSound.setParameterValue("Vent Loop", 0.2f);
        }
        else if (SceneManager.GetActiveScene().name == "Mapa_2")
        {
            backgroudMusic = RuntimeManager.CreateInstance("event:/BipedSeek/Music/Mapa 2");
            backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Ambient/Birds");
        }
        else if (SceneManager.GetActiveScene().name == "Mapa_3")
        {
            backgroudMusic = RuntimeManager.CreateInstance("event:/BipedSeek/Music/Mapa 3");
        }

    }
    void Start()
    {
        //soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        Time.timeScale = 0;
        //control HZ monitors
        res = Screen.currentResolution;
        if (res.refreshRate == 60)
            QualitySettings.vSyncCount = 1;
        if (res.refreshRate == 120)
            QualitySettings.vSyncCount = 2;
        print(QualitySettings.vSyncCount);  

        StartGame();
        //soundSource.PlayOneShot(startNumbers);
        RuntimeManager.PlayOneShot("event:/BipedSeek/Stuff/CountDown", Vector3.zero);

    }
    public void StartGame()
    {

        VariablesOnDefault();
        RecalculaObjetivo();
        RespawnNPCS();
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerControl>().Respawn(player.gameObject);
            player.GetComponent<FieldOfView>().Start();
        }
        //eleccio objectiu

        timeLeft = time;//UnityEngine.Random.Range(minMinutes*60, maxMinutes * 60);
        textTiempo.text = GetMinutes(timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startGame && !Tutorial_InGame.showIt && !Abilities_Tutorial.show)
        {
            
            timeBack -= Time.fixedUnscaledDeltaTime;

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
                backgroudSound.start();
                backgroudMusic.start();
            }
        }
        else
        {

            if (!finalWinnerCanvas.activeInHierarchy)
                Pausa();

            timeLeft -= Time.deltaTime;
            textTiempo.text = GetMinutes(timeLeft);

            //asignar puntos ganador parcial
            if (objComplete && parcialWinner != null)
            {
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreWins += 1;
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreKills += 1;
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 40;
                //parcialWinner.gameObject.GetComponent<PlayerControl>().scoreWinsRound += 1;
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneralRound += 40;
                //StartGame();
                objComplete = false;
                this.GetComponent<EventosMapa>().Default();
                // Rondes.timesPlayed++;
                //Ranking.OrdenarRanking();
                rankingCanvas.SetActive(true);

            }
            if (timeLeft <= 0 && !objComplete)
            {
                parcialWinner = objective;
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreWins += 1;
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneral += 50;
                //parcialWinner.gameObject.GetComponent<PlayerControl>().scoreWinsRound += 1;
                parcialWinner.gameObject.GetComponent<PlayerControl>().scoreGeneralRound += 50;
                Rondes.timesPlayed++;
                objComplete = false;
                timeLeft = time;
                RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Objective_Death", parcialWinner.transform.position);
                //Ranking.OrdenarRanking();
                //if (GameObject.Find("MapEvent") != null)
                this.GetComponent<EventosMapa>().Default();
                rankingCanvas.SetActive(true);
                //StartGame();

            }
            if (objKilledByGuard)
            {
                Rondes.timesPlayed++;
                objKilledByGuard = false;
                objComplete = false;
                RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Objective_Death", Vector3.zero);
                //Ranking.OrdenarRanking();
                //if (GameObject.Find("MapEvent") != null)
                this.GetComponent<EventosMapa>().Default();
                rankingCanvas.SetActive(true);
                //StartGame();

            }
            if(Rondes.timesPlayed == Rondes.rondas - 1)
            {
                if (timeLeft < 10)
                    backgroudSound.setParameterValue("Vent Loop", 1);
            }
            if (Rondes.timesPlayed >= Rondes.rondas)
            {
               // Debug.Log(Rondes.rondas);
               // Debug.Log("iee");
                backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                this.GetComponent<EventosMapa>().Default();
                NewControl.finalWinner = players[0].gameObject;
                objComplete = false;
                Ranking.Guanyador();
                if (!once)
                {
                    rankingCanvas.SetActive(true);
                    once = true;
                }
            }
        }
    }
    private void VariablesOnDefault()
    {
        objKilledByGuard = false;
        //parcialWinner = null;
        objComplete = false;
        finalWinnerCanvas.SetActive(false);
        rankingCanvas.SetActive(false);
        paused = false;
        pausa.SetActive(false);

        //timeStartLeft = timeLeft;
        objective = null;
        foreach(GameObject player in players)
        {
            if (PlayerPrefs.GetInt("Ability 1") == (int)Abilities.SMOKE || PlayerPrefs.GetInt("Ability 2") == (int)Abilities.SMOKE)
                player.GetComponent<Smoke>().Restart();
            if (PlayerPrefs.GetInt("Ability 1") == (int)Abilities.IMMOBILIZER || PlayerPrefs.GetInt("Ability 2") == (int)Abilities.IMMOBILIZER)
                player.GetComponent<Immobilizer>().Restart();
            if (PlayerPrefs.GetInt("Ability 1") == (int)Abilities.INVISIBLITY || PlayerPrefs.GetInt("Ability 2") == (int)Abilities.INVISIBLITY)
                player.GetComponent<Invisibility>().Restart();
            if (PlayerPrefs.GetInt("Ability 1") == (int)Abilities.TELEPORT || PlayerPrefs.GetInt("Ability 2") == (int)Abilities.TELEPORT)
                player.GetComponent<Teleport>().Restart();
            if (PlayerPrefs.GetInt("Ability 1") == (int)Abilities.SPRINT || PlayerPrefs.GetInt("Ability 2") == (int)Abilities.SPRINT)
                player.GetComponent<Sprint>().Restart();
            if (PlayerPrefs.GetInt("Ability 1") == (int)Abilities.CONTROL || PlayerPrefs.GetInt("Ability 2") == (int)Abilities.CONTROL)
                player.GetComponent<ControlAbility>().Restart();
        }
        GameObject[] deads = GameObject.FindGameObjectsWithTag("Death");
        foreach (GameObject death in deads)
        {
            Destroy(death);
        }
        deads = null;
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


            if (PlayerPrefs.GetInt("characterPlayer_" + i.ToString()) == 1)
            {
                if (i == 1) {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_1_Blue;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_1_Blue;
                }
                else if (i == 2) {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_1_Red;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_1_Red;
                }
                else if (i == 3)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_1_Green;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_1_Green;
                }
                else if (i == 4)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_1_Yellow;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_1_Yellow;
                }
                    
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Bear");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + i.ToString()) == 2)
            {
                if (i == 1)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_2_Blue;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_2_Blue;
                }
                else if (i == 2)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_2_Red;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_2_Red;
                }
                else if (i == 3)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_2_Green;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_2_Green;
                }
                else if (i == 4)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_2_Yellow;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_2_Yellow;
                }
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Bunny");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + i.ToString()) == 3)
            {
                if (i == 1)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_3_Blue;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_3_Blue;
                }
                else if (i == 2)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_3_Red;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_3_Red;
                }
                else if (i == 3)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_3_Green;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_3_Green;
                }
                else if (i == 4)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_3_Yellow;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_3_Yellow;
                }
                Material mat = (Material)Resources.Load("Materials/Player " + i.ToString() + "/Penguin");
                player.gameObject.GetComponentInChildren<Renderer>().material = mat;
            }
            else if (PlayerPrefs.GetInt("characterPlayer_" + i.ToString()) == 4)
            {
                if (i == 1)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_4_Blue;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_4_Blue;
                }
                else if (i == 2)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_4_Red;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_4_Red;
                }
                else if (i == 3)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_4_Green;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_4_Green;
                }
                else if (i == 4)
                {
                    GameObject.Find("IconPlayer_" + i.ToString()).GetComponent<Image>().sprite = SpriteTipo_4_Yellow;
                    GameObject.Find("IconPlayer_" + i.ToString() + "_Grey").GetComponent<Image>().sprite = SpriteTipo_4_Yellow;
                }
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
                //Destroy(guard.gameObject.GetComponentInChildren<CapsuleCollider>());
                Destroy(guard.gameObject.GetComponentInChildren<Kill>());
                AddScript(guard);

            }
        }

        //añadir jugadores activos a una lista de control
        for (int i = 1; i <= numOfPlayers; i++)
        {
            if (GameObject.Find("Player " + i.ToString()) != null)
                players.Add(GameObject.Find("Player " + i.ToString()));
        }

        guards = GameObject.FindGameObjectsWithTag("Guard");
    }
    public void AddScript(GameObject tipo)
    {
        if (tipo.CompareTag("Guard"))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().playerControl.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().fieldOfView.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = true;

            tipo.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().sprint.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().smoke.enabled = false;
            // tipo.gameObject.GetComponent<Repel>().enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().teleport.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().control.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().guardController.enabled = false;

        }
        else
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = false;
            tipo.gameObject.GetComponent<AbilitiesControl>().playerControl.enabled = true;
            tipo.gameObject.GetComponent<AbilitiesControl>().fieldOfView.enabled = true;

            AbilitiesAsignation(habilitat_1, tipo);
            AbilitiesAsignation(habilitat_2, tipo);
          
        }
    }
    private void AbilitiesAsignation(int habilitat, GameObject tipo)
    {
        //asignacio habiltats
        if ((habilitat == (int)Abilities.CONTROL))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().control.enabled = true;
            tipo.gameObject.GetComponent<AbilitiesControl>().guardController.enabled = false;
        }
        else if ((habilitat == (int)Abilities.IMMOBILIZER))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = true;
        }
        else if ((habilitat == (int)Abilities.INVISIBLITY))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = true;
        }
        else if ((habilitat == (int)Abilities.SMOKE))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().smoke.enabled = true;
        }
        else if ((habilitat == (int)Abilities.SPRINT))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().sprint.enabled = true;
        }
        else if ((habilitat == (int)Abilities.TELEPORT))
        {
            tipo.gameObject.GetComponent<AbilitiesControl>().teleport.enabled = true;
        }
    }

    void RespawnNPCS()
    {
        foreach (GameObject guard in guards)
        {
            if (guard != null)
                guard.GetComponent<NPCConnectedPatrol>().Respawn(guard.gameObject);
        }
        //if (killers != null)
        //{
        //    foreach (GameObject killer in killers)
        //    {
        //        if (killer != null)
        //            killer.GetComponent<NPCConnectedPatrol>().Respawn(killer.gameObject);
        //    }
        //}
    }

    void RecalculaObjetivo()
    {
        if (listPlayers.Count > 0)
        {
            int random = UnityEngine.Random.Range(0, listPlayers.Count);
            objective = GameObject.Find(listPlayers[random].name);
            Debug.Log(listPlayers[random]);
            listPlayers.RemoveAt(random);
            ShowObjectiveCanvas();
        }

    }
    void ShowObjectiveCanvas()
    {
        //objectiveCanvas.SetActive(true);
        this.GetComponent<ObjectiveCanvas>().Start();
    }
    private void Pausa()
    {
        if (Input.GetButtonDown("Start") || (paused && Input.GetButtonDown("Cancel")) && !Tutorial_InGame.showIt && !Abilities_Tutorial.show)
        {
			if (!paused) {
				//soundSource.PlayOneShot (pauseSound);    
				RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
			} else {
				//soundSource.PlayOneShot (backSound);
				RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
			}
            paused = !paused;
            foreach(GameObject player in players)
            {
                if(player.GetComponent<PlayerControl>().detected)
                    player.GetComponent<PlayerControl>().backgroudSound.setPaused(paused);
            }
            backgroudSound.setPaused(paused);
            backgroudMusic.setPaused(paused);
            pausa.SetActive(paused);
        }
        if (Input.GetButtonDown("Main Menu") && paused && !Tutorial_InGame.showIt && !Abilities_Tutorial.show)
        {
            //soundSource.PlayOneShot(menuSound);
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            foreach (GameObject player in players)
            {
                if (player.GetComponent<PlayerControl>().detected)
                    player.GetComponent<PlayerControl>().backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); 
            }
            pausa.SetActive(false);
            Time.timeScale = 1;
            VariablesOnDefault();
            // Default();
            paused = false;
            SceneManager.LoadScene("Menu");
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

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            countDown.text = count.ToString();
            // display something...
            yield return new WaitForSeconds(1);
            count -= (int)Time.fixedDeltaTime;
        }

        // count down is finished...
        countDown.gameObject.SetActive(false);
        startGame = true;

        foreach (GameObject player in players)
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
