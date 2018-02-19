using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumCanvasControllers : MonoBehaviour {

    private List<GameObject> toggles;
    // Use this for initialization
    void Start()
    {
        GameObject[] playersControlList = GameObject.FindGameObjectsWithTag("PlayersControllers");
        this.toggles = new List<GameObject>();
        for (int i = 0; i < playersControlList.Length; i++)
        {
            playersControlList[i].gameObject.GetComponent<Toggle>().interactable = false;
            this.toggles.Add(playersControlList[i].gameObject);
        }

        this.toggles.Sort(SortByName);

        for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++)
        {
            this.toggles[i].gameObject.GetComponent<Toggle>().interactable = true;
        }
    }

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

}
