﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    [SerializeField]
    Slider slider;
    [SerializeField]
    float loadingTime;
    [SerializeField]
    private int numOfMapas = 4;
    float time;
    private string scene;
    // Use this for initialization
    void Start () {
        slider.maxValue = loadingTime;
        time = 0;
        slider.value = time;
        MapaRandom();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.fixedDeltaTime;
        Debug.Log(time);
        slider.value = time;
        if (time >= loadingTime) SceneManager.LoadScene(this.scene);
    }

    private void MapaRandom()
    {
        List<string> mapas = new List<string>();
        for (int i = 0; i < numOfMapas; i++)
        {
            mapas.Add("Mapa_" + (i + 1).ToString());
        }
        mapas.Sort(SortByName);

        int mapaAleatrio = UnityEngine.Random.Range(0, mapas.Count);
        this.scene = mapas[mapaAleatrio];
        //Debug.Log(this.scene);
    }

    private static int SortByName(string o1, string o2)
    {
        return o1.CompareTo(o2);
    }
}
