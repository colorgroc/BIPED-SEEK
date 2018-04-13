using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


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
    private bool pressed, cooledDown, goodFeedback, winnerFeedback;
    public bool badFeedback;

    [HideInInspector]
    public int scoreGeneral, scoreKills, scoreWins;
    public bool wannaKill, onFieldView, detected;

    private Image feedback;
    private List<GameObject> feedbacks;
    private GameObject[] feedbackList;
    [SerializeField]
    private Color colorP1, colorP2, colorP3, colorP4, GoodFeedback, BadFeedback, WinnerFeedback, DetectedFeedback;
    private Color neutralColor;
    [SerializeField]
    private Animator anim;
    //private bool canAct;

    List<GameObject> guardsList = new List<GameObject>();
    NavMeshAgent _navMeshAgent;

    void Start ()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.anim = this.gameObject.GetComponent<Animator>();
        this.feedbackList = GameObject.FindGameObjectsWithTag("Feedback");
        this.feedbacks = new List<GameObject>();
        PlayerPrefs.SetFloat("Speed", defaultSpeed);

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
            this.hab2Button = "B_1";

            //axis
            this.hab3Button = "L_Trigger_1";
            this.hab4Button = "R_Trigger_1";

            this.hab5Button = "L_Bumper_1";
            this.hab6Button = "R_Bumper_1";
        
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
            this.hab2Button = "B_2";
            //axis
            this.hab3Button = "L_Trigger_2";
            this.hab4Button = "R_Trigger_2";

            this.hab5Button = "L_Bumper_2";
            this.hab6Button = "R_Bumper_2";

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
            this.hab2Button = "B_3";
            //axis
            this.hab3Button = "L_Trigger_3";
            this.hab4Button = "R_Trigger_3";

            this.hab5Button = "L_Bumper_3";
            this.hab6Button = "R_Bumper_3";

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
            this.hab2Button = "B_4";

            //son axis
            this.hab3Button = "L_Trigger_4"; 
            this.hab4Button = "R_Trigger_4";

            this.hab5Button = "L_Bumper_4";
            this.hab6Button = "R_Bumper_4";


        }
       // this.canAct = true;

        foreach (GameObject guard in NewControl.guards)
        {
            if (guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
            {
                guardsList.Add(guard);
            }
        }
    }
    private void FixedUpdate()
    {
    }
   
    void Update()
    {
        speed = PlayerPrefs.GetFloat("Speed");

        float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
        float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;

        transform.Translate(0, 0, y * speed);
        transform.Rotate(0, rX * speedRotation, 0);

        _navMeshAgent.SetDestination(transform.position);
        
        if (Input.GetButtonDown(this.killButton))
        {
            this.wannaKill = true;
        }

        if (Input.GetButtonUp(this.killButton)) this.wannaKill = false;

        if (y > 0) this.anim.SetBool("isWalkingForward", true);
        else if (y < 0) this.anim.SetBool("isWalkingBack", true);
        else
        {
            this.anim.SetBool("isWalkingForward", false);
            this.anim.SetBool("isWalkingBack", false);
        }

            this.anim.SetBool("wannaKill", this.wannaKill);

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

        if (this.cooledDown)
        {
            this.gameObject.transform.position = new Vector3(this.transform.position.x, 1000f, this.transform.position.z);
            this.timeCoolDown -= Time.deltaTime;
            if(this.timeCoolDown <= 0)
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

    public void Kill(GameObject gO)
    {
        
        if (gO.gameObject.tag.Equals("Guard") || gO.gameObject.tag.Equals("Killer Guards")) //canviar aixo?
        {
            this.detected = false;
            this.scoreGeneral -= 3;
            this.badFeedback = true;
            //this.canAct = false;
            Destroy(gO);
            //if (AnimatorIsPlaying("Punch"))
            this.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();
            //Respawn(this.gameObject);

        }
        else if (gO.gameObject.layer == 8 && gO != NewControl.objective)
        {
            this.detected = false;
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            this.scoreGeneral += 5;
            this.scoreKills += 1;
            this.goodFeedback = true;
            // if (AnimatorIsPlaying("Punch"))
            gO.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();
            //Respawn(gO);
        }
        else if (gO.gameObject.layer == 8 && gO == NewControl.objective)
        {
            this.winnerFeedback = true;
            this.detected = false;
            //this.canAct = false;
            //if (AnimatorIsPlaying("Punch"))
            gO.gameObject.GetComponent<PlayerControl>().detected = false;
            NewControl.parcialWinner = this.gameObject;
            NewControl.objComplete = true;
            Rondes.timesPlayed++;
        }
    
        
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return this.speed;
    }

    public void Respawn(GameObject gO)
    {

       // this.gameObject.GetComponent<FieldOfView>().alive = true;
        this.detected = false;
        this.timeFeedback = 0;
        GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length); 
        gO.gameObject.transform.position = new Vector3(allMyRespawnPoints[random].transform.position.x, 10.14516f, allMyRespawnPoints[random].transform.position.z);
        gO.gameObject.SetActive(true);
        gO.gameObject.GetComponent<FieldOfView>().Start();
        //this.canAct = true;
    }
    public void RespawnCoolDown()
    {
        this.cooledDown = true;
        this.timeCoolDown = this.coolDown;
        //Respawn();
    }
   /* public void DeadCoolDown(GameObject gO)
    {
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
    }*/
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Killzone"))
        {
            this.gameObject.SetActive(false);
            Respawn(this.gameObject);
            
        }
    }

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }



   

}
