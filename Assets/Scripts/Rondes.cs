using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondes : MonoBehaviour {

    public static int timesPlayed;
    public static int rondas;
    private int rondes;
    private void Awake()
    {
        rondes = NewControl.numRondesPerJugador * PlayerPrefs.GetInt("NumPlayers");
        timesPlayed = 0;
        rondas = rondes;
    }
}
