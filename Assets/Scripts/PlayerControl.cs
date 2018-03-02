using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private float speed, speedRotation;
    
    [SerializeField]
    private int tipo_de_character;
	//public int playerID;
    [HideInInspector]
    public string AxisMovement, AxisRotation, killButton, hab1Button, hab2Button, submitButton, pauseButton, cancelButton, habSpecialButton;

    private float distToGround, count, timeCoolDown, timeFeedback;
    private int coolDown;
    private bool pressed, cooledDown, badFeedback, goodFeedback;
    [HideInInspector]
    public int scoreGeneral, scoreKills, scoreSurvived, random;
   // [HideInInspector]
    public bool wannaKill, onFieldView, detected;
    [HideInInspector]
    public float jumpSpeed = 100.0F, timePast;
    private Outline feedback;
    //[SerializeField]
    //private GameObject HUD_1, HUD_2, HUD_3, HUD_4;

    // Use this for initialization
    void Start () {
        distToGround = this.gameObject.GetComponent<Collider>().bounds.extents.y;
        this.gameObject.GetComponent<Light>().enabled = false;
        
    
        if (this.gameObject.name.Equals("Player 1"))
        {
        //this.feedback = this.HUD_1.GetComponent<Outline>();
        // this.feedback = GameObject.Find("Player1HUD").GetComponent<Outline>();
            this.AxisMovement = PlayerPrefs.GetString("Movement_P1");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P1");
            this.killButton = PlayerPrefs.GetString("Kill_P1");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P1");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P1");
            /* this.pauseButton = PlayerPrefs.GetString("Pause_P1");
                this.submitButton = PlayerPrefs.GetString("Submit_P1");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P1");*/

                
        }
        else if (this.gameObject.name.Equals("Player 2"))
        {
        // this.feedback = this.HUD_2.GetComponent<Outline>();
        //this.feedback = GameObject.Find("Player2HUD").GetComponent<Outline>();
            this.AxisMovement = PlayerPrefs.GetString("Movement_P2");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P2");
            this.killButton = PlayerPrefs.GetString("Kill_P2");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P2");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P2");
            /*this.pauseButton = PlayerPrefs.GetString("Pause_P2");
            this.submitButton = PlayerPrefs.GetString("Submit_P2");
            this.cancelButton = PlayerPrefs.GetString("Cancel_P2");*/

           
        }
        else if (this.gameObject.name.Equals("Player 3"))
        {
        //this.feedback = this.HUD_3.GetComponent<Outline>();
        // this.feedback = GameObject.Find("Player3HUD").GetComponent<Outline>();
        this.AxisMovement = PlayerPrefs.GetString("Movement_P3");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P3");
            this.killButton = PlayerPrefs.GetString("Kill_P3");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P3");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P3");
            /* this.pauseButton = PlayerPrefs.GetString("Pause_P3");
                this.submitButton = PlayerPrefs.GetString("Submit_P3");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P3");*/
        }
        else if (this.gameObject.name.Equals("Player 4"))
        {
        //this.feedback = this.HUD_4.GetComponent<Outline>();
        //this.feedback = GameObject.Find("Player4HUD").GetComponent<Outline>();
        this.AxisMovement = PlayerPrefs.GetString("Movement_P4");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P4");
            this.killButton = PlayerPrefs.GetString("Kill_P4");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P4");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P4");
            /* this.pauseButton = PlayerPrefs.GetString("Pause_P4");
                this.submitButton = PlayerPrefs.GetString("Submit_P4");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P4");*/
        }
        //this.feedback.enabled = false;
    //}
        
        
    }
    private void FixedUpdate()
    {
       
        

      
       
    }
    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
        float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;
        transform.Translate(0, 0, y * speed);
        transform.Rotate(0, rX * speedRotation, 0);
        if (Input.GetButtonDown(this.killButton)) this.wannaKill = true;
        if (Input.GetButtonUp(this.killButton)) this.wannaKill = false;

        if (Input.GetAxis(this.AxisMovement) != 0)
            Debug.LogError("Movement");
        if (Input.GetButtonDown(this.killButton)) Debug.LogError("Kill");

            /*if (this.goodFeedback)
            {
                this.feedback.enabled = true;
                this.feedback.effectColor = Color.green;
                this.timeFeedback += Time.deltaTime;
                if (this.timeFeedback >= 0.5) this.goodFeedback = false;
            }
            else
            {
                this.feedback.enabled = false;
                this.timeFeedback = 0;
            }
            if (this.badFeedback)
            {
                this.feedback.enabled = true;
                this.feedback.effectColor = Color.red;
                this.timeFeedback += Time.deltaTime;
                if (this.timeFeedback >= 0.5) this.badFeedback = false;
            }
            else
            {
                this.feedback.enabled = false;
                this.timeFeedback = 0;
            }*/


            /*CalcCoolDown();
            if (this.cooledDown) {
                this.timeCoolDown += Time.deltaTime;
                if (this.timeCoolDown >= this.coolDown)
                    RespawnCoolDown(this.gameObject);

            }*/

            //Debug.Log(this.pressed + ", " + this.timePress + ", " + this.wannaKill);
            if (this.detected)
        {
            this.gameObject.GetComponent<Light>().enabled = true;
            this.timePast+= Time.deltaTime;
            if (this.timePast > 5)
            {
                this.detected = false;
            }
        }
        else
        {
            this.gameObject.GetComponent<Light>().enabled = false;
            this.timePast = 0;
        }
        //Debug.Log(this.timeFeedback);
        //Debug.Log(this.timeCoolDown);

    }

    public void Kill(GameObject gO)
    {
        if (gO.gameObject.tag.Equals("Guard") || gO.gameObject.tag.Equals("Killer Guards")) //canviar aixo?
        {
            //Debug.Log("Kill Guard");
            this.scoreGeneral -= 3;
           // this.badFeedback = true;
            Destroy(gO);
            Respawn(this.gameObject);
            //puntuacio -100;
        }
        else if (gO.gameObject.layer == 8 && gO != NewControl.objective)
        {
            //Debug.Log("Kill player");
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            this.scoreGeneral += 5;
			this.scoreKills += 1;
            //puntuacio -50;
            Respawn(gO);
        }
        else if (gO.gameObject.layer == 8 && gO == NewControl.objective)
        {

            NewControl.objComplete = true;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            NewControl.parcialWinner = this.gameObject;
            //puntuacio +200;
            //recalcular objectiu
            //avisar del nou objectiu
        }
    }


    public void Respawn(GameObject gO)
    {
        
		this.gameObject.GetComponent<FieldOfView>().alive = true;
        this.detected = false;
       
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
        gO.gameObject.transform.position = allMyRespawnPoints[random].transform.position;
        gO.gameObject.SetActive(true);
        //this.isDead = false;
		//this.gameObject.GetComponent<FieldOfView>().alive = true;
		/*GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
		GameObject playerNew = Instantiate (player_New, allMyRespawnPoints [random].transform);
		Destroy (gO);*/
    }
    public void DeadCoolDown(GameObject gO)
    {
        this.gameObject.GetComponent<FieldOfView>().alive = true;
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
        gO.gameObject.transform.position = allMyRespawnPoints[random].transform.position;
        this.cooledDown = true;   
    }
    public void RespawnCoolDown(GameObject gO)
    {
        gO.gameObject.SetActive(true);
        this.cooledDown = false;
        this.timeCoolDown = 0;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Killzone"))
        {
            //_waypointsVisited++;
            this.gameObject.SetActive(false);
            Respawn(this.gameObject);
            
        }
    }
    void CalcCoolDown()
    {
        if (NewControl.timeLeft >= 120) this.coolDown = 3;
        else if(NewControl.timeLeft >= 60 && NewControl.timeLeft < 120) this.coolDown = 4;
        else if (NewControl.timeLeft >= 30 && NewControl.timeLeft < 60) this.coolDown = 6;
        else if (NewControl.timeLeft >= 20 && NewControl.timeLeft < 30) this.coolDown = 8;
        else if (NewControl.timeLeft < 20) this.coolDown = 10;

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
