using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Ranking : MonoBehaviour
{

    private GameObject[] rankings;
    public static List<GameObject> rankList = new List<GameObject>();
    public static List<GameObject> orderedRank = new List<GameObject>();
    // Use this for initialization
	void Awake(){

        rankings = GameObject.FindGameObjectsWithTag("Ranking");
		foreach (GameObject rank in rankings) {
			rankList.Add (rank);
			rank.SetActive(false);
		}
            rankList.Sort(SortByName);
		for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++)
		{
				rankList[i].SetActive(true);
		}
	}
    void Start()
    {

    }
    // Update is called once per frame

    private static int SortByName(GameObject o1, GameObject o2)
    {
        
        return o1.name.CompareTo(o2.name);
    }

    public static void Guanyador()
    {
        if(orderedRank.Count > 0 && NewControl.players[0].gameObject != null) NewControl.finalWinner = orderedRank[0].gameObject;
    }
}
