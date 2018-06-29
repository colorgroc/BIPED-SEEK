using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Tutorial_InGame : MonoBehaviour {
    [SerializeField]
    private GameObject welcome, score, abilities, feedback, icon, rounds_time, rounds_time2, objectiveTuto, objectiveCanvas, welcome1, welcome2, thereUR, objetive, player1, player2, player3, welcome3, HUD, box, goKill, goKill2, goKill3, guards, guards_3, score2, score3, score4, winner, finnish, getClose, pausa, back;//, npcReduction_halfNonObjective;
    public Text titol;
    bool once, _once, _Once, __Once, __once, _ONce, ope, timed3, timed4, timed5;
    public static bool tutorialPaused;
    private float time, time2, time3, time4, time5;
    private int OK;
    private float timeGame = 120, timeLeft;
    [SerializeField]
    private Text textTiempo;
    public static bool showIt;
    public Material glowP1, glowP2, glowP3, outlineP3, outlineP1, outlineP2;
    private Material[] p1mat, p2mat, p3mat;
    [SerializeField]
    private int numOfAbilities = 6, numOfUsedAbilities = 2;
    private int ability1, ability2;

    [SerializeField]
    private Sprite freeze, control, invisible, teleport, sprint, smoke;
    public FMOD.Studio.EventInstance backgroudSound;
    public FMOD.Studio.EventInstance backgroudMusic;

    // Use this for initialization
    private void Awake()
    {
        showIt = true;
        Time.timeScale = 0;
        titol = GameObject.Find("Titol").GetComponent<Text>();
        time = time2 = time3 = time4 = time5 = OK = 0;
        once = _once = _Once =__Once = __once = _ONce = ope = timed3 =timed4 = timed5 = tutorialPaused = false;
        box.SetActive(true);
        welcome2.SetActive(false);
        thereUR.SetActive(false);
        HUD.SetActive(false);
        score.SetActive(false);
        feedback.SetActive(false);
        abilities.SetActive(false);
        icon.SetActive(false);
        rounds_time.SetActive(false);
        rounds_time2.SetActive(false);
        objectiveTuto.SetActive(false);
        finnish.SetActive(false);
        score2.SetActive(false);
        score3.SetActive(false);
        score4.SetActive(false);
        goKill.SetActive(false);
        goKill2.SetActive(false);
        goKill3.SetActive(false);
        winner.SetActive(false);
        getClose.SetActive(false);
        back.SetActive(false);
        welcome3.SetActive(false);
        p1mat = player1.GetComponentInChildren<Renderer>().materials;
        p2mat = player2.GetComponentInChildren<Renderer>().materials;
        p3mat = player3.GetComponentInChildren<Renderer>().materials;
        PlayerPrefs.SetInt("Ability 1", (int)NewControl.Abilities.SMOKE);
        PlayerPrefs.SetInt("Ability 2", (int)NewControl.Abilities.IMMOBILIZER);

        backgroudMusic = RuntimeManager.CreateInstance("event:/BipedSeek/Music/Mapa 1");
        backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Ambient/Wind");
        backgroudSound.setParameterValue("Vent Loop", 0.2f);
    }
    void Start () {
        showIt = true;
        Abilities_Tutorial.show = false;
        timeLeft = timeGame;
        textTiempo.text = GetMinutes(timeLeft);
        objectiveCanvas.GetComponent<ObjectiveCanvas>().Start();
        backgroudMusic.start();
        backgroudSound.start();
    }

    // Update is called once per frame
    void Update () {

        if (!finnish.activeInHierarchy)
            Pausa();
        if (timeLeft <= 0) timeLeft = 0;
        timeLeft -= Time.deltaTime;
        textTiempo.text = GetMinutes(timeLeft);

        if (OK == 3 && !once) 
            time += Time.deltaTime;
        if (OK == 12 && !__once)
            time2 += Time.deltaTime;
        if (OK == 17 && timed3)
            time3 += Time.deltaTime;
        if (OK == 15 && timed4)
            time4 += Time.deltaTime; 
        if (OK == 10 && timed5)
            time5 += Time.deltaTime;

        if (Input.GetButtonDown("Submit") && Time.timeScale == 0 && !tutorialPaused)
        {
            if (OK != 19)
            {
                OK++;
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
            }
        }
        if (Input.GetButtonDown("Cancel") && Time.timeScale == 0 && !tutorialPaused)
        {
            if (OK > 0)
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
            OK--;
            
        }
        if (Input.GetButtonDown("Main Menu") && Time.timeScale == 0 && !tutorialPaused)
        {
            if (OK == 19)
            {
                backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                SceneManager.LoadScene("TutorialMenu");
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            }
        }
        if (OK < 0) OK = 0;
        if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")) && Time.timeScale == 0 && !tutorialPaused)
        {
            if (OK == 0)
            {
                welcome1.SetActive(true);
                titol.text = "Tutorial";
                welcome2.SetActive(false);
                back.SetActive(false);
            }
            if (OK == 1)
            {
                welcome1.SetActive(false);
                titol.text = "Movement";
                welcome2.SetActive(true);
                welcome3.SetActive(false);
                back.SetActive(true);
            }
            else if (OK == 2)
            {
                welcome3.SetActive(true);
                titol.text = "Movement";
                welcome2.SetActive(false);
                thereUR.SetActive(false);
            }
            else if (OK == 3)
            {
                welcome3.SetActive(false);
                if (!once)
                {
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                titol.text = "There you are";
                HUD.SetActive(false);
                score.SetActive(false);
                thereUR.SetActive(true);
                welcome.SetActive(true);

            }
            else if (OK == 4)
            {
                thereUR.SetActive(false);
                welcome.SetActive(false);
                HUD.SetActive(true);
                titol.text = "HUD - Score";
                score.SetActive(true);
                abilities.SetActive(false);
            }
            else if (OK == 5)
            {
                titol.text = "HUD - Abilities";
                score.SetActive(false);
                abilities.SetActive(true);
                rounds_time.SetActive(false);
            }
            else if (OK == 6)
            {
                titol.text = "HUD - General";
                rounds_time.SetActive(true);
                abilities.SetActive(false);
                rounds_time2.SetActive(false);
            }
            else if (OK == 7)
            {
                titol.text = "HUD - General";
                rounds_time.SetActive(false);
                rounds_time2.SetActive(false);
                objectiveTuto.SetActive(true);
            }
            else if (OK == 8)
            {
                titol.text = "HUD - General";
                objectiveTuto.SetActive(false);
                rounds_time2.SetActive(true);
                goKill.SetActive(false);
            }
            else if (OK == 9)
            {
                box.SetActive(true);
                titol.text = "Let's Kill";
                goKill.SetActive(true);
                rounds_time2.SetActive(false);
                score2.SetActive(false);
            }
            else if (OK == 10)
            {
                goKill.SetActive(false);
                
                if (!_once)
                {
                    guards.SetActive(true);
                    guards_3.SetActive(true);
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                score2.SetActive(true);
                icon.SetActive(false);
                titol.text = "Loosing Score";
            }
            else if (OK == 11)
            {
                box.SetActive(true);
                titol.text = "Respawn";
                icon.SetActive(true);
                score2.SetActive(false);
                getClose.SetActive(false);
            }
            else if (OK == 12)
            {
                icon.SetActive(false);
                if (!__once)
                {
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                feedback.SetActive(false);
                getClose.SetActive(true);
                titol.text = "Chase a player";
            }
            else if (OK == 13)
            {
                getClose.SetActive(false);
                if (!_Once)
                {
                    player3.SetActive(true);
                    p3mat[1] = glowP3;
                    p3mat[2] = outlineP3;
                    player3.GetComponentInChildren<Renderer>().materials = p3mat;
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                titol.text = "Player Detected";
                feedback.SetActive(true);
                goKill2.SetActive(false);
            }
            
            else if (OK == 14)
            {
                
                feedback.SetActive(false);
                titol.text = "Let's kill a player";
                goKill2.SetActive(true);
                goKill3.SetActive(false);
                score3.SetActive(false);
                box.SetActive(true);
            }
            else if (OK == 15)
            {
                if (!__Once)
                {
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                titol.text = "Player Killed";
                goKill2.SetActive(false);
                goKill3.SetActive(false);
                score3.SetActive(true);
            }
            else if (OK == 16)
            {
                if (!ope)
                {
                    player2.SetActive(true);
                    p2mat[1] = glowP2;
                    p2mat[2] = outlineP2;
                    player2.GetComponentInChildren<Renderer>().materials = p2mat;
                    ope = true;
                }
                titol.text = "kill the objective";
                goKill3.SetActive(true);
                score4.SetActive(false);
                score3.SetActive(false);
                box.SetActive(true);
            }
            else if (OK == 17)
            {
                winner.SetActive(false);
                if (!_ONce)
                {
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                titol.text = "Objective Killed";
                goKill3.SetActive(false);
                score4.SetActive(true);
            }
            else if (OK == 18)
            {
                titol.text = "Winner";
                score4.SetActive(false);
                winner.SetActive(true);
                finnish.SetActive(false);

            }
            else if(OK == 19)
            {
                winner.SetActive(false);
                finnish.SetActive(true);
            }
        }
        if(OK == 3 && time > 3 && ((Input.GetAxis(player1.GetComponent<PlayerControl>().AxisMovement) * Time.deltaTime) != 0 || (Input.GetAxis(player1.GetComponent<PlayerControl>().AxisRotation) * Time.deltaTime) != 0) && !once)
        {
            p1mat[1] = glowP1;
            p1mat[2] = outlineP1;
            player1.GetComponentInChildren<Renderer>().materials = p1mat;
            Debug.Log("nye");

        }
        if (OK == 3 && time > 6 && !once) 
        {
            Time.timeScale = 0;
            titol.text = "There you are";
            box.SetActive(true);
            thereUR.SetActive(true);
            once = true;
        }
        if(OK == 10 && Time.timeScale == 1 && !_once)
        {
            if(player1.GetComponent<PlayerControl>().cooledDown && !timed5)
                timed5 = true;
            if(time5 > 3 && timed5){
                _once = true;
                timed5 = false;
                Time.timeScale = 0;
                box.SetActive(true);
                titol.text = "Loosing Score";
            }
        }
        if (OK == 13 && Time.timeScale == 1 && !_Once)
        {
            if (player1.GetComponent<PlayerControl>().detected)
            {
                _Once = true;
                Time.timeScale = 0;
                box.SetActive(true);
                titol.text = "Player Detected";
                feedback.SetActive(true);
            }
        }
        if (OK == 12 && Time.timeScale == 1 && !__once)
        {
            if (time2 >= 10)
            {
                __once = true;
                Time.timeScale = 0;
                box.SetActive(true);
                titol.text = "Chase a player";
                getClose.SetActive(true);
            }
        }
        if (OK == 15 && Time.timeScale == 1 && !__Once)
        {
            if (player3.GetComponent<PlayerControl>().cooledDown && !timed4)
                timed4 = true;
            if(timed4 && time4 > 3){
                __Once = true;
                timed4 = false;
                Time.timeScale = 0;
                box.SetActive(true);
                titol.text = "Player Killed";
                score3.SetActive(true);
            }
        }
        if (OK == 17 && Time.timeScale == 1 && !_ONce)
        {
            if (player2.GetComponent<PlayerControl>().cooledDown && !timed3)
                timed3 = true;
            if (timed3 && time3 > 3)
            {
                _ONce = true;
                timed3 = false;
                Time.timeScale = 0;
                box.SetActive(true);
                player1.GetComponent<PlayerControl>().scoreGeneralRound += 30;
                player1.GetComponent<PlayerControl>().scoreGeneral += 30;
                titol.text = "Objective killed";
                score4.SetActive(true);
            }
        }
    }

    private string GetMinutes(float timeLeft)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
        return string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }

    private void Pausa()
    {
        if (Input.GetButtonDown("Start") || (tutorialPaused && Input.GetButtonDown("Cancel")))
        {
            if (!tutorialPaused)
            {
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
            }
                
            else  {
                RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Back", Vector3.zero);
            }    
                
            tutorialPaused = !tutorialPaused;
            backgroudMusic.setPaused(tutorialPaused);
            backgroudSound.setPaused(tutorialPaused);
            pausa.SetActive(tutorialPaused);
        }
        if (Input.GetButtonDown("Main Menu") && tutorialPaused)
        {
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            pausa.SetActive(false);
            Time.timeScale = 1;
            backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            tutorialPaused = false;
            SceneManager.LoadScene("TutorialMenu");
        }
        if (tutorialPaused)
            Time.timeScale = 0;
        else if (!tutorialPaused && !box.activeInHierarchy) Time.timeScale = 1;

    }
}
