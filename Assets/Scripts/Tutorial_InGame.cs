using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Tutorial_InGame : MonoBehaviour {
    [SerializeField]
    private GameObject welcome, score, abilities, feedback, icon, rounds_time, objectiveTuto, objectiveCanvas, welcome1, welcome2, thereUR, objetive, player1, player2, killer, HUD, box, goKill, goKill2, goKill3, guards, score2, score3, winner, finnish, events, mapEvent, getClose, eventText, killerEvent, killerEvent_score, npcReduction_half, speedy, npcReduction_convertToObjective;//, npcReduction_halfNonObjective;
    public Text titol;
    bool once, _once, _Once, __Once, __once, _oNce;
    //public GameObject player1, player2, HUD, box, objectiveCanvas;
    private float time, time2, time3, time4, time5;
    //private bool proceed;
    private int OK;
    private float timeGame = 120, timeLeft;
    [SerializeField]
    private Text textTiempo;
    public static bool showIt;
    public Material glowP1, glowP2, outlineP1, outlineP2;
    private Material[] p1mat, p2mat;
    [SerializeField]
    private int numOfAbilities = 6, numOfUsedAbilities = 2;
    private int ability1, ability2;
    //[SerializeField]
    //private Image iAb1, iAb2;
    [SerializeField]
    private Sprite freeze, control, invisible, teleport, sprint, smoke;
    // Use this for initialization
    private void Awake()
    {
        showIt = true;
        Time.timeScale = 0;
        titol = GameObject.Find("Titol").GetComponent<Text>();
        time = time2 = time3 = time4 = time5 = OK = 0;
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
        killer.SetActive(false);
        eventText.SetActive(true);
        killerEvent.SetActive(false);
        killerEvent_score.SetActive(false);
        npcReduction_convertToObjective.SetActive(false);
        npcReduction_half.SetActive(false);
        speedy.SetActive(false);
        goKill3.SetActive(false);
        p1mat = player1.GetComponentInChildren<Renderer>().materials;
        p2mat = player2.GetComponentInChildren<Renderer>().materials;
        //RandomAbilities();
        PlayerPrefs.SetInt("Ability 1", (int)NewControl.Abilities.SMOKE);
        PlayerPrefs.SetInt("Ability 2", (int)NewControl.Abilities.IMMOBILIZER);
        //feedBackIm.color = normalCol;
        //objective = player2;
    }
    void Start () {
        timeLeft = timeGame;//UnityEngine.Random.Range(minMinutes*60, maxMinutes * 60);
        textTiempo.text = GetMinutes(timeLeft);
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
        if(OK == 20 && !__Once)
            time4 += Time.deltaTime; 
        if (OK == 15 && !_oNce)
            time3 += Time.deltaTime;
        if (OK == 8 && !_once)
            time5 += Time.deltaTime;

        if (Input.GetButtonDown("Submit") && Time.timeScale == 0)
        {
            if (OK != 22)
                OK++;
        }
        if (Input.GetButtonDown("Cancel") && Time.timeScale == 0)
        {
                OK--;  
        }
        if (Input.GetButtonDown("Main Menu") && Time.timeScale == 0)
        {
            if (OK == 22)
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
                    p2mat[1] = glowP2;
                    p2mat[2] = outlineP2;
                    player2.GetComponentInChildren<Renderer>().materials = p2mat;
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
                eventText.SetActive(true);
                mapEvent.SetActive(true);
                killerEvent.SetActive(false);
                // feedBackIm.color = normalCol;
            }
            else if (OK == 13)
            {
                titol.text = "Killers";
                killerEvent.SetActive(true);                   
                eventText.SetActive(false);
            }
            else if (OK == 14)
            {
                killerEvent.SetActive(false);
                titol.text = "Killers";
                goKill3.SetActive(true);
                box.SetActive(true);
            }
            else if (OK == 15)
            {
                goKill3.SetActive(false);
                events.SetActive(false);
                mapEvent.SetActive(false);
                //titol.text = "HUD - General";
                if (!_oNce)
                {
                    box.SetActive(false);
                    killer.SetActive(true);
                    Time.timeScale = 1;
                }
                titol.text = "Killers";
                killerEvent_score.SetActive(true);
                //feedBackIm.color = feedCol; 
            }
            else if (OK == 16)
            {
                if(killer != null && GameObject.Find("Killer") == null)
                    killer.SetActive(false);
                events.SetActive(true);
                mapEvent.SetActive(true);
                killerEvent_score.SetActive(false);
                titol.text = "Crazynest";
                speedy.SetActive(true);
                box.SetActive(true);
            }
            else if (OK == 17)
            {
                speedy.SetActive(false);
                titol.text = "NPC Reduction";
                npcReduction_half.SetActive(true);
            }
            //else if (OK == 18)
            //{
            //    npcReduction_half.SetActive(false);
            //    titol.text = "NPC Reduction";
            //    npcReduction_halfNonObjective.SetActive(true);
            //}
            else if (OK == 18)
            {
                npcReduction_half.SetActive(false);
                titol.text = "NPC Reduction";
                npcReduction_convertToObjective.SetActive(true);
                events.SetActive(true);
                mapEvent.SetActive(true);
            }
            else if (OK == 19)
            {
                events.SetActive(false);
                mapEvent.SetActive(false);
                npcReduction_convertToObjective.SetActive(false);
                titol.text = "Let's kill again";
                goKill2.SetActive(true);
                score3.SetActive(false);
                box.SetActive(true);
                // feedBackIm.color = normalCol;
            }
            else if (OK == 20)
            {
                winner.SetActive(false);
                events.SetActive(false);
                mapEvent.SetActive(false);
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
            else if (OK == 21)
            {
                titol.text = "Winner";
                score3.SetActive(false);
                winner.SetActive(true);
                events.SetActive(false);
                // feedBackIm.color = normalCol;
            }
            else if(OK == 22)
            {
                finnish.SetActive(true);
                events.SetActive(false);
            }
        }
        if(OK == 2 && time > 3 && (Input.GetAxis(player1.GetComponent<PlayerControl>().AxisMovement) * Time.deltaTime) != 0 && !once)
        {
            p1mat[1] = glowP1;
            p1mat[2] = outlineP1;
            player1.GetComponentInChildren<Renderer>().materials = p1mat;

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
            if(player1.GetComponent<PlayerControl>().cooledDown && time5 > 3)
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
        if (OK == 15 && Time.timeScale == 1 && !_oNce)
        {
            if (killer == null && GameObject.Find("Killer") == null || (player1.GetComponent<PlayerControl>().cooledDown && time3 > 3))
            {
                _oNce = true;
                Time.timeScale = 0;
                box.SetActive(true);
                titol.text = "Killers";
                killerEvent_score.SetActive(true);
                events.SetActive(true);
            }
        }
        if (OK == 20 && Time.timeScale == 1 && !__Once)
        {
            if (player2.GetComponent<PlayerControl>().cooledDown && time4 > 3)
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
