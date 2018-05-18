using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_InGame : MonoBehaviour {

    private GameObject welcome, HUD, scroe, abilities, feedbackicon, rounds_time, objective, events, tutorial, welcome1, welcome2;
    private Text titol;
    public GameObject player1, player2;
    private float time;
    private bool proceed;
    private int OK;
    public Material glowP1, glowP2, outlineP1, outlineP2;
    private Material[] matP1, matP2;
    // Use this for initialization
    private void Awake()
    {
        //Tutorial.showIt = true;
        //Time.timeScale = 0;
        //welcome = GameObject.Find("Welcome");
        //tutorial = GameObject.Find("Tutorial");
        //HUD = GameObject.Find("HUD");
        //welcome1 = GameObject.Find("welcome1");
        //welcome2 = GameObject.Find("welcome2");
        //titol = GameObject.Find("Titol").GetComponent<Text>();
        //time = OK = 0;
        //matP1 = player1.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        //matP2 = player2.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        //matP1[0] = matP1[1] = null;

        //welcome2.SetActive(false);
        //HUD.SetActive(false);
    }
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        

        //if (Input.GetButtonDown("Submit") && Time.timeScale == 0)
        //{
        //    OK++;
        //}

        //if (Input.GetButtonDown("Submit") && Time.timeScale == 0)
        //{
        //    if(OK == 1)
        //    {
        //        welcome1.SetActive(false);
        //        welcome2.SetActive(true);
        //    }
        //    else if (OK == 2)
        //    {
        //        welcome2.SetActive(false);
        //        Time.timeScale = 1;
        //        time += Time.deltaTime;
        //    }
        //}

        //if (OK == 2 && time > 3)// && (Input.GetAxis(player1.GetComponent<PlayerControl>().AxisMovement)*Time.deltaTime) != 0 && time > 3 ) 
        //{
        //    Time.timeScale = 0;
        //    matP1[0] = outlineP1;
        //    matP1[1] = glowP1;
        //}
	}
}
