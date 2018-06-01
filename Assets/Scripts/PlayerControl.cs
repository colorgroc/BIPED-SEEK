using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using FMODUnity;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {

    [SerializeField]
    private float speed = 20, speedRotation = 140;
    [SerializeField]
    public static float defaultSpeed = 20;
    [HideInInspector]
    public string AxisMovement, AxisRotation, AxisRotation2, killButton, hab1Button, hab2Button, hab3Button, hab4Button, hab5Button, hab6Button;

    private float distToGround, count, timeCoolDown, timeFeedback;
    [SerializeField]
    private int coolDown;

    public bool cooledDown, usingAbility, vibration;

    [HideInInspector]
    public int scoreGeneral, scoreKills, scoreWins, scoreGeneralRound, scoreKillsRound, scoreWinsRound;
    public bool wannaKill, onFieldView, detected, _sprint, canAct;

    [SerializeField]
    private Color colorP1, colorP2, colorP3, colorP4;
    private Color neutralColor;
    [SerializeField]
    private Animator anim;
    private GameObject[] guards;
    List<GameObject> guardsList = new List<GameObject>();
    NavMeshAgent _navMeshAgent;
    public FMOD.Studio.EventInstance backgroudSound;

   // public static PlayerIndex player1, player2, player3, player4;
    private PlayerIndex player1, player2, player3, player4;
    public static bool p1, p2, p3, p4;
    private GamePadState state;
    private GamePadState prevState;

    private void Awake()
    {
        
        //PlayerIndex player = PlayerIndex.One;
        // QualitySettings.SetQualityLevel(5);
        //this.backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 1");
        this.backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 2");
        //this.backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 3");
    }

    void Start ()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.anim = this.gameObject.GetComponent<Animator>();
        PlayerPrefs.SetFloat("Speed", defaultSpeed);
        this.canAct = true;

        if (Tutorial_InGame.showIt || Abilities_Tutorial.show)
            guards = GameObject.FindGameObjectsWithTag("Guard");

        scoreGeneral = scoreKills = scoreWins = scoreGeneralRound = 0;// = scoreKillsRound = scoreWinsRound = 0;


        if (this.gameObject.name.Equals("Player 1"))
        {
            //this.player1 = PlayerIndex.One;
            this.AxisMovement = "V_LPad_1";
            this.AxisRotation2 = "H_LPad_1";
            this.AxisRotation = "H_RPad_1";
            this.killButton = "X_1";
            this.hab1Button = "LB_1";
            this.hab2Button = "RB_1";   
        }
        else if (this.gameObject.name.Equals("Player 2"))
        {
            //this.player2 = PlayerIndex.Two;
            this.AxisMovement = "V_LPad_2";
            this.AxisRotation2 = "H_LPad_2";
            this.AxisRotation = "H_RPad_2";
            this.killButton = "X_2";
            this.hab1Button = "LB_2";
            this.hab2Button = "RB_2";
      
        }
        else if (this.gameObject.name.Equals("Player 3"))
        {
            //this.player3 = PlayerIndex.Three;
            this.AxisMovement = "V_LPad_3";
            this.AxisRotation2 = "H_LPad_3";
            this.AxisRotation = "H_RPad_3";
            this.killButton = "X_3";
            this.hab1Button = "LB_3";
            this.hab2Button = "RB_3";

        }
        else if (this.gameObject.name.Equals("Player 4"))
        {
            //this.player4 = PlayerIndex.Four;
            this.AxisMovement = "V_LPad_4";
            this.AxisRotation2 = "H_LPad_4";
            this.AxisRotation = "H_RPad_4";
            this.killButton = "X_4";
            this.hab1Button = "LB_4";
            this.hab2Button = "RB_4";
        }

        if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show)
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
        

        //---------------------/probar amb mandos oficials!! ----------------
        //if (this.gameObject.name.Equals("Player 1"))
        //{
        //    this.prevState = this.state;
        //    this.state = GamePad.GetState(player1);
        //    if (this.detected)
        //    {
        //        GamePad.SetVibration(player1, state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
        //        // GamePad.SetVibration(this.player1, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        //    }
        //    else
        //    {
        //        GamePad.SetVibration(player1, 0, 0);
        //    }
        //}
        //else if (this.gameObject.name.Equals("Player 2"))
        //{
        //    this.prevState = this.state;
        //    this.state = GamePad.GetState(player2);
        //    if (this.detected)
        //    {
        //        GamePad.SetVibration(player2, state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
        //        //GamePad.SetVibration(this.player2, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        //    }
        //    else
        //    {
        //        GamePad.SetVibration(player2, 0, 0);
        //    }
        //}
        //else if (this.gameObject.name.Equals("Player 3"))
        //{
        //    this.prevState = this.state;
        //    this.state = GamePad.GetState(player3);
        //    if (this.detected)
        //    {
        //        //GamePad.SetVibration(this.player3, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        //        GamePad.SetVibration(player3, state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
        //    }
        //    else
        //    {
        //        GamePad.SetVibration(player3, 0, 0);
        //    }
        //}
        //else if (this.gameObject.name.Equals("Player 4"))
        //{
        //    this.prevState = this.state;
        //    this.state = GamePad.GetState(player4);
        //    if (this.detected)
        //    {
        //        GamePad.SetVibration(player4, state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
        //        //GamePad.SetVibration(this.player4, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        //    }
        //    else
        //    {
        //        GamePad.SetVibration(player4, 0, 0);
        //    }
        //}



        if (!_sprint)
        {
            speed = PlayerPrefs.GetFloat("Speed");
        }
        if (this.canAct && !this.usingAbility)
        {
            float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
            float x = Input.GetAxis(this.AxisRotation2) * Time.deltaTime;
            float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;

            transform.Translate(0, 0, y * speed);
            
            if(rX != 0)
                transform.Rotate(0, rX * speedRotation, 0);
            else transform.Rotate(0, x * speedRotation, 0);

            _navMeshAgent.SetDestination(transform.position);

            if (Input.GetButtonDown(this.killButton) && !Abilities_Tutorial.show)
            {
                this.wannaKill = true;
                RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
            }
            if (Input.GetButtonUp(this.killButton) && !Abilities_Tutorial.show) this.wannaKill = false;

            if (y > 0)
            {
                this.anim.SetBool("isWalkingForward", true);
            }
            else if (y < 0) this.anim.SetBool("isWalkingBack", true);
            else
            {
                this.anim.SetBool("isWalkingForward", false);
                this.anim.SetBool("isWalkingBack", false);
            }
            if(y != 0 && (_sprint || speed > defaultSpeed)) anim.SetBool("isRunning", true);
            else anim.SetBool("isRunning", false);
            if (y == 0 && (rX > 0 ||x > 0)) anim.SetBool("Rot_Right", true);
            else if(y == 0 && (rX < 0 || x < 0)) anim.SetBool("Rot_Left", true);
            else
            {
                anim.SetBool("Rot_Right", false);
                anim.SetBool("Rot_Left", false);
            }
            //
        }else if(this.canAct && this.usingAbility)
        {
            if (Input.GetButtonDown(this.killButton) && !Abilities_Tutorial.show)
            {
                this.wannaKill = true;
				RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
            }
            if (Input.GetButtonUp(this.killButton) && !Abilities_Tutorial.show) this.wannaKill = false;
        }
            

        this.anim.SetBool("wannaKill", this.wannaKill);
        this.anim.SetBool("isFreezed", !this.canAct);
        if (this.detected && !this.cooledDown)
        {
            if (!this.vibration)
            {
                this.backgroudSound.start();
                this.vibration = true;
            }

            this.timeFeedback += Time.deltaTime;
            if (this.timeFeedback >= 1)
            {
                this.detected = false;
                this.vibration = false;
                this.backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else {
            this.timeFeedback = 0;
            this.vibration = false;
            this.backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show)
        {
            if (NewControl.paused || Tutorial_InGame.tutorialPaused ||  Abilities_Tutorial.tutorialPaused || Time.timeScale == 0 || GameObject.Find("Control").GetComponent<NewControl>().rankingCanvas.activeInHierarchy)
            {
                this.backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                this.vibration = false;
            }
        }
        else
        {
            if (NewControl.paused || Tutorial_InGame.tutorialPaused || Abilities_Tutorial.tutorialPaused || Time.timeScale == 0)
            {
                this.backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                this.vibration = false;
            }
        }
        
        if (this.cooledDown)
        {
            this.detected = false;
           
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
        if (gO.gameObject.tag.Equals("Guard") || gO.gameObject.tag.Equals("Killer Guards")) //canviar aixo?
        {
            this.detected = false;
            this.scoreGeneral -= 5;
            this.scoreGeneralRound -= 5;

            if (gO.gameObject.tag.Equals("Guard"))
            {
                gO.GetComponent<NPCConnectedPatrol>().Respawn(gO);
            }
            else if (gO.gameObject.tag.Equals("Killer Guards"))
            {
                Destroy(gO.gameObject);
            }
			RuntimeManager.PlayOneShot("event:/BipedSeek/NPC/Death", gO.transform.position);
			RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Death", this.transform.position);
            Death.AnimDeath(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation);
            this.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();

        }
        else if (gO.gameObject.layer == 8 && gO != NewControl.objective)
        {
            this.detected = false;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            this.scoreGeneral += 10;
            this.scoreKills += 1;
            this.scoreGeneralRound += 10;

            gO.gameObject.GetComponent<PlayerControl>().scoreGeneral -= 10;
            gO.gameObject.GetComponent<PlayerControl>().scoreGeneralRound -= 10;

            //this.scoreKillsRound += 1;
            RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Death", gO.transform.position);
            gO.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();

        }
        else if (gO.gameObject.layer == 8 && gO == NewControl.objective)
        {
            this.detected = false;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show)
            {
                NewControl.parcialWinner = this.gameObject;
                NewControl.objComplete = true;
            }
            Rondes.timesPlayed++;
            if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show)
                GameObject.Find("Control").GetComponent<EventosMapa>().Default();
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
        gO.gameObject.GetComponent<FieldOfView>().Start();
    }
    public void RespawnCoolDown()
    {
        this.cooledDown = true;
        //if (this.gameObject != NewControl.objective)
        //{
        //    this.gameObject.GetComponent<PlayerControl>().scoreGeneral -= 10;
        //    this.gameObject.GetComponent<PlayerControl>().scoreGeneralRound -= 10;
        //}
        this.timeCoolDown = 0;
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
