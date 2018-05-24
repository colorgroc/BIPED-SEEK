﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using FMODUnity;

public class PlayerControl : MonoBehaviour {

    [SerializeField]
    private float speed = 20, speedRotation = 140;
    [SerializeField]
    public static float defaultSpeed = 20;
    [HideInInspector]
    public string AxisMovement, AxisRotation, killButton, hab1Button, hab2Button, hab3Button, hab4Button, hab5Button, hab6Button;

    private float distToGround, count, timeCoolDown, timeFeedback;
    [SerializeField]
    private int coolDown;
    [SerializeField]
    private AudioClip killPlayerSound, killNPCSound, killObjectiveSound, punchSound;
    private AudioSource soundSource;
    public bool cooledDown, usingAbility;//, goodFeedback, winnerFeedback;
    //public bool badFeedback;

   // [HideInInspector]
    public int scoreGeneral, scoreKills, scoreWins, scoreGeneralRound, scoreKillsRound, scoreWinsRound;
    public bool wannaKill, onFieldView, detected, _sprint, canAct;

    //private Image feedback;
    //private List<GameObject> feedbacks;
    //private GameObject[] feedbackList;
    [SerializeField]
    private Color colorP1, colorP2, colorP3, colorP4, DetectedFeedback;
    private Color neutralColor;
    [SerializeField]
    private Animator anim;
    //private bool canAct;
    private GameObject[] guards;
    List<GameObject> guardsList = new List<GameObject>();
    NavMeshAgent _navMeshAgent;
    //public float shakeOffset, shakeDuration = 0.5f, shakeAmount = 0.5f;
    

