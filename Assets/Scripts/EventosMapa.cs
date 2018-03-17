using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosMapa : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float minVel = 10f, maxVel = 30f;
    private List<GameObject> guardsToDisplay = new List<GameObject>();

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void ChangeSpeed()
    {
        float speedRandom = UnityEngine.Random.Range(minVel, maxVel);
        PlayerPrefs.SetFloat("Speed", speedRandom);
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
        foreach(GameObject guard in guardsToDisplay)
        {
            if (!guard.activeInHierarchy)
            {
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
            killer.transform.parent = GameObject.Find("KillerGuards").transform;
            killer.gameObject.name = "Killer";
            killer.gameObject.tag = "Killer Guards";
        }
        NewControl.killers = GameObject.FindGameObjectsWithTag("Killer Guards");
    }

    private void KillersDestruction()
    {
        foreach(GameObject killer in NewControl.killers)
        {
            Destroy(killer);
        }
    }
}
