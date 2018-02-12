using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour {
    public float speed;
    float distToGround;
    public float speedRotation;
    public float jumpSpeed = 100.0F;
    [SerializeField]
    private int tipo_de_character;
	public int playerID;
   // public bool isDead;
    private float count;

    //private GameObject[] allWaypoints;
    public int random;
    //private NPCConnectedPatrol npc;
    public int scoreGeneral;
    public int scoreKills;
    public int scoreSurvived;
    public bool wannaKill;
    public bool onFieldView;
    public float timePast;
    public bool detected;
	public GameObject player_New;
    // Use this for initialization
    void Start () {
        distToGround = this.gameObject.GetComponent<Collider>().bounds.extents.y;
        this.gameObject.GetComponent<Light>().enabled = false;
       /* if (this.gameObject.tag.Equals("Player 1"))
        {
            this.tipo_de_character = PlayerPrefs.GetInt("characterPlayer_1");
            
            //GameObject prefab = (GameObject)Resources.Load("Tipo_1");
            //if(prefab == null)
            //GameObject newPlayer = (GameObject)Instantiate(prefab, this.gameObject.transform);
            Debug.Log("Jugador 1 Tipo: " + this.tipo_de_character);
        }
        else if (this.gameObject.tag.Equals("Player 2"))
        {
            this.tipo_de_character = PlayerPrefs.GetInt("characterPlayer_2");
            Debug.Log("Jugador 2 Tipo: " + this.tipo_de_character);
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            if (this.gameObject.tag.Equals("Player 1"))
            {
                this.scoreGeneral += 10;
                Debug.Log("Score 1: " + this.scoreGeneral);
            }
            else if (this.gameObject.tag.Equals("Player 2"))
            {
                this.scoreGeneral += 20;
                Debug.Log("Score 2: " + this.scoreGeneral);
            }
        }

        if (this.gameObject.tag.Equals("Player 1"))
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime;
            float y = Input.GetAxis("Vertical") * Time.deltaTime;
            transform.Translate(0, 0, y * speed);
            transform.Rotate(0, x * speedRotation, 0);
            if (Input.GetKeyDown(KeyCode.P)) this.wannaKill = true;
            if (Input.GetKeyDown(KeyCode.RightControl) && IsGrounded()) GetComponent<Rigidbody>().AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
        }
        else if (this.gameObject.tag.Equals("Player 2"))
        {
            /*float x = Input.GetAxis("Horizontal 2") * Time.deltaTime;
            float y = Input.GetAxis("Vertical 2") * Time.deltaTime;
            transform.Translate(0, 0, y * speed);
            transform.Rotate(0, x * speedRotation, 0);

			if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded()) GetComponent<Rigidbody>().AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
			
			if(Input.GetKeyUp(KeyCode.E)) this.wannaKill = false;*/
        }

       
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
