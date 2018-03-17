using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondes : MonoBehaviour {

    public static int timesPlayed;
    public static int rondas;
    private int rondes;
    private void Awake()
    {
        rondes = PlayerPrefs.GetInt("NumPlayers") * 2;
        // PlayerPrefs.SetInt("Rondes", 0);
        timesPlayed = 0;
        rondas = rondes;
        //timesPlayed = PlayerPrefs.GetInt("Rondes");
    }


}
