using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public Text playerID;
	public Text score;
	public Text kills;
	public Text survived;
	public GameObject player;
	//Canvas player1, player2;
	// Use this for initialization
	void Start () {
		//player = GetComponent<GameObject>();
		score.text = player.GetComponent<PlayerControl> ().scoreGeneral.ToString();
		kills.text = player.GetComponent<PlayerControl> ().scoreKills.ToString();
		survived.text = player.GetComponent<PlayerControl> ().scoreSurvived.ToString();
		playerID.text = player.GetComponent<PlayerControl> ().playerID.ToString();
	}

	// Update is called once per frame
	void Update () {
		score.text = player.GetComponent<PlayerControl> ().scoreGeneral.ToString();
		kills.text = player.GetComponent<PlayerControl> ().scoreKills.ToString();
		survived.text = player.GetComponent<PlayerControl> ().scoreSurvived.ToString();

	}
}
