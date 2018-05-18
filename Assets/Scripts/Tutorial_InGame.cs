using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Tutorial_InGame : MonoBehaviour {
    [SerializeField]
    private GameObject welcome, score, abilities, feedback, icon, rounds_time, objectiveTuto, welcome1, welcome2, thereUR, objetive, player1, player2, HUD, box, objectiveCanvas, goKill, goKill2, guards, score2, score3, winner, finnish, events, mapEvent, getClose;
    public Text titol;
    bool once, _once, _Once, __Once, __once;
    //public GameObject player1, player2, HUD, box, objectiveCanvas;
    private float time, time2;
    //private bool proceed;
    private int OK;
    public Behaviour haloP1, haloP2;
    private float timeGame = 60, timeLeft;
    [SerializeField]
    private Text textTiempo;
    public static bool showIt;
    //[SerializeField]
    //Color feedCol, normalCol;
    //public Material glowP1, glowP2, outlineP1, outlineP2;
    // Use this for initialization
    private void Awake()
    {
        showIt = true;
        Time.timeScale = 0;
        titol = GameObject.Find("Titol").GetComponent<Text>();
        time = time2 = OK = 0;
        haloP1.enabled = false;
        box.SetActive(true);
        welcome2.SetActive(false);
        thereUR.SetActive(false);
        HUD.SetActive(false);
        score.SetActive(false);
        feedback.SetActive(false);
        abilities.SetActive(false);
        icon.SetActive(false);
        rounds_time.SetActive(false);
        objectiveTuto.SetActive(false);
        finnish.SetActive(false);
        score2.SetActive(false);
        score3.SetActive(false);
        goKill.SetActive(false);
        goKill2.SetActive(false);
        winner.SetActive(false);
        events.SetActive(false);
        mapEvent.SetActive(false);
        getClose.SetActive(false);
        //feedBackIm.color = normalCol;
        //objective = player2;
    }
    void Start () {
        timeLeft = timeGame;//UnityEngine.Random.Range(minMinutes*60, maxMinutes * 60);
        textTiempo.text = GetMinutes(timeLeft);
    }
    void ShowObjectiveCanvas()
    {
        //objectiveCanvas.SetActive(true);
        objectiveCanvas.GetComponent<ObjectiveCanvas>().Start();
    }
    // Update is called once per frame
    void Update () {

        timeLeft -= Time.deltaTime;
        textTiempo.text = GetMinutes(timeLeft);

        if (OK == 2 && !once) 
            time += Time.deltaTime;
        if (OK == 10 && !__once)
            time2 += Time.deltaTime;

        if (Input.GetButtonDown("Submit") && Time.timeScale == 0)
        {
            if (OK != 16)
                OK++;
        }
        if (Input.GetButtonDown("Cancel") && Time.timeScale == 0)
        {
                OK--;  
        }
        if (Input.GetButtonDown("Main Menu") && Time.timeScale == 0)
        {
            if (OK == 16)
                SceneManager.LoadScene("Menu");
        }
        if (OK < 0) OK = 0;
        if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")) && Time.timeScale == 0)
        {
            if (OK == 0)
            {
                welcome1.SetActive(true);
                titol.text = "Tutorial";
                welcome2.SetActive(false);
            }
            if (OK == 1)
            {
                welcome1.SetActive(false);
                titol.text = "Movement";
                welcome2.SetActive(true);
                thereUR.SetActive(false);
            }
            else if (OK == 2)
            {
                welcome2.SetActive(false);       
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
            else if (OK == 3)
            {
                thereUR.SetActive(false);
                welcome.SetActive(false);
                HUD.SetActive(true);
                titol.text = "HUD - Score";
                score.SetActive(true);
                abilities.SetActive(false);
            }
            else if (OK == 4)
            {
                titol.text = "HUD - Abilities";
                score.SetActive(false);
                abilities.SetActive(true);
            }
            else if (OK == 5)
            {
                titol.text = "HUD - General";
                rounds_time.SetActive(true);
                abilities.SetActive(false);
            }
            else if (OK == 6)
            {
                titol.text = "HUD - General";
                objectiveTuto.SetActive(true);
                rounds_time.SetActive(false);
            }
            else if (OK == 7)
            {
                box.SetActive(true);
                titol.text = "Let's Kill";
                goKill.SetActive(true);
                objectiveTuto.SetActive(false);
                score2.SetActive(false);
            }
            else if (OK == 8)
            {
                goKill.SetActive(false);
                //titol.text = "HUD - General";
                
                if (!_once)
                {
                    guards.SetActive(true);
                    //player2.SetActive(true);
                    //haloP2.enabled = true;
                    objectiveTuto.SetActive(false);
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                score2.SetActive(true);
                icon.SetActive(false);
                titol.text = "Loosing Score";
            }
            else if (OK == 9)
            {
                box.SetActive(true);
                titol.text = "Respawn";
                icon.SetActive(true);
                score2.SetActive(false);
                getClose.SetActive(false);
            }
            else if (OK == 10)
            {
                icon.SetActive(false);
                if (!__once)
                {
                    box.SetActive(false);
                    titol.text = "Get close to a player";
                    getClose.SetActive(true);
                    Time.timeScale = 1;
                }
                feedback.SetActive(false);
            }
            else if (OK == 11)
            {
                getClose.SetActive(false);
                //titol.text = "HUD - General";
                if (!_Once)
                {
                    //guards.SetActive(true);
                    player2.SetActive(true);
                    haloP2.enabled = true;
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                titol.text = "Player Detected";
                feedback.SetActive(true);
                events.SetActive(false);
                //feedBackIm.color = feedCol; 
            }
            else if (OK == 12)
            {
                box.SetActive(true);
                titol.text = "Events";
                feedback.SetActive(false);
                events.SetActive(true);
                mapEvent.SetActive(true);
                goKill2.SetActive(false);
                // feedBackIm.color = normalCol;
            }
            else if (OK == 13)
            {
                events.SetActive(false);
                mapEvent.SetActive(false);
                titol.text = "Let's kill again";
                goKill2.SetActive(true);
                score3.SetActive(false);
                // feedBackIm.color = normalCol;
            }
            else if (OK == 14)
            {
                winner.SetActive(false);
                events.SetActive(false);
                //titol.text = "HUD - General";
                if (!__Once)
                {
                    box.SetActive(false);
                    Time.timeScale = 1;
                }
                titol.text = "Winning score";
                goKill2.SetActive(false);
                score3.SetActive(true);
                //feedBackIm.color = feedCol; 
            }
            else if (OK == 15)
            {
                titol.text = "Winner";
                score3.SetActive(false);
                winner.SetActive(true);
                events.SetActive(false);
                // feedBackIm.color = normalCol;
            }
            else if(OK == 16)
            {
                finnish.SetActive(true);
                events.SetActive(false);
            }
        }
        if(OK == 2 && time > 3 && (Input.GetAxis(player1.GetComponent<PlayerControl>().AxisMovement) * Time.deltaTime) != 0 && !once)
        {
            haloP1.enabled = true;
            
        }
        if (OK == 2 && time > 6 && !once) 
        {
            Time.timeScale = 0;
            titol.text = "There you are";
            box.SetActive(true);
            thereUR.SetActive(true);
            events.SetActive(false);
            once = true;
            //matP1[0].enableInstancing = true;
            //matP1[1].enableInstancing = true;
        }
        if(OK == 8 && Time.timeScale == 1 && !_once)
        {
            if(player1.GetComponent<PlayerControl>().cooledDown)
            {
                _once = true;
                Time.timeScale = 0;
                box.SetActive(true);
                score2.SetActive(true);
                events.SetActive(false);
                titol.text = "Loosing Score";
            }
        }
        if (OK == 11 && Time.timeScale == 1 && !_Once)
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
        if (OK == 10 && Time.timeScale == 1 && !__once)
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
        if (OK == 14 && Time.timeScale == 1 && !__Once)
        {
            if (player2.GetComponent<PlayerControl>().cooledDown)
            {
                __Once = true;
                Time.timeScale = 0;
                box.SetActive(true);
                titol.text = "Winning score";
                score3.SetActive(true);
            }
        }
    }

    private string GetMinutes(float timeLeft)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
        return string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
