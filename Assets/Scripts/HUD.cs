using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public Text playerID;
	public Text score;
	public Text kills;
	public Text survived;
    [SerializeField]
	private GameObject player;
	//Canvas player1, player2;
	// Use this for initialization
	void Start () {
        //player = GetComponent<GameObject>();
        if(this.gameObject.name == "Player1HUD")
            this.player = GameObject.Find("Player_1");
        else if (this.gameObject.name == "Player2HUD")
            this.player = GameObject.Find("Player_2");
        else if (this.gameObject.name == "Player3HUD")
            this.player = GameObject.Find("Player_3");
        else if (this.gameObject.name == "Player4HUD")
            this.player = GameObject.Find("Player_4");

        score.text = this.player.GetComponent<PlayerControl> ().scoreGeneral.ToString();
		kills.text = this.player.GetComponent<PlayerControl> ().scoreKills.ToString();
		survived.text = this.player.GetComponent<PlayerControl> ().scoreSurvived.ToString();
		playerID.text = this.player.GetComponent<PlayerControl> ().playerID.ToString();
	}

	// Update is called once per frame
	void Update () {
		score.text = this.player.GetComponent<PlayerControl> ().scoreGeneral.ToString();
		kills.text = this.player.GetComponent<PlayerControl> ().scoreKills.ToString();
		survived.text = this.player.GetComponent<PlayerControl> ().scoreSurvived.ToString();
	}
}
