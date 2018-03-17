using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosMapa : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float minVel = 10f, maxVel = 30f;
    private List<GameObject> guardsToDisplay = new List<GameObject>();
    private int evento;
    private List<int> eventos = new List<int>();
    private int i = 0;
    private float timeEvent1, timeEvent2;
    private bool nothing;

	void Start () {
        for (int i = 0; i < 3; i++)
        {
            evento = UnityEngine.Random.Range(0, 4);
            eventos.Add(evento);    
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        if (nothing)
        {
            timeEvent2 += Time.deltaTime;
            if(timeEvent2 >= (NewControl.timeLeft / 5))
            {
                Default();
                nothing = false;
            }
        }
        else
        {
            timeEvent1 += Time.deltaTime;
            // if(timeEvent1 >= (NewControl.timeLeft / 4))
            if (timeEvent1 >= (NewControl.timeLeft / 4))
            {
                Eventos(i);
            }
        }
	}
    private void Eventos(int i)
    {
       
        switch (eventos[i])
        {
            case 0:
                Default();
                break;
            case 1:
                //Default();
                NPCReduction();
                break;
            case 2:
               // Default();
                ChangeSpeed();
                break;
            case 3:
                //Default();
                KillersCreation();
                break;
        }
        timeEvent1 = timeEvent2 = 0;
        i++;
        nothing = true;
    }
    private void ChangeSpeed()
    {
        // float speedRandom = UnityEngine.Random.Range(minVel, maxVel);
        
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
            string type = NewControl.objective.name.Substring(NewControl.objective.name.Length - 4);
            if (NewControl.guards[i].name.Equals("Guard_Tipo_" + type))
            {
                guardsToDisplay.Add(NewControl.guards[i]);
            }
        }
        for(int i = 0; i < guardsToDisplay.Count; i++)
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
                if (!guard.activeInHierarchy)
                {
                    guard.SetActive(true);
                }
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
