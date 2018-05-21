using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RankingCanvas : MonoBehaviour {
    [SerializeField]
    private AudioClip roundSound;
    private AudioSource soundSource;
    [SerializeField]
    private Text round;
    // Use this for initialization
    void Start () {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        Time.timeScale = 0;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Rondes.timesPlayed == Rondes.rondas)
            round.text = "End Game";
        else round.text = "Next Round";

        Ranking.orderedRank = NewControl.players.OrderByDescending(p => p.GetComponent<PlayerControl>().scoreGeneral).ToList();

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

            if (GameObject.Find("IconPlayer_" + Ranking.orderedRank[i].name.Substring(Ranking.orderedRank[i].name.Length -1)) != null)
                Ranking.rankList[i].transform.Find("Image").GetComponent<Image>().sprite = GameObject.Find("IconPlayer_" + Ranking.orderedRank[i].name.Substring(Ranking.orderedRank[i].name.Length - 1)).GetComponent<Image>().sprite;
        }

        if (Input.GetButtonDown("Submit"))
        {
            this.gameObject.SetActive(false);  
            if (Rondes.timesPlayed == Rondes.rondas)
                GameObject.Find("Control").GetComponent<NewControl>().finalWinnerCanvas.SetActive(true);
            else
                GameObject.Find("Control").GetComponent<NewControl>().StartGame();
            
            foreach (GameObject player in NewControl.players)
                player.GetComponent<PlayerControl>().scoreGeneralRound = player.GetComponent<PlayerControl>().scoreKillsRound = player.GetComponent<PlayerControl>().scoreWinsRound = 0;
            Time.timeScale = 1;
        }
        else Time.timeScale = 0;
    }
}
