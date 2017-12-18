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
	public static int random;
	// Use this for initialization
	void Start () {
		timePast1 = 0;
		timePast2 = 0;
		timePast3 = 0;
		timePast4 = 0;

		timeStartLeft = timeLeft;

		random = UnityEngine.Random.Range (0, numOfPlayers);
		if (random == 0)
			objective = GameObject.FindGameObjectWithTag ("Player 1");
		else if(random == 1)
			objective = GameObject.FindGameObjectWithTag ("Player 2");

	}
	
	// Update is called once per frame
	void Update () {

		timeLeft -= Time.deltaTime;

		if (objComplete) {
			random = UnityEngine.Random.Range (0, numOfPlayers);
			if (random == 0)
				objective = GameObject.FindGameObjectWithTag ("Player 1");
			else if(random == 1)
				objective = GameObject.FindGameObjectWithTag ("Player 2");
			objComplete = false;
		}
		if (timeLeft <= 0 && !objComplete) {
			//posar puntuacions negatives
			random = UnityEngine.Random.Range (0, numOfPlayers);
			if (random == 0)
				objective = GameObject.FindGameObjectWithTag ("Player 1");
			else if(random == 1)
				objective = GameObject.FindGameObjectWithTag ("Player 2");
			timeLeft = timeStartLeft;
		}
	}
}
