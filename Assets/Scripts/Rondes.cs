using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondes : MonoBehaviour {

    public static int timesPlayed;
    public static int rondas;
    [SerializeField]
    private int rondes = 1;
    private void Awake()
    {
        PlayerPrefs.SetInt("Rondes", 0);
        timesPlayed = 0;
        rondas = rondes;
        //timesPlayed = PlayerPrefs.GetInt("Rondes");
    }


}
