using System.Collections;
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

    void Start () {
        slider.maxValue = loadingTime;
        time = 0;
        slider.value = time;
        MapaRandom();
	}
	
	void Update () {
        time += Time.deltaTime;
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

    }

    private static int SortByName(string o1, string o2)
    {
        return o1.CompareTo(o2);
    }
    void Default()
    {
        NewControl.killers = null;
        NewControl.players = null;
        NewControl.guards = null;
        NewControl.numOfPlayers = 0;
        NewControl.objComplete = false;
        NewControl.objKilledByGuard = false;
        NewControl.timeLeft = 0;
        NewControl.objective = null;
        NewControl.finalWinner = null;
        NewControl.parcialWinner = null;
        NumCanvasSeleccionJugadores.ready_P1 = false;
        NumCanvasSeleccionJugadores.ready_P2 = false;
        NumCanvasSeleccionJugadores.ready_P3 = false;
        NumCanvasSeleccionJugadores.ready_P4 = false;
    }
}
