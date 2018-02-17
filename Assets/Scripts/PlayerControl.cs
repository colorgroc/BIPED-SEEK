using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private float speed, speedRotation;
    
    [SerializeField]
    private int tipo_de_character;
	public int playerID;
    [HideInInspector]
    public string AxisMovement, AxisRotation, killButton, hab1Button, hab2Button, submitButton, pauseButton, cancelButton, habSpecialButton;
    
    private float count;
    private float distToGround;

    [HideInInspector]
    public int scoreGeneral, scoreKills, scoreSurvived, random;
    [HideInInspector]
    public bool wannaKill, onFieldView, detected;
    [HideInInspector]
    public float jumpSpeed = 100.0F, timePast;

    // Use this for initialization
    void Start () {
        distToGround = this.gameObject.GetComponent<Collider>().bounds.extents.y;
        this.gameObject.GetComponent<Light>().enabled = false;

        //asignacion controles
        if (PlayerPrefs.GetInt("NumPlayers") == 1) {
            this.AxisMovement = PlayerPrefs.GetString("Movement_P1");
            this.AxisRotation = PlayerPrefs.GetString("Rotation_P1");
            this.killButton = "Kill";
        }
        else
        {
            if (this.gameObject.name.Equals("Player_1"))
            {
                this.AxisMovement = PlayerPrefs.GetString("Movement_P1");
                this.AxisRotation = PlayerPrefs.GetString("Rotation_P1");
                this.killButton = PlayerPrefs.GetString("Kill_P1");
                this.hab1Button = PlayerPrefs.GetString("Hab1_P1");
                this.hab2Button = PlayerPrefs.GetString("Hab2_P1");
                /* this.pauseButton = PlayerPrefs.GetString("Pause_P1");
                    this.submitButton = PlayerPrefs.GetString("Submit_P1");
                    this.cancelButton = PlayerPrefs.GetString("Cancel_P1");*/

                Debug.Log("P1: " + this.AxisMovement);
                Debug.Log("P1: " + this.AxisRotation);
                Debug.Log("P1: " + this.killButton);
            }
            else if (this.gameObject.name.Equals("Player_2"))
            {
                this.AxisMovement = PlayerPrefs.GetString("Movement_P2");
                this.AxisRotation = PlayerPrefs.GetString("Rotation_P2");
                this.killButton = PlayerPrefs.GetString("Kill_P2");
                this.hab1Button = PlayerPrefs.GetString("Hab1_P2");
                this.hab2Button = PlayerPrefs.GetString("Hab2_P2");
                /*this.pauseButton = PlayerPrefs.GetString("Pause_P2");
                this.submitButton = PlayerPrefs.GetString("Submit_P2");
                this.cancelButton = PlayerPrefs.GetString("Cancel_P2");*/

                Debug.Log("P2: " + this.AxisMovement);
                Debug.Log("P2: " + this.AxisRotation);
                Debug.Log("P2: " + this.killButton);
            }
            else if (this.gameObject.name.Equals("Player_3"))
            {
                this.AxisMovement = PlayerPrefs.GetString("Movement_P3");
                this.AxisRotation = PlayerPrefs.GetString("Rotation_P3");
                this.killButton = PlayerPrefs.GetString("Kill_P3");
                this.hab1Button = PlayerPrefs.GetString("Hab1_P3");
                this.hab2Button = PlayerPrefs.GetString("Hab2_P3");
                /* this.pauseButton = PlayerPrefs.GetString("Pause_P3");
                    this.submitButton = PlayerPrefs.GetString("Submit_P3");
                    this.cancelButton = PlayerPrefs.GetString("Cancel_P3");*/
            }
            else if (this.gameObject.name.Equals("Player_4"))
            {
                this.AxisMovement = PlayerPrefs.GetString("Movement_P4");
                this.AxisRotation = PlayerPrefs.GetString("Rotation_P4");
                this.killButton = PlayerPrefs.GetString("Kill_P4");
                this.hab1Button = PlayerPrefs.GetString("Hab1_P4");
                this.hab2Button = PlayerPrefs.GetString("Hab2_P4");
                /* this.pauseButton = PlayerPrefs.GetString("Pause_P4");
                    this.submitButton = PlayerPrefs.GetString("Submit_P4");
                    this.cancelButton = PlayerPrefs.GetString("Cancel_P4");*/
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
       //Controles
        float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
        float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;
        transform.Translate(0, 0, y * speed);
        transform.Rotate(0, rX * speedRotation, 0);
        if (Input.GetButtonDown(killButton)) this.wannaKill = true;
   

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
    }

    public void Kill(GameObject gO)
    {
        if (gO.gameObject.tag.Equals("Guard") || gO.gameObject.tag.Equals("Killer Guards"))
        {
            Debug.Log("Kill Guard");
            this.scoreGeneral -= 5;
            Destroy(gO);
            //puntuacio -100;
        }
        else if (gO.gameObject.layer == 8 && gO != NewControl.objective)
        {
            Debug.Log("Kill player");
			this.scoreGeneral += 30;
			this.scoreKills += 1;
            //puntuacio -50;
            Respawn(gO);
        }
        else if (gO.gameObject.layer == 8 && gO == NewControl.objective)
        {
            NewControl.objComplete = true;
			NewControl.parcialWinner = this.gameObject;
            //puntuacio +200;
            //recalcular objectiu
            //avisar del nou objectiu
        }
    }


    public void Respawn(GameObject gO)
    {
        
		this.gameObject.GetComponent<FieldOfView>().alive = true;
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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Killzone"))
        {
            //_waypointsVisited++;
            this.gameObject.SetActive(false);
            Respawn(this.gameObject);

        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
