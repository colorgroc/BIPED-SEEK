using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using System.Linq;

public class NumCanvasSeleccionJugadores : MonoBehaviour {

    public static List<GameObject> characterTypes_P1;
    public static List<GameObject> characterTypes_P2;
    public static List<GameObject> characterTypes_P3;
    public static List<GameObject> characterTypes_P4;
    private List<GameObject> players;
    private int select_1, select_2, select_3, select_4;
    private static bool ready_P1, ready_P2, ready_P3, ready_P4;
    [SerializeField]
    private Vector2 outline = new Vector2(10, 10);
    [SerializeField]
    private Vector4 gold_Color = new Vector4(255, 215, 0, 255);
    private Vector4 default_Color = new Vector4(0, 0, 0, 128);
    private Vector2 default_outline = new Vector2(4, 4);
    private GameObject[] characterTypes;
   
    //bool changeStatus;

    
    // Use this for initialization
    void Start() {

        //inicialitzar variables
        GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Seleccion Personajes");
        characterTypes_P1 = new List<GameObject>();
        characterTypes_P2 = new List<GameObject>();
        characterTypes_P3 = new List<GameObject>();
        characterTypes_P4 = new List<GameObject>();
        players = new List<GameObject>();
        select_1 = select_2 = select_3 = select_4 = 0;
        
        //guardar num of players
        //PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);
        //PlayerPrefs.SetInt("NumPlayers", 2);

        //inicialitzar canvas seleccio personatges
        for (int i = 0; i < jugadores.Length; i++)
        {
            players.Add(jugadores[i].gameObject);
            jugadores[i].gameObject.SetActive(false);
        }

        players.Sort(SortByName);

        for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++) {
            players[i].SetActive(true);
        }
        foreach (GameObject child in players)
        {
            if (child.gameObject.activeInHierarchy)
            {
                this.characterTypes = GameObject.FindGameObjectsWithTag("Character");
            }
        }
        for(int i = 0; i< this.characterTypes.Length; i++)
        {
            //butons
            this.characterTypes[i].GetComponent<Outline>().enabled = false;
            if (i < 4)
            {
                characterTypes_P1.Add(this.characterTypes[i].gameObject);
                characterTypes_P1[0].GetComponent<Outline>().enabled = true;
            }
            else if (i >= 4 && i < 8)
            {
                characterTypes_P2.Add(this.characterTypes[i].gameObject);
                characterTypes_P2[0].GetComponent<Outline>().enabled = true;
            }
            else if (i >= 8 && i < 12)
            {
                characterTypes_P3.Add(this.characterTypes[i].gameObject);
                characterTypes_P3[0].GetComponent<Outline>().enabled = true;
            }
            else
            {
                characterTypes_P4.Add(this.characterTypes[i].gameObject);
                characterTypes_P4[0].GetComponent<Outline>().enabled = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        //codi de prova
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }

        SeleccionJugadores(PlayerPrefs.GetInt("NumPlayers"));

        
        //proves per 1 jugador
        if (PlayerPrefs.GetInt("NumPlayers") == 1 && ready_P1) SceneManager.LoadScene(2);
        //quan tothom ready, comença joc
        if (PlayerPrefs.GetInt("NumPlayers") == 2 && ready_P1 && ready_P2) SceneManager.LoadScene(2);
        else if (PlayerPrefs.GetInt("NumPlayers") == 3 && ready_P1 && ready_P2 && ready_P3) SceneManager.LoadScene(2);
        else if (PlayerPrefs.GetInt("NumPlayers") == 4 && ready_P1 && ready_P2 && ready_P3 && ready_P4) SceneManager.LoadScene(2);

    }
    
    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }
    
    void SeleccionJugadores(int numOfPlayers)
    {
        //proves per 1 jugador
        if(numOfPlayers == 1)
        {
            if (!ready_P1)
            {
                if ((Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal Arrows") < 0))
                {
                    MoveLeft(1);
                }
                else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal Arrows") > 0)
                {
                    MoveRight(1);
                }

                //Vertical
                if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical Arrows") > 0)
                {
                    MoveDown(1);
                }
                else if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical Arrows") < 0)
                {
                    MoveUp(1);
                }
            }
            if (Input.GetButtonDown("Submit"))
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 1);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                ready_P1 = true;
            }
            if (Input.GetButtonDown("Cancel") && ready_P1)
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 0);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                ready_P1 = false;
            }
        }
        if (numOfPlayers == 2) 
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1") < 0))
                {
                    MoveLeft(1);
                }
                else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1") > 0)
                {
                    MoveRight(1);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") < 0)
                {
                    MoveDown(1);
                }
                else if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") > 0)
                {
                    MoveUp(1);
                }
            }
            if (Input.GetButtonDown("A_1"))
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 1);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                ready_P1 = true;
            }
            if (Input.GetButtonDown("B_1") && ready_P1)
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 0);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                ready_P1 = false;
            }

            //-------------------------Moviments player 2-----------------------------------
            //Horitzontal
            if (!ready_P2)
            {
                if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2") < 0))
                {
                    MoveLeft(2);
                }
                else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2") > 0)
                {
                    MoveRight(2);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2") < 0)
                {
                    MoveDown(2);
                }
                else if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2") > 0)
                {
                    MoveUp(2);
                }
            }
            if (Input.GetButtonDown("A_2"))
            {
                characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P2[select_2].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_2", 2);
                GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                ready_P1 = true;
            }
            if (Input.GetButtonDown("B_2"))
            {
                characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_2", 0);
                GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                ready_P1 = false;
            }


        }
        else if (numOfPlayers == 3)
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1") < 0))
                {
                    MoveLeft(1);
                }
                else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1") > 0)
                {
                    MoveRight(1);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") < 0)
                {
                    MoveDown(1);
                }
                else if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") > 0)
                {
                    MoveUp(1);
                }
            }
            if (Input.GetButtonDown("A_1"))
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 1);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                ready_P1 = true;
            }
            if (Input.GetButtonDown("B_1") && ready_P1)
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 0);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                ready_P1 = false;
            }

            //-------------------------Moviments player 2-----------------------------------
            //Horitzontal
            if (!ready_P2)
            {
                if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2") < 0))
                {
                    MoveLeft(2);
                }
                else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2") > 0)
                {
                    MoveRight(2);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2") < 0)
                {
                    MoveDown(2);
                }
                else if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2") > 0)
                {
                    MoveUp(2);
                }
            }
            if (Input.GetButtonDown("A_2"))
            {
                characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P2[select_2].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_2", 2);
                GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                ready_P2 = true;
            }
            if (Input.GetButtonDown("B_2") && ready_P2)
            {
                characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_2", 0);
                GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                ready_P2 = false;
            }

            //-------------------------Moviments player 3-----------------------------------
            //Horitzontal
            if (!ready_P3)
            {
                if ((Input.GetAxis("H_LPad_3") < 0 || Input.GetAxis("H_Arrows_3") < 0))
                {
                    MoveLeft(3);
                }
                else if (Input.GetAxis("H_LPad_3") > 0 || Input.GetAxis("H_Arrows_3") > 0)
                {
                    MoveRight(3);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3") < 0)
                {
                    MoveDown(3);
                }
                else if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3") > 0)
                {
                    MoveUp(3);
                }
            }
            if (Input.GetButtonDown("A_3"))
            {
                characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P3[select_3].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_3", 3);
                GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                ready_P3 = true;
            }
            if (Input.GetButtonDown("B_3") && ready_P3)
            {
                characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_3", 0);
                GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                ready_P3 = false;
            }
        }
        else if (numOfPlayers == 4)
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1") < 0))
                {
                    MoveLeft(1);
                }
                else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1") > 0)
                {
                    MoveRight(1);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") < 0)
                {
                    MoveDown(1);
                }
                else if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") > 0)
                {
                    MoveUp(1);
                }
            }
            if (Input.GetButtonDown("A_1"))
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 1);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                ready_P1 = true;
            }
            if (Input.GetButtonDown("B_1") && ready_P1)
            {
                characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_1", 0);
                GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                ready_P1 = false;
            }

            //-------------------------Moviments player 2-----------------------------------
            //Horitzontal
            if (!ready_P2)
            {
                if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2") < 0))
                {
                    MoveLeft(2);
                }
                else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2") > 0)
                {
                    MoveRight(2);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2") < 0)
                {
                    MoveDown(2);
                }
                else if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2") > 0)
                {
                    MoveUp(2);
                }
            }
            if (Input.GetButtonDown("A_2"))
            {
                characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P2[select_2].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_2", 2);
                GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                ready_P2 = true;
            }
            if (Input.GetButtonDown("B_2") && ready_P2)
            {
                characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_2", 0);
                GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                ready_P2 = false;
            }

            //-------------------------Moviments player 3-----------------------------------
            //Horitzontal
            if (!ready_P3)
            {
                if ((Input.GetAxis("H_LPad_3") < 0 || Input.GetAxis("H_Arrows_3") < 0))
                {
                    MoveLeft(3);
                }
                else if (Input.GetAxis("H_LPad_3") > 0 || Input.GetAxis("H_Arrows_3") > 0)
                {
                    MoveRight(3);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3") < 0)
                {
                    MoveDown(3);
                }
                else if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3") > 0)
                {
                    MoveUp(3);
                }
            }
            if (Input.GetButtonDown("A_3"))
            {
                characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P3[select_3].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_3", 3);
                GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                ready_P3 = true;
            }
            if (Input.GetButtonDown("B_3") && ready_P3)
            {
                characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_3", 0);
                GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                ready_P3 = false;
            }
            //-------------------------Moviments player 4-----------------------------------
            //Horitzontal
            if (!ready_P4)
            {
                if ((Input.GetAxis("H_LPad_4") < 0 || Input.GetAxis("H_Arrows_4") < 0))
                {
                    MoveLeft(4);
                }
                else if (Input.GetAxis("H_LPad_4") > 0 || Input.GetAxis("H_Arrows_4") > 0)
                {
                    MoveRight(4);
                }

                //Vertical
                if (Input.GetAxis("V_LPad_4") < 0 || Input.GetAxis("V_Arrows_4") < 0)
                {
                    MoveDown(4);
                }
                else if (Input.GetAxis("V_LPad_4") > 0 || Input.GetAxis("V_Arrows_4") > 0)
                {
                    MoveUp(4);
                }
            }
            if (Input.GetButtonDown("A_4"))
            {
                characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = outline;
                characterTypes_P4[select_4].GetComponent<Outline>().effectColor = gold_Color;
                PlayerPrefs.SetInt("characterPlayer_4", 4);
                GameObject.Find("Ready_4").GetComponent<Text>().enabled = true;
                ready_P4 = true;
            }
            if (Input.GetButtonDown("B_4") && ready_P4)
            {
                characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = default_outline;
                characterTypes_P4[select_4].GetComponent<Outline>().effectColor = default_Color;
                PlayerPrefs.SetInt("characterPlayer_4", 0);
                GameObject.Find("Ready_4").GetComponent<Text>().enabled = false;
                ready_P4 = false;
            }
        }
    }

    void MoveRight(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                if (select_1 == 0)
                    select_1 = 1;
                else if (select_1 == 2)
                    select_1 = 3;
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                if (select_2 == 0)
                    select_2 = 1;
                else if (select_2 == 2)
                    select_2 = 3;
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                if (select_3 == 0)
                    select_3 = 1;
                else if (select_3 == 2)
                    select_3 = 3;
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                if (select_4 == 0)
                    select_4 = 1;
                else if (select_4 == 2)
                    select_4 = 3;
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                break;
        }
    }
    void MoveLeft(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                if (select_1 == 1)
                    select_1 = 0;
                else if (select_1 == 3)
                    select_1 = 2;
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                if (select_2 == 1)
                    select_2 = 0;
                else if (select_2 == 3)
                    select_2 = 2;
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                if (select_3 == 1)
                    select_3 = 0;
                else if (select_3 == 3)
                    select_3 = 2;
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                if (select_4 == 1)
                    select_4 = 0;
                else if (select_4 == 3)
                    select_4 = 2;
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                break;
        }
    }
    void MoveUp(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                if (select_1 == 0)
                    select_1 = 2;
                else if (select_1 == 1)
                    select_1 = 3;
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                if (select_2 == 0)
                    select_2 = 2;
                else if (select_2 == 1)
                    select_2 = 3;
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                if (select_3 == 0)
                    select_3 = 2;
                else if (select_3 == 1)
                    select_3 = 3;
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                if (select_4 == 0)
                    select_4 = 2;
                else if (select_4 == 1)
                    select_4 = 3;
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                break;
        }
    }
    void MoveDown(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                if (select_1 == 2)
                    select_1 = 0;
                else if (select_1 == 3)
                    select_1 = 1;
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                if (select_2 == 2)
                    select_2 = 0;
                else if (select_2 == 3)
                    select_2 = 1;
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                if (select_3 == 2)
                    select_3 = 0;
                else if (select_3 == 3)
                    select_3 = 1;
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                if (select_4 == 2)
                    select_4 = 0;
                else if (select_4 == 3)
                    select_4 = 1;
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                break;
        }
    }
}