    void Start ()
    {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.anim = this.gameObject.GetComponent<Animator>();
        //this.feedbackList = GameObject.FindGameObjectsWithTag("Feedback");
        //this.feedbacks = new List<GameObject>();
        PlayerPrefs.SetFloat("Speed", defaultSpeed);
        this.canAct = true;

        if (Tutorial_InGame.showIt)
            guards = GameObject.FindGameObjectsWithTag("Guard");

        //for (int i = 0; i < feedbackList.Length; i++)
        //{
        //    this.feedbacks.Add(feedbackList[i].gameObject);
        //}

        //this.feedbacks.Sort(SortByName);

        scoreGeneral = scoreKills = scoreWins = scoreGeneralRound = scoreKillsRound = scoreWinsRound = 0;

        if (this.gameObject.name.Equals("Player 1"))
        {
            //this.feedback = this.feedbacks[0].GetComponent<Image>();
            //this.neutralColor = colorP1;
            //this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_1";
            this.AxisRotation = "H_RPad_1";
            this.killButton = "X_1";
            this.hab1Button = "LB_1";
            this.hab2Button = "RB_1";
           
        }
        else if (this.gameObject.name.Equals("Player 2"))
        {
            //this.feedback = this.feedbacks[1].GetComponent<Image>();
            //this.neutralColor = colorP2;
            //this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_2";
            this.AxisRotation = "H_RPad_2";
            this.killButton = "X_2";
            this.hab1Button = "LB_2";
            this.hab2Button = "RB_2";
       

        }
        else if (this.gameObject.name.Equals("Player 3"))
        {
            //this.feedback = this.feedbacks[2].GetComponent<Image>();
            //this.neutralColor = colorP3;
            //this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_3";
            this.AxisRotation = "H_RPad_3";
            this.killButton = "X_3";
            this.hab1Button = "LB_3";
            this.hab2Button = "RB_3";

        }
        else if (this.gameObject.name.Equals("Player 4"))
        {
            //this.feedback = this.feedbacks[3].GetComponent<Image>();
            //this.neutralColor = colorP4;
            //this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_4";
            this.AxisRotation = "H_RPad_4";
            this.killButton = "X_4";
            this.hab1Button = "LB_4";
            this.hab2Button = "RB_4";
        }
        // this.canAct = true;

        if (!Tutorial_InGame.showIt)
        {
            foreach (GameObject guard in NewControl.guards)
            {
                if (guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
                {
                    guardsList.Add(guard);
                }
            }
        }
        else
        {
            foreach (GameObject guard in guards)
            {
                if (guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
                {
                    guardsList.Add(guard);
                }
            }
        }
    }

   
    void Update()
    {
        
        if (!_sprint)
        {
            speed = PlayerPrefs.GetFloat("Speed");
        }
        if (this.canAct && !this.usingAbility)
        {
            float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
            float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;

            transform.Translate(0, 0, y * speed);
            transform.Rotate(0, rX * speedRotation, 0);

            _navMeshAgent.SetDestination(transform.position);

            if (Input.GetButtonDown(this.killButton))
            {
                this.wannaKill = true;
                RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
                //soundSource.PlayOneShot(punchSound);
            }
            if (Input.GetButtonUp(this.killButton)) this.wannaKill = false;

            if (y > 0) this.anim.SetBool("isWalkingForward", true);
            else if (y < 0) this.anim.SetBool("isWalkingBack", true);
            else
            {
                this.anim.SetBool("isWalkingForward", false);
                this.anim.SetBool("isWalkingBack", false);
            }
            if(y != 0 && (_sprint || speed > defaultSpeed)) anim.SetBool("isRunning", true);
            else anim.SetBool("isRunning", false);
            if (y == 0 && rX > 0) anim.SetBool("Rot_Right", true);
            else if(y == 0 && rX < 0) anim.SetBool("Rot_Left", true);
            else
            {
                anim.SetBool("Rot_Right", false);
                anim.SetBool("Rot_Left", false);
            }
            //
        }else if(this.canAct && this.usingAbility)
        {
            if (Input.GetButtonDown(this.killButton))
            {
                this.wannaKill = true;
                //soundSource.PlayOneShot(punchSound);
				RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
            }
            if (Input.GetButtonUp(this.killButton)) this.wannaKill = false;
        }
            

        this.anim.SetBool("wannaKill", this.wannaKill);
        this.anim.SetBool("isFreezed", !this.canAct);
        //Debug.Log(this.canAct);
        if (this.detected)
        {    
            this.timeFeedback += Time.deltaTime;
            if (this.timeFeedback >= 1) this.detected = false;
        }
        else
        {
            this.timeFeedback = 0;
            //this.feedback.color = this.neutralColor;
        }

        if (this.cooledDown)
        {
            this.gameObject.transform.position = new Vector3(this.transform.position.x, 1000f, this.transform.position.z);
            this.timeCoolDown += Time.deltaTime;
            GameObject.Find("IconPlayer_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).GetComponent<Image>().fillAmount = this.timeCoolDown / this.coolDown;
            if (this.timeCoolDown >= this.coolDown)
            {
                GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
                for (int i = 0; i < guardsList.Count; i++)
                {
                    int r = Random.Range(0, guardsList.Count);
                    int rand = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
                    while(guardsList[r] == null) r = Random.Range(0, guardsList.Count);
                    if (guardsList[r] != null)
                        guardsList[r].gameObject.transform.position = new Vector3(allMyRespawnPoints[rand].transform.position.x, 10.14516f, allMyRespawnPoints[rand].transform.position.z);
                }
           
                Respawn(this.gameObject);
                this.cooledDown = false;
            }
        }
    }
    public void InicializandoHabilidad()
    {
        usingAbility = true;
    }
    public void FinalizandoHabilidad()
    {
        usingAbility = false;
    }
    public void Kill(GameObject gO)
    {
        //soundSource.PlayOneShot(killPlayerSound);
        if (gO.gameObject.tag.Equals("Guard") || gO.gameObject.tag.Equals("Killer Guards")) //canviar aixo?
        {
            this.detected = false;
            this.scoreGeneral -= 3;
            this.scoreGeneralRound -= 3;

            if (gO.gameObject.tag.Equals("Guard"))
            {
                gO.GetComponent<NPCConnectedPatrol>().Respawn(gO);
            }
            else if (gO.gameObject.tag.Equals("Killer Guards"))
            {
                Destroy(gO.gameObject);
            }
            //soundSource.PlayOneShot(killNPCSound);
			RuntimeManager.PlayOneShot("event:/BipedSeek/NPC/Death", gO.transform.position);
			RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Death", this.transform.position);
            Death.AnimDeath(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation);
            this.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();

        }
        else if (gO.gameObject.layer == 8 && gO != NewControl.objective)
        {
            //gO.GetComponent<Animator>().Play("Death");
            this.detected = false;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            this.scoreGeneral += 5;
            this.scoreKills += 1;
            this.scoreGeneralRound += 5;
            this.scoreKillsRound += 1;
            //soundSource.PlayOneShot(killPlayerSound);
			RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Death", gO.transform.position);
            gO.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();

        }
        else if (gO.gameObject.layer == 8 && gO == NewControl.objective)
        {
            //gO.GetComponent<Animator>().Play("Death");
            this.detected = false;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            if (!Tutorial_InGame.showIt)
            {
                NewControl.parcialWinner = this.gameObject;
                NewControl.objComplete = true;
            }
            Rondes.timesPlayed++;
            //soundSource.PlayOneShot(killObjectiveSound);

			RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Objective_Death", gO.transform.position);
        }
    
        
    }

    public void SetSpeed(float sprint)
    {
        this.speed *= sprint;
    }
    public float GetSpeed()
    {
        return this.speed;
    }

    public void Respawn(GameObject gO)
    {

            gO.GetComponent<PlayerControl>().detected = false;
            gO.GetComponent<PlayerControl>().timeFeedback = 0;
            GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
            int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            gO.gameObject.transform.position = new Vector3(allMyRespawnPoints[random].transform.position.x, 10.14516f, allMyRespawnPoints[random].transform.position.z);
            // gO.gameObject.SetActive(true);
            gO.gameObject.GetComponent<FieldOfView>().Start();

        //this.canAct = true;
    }
    public void RespawnCoolDown()
    {
       
            this.cooledDown = true;
            this.timeCoolDown = 0;
        //Respawn();
    }
  

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

    bool AnimatorIsPlaying(string stateName, Animator anim)
    {
        return AnimatorIsPlaying(anim) && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    bool AnimatorIsPlaying(Animator anim)
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    



}
