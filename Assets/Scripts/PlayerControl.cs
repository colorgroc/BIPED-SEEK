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
    public string AxisMovement, AxisRotation, AxisRotation2, AxisMovement_Arrows, AxisRotation_Arrows, killButton, hab1Button, hab2Button, hab3Button, hab4Button, hab5Button, hab6Button;

    private float distToGround, count, timeFeedback;

    public int coolDown;
    public float timeCoolDown;
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
    private PlayerIndex player;
   // public static PlayerIndex player1, player2, player3, player4;
    //private PlayerIndex player1, player2, player3, player4;
    //public static bool p1, p2, p3, p4;
    private GamePadState state;
    private GamePadState prevState;

    private void Awake()
    {
        this.backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 2");
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
            this.player = PlayerIndex.One;
            this.AxisMovement = "V_LPad_1";
            this.AxisRotation2 = "H_LPad_1";
            this.AxisRotation = "H_RPad_1";
            this.AxisMovement_Arrows = "V_Arrows_1";
            this.AxisRotation_Arrows = "H_Arrows_1";
            this.killButton = "X_1";
            this.hab1Button = "LB_1";
            this.hab2Button = "RB_1";   
        }
        else if (this.gameObject.name.Equals("Player 2"))
        {
            this.player = PlayerIndex.Two;
            this.AxisMovement = "V_LPad_2";
            this.AxisRotation2 = "H_LPad_2";
            this.AxisRotation = "H_RPad_2";
            this.AxisMovement_Arrows = "V_Arrows_2";
            this.AxisRotation_Arrows = "H_Arrows_2";
            this.killButton = "X_2";
            this.hab1Button = "LB_2";
            this.hab2Button = "RB_2";
      
        }
        else if (this.gameObject.name.Equals("Player 3"))
        {
            this.player = PlayerIndex.Three;
            this.AxisMovement = "V_LPad_3";
            this.AxisRotation2 = "H_LPad_3";
            this.AxisRotation = "H_RPad_3";
            this.AxisMovement_Arrows = "V_Arrows_3";
            this.AxisRotation_Arrows = "H_Arrows_3";
            this.killButton = "X_3";
            this.hab1Button = "LB_3";
            this.hab2Button = "RB_3";

        }
        else if (this.gameObject.name.Equals("Player 4"))
        {
            this.player = PlayerIndex.Four;
            this.AxisMovement = "V_LPad_4";
            this.AxisRotation2 = "H_LPad_4";
            this.AxisRotation = "H_RPad_4";
            this.AxisMovement_Arrows = "V_Arrows_4";
            this.AxisRotation_Arrows = "H_Arrows_4";
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
        if (PlayerPrefs.GetInt("Vibration") == 0)
        {
            this.prevState = this.state;
            this.state = GamePad.GetState(this.player);
            if (this.detected)
            {
                GamePad.SetVibration(this.player, this.state.PacketNumber, this.state.PacketNumber);
                Debug.Log("vibrating: " + this.player);
            }
            else
            {
                GamePad.SetVibration(this.player, 0, 0);
            }
        }

        if (!_sprint)
        {
            speed = PlayerPrefs.GetFloat("Speed");
        }
        if (this.canAct && !this.usingAbility && !this.cooledDown)
        {
            float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
            float y_Arrows = Input.GetAxis(this.AxisMovement_Arrows) * Time.deltaTime;
            float x = Input.GetAxis(this.AxisRotation2) * Time.deltaTime;
            float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;
            float x_Arrows = Input.GetAxis(this.AxisRotation_Arrows) * Time.deltaTime;

            if (y != 0)
                transform.Translate(0, 0, y * speed);
            else transform.Translate(0, 0, (-y_Arrows) * speed);

            if (rX != 0)
                transform.Rotate(0, rX * speedRotation, 0);
            else if (x_Arrows != 0)
                transform.Rotate(0, x_Arrows * speedRotation, 0);
            else transform.Rotate(0, x * speedRotation, 0);

            _navMeshAgent.SetDestination(transform.position);

            if (Input.GetButtonDown(this.killButton) && !Abilities_Tutorial.show)
            {
                this.wannaKill = true;
                //RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
            }
            if (Input.GetButtonUp(this.killButton) && !Abilities_Tutorial.show) this.wannaKill = false;

            if (y > 0 || y_Arrows > 0)
            {
                this.anim.SetBool("isWalkingForward", true);
            }
            else if (y < 0 || y_Arrows < 0) this.anim.SetBool("isWalkingBack", true);
            else
            {
                this.anim.SetBool("isWalkingForward", false);
                this.anim.SetBool("isWalkingBack", false);
            }
            if((y != 0 || y_Arrows != 0) && (_sprint || speed > defaultSpeed)) anim.SetBool("isRunning", true);
            else anim.SetBool("isRunning", false);

            if ((y == 0 && y_Arrows == 0) && (rX > 0 || x > 0 || x_Arrows > 0)) anim.SetBool("Rot_Right", true);
            else if((y == 0 && y_Arrows == 0) && (rX < 0 || x < 0 || x_Arrows < 0)) anim.SetBool("Rot_Left", true);
            else
            {
                anim.SetBool("Rot_Right", false);
                anim.SetBool("Rot_Left", false);
            }
            //
        }else if(this.canAct && this.usingAbility && !this.cooledDown)
        {
            if (Input.GetButtonDown(this.killButton) && !Abilities_Tutorial.show)
            {
                this.wannaKill = true;
				//RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
            }
            if (Input.GetButtonUp(this.killButton) && !Abilities_Tutorial.show) this.wannaKill = false;
        }
        //if(this.wannaKill)
        //{
        //    RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
        //} 
        this.anim.SetBool("wannaKill", this.wannaKill);
        this.anim.SetBool("isFreezed", !this.canAct);

        if (this.detected && !this.cooledDown && Time.timeScale == 1)
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
    public void PlayAttackSound()
    {
        RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Attack", this.transform.position);
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
    
    //private bool PressedDown(string button)
    //{
    //    // Detect if a button was pressed this frame
    //    if (button.Equals("A"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }  
    //    }
    //    else if (button.Equals("B")) { 
        
    //        if (this.prevState.Buttons.B == ButtonState.Released && this.state.Buttons.B == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("X"))
    //    {
    //        if (this.prevState.Buttons.X == ButtonState.Released && this.state.Buttons.X == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Y"))
    //    {
    //        if (this.prevState.Buttons.Y == ButtonState.Released && this.state.Buttons.Y == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Move_Left"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Move_Right"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Move_Up"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Move_Down"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Rot_Left"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Rot_Right"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Arrow_Left"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Arrow_Right"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Arrow_Up"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Arrow_Down"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("Start"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("BL"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("BR"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("TL"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    else if (button.Equals("TR"))
    //    {
    //        if (this.prevState.Buttons.A == ButtonState.Released && this.state.Buttons.A == ButtonState.Pressed)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}



}
