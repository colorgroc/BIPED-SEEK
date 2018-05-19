using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondes : MonoBehaviour {

    public static int timesPlayed;
    public static int rondas;
    private int rondes;
    private void Awake()
    {
        rondes = NewControl.numRondesPerJugador * 2;
        timesPlayed = 0;
        rondas = rondes;
    }
}
