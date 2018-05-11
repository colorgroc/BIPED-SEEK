using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public static bool showIt;
    [SerializeField]
    private Canvas panel, tutorial, mapaEvent, feedbackCanvas;
    [SerializeField]
    private GameObject tutorialGb, welcome, HUD, score, abilities, feedback, icon, rounds, objective, events;
    [SerializeField]
    private Text welcome1, welcome2, play;
    private int OK;
    [SerializeField]
    Image feedBack;
    [SerializeField]
    Color feedCol;

    // Use this for initialization
    void Start () {
        showIt = true;
        if (showIt)
        {
            Debug.Log("siTuto");
            NewControl.paused = true;
            tutorialGb.SetActive(true);
            panel.enabled = tutorial.enabled = true;
            welcome.SetActive(true);
            welcome1.enabled = true;
            
        }
        else {
            Debug.Log("noTuto");
            //Time.timeScale = 1;
            tutorialGb.SetActive(false);
            panel.enabled = tutorial.enabled = false;
        }

        HUD.SetActive(false);
        score.SetActive(false);
        abilities.SetActive(false);
        feedback.SetActive(false);
        icon.SetActive(false);
        rounds.SetActive(false);
        objective.SetActive(false);
        events.SetActive(false);
        mapaEvent.enabled = false;
        feedBack.enabled = false;
        welcome2.enabled = play.enabled = false;
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
                score.SetActive(true);
            }
            else if (OK == 3)
            {
                score.SetActive(false);
                abilities.SetActive(true);
            }
            else if (OK == 4)
            {
                abilities.SetActive(false);
                feedback.SetActive(true);
                feedBack.enabled = true;
                feedBack.color = feedCol;
            }
            else if (OK == 5)
            {
                feedback.SetActive(false);
                icon.SetActive(true);
                feedbackCanvas.enabled = true;
                //feedBack.enabled = false;
                
            }
            else if (OK == 6)
            {
                icon.SetActive(false);
                rounds.SetActive(true);
            }
            else if (OK == 7)
            {
                rounds.SetActive(false);
                objective.SetActive(true);
            }
            else if (OK == 8)
            {
                objective.SetActive(false);
                events.SetActive(true);
                mapaEvent.enabled = true;
            }
            else if (OK == 9)
            {
                events.SetActive(false);
                mapaEvent.enabled = false;
                HUD.SetActive(false);
                play.enabled = true;
            }
            else if (OK == 10)
            {
                play.enabled = false;
                tutorial.enabled = false;
                panel.enabled = false;
                tutorialGb.SetActive(false);
                feedBack.enabled = true;
                NewControl.paused = false;
                showIt = false;
            }
        }
    }
}
