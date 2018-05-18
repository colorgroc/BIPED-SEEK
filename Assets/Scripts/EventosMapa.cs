using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosMapa : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float minVel = 10f, maxVel = 30f, tempsNothing = 30, tempsEvent = 30;
    private List<GameObject> guardsToDisplay = new List<GameObject>();
    private int evento;
    private List<int> eventos = new List<int>();
    private int i;
    private float timeEvent1, timeEvent2;
    private bool nothing;
    private int ronda;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private AudioClip eventSound;
   // bool nada;
    private AudioSource soundSource;

    void Start () {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        for (int i = 0; i < Rondes.rondas; i++)
        {
            evento = UnityEngine.Random.Range(0, 4);
            eventos.Add(evento);
        }
        ronda = Rondes.timesPlayed;
        nothing = false;
        canvas.GetComponent<Canvas>().enabled = false;
        i = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (NewControl.startGame)
        {
            if (ronda < Rondes.timesPlayed)
            {
                ronda = Rondes.timesPlayed;

                nothing = false;
            }
            if (i < eventos.Count && ronda <= Rondes.rondas)
            {
                if (nothing)
                {
                    timeEvent2 += Time.deltaTime;
                    //if (timeEvent2 == Time.deltaTime) soundSource.PlayOneShot(eventSound);
                    if (timeEvent2 >= tempsNothing)
                    {
                        canvas.GetComponent<Canvas>().enabled = false;
                        //nada = false;
                        Default();

                    }
                }
                else
                {
                    timeEvent1 += Time.deltaTime;
                    //if (timeEvent1 == Time.deltaTime) soundSource.PlayOneShot(eventSound);
                    if (timeEvent1 >= tempsEvent)
                    {
                        Eventos(i);
                        //if (!nada) canvas.GetComponent<Canvas>().enabled = true;
                        i++;

                    }
                }
            }
        }

	}
    private void Eventos(int i)
    {
       
        switch (eventos[i])
        {
            case 0:
                //nada = true;
                Default();
                break;
            case 1: 
                //nada = false;
                NPCReduction();
                soundSource.PlayOneShot(eventSound);
                canvas.GetComponent<Canvas>().enabled = true;
                CameraShake.Shake(1f, 4f);
                break;
            case 2:      
                //nada = false;
                ChangeSpeed();
                soundSource.PlayOneShot(eventSound);
                canvas.GetComponent<Canvas>().enabled = true;
                CameraShake.Shake(1f, 4f);
                break;
            case 3:           
               // nada = false;
                KillersCreation();
                soundSource.PlayOneShot(eventSound);
                canvas.GetComponent<Canvas>().enabled = true;
                CameraShake.Shake(1f, 4f);
                break;
        }
        timeEvent1 = timeEvent2 = 0;
        nothing = true;
    }
    private void ChangeSpeed()
    {     
        PlayerPrefs.SetFloat("Speed", PlayerControl.defaultSpeed*3);
    }
    private void DefaultSpeed()
    {
        PlayerPrefs.SetFloat("Speed", PlayerControl.defaultSpeed);
    }

    private void NPCReduction()
    {
        for (int i = 0; i < NewControl.guards.Length/2; i++)
        {
            string type = NewControl.objective.name.Substring(NewControl.objective.name.Length - 1);
            
            if (NewControl.guards[i] != null)
            {
                if (NewControl.guards[i].name.Equals("Guard_Tipo " + type))
                {
                    guardsToDisplay.Add(NewControl.guards[i]);
                }
            }
        }
        for(int i = 0; i < (guardsToDisplay.Count/2); i++)
        {
            guardsToDisplay[i].SetActive(false);
        }
    }
    private void NPCRestablishment()
    {
        if (guardsToDisplay.Count > 0)
        {

            foreach (GameObject guard in guardsToDisplay)
            {
                if(guard != null)
                    guard.SetActive(true);
            }
        }
    }
    private void KillersCreation()
    {
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPointKillers");
        for (int y = 0; y < NewControl.numKillers; y++)
        {
            int rand = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            GameObject prefabG = (GameObject)Resources.Load("Prefabs/Killer");
            GameObject killer = (GameObject)Instantiate(prefabG, allMyRespawnPoints[rand].transform.position, allMyRespawnPoints[rand].transform.rotation);
            killer.transform.parent = GameObject.Find("Killer Guards").transform;
            killer.gameObject.name = "Killer";
            killer.gameObject.tag = "Killer Guards";
            killer.gameObject.layer = 9;
        }
        NewControl.killers = GameObject.FindGameObjectsWithTag("Killer Guards");
    }

    private void KillersDestruction()
    {
        if (NewControl.killers != null)
        {
            foreach (GameObject killer in NewControl.killers)
            {
                Destroy(killer);
            }
        }
    }

    private void Default()
    {
        KillersDestruction();
        NPCRestablishment();
        DefaultSpeed();
        
    }
}
