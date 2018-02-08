using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using System.Linq;

public class NumCanvasSeleccionJugadores : MonoBehaviour {
    bool pressed = false;
    int pos = 0;
    float c = 0;
    int size = 0;
    List<GameObject> characterTypes;
    
    //List<GameObject> characters;
    List<GameObject> players;
    // Use this for initialization
    void Start() {
        this.characterTypes = new List<GameObject>();
        //this.characters = new List<GameObject>();
        GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Seleccion Personajes");


        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(this.characterTypes[0]);
        //this.characterTypes[0].GetComponent<Button>().Select();

        // EventSystem.current.SetSelectedGameObject(null);
        // if (this.gameObject.name.EndsWith("1"))
        // EventSystem.current.SetSelectedGameObject(this.gameObject);

        //PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);
        PlayerPrefs.SetInt("NumPlayers", 2);

        players = new List<GameObject>();

        for (int i = 0; i < jugadores.Length; i++)
        {
            players.Add(jugadores[i].gameObject);
            jugadores[i].gameObject.SetActive(false);

        }
        //players.OrderBy(go => go.name).ToList();
        players.Sort(SortByName);

        for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++) {
            players[i].SetActive(true);
            //  Button[] buton = players[i].GetComponentsInChildren<Button>();
            //if(buton[i].gameObject.name.EndsWith("1")) EventSystem.current.SetSelectedGameObject(buton[i].gameObject);
        }
        foreach (GameObject child in players)
        {
            if (child.gameObject.activeInHierarchy)
            {
                
                
                /* foreach(GameObject kid in child.gameObject.transform.parent)
                     this.characterTypes.Add(child.gameObject);*/

            }
        }
        Debug.Log(size);
        Debug.Log(this.characterTypes.Count);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
        
      /* if(players.Count == 2)
        {
            //Button[] butons_1 = GameObject.Find("Tipos P_1").GetComponentsInChildren<Button>();
            //Button[] butons_2 = GameObject.Find("Tipos P_2").GetComponentsInChildren<Button>();
            float ControlPlayer_1 = Input.GetAxisRaw("Horizontal");
            float ControlPlayer_2 = Input.GetAxisRaw("Horizontal_2");
        }*/
     

        }
    
    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

   /* public int Arrows_Horizontal(float x)
    {
        bool pressed = true;
        int pos = 0;
        //float x = Input.GetAxisRaw("Horizontal");

        if (x < 0 && pressed)
        {
            pos = -1;
            pressed = false;
            Debug.Log("Izq");

        }
        else if (x > 0 && pressed)
        {
            pos = 1;
            pressed = false;
            Debug.Log("Derch");
        }
        if (pos < 0) pos = 0;
        return pos;
        
    }*/
    /*public int Arrows_Vertical(float y, int pos, float change)
    {
        
        //int pos = 0;
        //float y = Input.GetAxisRaw("Vertical");
        change = y;
        if (y != change) pressed = true;
        if (y < 0 && pressed)
        {
            pos--;
            pressed = false;
            //Debug.Log("Arriba");

        }
        else if (y > 0 && pressed)
        {
            pos++;
            pressed = false;
           // Debug.Log("Abajo");
        }
        if (pos < 0) pos = 0;
        Debug.Log(pos);
        return pos;
    }*/
}
