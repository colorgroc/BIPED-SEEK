﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using FMODUnity;
using XInputDotNetPure;

public class NumCanvasSeleccionJugadores : MonoBehaviour
{

    //[SerializeField]
    //private AudioClip onButton, clickButton, backButton;
    //[SerializeField]
    //private AudioSource music, sounds;
    [SerializeField]
    private Text restriccion;

    public static List<GameObject> characterTypes_P1, characterTypes_P2, characterTypes_P3, characterTypes_P4;
    public static List<GameObject> munyequito_P1, munyequito_P2, munyequito_P3, munyequito_P4;
    private List<GameObject> players;

    private int select_1, select_2, select_3, select_4;
    public static bool ready_P1, ready_P2, ready_P3, ready_P4;
    private bool cancel_1, cancel_2, cancel_3, cancel_4, moved;
    [SerializeField]
    private Vector2 outline = new Vector2(10, -10);
    [SerializeField]

    private Color32 blue_color = new Color32(80, 165, 248, 255);
    private Color32 red_color = new Color32(174, 44, 44, 255);
    private Color32 green_color = new Color32(2, 117, 16, 255);
    private Color32 yellow_color = new Color32(255, 215, 0, 255);
    private Vector4 default_Color = new Vector4(0, 0, 0, 128);
    private Vector2 default_outline = new Vector2(4, 4);
    // private int num;
    private GameObject[] characterTypes, munyequitos, jugadores;


    void Start()
    {
        //Music
        //music.volume = PlayerPrefs.GetFloat("MusicVolume");
        // PlayerControl.p1 = PlayerControl.p2 = PlayerControl.p3 = PlayerControl.p4 = false;
        //inicialitzar variables
        jugadores = GameObject.FindGameObjectsWithTag("Seleccion Personajes");
        characterTypes_P1 = new List<GameObject>();
        characterTypes_P2 = new List<GameObject>();
        characterTypes_P3 = new List<GameObject>();
        characterTypes_P4 = new List<GameObject>();
        munyequito_P1 = new List<GameObject>();
        munyequito_P2 = new List<GameObject>();
        munyequito_P3 = new List<GameObject>();
        munyequito_P4 = new List<GameObject>();
        players = new List<GameObject>();
        select_1 = select_2 = select_3 = select_4 = 0;
        this.restriccion.enabled = false;

        //guardar num of players
        PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);
        //Debug.Log(Input.GetJoystickNames()[0].Length);
        //Debug.Log(Input.GetJoystickNames()[1].Length);
        //num = PlayerPrefs.GetInt("NumPlayers");
        InicialitzarJugadors();

