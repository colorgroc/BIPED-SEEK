using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumHUDS : MonoBehaviour {

    private List<GameObject> huds;
    // Use this for initialization
    void Start () {
        GameObject[] hudsList = GameObject.FindGameObjectsWithTag("HUD");
        this.huds = new List<GameObject>();

        for (int i = 0; i < hudsList.Length; i++)
        {
            this.huds.Add(hudsList[i].gameObject);
            hudsList[i].gameObject.SetActive(false);
        }

        this.huds.Sort(SortByName);

        for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++)
        {
            this.huds[i].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }
}
