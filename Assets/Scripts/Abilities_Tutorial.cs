using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Abilities_Tutorial : MonoBehaviour {

    [SerializeField]
    private GameObject welcome1, welcome2, smoke, freeze, control, invisible, sprint, teleport, finish, back, pausa;
    public Text titol;
    public static bool tutorialPaused;
    private int OK;
    public static bool show;//, tuto;
    public FMOD.Studio.EventInstance backgroudSound;
    public FMOD.Studio.EventInstance backgroudMusic;

    // Use this for initialization
    private void Awake()
    {
        //QualitySettings.SetQualityLevel(5);
        show = true;
        Time.timeScale = 0;
        titol = GameObject.Find("Titol").GetComponent<Text>();
        OK = 0;
        tutorialPaused = false;
        //tuto = true;
        welcome1.SetActive(true);
        welcome2.SetActive(false);
        smoke.SetActive(false);
        freeze.SetActive(false);
        control.SetActive(false);
        invisible.SetActive(false);
        sprint.SetActive(false);
        teleport.SetActive(false);
        finish.SetActive(false);
        pausa.SetActive(false);

        backgroudMusic = RuntimeManager.CreateInstance("event:/BipedSeek/Music/Mapa 1");
        backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Ambient/Wind");
        backgroudSound.setParameterValue("Vent Loop", 0.2f);

    }
    void Start()
    {
        backgroudMusic.start();
        backgroudSound.start();
    }

    // Update is called once per frame
    void Update()
    {
        Pausa();

        if (Input.GetButtonDown("Submit") && Time.timeScale == 0 && !tutorialPaused)
        {
            OK++;
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
        }
        if (Input.GetButtonDown("Cancel") && Time.timeScale == 0 && !tutorialPaused)
        {
            if (OK > 0)
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
            OK--;

        }
        if (OK < 0) OK = 0;
        if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")) && Time.timeScale == 0 && !tutorialPaused)
        {
            //tuto = true;
            if (OK == 0)
            {
                welcome1.SetActive(true);
                titol.text = "Abilities tutorial";
                welcome2.SetActive(false);
                back.SetActive(false);
            }
            if (OK == 1)
            {
                welcome1.SetActive(false);
                titol.text = "Abilities Controls";
                welcome2.SetActive(true);
                smoke.SetActive(false);
                back.SetActive(true);
            }
            else if (OK == 2)
            {
                welcome2.SetActive(false);
                titol.text = "Smoke";
                smoke.SetActive(true);
                freeze.SetActive(false);
            }
            else if (OK == 3)
            {
                smoke.SetActive(false);
                titol.text = "Freeze";
                freeze.SetActive(true);
                control.SetActive(false);
            }
            else if (OK == 4)
            {
                titol.text = "NPC Control";
                freeze.SetActive(false);
                control.SetActive(true);
                invisible.SetActive(false);
            }
            else if (OK == 5)
            {
                titol.text = "Invisibility";
                invisible.SetActive(true);
                control.SetActive(false);
                sprint.SetActive(false);
            }
            else if (OK == 6)
            {
                titol.text = "Sprint";
                sprint.SetActive(true);
                invisible.SetActive(false);
                teleport.SetActive(false);
            }
            else if (OK == 7)
            {
                titol.text = "Teleport";
                teleport.SetActive(true);
                sprint.SetActive(false);
                finish.SetActive(false);
            }
            else if (OK == 8)
            {
                teleport.SetActive(false);
                titol.text = "Finnish";
                finish.SetActive(true);
            }
            else if (OK == 9)
            {
                finish.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    private void Pausa()
    {
        if (Input.GetButtonDown("Start") || (tutorialPaused && Input.GetButtonDown("Cancel")))
        {
            if (!tutorialPaused)
            {
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
                //soundSource.PlayOneShot(pauseSound);
            }

            else
            {
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
                //soundSource.PlayOneShot(backSound);
            }

            tutorialPaused = !tutorialPaused;
            backgroudMusic.setPaused(tutorialPaused);
            backgroudSound.setPaused(tutorialPaused);
            pausa.SetActive(tutorialPaused);
        }
        if (Input.GetButtonDown("Submit") && tutorialPaused)
        {
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
            OK = 0;
            welcome1.SetActive(true);
        }
        if (Input.GetButtonDown("Main Menu") && tutorialPaused)
        {
            //soundSource.PlayOneShot(menuSound);
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            pausa.SetActive(false);
            Time.timeScale = 1;
            backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            tutorialPaused = false;
            SceneManager.LoadScene("Menu");
        }
        if (tutorialPaused)
            Time.timeScale = 0;
        else if (!tutorialPaused) Time.timeScale = 1;

    }
}
