using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private float speed, speedRotation;
    
    //[SerializeField]
    //private int tipo_de_character;
	//public int playerID;
    [HideInInspector]
    public string AxisMovement, AxisRotation, killButton, hab1Button, hab2Button, submitButton, pauseButton, cancelButton, habSpecialButton;

    private float distToGround, count, timeCoolDown, timeFeedback;
    private int coolDown;
    private bool pressed, cooledDown, goodFeedback, winnerFeedback;
    public bool badFeedback;
    [HideInInspector]
    public int scoreGeneral, scoreKills, scoreSurvived, random;
   // [HideInInspector]
    public bool wannaKill, onFieldView, detected;
    [HideInInspector]
    public float jumpSpeed = 100.0F, timePast;
    private Image feedback;
    private List<GameObject> feedbacks;
    private GameObject[] feedbackList;
    [SerializeField]
    private Color colorP1, colorP2, colorP3, colorP4, GoodFeedback, BadFeedback, WinnerFeedback, DetectedFeedback;
    private Color neutralColor;
    [SerializeField]
    private Animator anim;
    private bool canAct;
    //[SerializeField]
    //private GameObject HUD_1, HUD_2, HUD_3, HUD_4;

    // Use this for initialization
    void Start () {
        
        //distToGround = this.gameObject.GetComponent<Collider>().bounds.extents.y;
        this.anim = this.gameObject.GetComponent<Animator>();
        this.gameObject.GetComponent<Light>().enabled = false;
        this.feedbackList = GameObject.FindGameObjectsWithTag("Feedback");
        this.feedbacks = new List<GameObject>();

        for (int i = 0; i < feedbackList.Length; i++)
        {
            this.feedbacks.Add(feedbackList[i].gameObject);
        }

        this.feedbacks.Sort(SortByName);



        if (this.gameObject.name.Equals("Player 1"))
        {
            this.feedback = this.feedbacks[0].GetComponent<Image>();
            this.neutralColor = colorP1;
            this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_1";
            this.AxisRotation = "H_RPad_1";
            this.killButton = "X_1";
            this.hab1Button = "Y_1";
            this.hab2Button = "A_1";

            /*this.AxisMovement = PlayerPrefs.GetString("Movement_P1"); 
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P1");
            this.killButton = PlayerPrefs.GetString("Kill_P1");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P1");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P1");*/
            /* this.pauseButton = PlayerPrefs.GetString("Pause_P1");
                this.submitButton = PlayerPrefs.GetString("Submit_P1");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P1");*/


        }
        else if (this.gameObject.name.Equals("Player 2"))
        {
            this.feedback = this.feedbacks[1].GetComponent<Image>();
            this.neutralColor = colorP2;
            this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_2";
            this.AxisRotation = "H_RPad_2";
            this.killButton = "X_2";
            this.hab1Button = "Y_2";
            this.hab2Button = "A_2";

            /*this.AxisMovement = PlayerPrefs.GetString("Movement_P2");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P2");
            this.killButton = PlayerPrefs.GetString("Kill_P2");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P2");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P2");*/
            /*this.pauseButton = PlayerPrefs.GetString("Pause_P2");
            this.submitButton = PlayerPrefs.GetString("Submit_P2");
            this.cancelButton = PlayerPrefs.GetString("Cancel_P2");*/


        }
        else if (this.gameObject.name.Equals("Player 3"))
        {
            this.feedback = this.feedbacks[2].GetComponent<Image>();
            this.neutralColor = colorP3;
            this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_3";
            this.AxisRotation = "H_RPad_3";
            this.killButton = "X_3";
            this.hab1Button = "Y_3";
            this.hab2Button = "A_3";

            /*this.AxisMovement = PlayerPrefs.GetString("Movement_P3");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P3");
            this.killButton = PlayerPrefs.GetString("Kill_P3");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P3");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P3");*/
            /* this.pauseButton = PlayerPrefs.GetString("Pause_P3");
                this.submitButton = PlayerPrefs.GetString("Submit_P3");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P3");*/
        }
        else if (this.gameObject.name.Equals("Player 4"))
        {
            this.feedback = this.feedbacks[3].GetComponent<Image>();
            this.neutralColor = colorP4;
            this.feedback.color = this.neutralColor;

            this.AxisMovement = "V_LPad_4";
            this.AxisRotation = "H_RPad_4";
            this.killButton = "X_4";
            this.hab1Button = "Y_4";
            this.hab2Button = "A_4";

            /*this.AxisMovement = PlayerPrefs.GetString("Movement_P4");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P4");
            this.killButton = PlayerPrefs.GetString("Kill_P4");
            this.hab1Button = PlayerPrefs.GetString("Hab1_P4");
            this.hab2Button = PlayerPrefs.GetString("Hab2_P4");*/
            /* this.pauseButton = PlayerPrefs.GetString("Pause_P4");
                this.submitButton = PlayerPrefs.GetString("Submit_P4");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P4");*/
        }
        this.canAct = true;
    }
    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (this.canAct)
        {
            float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
            float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;

            transform.Translate(0, 0, y * speed);
            transform.Rotate(0, rX * speedRotation, 0);
            if (Input.GetButtonDown(this.killButton)) this.wannaKill = true;
            if (Input.GetButtonUp(this.killButton)) this.wannaKill = false;

            if (y > 0) this.anim.SetBool("isWalkingForward", true);
            else if (y < 0) this.anim.SetBool("isWalkingBack", true);
            else
            {
                this.anim.SetBool("isWalkingForward", false);
                this.anim.SetBool("isWalkingBack", false);
            }

            this.anim.SetBool("wannaKill", this.wannaKill);
        }

        /*if (Input.GetAxis(this.AxisMovement) != 0)
            Debug.LogError("Movement");
        if (Input.GetButtonDown(this.killButton)) Debug.LogError("Kill");*/

        if (this.goodFeedback)
        {
            this.timeFeedback += Time.deltaTime;
            this.feedback.color = this.GoodFeedback;
            if (this.timeFeedback >= 0.5) this.goodFeedback = false;
        }
        else if (this.badFeedback)
        {
            this.timeFeedback += Time.deltaTime;
            this.feedback.color = this.BadFeedback;
            if (this.timeFeedback >= 0.5) this.badFeedback = false;
        }
        else if (this.winnerFeedback)
        {
            this.timeFeedback += Time.deltaTime;
            this.feedback.color = this.WinnerFeedback;
            if (this.timeFeedback >= 0.5) this.winnerFeedback = false;
        }
        else if (this.detected)
        {
            this.timeFeedback += Time.deltaTime;
            this.feedback.color = this.DetectedFeedback;
            if (this.timeFeedback >= 5) this.detected = false;
        }
        else
        {
            this.timeFeedback = 0;
            this.feedback.color = this.neutralColor;
        }


        /*if (this.detected)
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
        }*/


    }

    public void Kill(GameObject gO)
    {
        if (gO.gameObject.tag.Equals("Guard") || gO.gameObject.tag.Equals("Killer Guards")) //canviar aixo?
        {
           
            this.scoreGeneral -= 3;
            this.badFeedback = true;
            this.canAct = false;
            Destroy(gO);
            Respawn(this.gameObject);

        }
        else if (gO.gameObject.layer == 8 && gO != NewControl.objective)
        {

            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            this.scoreGeneral += 5;
			this.scoreKills += 1;
            this.goodFeedback = true;

            Respawn(gO);
        }
        else if (gO.gameObject.layer == 8 && gO == NewControl.objective)
        {
            this.winnerFeedback = true;
            this.canAct = false;
            NewControl.objComplete = true;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            NewControl.parcialWinner = this.gameObject;

        }
    }


    public void Respawn(GameObject gO)
    {

        this.gameObject.GetComponent<FieldOfView>().alive = true;
        this.detected = false;
        this.timeFeedback = 0;
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
        gO.gameObject.transform.position = allMyRespawnPoints[random].transform.position;
        gO.gameObject.SetActive(true);
        this.canAct = true;
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
            this.gameObject.SetActive(false);
            Respawn(this.gameObject);
            
        }
    }
    /*void CalcCoolDown()
    {
        if (NewControl.timeLeft >= 120) this.coolDown = 3;
        else if(NewControl.timeLeft >= 60 && NewControl.timeLeft < 120) this.coolDown = 4;
        else if (NewControl.timeLeft >= 30 && NewControl.timeLeft < 60) this.coolDown = 6;
        else if (NewControl.timeLeft >= 20 && NewControl.timeLeft < 30) this.coolDown = 8;
        else if (NewControl.timeLeft < 20) this.coolDown = 10;

    }*/

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }

    /*bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }*/

}
