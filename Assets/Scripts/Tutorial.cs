using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public static bool showIt;
    [SerializeField]
    private Canvas panel, tutorial;
    [SerializeField]
    private GameObject tutorialGb, welcome, HUD;
    [SerializeField]
    private Text welcome1, welcome2, score, abilities, feedback, icon, rounds, objective, events, play;
    private int OK;
    // Use this for initialization
    void Start () {
        if (showIt)
        {
            Time.timeScale = 0;
            tutorialGb.SetActive(true);
            panel.enabled = tutorial.enabled = true;
            welcome.SetActive(true);
            welcome1.enabled = true;
        }
        else {
            //Time.timeScale = 1;
            tutorialGb.SetActive(false);
            panel.enabled = tutorial.enabled = false;
        }

        HUD.SetActive(false);
        welcome2.enabled = score.enabled = abilities.enabled = feedback.enabled = icon.enabled = rounds.enabled = objective.enabled = events.enabled = false;
        OK = 0;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            OK++;
        }
        if (Input.GetButtonUp("Submit"))
        {
            if(OK == 1)
            {
                welcome1.enabled = false;
                welcome2.enabled = true;
            }
            else if (OK == 2){
                welcome2.enabled = false;
                welcome.SetActive(false);
                HUD.SetActive(true);
                score.enabled = true;
            }
            else if (OK == 3)
            {     
                score.enabled = false;
                abilities.enabled = true;
            }
            else if (OK == 4)
            {
                abilities.enabled = false;
                feedback.enabled = true;
            }
            else if (OK == 5)
            {
                feedback.enabled = false;
                icon.enabled = true;
            }
            else if (OK == 6)
            {
                icon.enabled = false;
                rounds.enabled = true;
            }
            else if (OK == 7)
            {
                rounds.enabled = false;
                objective.enabled = true;
            }
            else if (OK == 8)
            {
                objective.enabled = false;
                events.enabled = true;
            }
            else if (OK == 9)
            {
                events.enabled = false;
                HUD.SetActive(false);
                play.enabled = true;
            }
            else if (OK == 10)
            {
                play.enabled = false;
                tutorial.enabled = false;
                panel.enabled = false;
                tutorialGb.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