        //MapaRandom();     
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);
        if (Input.GetJoystickNames().Length < 2) this.restriccion.enabled = true;
        else this.restriccion.enabled = false;
        //InicialitzarJugadors();
        SeleccionJugadores(Input.GetJoystickNames().Length);

        //reset cancel buton

        /* //proves per 1 jugador
         if (PlayerPrefs.GetInt("NumPlayers") == 1 && ready_P1) SceneManager.LoadScene("Juego");*/
        //quan tothom ready, comença joc
        if (PlayerPrefs.GetInt("NumPlayers") == 2 && ready_P1 && ready_P2) SceneManager.LoadScene("Loading");
        else if (PlayerPrefs.GetInt("NumPlayers") == 3 && ready_P1 && ready_P2 && ready_P3) SceneManager.LoadScene("Loading");
        else if (PlayerPrefs.GetInt("NumPlayers") == 4 && ready_P1 && ready_P2 && ready_P3 && ready_P4) SceneManager.LoadScene("Loading");
    }

    void InicialitzarJugadors()
    {
        //inicialitzar canvas seleccio personatges
        for (int i = 0; i < jugadores.Length; i++)
        {
            players.Add(jugadores[i].gameObject);
            jugadores[i].gameObject.SetActive(false);
        }

        players.Sort(SortByName);

        for (int i = 0; i < PlayerPrefs.GetInt("NumPlayers"); i++)
        {
            players[i].SetActive(true);
        }
        foreach (GameObject child in players)
        {
            if (child.gameObject.activeInHierarchy)
            {
                this.characterTypes = GameObject.FindGameObjectsWithTag("Character");
                this.munyequitos = GameObject.FindGameObjectsWithTag("Muñequito");
            }
        }
        if (PlayerPrefs.GetInt("NumPlayers") >= 1)
        {
            for (int i = 0; i < this.characterTypes.Length; i++)
            {
                //butons
                this.characterTypes[i].GetComponent<Outline>().enabled = false;
                this.characterTypes[i].GetComponent<Outline>().effectDistance = default_outline;
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

            for (int i = 0; i < this.munyequitos.Length; i++)
            {
                //muñequitos ballant
                this.munyequitos[i].gameObject.SetActive(false);
                if (i < 4)
                {
                    munyequito_P1.Add(this.munyequitos[i].gameObject);
                    munyequito_P1[0].gameObject.SetActive(true);
                }
                else if (i >= 4 && i < 8)
                {
                    munyequito_P2.Add(this.munyequitos[i].gameObject);
                    munyequito_P2[0].gameObject.SetActive(true);
                }
                else if (i >= 8 && i < 12)
                {
                    munyequito_P3.Add(this.munyequitos[i].gameObject);
                    munyequito_P3[0].gameObject.SetActive(true);
                }
                else
                {
                    munyequito_P4.Add(this.munyequitos[i].gameObject);
                    munyequito_P4[0].gameObject.SetActive(true);
                }
            }
        }
    }
    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

    void SeleccionJugadores(int numOfPlayers)
    {
        ////proves per 1 jugador
        if (numOfPlayers == 1)
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if (Input.GetJoystickNames()[0].Length == 33)
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
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 PS4") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 PS4") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 PS4") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 PS4") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 Fake") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 Fake") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 Fake") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 Fake") > 0)
                    {
                        MoveUp(1);
                    }
                }

            }
            if (Input.GetJoystickNames()[0].Length == 33)
            {
                if (Input.GetButtonDown("A_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 19) //ps4
            {
                if (Input.GetButtonDown("B_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("X_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("X_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 25)
            {
                if (Input.GetButtonDown("X_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }

        }
        if (numOfPlayers == 2)
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if (Input.GetJoystickNames()[0].Length == 33)
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
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 PS4") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 PS4") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 PS4") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 PS4") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 Fake") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 Fake") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 Fake") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 Fake") > 0)
                    {
                        MoveUp(1);
                    }
                }

            }
            if (Input.GetJoystickNames()[0].Length == 33)
            {
                if (Input.GetButtonDown("A_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 19) //ps4
            {
                if (Input.GetButtonDown("B_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("X_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("X_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 25)
            {
                if (Input.GetButtonDown("X_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            //-------------------------Moviments player 2-----------------------------------
            //Horitzontal
            if (!ready_P2)
            {
                if (Input.GetJoystickNames()[1].Length == 33)
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
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2") > 0)
                    {
                        MoveUp(2);
                    }
                }
                else if (Input.GetJoystickNames()[1].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2 PS4") < 0))
                    {
                        MoveLeft(2);
                    }
                    else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2 PS4") > 0)
                    {
                        MoveRight(2);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2 PS4") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2 PS4") > 0)
                    {
                        MoveUp(2);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2 Fake") < 0))
                    {
                        MoveLeft(2);
                    }
                    else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2 Fake") > 0)
                    {
                        MoveRight(2);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2 Fake") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2 Fake") > 0)
                    {
                        MoveUp(2);
                    }
                }
            }
            if (Input.GetJoystickNames()[1].Length == 33)
            {
                if (Input.GetButtonDown("A_2"))
                {

                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("B_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("B_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[1].Length == 19)
            {
                if (Input.GetButtonDown("B_2"))
                {
                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("X_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("X_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[1].Length == 25)
            {
                if (Input.GetButtonDown("X_2"))
                {
                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("B_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("B_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }

        }
        else if (numOfPlayers == 3)
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if (Input.GetJoystickNames()[0].Length == 33)
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
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 PS4") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 PS4") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 PS4") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 PS4") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 Fake") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 Fake") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 Fake") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 Fake") > 0)
                    {
                        MoveUp(1);
                    }
                }

            }
            if (Input.GetJoystickNames()[0].Length == 33)
            {
                if (Input.GetButtonDown("A_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 19) //ps4
            {
                if (Input.GetButtonDown("B_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("X_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("X_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 25)
            {
                if (Input.GetButtonDown("X_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            //-------------------------Moviments player 2-----------------------------------
            //Horitzontal
            if (!ready_P2)
            {
                if (Input.GetJoystickNames()[1].Length == 33)
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
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2") > 0)
                    {
                        MoveUp(2);
                    }
                }
                else if (Input.GetJoystickNames()[1].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2 PS4") < 0))
                    {
                        MoveLeft(2);
                    }
                    else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2 PS4") > 0)
                    {
                        MoveRight(2);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2 PS4") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2 PS4") > 0)
                    {
                        MoveUp(2);
                    }
                }
                else if (Input.GetJoystickNames()[1].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2 Fake") < 0))
                    {
                        MoveLeft(2);
                    }
                    else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2 Fake") > 0)
                    {
                        MoveRight(2);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2 Fake") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2 Fake") > 0)
                    {
                        MoveUp(2);
                    }
                }
            }
            if (Input.GetJoystickNames()[1].Length == 33)
            {
                if (Input.GetButtonDown("A_2"))
                {

                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("B_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("B_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[1].Length == 19)
            {
                if (Input.GetButtonDown("B_2"))
                {
                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("X_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("X_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[1].Length == 25)
            {
                if (Input.GetButtonDown("X_2"))
                {
                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("B_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("B_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }


            //-------------------------Moviments player 3-----------------------------------
            //Horitzontal
            if (!ready_P3)
            {
                if (Input.GetJoystickNames()[2].Length == 33)
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
                    if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3") < 0)
                    {
                        MoveDown(3);
                    }
                    else if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3") > 0)
                    {
                        MoveUp(3);
                    }
                }
                else if (Input.GetJoystickNames()[2].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_3") < 0 || Input.GetAxis("H_Arrows_3 PS4") < 0))
                    {
                        MoveLeft(3);
                    }
                    else if (Input.GetAxis("H_LPad_3") > 0 || Input.GetAxis("H_Arrows_3 PS4") > 0)
                    {
                        MoveRight(3);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3 PS4") < 0)
                    {
                        MoveDown(3);
                    }
                    else if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3 PS4") > 0)
                    {
                        MoveUp(3);
                    }
                }
                else if (Input.GetJoystickNames()[2].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_3") < 0 || Input.GetAxis("H_Arrows_3 Fake") < 0))
                    {
                        MoveLeft(3);
                    }
                    else if (Input.GetAxis("H_LPad_3") > 0 || Input.GetAxis("H_Arrows_3 Fake") > 0)
                    {
                        MoveRight(3);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3 Fake") < 0)
                    {
                        MoveDown(3);
                    }
                    else if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3 Fake") > 0)
                    {
                        MoveUp(3);
                    }
                }

            }
            if (Input.GetJoystickNames()[2].Length == 33)
            {
                if (Input.GetButtonDown("A_3"))
                {

                    if (!ready_P3)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    }
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = green_color;
                    PlayerPrefs.SetInt("characterPlayer_3", select_3 + 1);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                    ready_P3 = true;
                    cancel_3 = false;
                }
                if (Input.GetButtonDown("B_3") && ready_P3)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_3", 0);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                    ready_P3 = false;
                    cancel_3 = true;
                }
                else if (Input.GetButtonDown("B_3") && ((!cancel_3 && !ready_P3) || (cancel_3)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[2].Length == 19)
            {
                if (Input.GetButtonDown("B_3"))
                {

                    if (!ready_P3)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    }
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = green_color;
                    PlayerPrefs.SetInt("characterPlayer_3", select_3 + 1);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                    ready_P3 = true;
                    cancel_3 = false;
                }
                if (Input.GetButtonDown("X_3") && ready_P3)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_3", 0);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                    ready_P3 = false;
                    cancel_3 = true;
                }
                else if (Input.GetButtonDown("X_3") && ((!cancel_3 && !ready_P3) || (cancel_3)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[2].Length == 25)
            {
                if (Input.GetButtonDown("X_3"))
                {

                    if (!ready_P3)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    }
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = green_color;
                    PlayerPrefs.SetInt("characterPlayer_3", select_3 + 1);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                    ready_P3 = true;
                    cancel_3 = false;
                }
                if (Input.GetButtonDown("B_3") && ready_P3)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_3", 0);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                    ready_P3 = false;
                    cancel_3 = true;
                }
                else if (Input.GetButtonDown("B_3") && ((!cancel_3 && !ready_P3) || (cancel_3)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
        }
        else if (numOfPlayers == 4)
        {
            //-------------------------Moviments player 1-----------------------------------
            //Horitzontal
            if (!ready_P1)
            {
                if (Input.GetJoystickNames()[0].Length == 33)
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
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 PS4") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 PS4") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 PS4") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 PS4") > 0)
                    {
                        MoveUp(1);
                    }
                }
                else if (Input.GetJoystickNames()[0].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_1") < 0 || Input.GetAxis("H_Arrows_1 Fake") < 0))
                    {
                        MoveLeft(1);
                    }
                    else if (Input.GetAxis("H_LPad_1") > 0 || Input.GetAxis("H_Arrows_1 Fake") > 0)
                    {
                        MoveRight(1);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_1") > 0 || Input.GetAxis("V_Arrows_1 Fake") < 0)
                    {
                        MoveDown(1);
                    }
                    else if (Input.GetAxis("V_LPad_1") < 0 || Input.GetAxis("V_Arrows_1 Fake") > 0)
                    {
                        MoveUp(1);
                    }
                }

            }
            if (Input.GetJoystickNames()[0].Length == 33)
            {
                if (Input.GetButtonDown("A_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 19) //ps4
            {
                if (Input.GetButtonDown("B_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("X_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("X_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[0].Length == 25)
            {
                if (Input.GetButtonDown("X_1"))
                {
                    if (!ready_P1)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = blue_color;
                    PlayerPrefs.SetInt("characterPlayer_1", select_1 + 1);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = true;
                    ready_P1 = true;
                    cancel_1 = false;
                }
                if (Input.GetButtonDown("B_1") && ready_P1)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P1[select_1].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P1[select_1].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_1", 0);
                    GameObject.Find("Ready_1").GetComponent<Text>().enabled = false;
                    ready_P1 = false;
                    cancel_1 = true;
                }
                else if (Input.GetButtonDown("B_1") && ((!cancel_1 && !ready_P1) || (cancel_1)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            //-------------------------Moviments player 2-----------------------------------
            //Horitzontal
            if (!ready_P2)
            {
                if (Input.GetJoystickNames()[1].Length == 33)
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
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2") > 0)
                    {
                        MoveUp(2);
                    }
                }
                else if (Input.GetJoystickNames()[1].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2 PS4") < 0))
                    {
                        MoveLeft(2);
                    }
                    else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2 PS4") > 0)
                    {
                        MoveRight(2);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2 PS4") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2 PS4") > 0)
                    {
                        MoveUp(2);
                    }
                }
                else if (Input.GetJoystickNames()[1].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_2") < 0 || Input.GetAxis("H_Arrows_2 Fake") < 0))
                    {
                        MoveLeft(2);
                    }
                    else if (Input.GetAxis("H_LPad_2") > 0 || Input.GetAxis("H_Arrows_2 Fake") > 0)
                    {
                        MoveRight(2);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_2") > 0 || Input.GetAxis("V_Arrows_2 Fake") < 0)
                    {
                        MoveDown(2);
                    }
                    else if (Input.GetAxis("V_LPad_2") < 0 || Input.GetAxis("V_Arrows_2 Fake") > 0)
                    {
                        MoveUp(2);
                    }
                }
            }
            if (Input.GetJoystickNames()[1].Length == 33)
            {
                if (Input.GetButtonDown("A_2"))
                {

                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("B_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("B_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[1].Length == 19)
            {
                if (Input.GetButtonDown("B_2"))
                {
                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("X_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("X_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[1].Length == 25)
            {
                if (Input.GetButtonDown("X_2"))
                {
                    if (!ready_P2)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = red_color;
                    PlayerPrefs.SetInt("characterPlayer_2", select_2 + 1);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = true;
                    ready_P2 = true;
                    cancel_2 = false;
                }
                if (Input.GetButtonDown("B_2"))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P2[select_2].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P2[select_2].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_2", 0);
                    GameObject.Find("Ready_2").GetComponent<Text>().enabled = false;
                    ready_P2 = false;
                    cancel_2 = true;
                }
                else if (Input.GetButtonDown("B_2") && ((!cancel_2 && !ready_P2) || (cancel_2)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }


            //-------------------------Moviments player 3-----------------------------------
            //Horitzontal
            if (!ready_P3)
            {
                if (Input.GetJoystickNames()[2].Length == 33)
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
                    if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3") < 0)
                    {
                        MoveDown(3);
                    }
                    else if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3") > 0)
                    {
                        MoveUp(3);
                    }
                }
                else if (Input.GetJoystickNames()[2].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_3") < 0 || Input.GetAxis("H_Arrows_3 PS4") < 0))
                    {
                        MoveLeft(3);
                    }
                    else if (Input.GetAxis("H_LPad_3") > 0 || Input.GetAxis("H_Arrows_3 PS4") > 0)
                    {
                        MoveRight(3);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3 PS4") < 0)
                    {
                        MoveDown(3);
                    }
                    else if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3 PS4") > 0)
                    {
                        MoveUp(3);
                    }
                }
                else if (Input.GetJoystickNames()[2].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_3") < 0 || Input.GetAxis("H_Arrows_3 Fake") < 0))
                    {
                        MoveLeft(3);
                    }
                    else if (Input.GetAxis("H_LPad_3") > 0 || Input.GetAxis("H_Arrows_3 Fake") > 0)
                    {
                        MoveRight(3);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_3") > 0 || Input.GetAxis("V_Arrows_3 Fake") < 0)
                    {
                        MoveDown(3);
                    }
                    else if (Input.GetAxis("V_LPad_3") < 0 || Input.GetAxis("V_Arrows_3 Fake") > 0)
                    {
                        MoveUp(3);
                    }
                }

            }
            if (Input.GetJoystickNames()[2].Length == 33)
            {
                if (Input.GetButtonDown("A_3"))
                {

                    if (!ready_P3)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    }
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = green_color;
                    PlayerPrefs.SetInt("characterPlayer_3", select_3 + 1);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                    ready_P3 = true;
                    cancel_3 = false;
                }
                if (Input.GetButtonDown("B_3") && ready_P3)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_3", 0);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                    ready_P3 = false;
                    cancel_3 = true;
                }
                else if (Input.GetButtonDown("B_3") && ((!cancel_3 && !ready_P3) || (cancel_3)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[2].Length == 19)
            {
                if (Input.GetButtonDown("B_3"))
                {

                    if (!ready_P3)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    }
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = green_color;
                    PlayerPrefs.SetInt("characterPlayer_3", select_3 + 1);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                    ready_P3 = true;
                    cancel_3 = false;
                }
                if (Input.GetButtonDown("X_3") && ready_P3)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_3", 0);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                    ready_P3 = false;
                    cancel_3 = true;
                }
                else if (Input.GetButtonDown("X_3") && ((!cancel_3 && !ready_P3) || (cancel_3)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[2].Length == 25)
            {
                if (Input.GetButtonDown("X_3"))
                {

                    if (!ready_P3)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    }
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = green_color;
                    PlayerPrefs.SetInt("characterPlayer_3", select_3 + 1);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = true;
                    ready_P3 = true;
                    cancel_3 = false;
                }
                if (Input.GetButtonDown("B_3") && ready_P3)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P3[select_3].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P3[select_3].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_3", 0);
                    GameObject.Find("Ready_3").GetComponent<Text>().enabled = false;
                    ready_P3 = false;
                    cancel_3 = true;
                }
                else if (Input.GetButtonDown("B_3") && ((!cancel_3 && !ready_P3) || (cancel_3)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }

            //-------------------------Moviments player 4-----------------------------------
            //Horitzontal
            if (!ready_P4)
            {
                if (Input.GetJoystickNames()[3].Length == 33)
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
                    if (Input.GetAxis("V_LPad_4") > 0 || Input.GetAxis("V_Arrows_4") < 0)
                    {
                        MoveDown(4);
                    }
                    else if (Input.GetAxis("V_LPad_4") < 0 || Input.GetAxis("V_Arrows_4") > 0)
                    {
                        MoveUp(4);
                    }
                }
                else if (Input.GetJoystickNames()[3].Length == 19)
                {
                    if ((Input.GetAxis("H_LPad_4") < 0 || Input.GetAxis("H_Arrows_4 PS4") < 0))
                    {
                        MoveLeft(4);
                    }
                    else if (Input.GetAxis("H_LPad_4") > 0 || Input.GetAxis("H_Arrows_4 PS4") > 0)
                    {
                        MoveRight(4);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_4") > 0 || Input.GetAxis("V_Arrows_4 PS4") < 0)
                    {
                        MoveDown(4);
                    }
                    else if (Input.GetAxis("V_LPad_4") < 0 || Input.GetAxis("V_Arrows_4 PS4") > 0)
                    {
                        MoveUp(4);
                    }
                }
                else if (Input.GetJoystickNames()[3].Length == 25)
                {
                    if ((Input.GetAxis("H_LPad_4") < 0 || Input.GetAxis("H_Arrows_4 Fake") < 0))
                    {
                        MoveLeft(4);
                    }
                    else if (Input.GetAxis("H_LPad_4") > 0 || Input.GetAxis("H_Arrows_4 Fake") > 0)
                    {
                        MoveRight(4);
                    }

                    //Vertical
                    if (Input.GetAxis("V_LPad_4") > 0 || Input.GetAxis("V_Arrows_4 Fake") < 0)
                    {
                        MoveDown(4);
                    }
                    else if (Input.GetAxis("V_LPad_4") < 0 || Input.GetAxis("V_Arrows_4 Fake") > 0)
                    {
                        MoveUp(4);
                    }
                }
            }
            if (Input.GetJoystickNames()[3].Length == 33)
            {
                if (Input.GetButtonDown("A_4"))
                {
                    if (!ready_P4)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P4[select_4].GetComponent<Outline>().effectColor = yellow_color;
                    PlayerPrefs.SetInt("characterPlayer_4", select_4 + 1);
                    GameObject.Find("Ready_4").GetComponent<Text>().enabled = true;
                    ready_P4 = true;
                    cancel_4 = false;
                }

                if (Input.GetButtonDown("B_4") && ready_P4)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P4[select_4].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_4", 0);
                    GameObject.Find("Ready_4").GetComponent<Text>().enabled = false;
                    ready_P4 = false;
                    cancel_4 = true;
                }
                else if (Input.GetButtonDown("B_4") && ((!cancel_4 && !ready_P4) || (cancel_4)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[3].Length == 19)
            {
                if (Input.GetButtonDown("B_4"))
                {
                    if (!ready_P4)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P4[select_4].GetComponent<Outline>().effectColor = yellow_color;
                    PlayerPrefs.SetInt("characterPlayer_4", select_4 + 1);
                    GameObject.Find("Ready_4").GetComponent<Text>().enabled = true;
                    ready_P4 = true;
                    cancel_4 = false;
                }

                if (Input.GetButtonDown("X_4") && ready_P4)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P4[select_4].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_4", 0);
                    GameObject.Find("Ready_4").GetComponent<Text>().enabled = false;
                    ready_P4 = false;
                    cancel_4 = true;
                }
                else if (Input.GetButtonDown("X_4") && ((!cancel_4 && !ready_P4) || (cancel_4)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
            else if (Input.GetJoystickNames()[3].Length == 25)
            {
                if (Input.GetButtonDown("X_4"))
                {
                    if (!ready_P4)
                    {
                        //sounds.PlayOneShot(clickButton);
                        RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                    }
                    characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = outline;
                    characterTypes_P4[select_4].GetComponent<Outline>().effectColor = yellow_color;
                    PlayerPrefs.SetInt("characterPlayer_4", select_4 + 1);
                    GameObject.Find("Ready_4").GetComponent<Text>().enabled = true;
                    ready_P4 = true;
                    cancel_4 = false;
                }

                if (Input.GetButtonDown("B_4") && ready_P4)
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    characterTypes_P4[select_4].GetComponent<Outline>().effectDistance = default_outline;
                    characterTypes_P4[select_4].GetComponent<Outline>().effectColor = default_Color;
                    PlayerPrefs.SetInt("characterPlayer_4", 0);
                    GameObject.Find("Ready_4").GetComponent<Text>().enabled = false;
                    ready_P4 = false;
                    cancel_4 = true;
                }
                else if (Input.GetButtonDown("B_4") && ((!cancel_4 && !ready_P4) || (cancel_4)))
                {
                    //sounds.PlayOneShot(backButton, 4.0F);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    void MoveRight(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                munyequito_P1[select_1].gameObject.SetActive(false);
                if (select_1 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 1;
                }
                else if (select_1 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 3;
                }
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                munyequito_P1[select_1].gameObject.SetActive(true);
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                munyequito_P2[select_2].gameObject.SetActive(false);
                if (select_2 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 1;
                }
                else if (select_2 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 3;
                }
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                munyequito_P2[select_2].gameObject.SetActive(true);
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                munyequito_P3[select_3].gameObject.SetActive(false);
                if (select_3 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 1;
                }
                else if (select_3 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 3;
                }
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                munyequito_P3[select_3].gameObject.SetActive(true);
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                munyequito_P4[select_4].gameObject.SetActive(false);
                if (select_4 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 1;
                }
                else if (select_4 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 3;
                }
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                munyequito_P4[select_4].gameObject.SetActive(true);
                break;
        }
    }
    void MoveLeft(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                munyequito_P1[select_1].gameObject.SetActive(false);
                if (select_1 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 0;
                }
                else if (select_1 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 2;
                }
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                munyequito_P1[select_1].gameObject.SetActive(true);
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                munyequito_P2[select_2].gameObject.SetActive(false);
                if (select_2 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 0;
                }
                else if (select_2 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 2;
                }
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                munyequito_P2[select_2].gameObject.SetActive(true);
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                munyequito_P3[select_3].gameObject.SetActive(false);
                if (select_3 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 0;
                }
                else if (select_3 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 2;
                }
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                munyequito_P3[select_3].gameObject.SetActive(true);
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                munyequito_P4[select_4].gameObject.SetActive(false);
                if (select_4 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 0;
                }
                else if (select_4 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 2;
                }
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                munyequito_P4[select_4].gameObject.SetActive(true);
                break;
        }
    }
    void MoveUp(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                munyequito_P1[select_1].gameObject.SetActive(false);
                if (select_1 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 2;
                }
                else if (select_1 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 3;
                }
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                munyequito_P1[select_1].gameObject.SetActive(true);
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                munyequito_P2[select_2].gameObject.SetActive(false);
                if (select_2 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 2;
                }
                else if (select_2 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 3;
                }
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                munyequito_P2[select_2].gameObject.SetActive(true);
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                munyequito_P3[select_3].gameObject.SetActive(false);
                if (select_3 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 2;
                }
                else if (select_3 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 3;
                }
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                munyequito_P3[select_3].gameObject.SetActive(true);
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                munyequito_P4[select_4].gameObject.SetActive(false);
                if (select_4 == 0)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 2;
                }
                else if (select_4 == 1)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 3;
                }
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                munyequito_P4[select_4].gameObject.SetActive(true);
                break;
        }
    }
    void MoveDown(int whichPlayer)
    {
        switch (whichPlayer)
        {
            case 1:
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = false;
                munyequito_P1[select_1].gameObject.SetActive(false);
                if (select_1 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 0;
                }
                else if (select_1 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_1 = 1;
                }
                characterTypes_P1[select_1].GetComponent<Outline>().enabled = true;
                munyequito_P1[select_1].gameObject.SetActive(true);
                break;
            case 2:
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = false;
                munyequito_P2[select_2].gameObject.SetActive(false);
                if (select_2 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 0;
                }
                else if (select_2 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_2 = 1;
                }
                characterTypes_P2[select_2].GetComponent<Outline>().enabled = true;
                munyequito_P2[select_2].gameObject.SetActive(true);
                break;
            case 3:
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = false;
                munyequito_P3[select_3].gameObject.SetActive(false);
                if (select_3 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 0;
                }
                else if (select_3 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_3 = 1;
                }
                characterTypes_P3[select_3].GetComponent<Outline>().enabled = true;
                munyequito_P3[select_3].gameObject.SetActive(true);
                break;
            case 4:
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = false;
                munyequito_P4[select_4].gameObject.SetActive(false);
                if (select_4 == 2)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 0;
                }
                else if (select_4 == 3)
                {
                    //sounds.PlayOneShot(onButton);
                    RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
                    select_4 = 1;
                }
                characterTypes_P4[select_4].GetComponent<Outline>().enabled = true;
                munyequito_P4[select_4].gameObject.SetActive(true);
                break;
        }
    }
}
