using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    [SerializeField]
    Scrollbar slider;
    [SerializeField]
    float loadingTime;
    [SerializeField]
    private int numOfMapas = 3, numOfAbilities = 6, numOfUsedAbilities = 2, ability1, ability2;
    [SerializeField]
    Text ab1Nom, ab2Nom, ab1Desc, ab2Desc, totalCargado;
    float time;
    private string scene;
    private int temp;
    [SerializeField]
    private Image iAb1, iAb2;
    [SerializeField]
    private Sprite freeze, control, invisible, teleport, sprint, smoke;
    Resolution res;

    void Start () {

        //control HZ monitors
        res = Screen.currentResolution;
        if (res.refreshRate == 60)
            QualitySettings.vSyncCount = 1;
        if (res.refreshRate == 120)
            QualitySettings.vSyncCount = 2;
        print(QualitySettings.vSyncCount);

        time = temp = 0;
        slider.value = time;
        MapaRandom();
        RandomAbilities();

	}
	
	void FixedUpdate () {
        time += Time.fixedDeltaTime;
        slider.value = time/loadingTime;
        temp++;//= (int) Time.fixedDeltaTime;
        totalCargado.text = ((int)(temp/loadingTime*2)).ToString();
        if (time >= loadingTime) SceneManager.LoadScene(this.scene);
    }
    private void RandomAbilities()
    {
        List<int> abilities = new List<int>();
        for(int i = 0; i < numOfAbilities; i++)
        {
            abilities.Add(i);
        }
        for (int i = 1; i <= numOfUsedAbilities; i++) {
            int rand = Random.Range(0, abilities.Count);
            PlayerPrefs.SetInt("Ability " + (i).ToString(), abilities[rand]);
            abilities.Remove(rand);
        }
        ability1 = PlayerPrefs.GetInt("Ability 1");
        ability2 = PlayerPrefs.GetInt("Ability 2");
        AbilityAsign(ability1, ab1Nom, ab1Desc, iAb1);
        AbilityAsign(ability2, ab2Nom, ab2Desc, iAb2);
    }
    private void AbilityAsign(int ab, Text nom, Text desc, Image i)
    {
        if (ab == (int)NewControl.Abilities.IMMOBILIZER)
        {
            nom.text = "Freeze";
            desc.text = "Freeze everyone around you during x seconds.";
            i.sprite = freeze;
        }
        else if (ab == (int)NewControl.Abilities.INVISIBLITY)
        {
            nom.text = "Invisibility";
            desc.text = "Become invisible during x seconds.";
            i.sprite = invisible;
        }
       /* else if (ab == (int)NewControl.Abilities.REPEL)
        {
            nom.text = "Repel";
            desc.text = "Repel everyone around you.";
        }*/
        else if (ab == (int)NewControl.Abilities.SMOKE)
        {
            nom.text = "Smoke Screen";
            desc.text = "Create smoke during x seconds to hide and escape.";
            i.sprite = smoke;
        }
        else if (ab == (int)NewControl.Abilities.SPRINT)
        {
            nom.text = "Sprint";
            desc.text = "Sprint to run away faster or to chase faster during x seconds.";
            i.sprite = sprint;
        }
        else if (ab == (int)NewControl.Abilities.TELEPORT)
        {
            nom.text = "Teleport";
            desc.text = "Change position with a random NPC of your type.";
            i.sprite = teleport;
        }
        else if (ab == (int)NewControl.Abilities.CONTROL)
        {
            nom.text = "NPC Control";
            desc.text = "Control a random NPC of your type during x seconds. Your character will be controlled by the IA.";
            i.sprite = control;
        }
    }
    private void MapaRandom()
    {
        List<string> mapas = new List<string>();
        for (int i = 1; i <= numOfMapas; i++)
        {
            mapas.Add("Mapa_" + i.ToString());
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
