using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Ranking : MonoBehaviour
{

    private GameObject[] rankings;
    public static List<GameObject> rankList = new List<GameObject>();
    // public static List<Rank> ranking = new List<Rank>();
    //private List<Rank> orderedRank = ranking.OrderByDescending(p => p.player.GetComponent<PlayerControl>().scoreGeneral).ToList();
    public static List<GameObject> orderedRank;
    // Use this for initialization

    void Start()
    {
        //NewControl.players.Where(p => p != null);
        rankings = GameObject.FindGameObjectsWithTag("Ranking");
        foreach (GameObject rank in rankings) rankList.Add(rank);
        rankList.Sort(SortByName);
        foreach (GameObject rank in rankList) rank.SetActive(false);
        for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++)
        {
            rankList[i].SetActive(true);
            //if(GameObject.Find("Player " + i.ToString()) != null)
            //    ranking.Add(new Rank(GameObject.Find("Player " + i.ToString()), rankList[i]));
        }
        //OrdenarRanking();
    }
    // Update is called once per frame

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

    public static void Guanyador()
    {
        //if (ranking[ranking.Count - 1].player != null)
        //    NewControl.finalWinner = ranking[ranking.Count - 1].player;
        //if(ranking[0].player != null) NewControl.finalWinner = ranking[0].player;
        if(orderedRank[0].gameObject != null) NewControl.finalWinner = orderedRank[0].gameObject;
    }
}
