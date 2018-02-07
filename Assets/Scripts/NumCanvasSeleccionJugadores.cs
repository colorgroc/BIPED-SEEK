using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class NumCanvasSeleccionJugadores : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Seleccion Personajes");
        List<GameObject> players = new List<GameObject>();

        for (int i = 0; i < jugadores.Length; i++)
        {
            players.Add(jugadores[i].gameObject);
            jugadores[i].gameObject.SetActive(false);
            
        }
        //players.OrderBy(go => go.name).ToList();
        players.Sort(SortByName);

        for (int i = 0; i < NewControl.numOfPlayers; i++) {
            players[i].SetActive(true);
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
