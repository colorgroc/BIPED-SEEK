using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript: MonoBehaviour{
	//[HideInInspector]
	public static bool detected1;
	//[HideInInspector]
	public static bool detected2;
	//[HideInInspector]
	public static bool detected3;
	//[HideInInspector]
	public static bool detected4;

	public static bool player_1_WannaKill;
	public static bool player_2_WannaKill;
	public static bool player_3_WannaKill;
	public static bool player_4_WannaKill;

	public static bool onFieldView_1;
	public static bool onFieldView_2;
	public static bool onFieldView_3;
	public static bool onFieldView_4;

	public static float timePast1;
	public static float timePast2;
	public static float timePast3;
	public static float timePast4;
	[SerializeField]
	public int numOfPlayers;
	public static bool objComplete;
	[SerializeField]
	public float timeLeft;
	private float timeStartLeft;
	public static GameObject objective;
	public  GameObject showObjective;
	public static int random;
	public List<GameObject> players = new List<GameObject>();
	public GameObject[] guards;
	public GameObject[] killers;
	// Use this for initialization
	void Start () {
		timePast1 = 0;
		timePast2 = 0;
		timePast3 = 0;
		timePast4 = 0;
		if (numOfPlayers == 2) {
			players.Add (GameObject.FindGameObjectWithTag ("Player 1"));
			players.Add (GameObject.FindGameObjectWithTag ("Player 2"));
		}
		guards = GameObject.FindGameObjectsWithTag("Guard");
		killers = GameObject.FindGameObjectsWithTag("Killer Guards");
			
		timeStartLeft = timeLeft;

		foreach (GameObject player in players) {
			player.GetComponent<Player> ().Respawn (player.gameObject);
		}

		/*foreach (GameObject guard in guards) {
			guard.GetComponent<NPCConnectedPatrol> ().Respawn (guard.gameObject);
		}

		foreach (GameObject killer in killers) {
			killer.GetComponent<NPCConnectedPatrol> ().Respawn (killer.gameObject);
		}*/

		random = UnityEngine.Random.Range (0, numOfPlayers);
		if (random == 0)
			objective = GameObject.FindGameObjectWithTag ("Player 1");
		else if(random == 1)
			objective = GameObject.FindGameObjectWithTag ("Player 2");
		showObjective = objective;
	}
	
	// Update is called once per frame
	void Update () {
		showObjective = objective;
		timeLeft -= Time.deltaTime;

		if (objComplete) {
			Debug.Log ("Congratulations");
			//Pause (p);
			random = UnityEngine.Random.Range (0, numOfPlayers);
			if (random == 0)
				objective = GameObject.FindGameObjectWithTag ("Player 1");
			else if(random == 1)
				objective = GameObject.FindGameObjectWithTag ("Player 2");
			objComplete = false;
			foreach (GameObject player in players) {
				player.GetComponent<Player> ().Respawn (player.gameObject);
			}
		}
		if (timeLeft <= 0 && !objComplete) {
			//posar puntuacions negatives
			random = UnityEngine.Random.Range (0, numOfPlayers);
			if (random == 0)
				objective = GameObject.FindGameObjectWithTag ("Player 1");
			else if(random == 1)
				objective = GameObject.FindGameObjectWithTag ("Player 2");
			timeLeft = timeStartLeft;
			foreach (GameObject player in players) {
				player.GetComponent<Player> ().Respawn (player.gameObject);
			}
		}
	}

	private IEnumerator Pause(){
		Time.timeScale = 0.1f;
		float pauseEndTime = Time.realtimeSinceStartup + 1;
		while (Time.realtimeSinceStartup < pauseEndTime) {
			
			yield return 0;
		}
		Time.timeScale = 1;
	}
	private IEnumerator Pause(int p){
		Time.timeScale = 0.1f;

		yield return new WaitForSeconds(p);
		//despres dels segons p, posat en marxa de nou
		//aqui posar un bool q controli el canvas?
		Time.timeScale = 1;
	}
}
