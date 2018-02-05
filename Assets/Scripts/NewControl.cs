using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject showObjective;
    public static int random;
    public static List<GameObject> players = new List<GameObject>();
    public GameObject[] guards;
    public GameObject[] killers;
    // Use this for initialization
    void Start()
    {
        
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
        showObjective = objective;
        timeLeft -= Time.deltaTime;

        if (objComplete)
        {
            Debug.Log("Congratulations");
            this.gameObject.GetComponent<PlayerControl>().scoreKills += 1;
            this.gameObject.GetComponent<PlayerControl>().scoreGeneral += 50;

            //Pause (p);
            //eleccio objectiu
            random = UnityEngine.Random.Range(0, numOfPlayers);
            if (random == 0)
                objective = GameObject.FindGameObjectWithTag("Player 1");
            else if (random == 1)
                objective = GameObject.FindGameObjectWithTag("Player 2");
            objComplete = false;
            foreach (GameObject player in players)
            {
                player.GetComponent<Player>().Respawn(player.gameObject);
            }
        }
        if (timeLeft <= 0 && !objComplete)
        {
            //posar puntuacions negatives
            random = UnityEngine.Random.Range(0, numOfPlayers);
            if (random == 0)
                objective = GameObject.FindGameObjectWithTag("Player 1");
            else if (random == 1)
                objective = GameObject.FindGameObjectWithTag("Player 2");
            timeLeft = timeStartLeft;
            foreach (GameObject player in players)
            {
                player.GetComponent<Player>().Respawn(player.gameObject);
            }
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
                player.GetComponent<Player>().Respawn(player.gameObject);
            }
        }
    }

    private IEnumerator Pause()
    {
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {

            yield return 0;
        }
        Time.timeScale = 1;
    }
    private IEnumerator Pause(int p)
    {
        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(p);
        //despres dels segons p, posat en marxa de nou
        //aqui posar un bool q controli el canvas?
        Time.timeScale = 1;
    }
}
