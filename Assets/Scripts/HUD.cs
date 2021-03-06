﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text score;
	public Text kills;
	public Text survived;
    public GameObject player1, player2, player3;
    private GameObject player;
    [SerializeField]
    private Color normal, grey;
    private void Awake()
    {
        this.score.text = this.kills.text = this.survived.text = "0";
    }
    void Start () {
        if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show)
        {
            if (this.gameObject.name == "Player1HUD")
                this.player = GameObject.Find("Player 1");
            else if (this.gameObject.name == "Player2HUD")
                this.player = GameObject.Find("Player 2");
            else if (this.gameObject.name == "Player3HUD")
                this.player = GameObject.Find("Player 3");
            else if (this.gameObject.name == "Player4HUD")
                this.player = GameObject.Find("Player 4");
        } else
        {
            if (!Abilities_Tutorial.show)
            {
                if (this.gameObject.name == "Player1HUD")
                    this.player = player1;
                else if (this.gameObject.name == "Player2HUD")
                    this.player = player2;
                else if (this.gameObject.name == "Player3HUD")
                    this.player = player3;
            }
        }

	}

	// Update is called once per frame
	void Update ()
    {
       
        this.score.text = this.player.GetComponent<PlayerControl>().scoreGeneralRound.ToString();
        this.kills.text = this.player.GetComponent<PlayerControl>().scoreKills.ToString();
        this.survived.text = this.player.GetComponent<PlayerControl>().scoreWins.ToString();

        if (this.player.GetComponent<PlayerControl>().cooledDown)
        {
            this.gameObject.GetComponent<Image>().color = grey;
        } else this.gameObject.GetComponent<Image>().color = normal;
    }
}
