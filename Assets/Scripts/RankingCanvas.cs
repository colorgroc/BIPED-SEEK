﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using FMODUnity;

public class RankingCanvas : MonoBehaviour {

    [SerializeField]
    private Text round;
    // Use this for initialization
    void Start () {
        
        Time.timeScale = 0;
        if(NewControl.startGame)
            RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Objective_Death", Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {
        if (Rondes.timesPlayed >= Rondes.rondas)
        {
            round.text = "End Game";
            if (NewControl.players != null && Ranking.orderedRank.Count > 0 && Ranking.orderedRank[0].gameObject != null)
            {
                NewControl.finalWinner = Ranking.orderedRank[0].gameObject;
                //Debug.Log("Guanyador: " + Ranking.orderedRank[0].gameObject.name);
            }
        }
        else round.text = "Next Round";
        if(NewControl.players != null)
            Ranking.orderedRank = NewControl.players.OrderByDescending(p => p.GetComponent<PlayerControl>().scoreGeneral).ToList();
        if (Ranking.orderedRank.Count > 0)
        {
            for (int i = 0; i < Ranking.orderedRank.Count; i++)
            {
                Ranking.rankList[i].GetComponent<Text>().text = Ranking.orderedRank[i].name;
                Ranking.rankList[i].transform.Find("Total Score").GetComponent<Text>().text = Ranking.orderedRank[i].GetComponent<PlayerControl>().scoreGeneral.ToString();

                if (Ranking.orderedRank[i].GetComponent<PlayerControl>().scoreGeneralRound > 0)
                {
                    Ranking.rankList[i].transform.Find("Round Score").GetComponent<Text>().text = "+" + Ranking.orderedRank[i].GetComponent<PlayerControl>().scoreGeneralRound.ToString();
                    Ranking.rankList[i].transform.Find("Round Score").GetComponent<Text>().color = Color.green;
                }
                else if (Ranking.orderedRank[i].GetComponent<PlayerControl>().scoreGeneralRound < 0)
                {
                    Ranking.rankList[i].transform.Find("Round Score").GetComponent<Text>().text = Ranking.orderedRank[i].GetComponent<PlayerControl>().scoreGeneralRound.ToString();
                    Ranking.rankList[i].transform.Find("Round Score").GetComponent<Text>().color = Color.red;
                }
                else Ranking.rankList[i].transform.Find("Round Score").GetComponent<Text>().text = "";

                if (GameObject.Find("IconPlayer_" + Ranking.orderedRank[i].name.Substring(Ranking.orderedRank[i].name.Length - 1)) != null)
                    Ranking.rankList[i].transform.Find("Image").GetComponent<Image>().sprite = GameObject.Find("IconPlayer_" + Ranking.orderedRank[i].name.Substring(Ranking.orderedRank[i].name.Length - 1)).GetComponent<Image>().sprite;
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
            this.gameObject.SetActive(false);
            if (Rondes.timesPlayed == Rondes.rondas)
            {
                GameObject.Find("Control").GetComponent<NewControl>().finalWinnerCanvas.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
                GameObject.Find("Control").GetComponent<NewControl>().StartGame();

            foreach (GameObject player in NewControl.players)
                player.GetComponent<PlayerControl>().scoreGeneralRound = 0;
            Time.timeScale = 1;
        }
        else Time.timeScale = 0;
    }
}
