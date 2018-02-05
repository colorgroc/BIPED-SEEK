using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
	// Use this for initialization
	public float speed;
	float distToGround;
	public float speedRotation;
	public GameObject[] players;
	public float jumpSpeed = 100.0F;

	[SerializeField]
	public HUDStat score1;

	public GameObject target;

	public bool isDead;
	private float count;

	//private GameObject[] allWaypoints;
	public int random;
    //private NPCConnectedPatrol npc;
   // public int scoreGeneral;


	void Start()
	{
        //this.scoreGeneral = 0;
		score1.ScoreVal = 0;
		score1.KillVal = 0;
		score1.SurvivedVal = 0;

		score1.ScoreVal2 = 0;
		score1.KillVal2 = 0;
		score1.SurvivedVal2 = 0;
		distToGround = this.gameObject.GetComponent<Collider> ().bounds.extents.y;

		 
		this.gameObject.GetComponent<Light> ().enabled = false;

	}
	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Q)) {
            //this.scoreGeneral += 10;
			score1.ScoreVal += 10;
			score1.KillVal += 5;
			score1.SurvivedVal += 1;
			score1.ScoreVal2 += 12;
			score1.KillVal2 += 52;
			score1.SurvivedVal2 += 31;
		}
		/*if (isDead) {
			this.enabled = false;
			count += Time.deltaTime;
			if (count >= 3)
				Respawn (random);
			//StartCoroutine (Respawn (random, 3));
			//Invoke ("Respawn", 3);
		}*/
		if (this.gameObject.tag.Equals ("Player 1")) {
			float x = Input.GetAxis ("Horizontal") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical") * Time.deltaTime;
			transform.Translate (0, 0, y * speed);
			transform.Rotate (0, x * speedRotation, 0);

			if (Input.GetKeyDown(KeyCode.RightControl) && isGrounded())
			{
				GetComponent<Rigidbody>().AddForce(0, jumpSpeed, 0, ForceMode.Impulse);

			}

			if (Input.GetKeyDown (KeyCode.P)) {
				ControlScript.player_1_WannaKill = true;
			}

		} else if (this.gameObject.tag.Equals ("Player 2")) {
			float x = Input.GetAxis ("Horizontal 2") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical 2") * Time.deltaTime;
			transform.Translate (0, 0, y * speed);
			transform.Rotate (0, x * speedRotation, 0);

			if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded())
			{
				GetComponent<Rigidbody>().AddForce(0, jumpSpeed, 0, ForceMode.Impulse);

			}

			if (Input.GetKeyDown (KeyCode.E)) {
				ControlScript.player_2_WannaKill = true;
			}
		}
			
		if (this.gameObject.tag.Equals ("Player 1")) {
			if (ControlScript.detected1) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				ControlScript.timePast1 += Time.deltaTime;
				if (ControlScript.timePast1 > 5) {
					ControlScript.detected1 = false;
				}

			} else if(!ControlScript.detected1) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				ControlScript.timePast1 = 0;
			}
		} else if (this.gameObject.tag.Equals ("Player 2")) {
			if (ControlScript.detected2) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				ControlScript.timePast2 += Time.deltaTime;
				if (ControlScript.timePast2 > 5) {
					ControlScript.detected2 = false;
				}

			} else if(!ControlScript.detected2) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				ControlScript.timePast2 = 0;
			}
		}else if (this.gameObject.tag.Equals ("Player 3")) {
			if (ControlScript.detected3) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				ControlScript.timePast3 += Time.deltaTime;
				if (ControlScript.timePast3 > 5)
					ControlScript.detected3 = false;

			} else if(!ControlScript.detected3){
				this.gameObject.GetComponent<Light> ().enabled = false;
				ControlScript.timePast3 = 0;
			}
		}else if (this.gameObject.tag.Equals ("Player 4")) {
			if (ControlScript.detected4) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				ControlScript.timePast4 += Time.deltaTime;
				if (ControlScript.timePast4 > 5)
					ControlScript.detected4 = false;

			} else if(!ControlScript.detected4) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				ControlScript.timePast4 = 0;
			}
		}
	

	}



	public void Kill(GameObject gO){
		
		if (gO.gameObject.tag.Equals ("Guard") || gO.gameObject.tag.Equals ("Killer Guards")) {
			Debug.Log ("Kill Guard");
            
			if (this.gameObject.tag.Equals ("Player 1")) {
				score1.ScoreVal -= 5;
			}
			else if (this.gameObject.tag.Equals ("Player 2")) {
				score1.ScoreVal2 -= 5;
			}
			Destroy (gO);
			//puntuacio -100;
		}else if(gO.gameObject.layer == 8 && gO != ControlScript.objective){
			Debug.Log ("Kill player");
			if (this.gameObject.tag.Equals ("Player 1")) {
				score1.KillVal += 10;
			}
			else if (this.gameObject.tag.Equals ("Player 2")) {
				score1.KillVal2 += 10;
			}
			//puntuacio -50;
			Respawn(gO);
		}else if(gO.gameObject.layer == 8 && gO == ControlScript.objective){
			ControlScript.objComplete = true;
			//puntuacio +200;
			//recalcular objectiu
			//avisar del nou objectiu
		}
	}


	/*IEnumerator Respawn(int random, float delay){
		yield return new WaitForSeconds (delay);
		//GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		//int random = UnityEngine.Random.Range (0, allWaypoints.Length);
		this.transform.position = allWaypoints [random].transform.position;
		//this.gameObject.SetActive (true);
		this.enabled = true;
		//this.transform.position = pos;
		isDead = false;
		 
	}*/
	public void Respawn(GameObject gO){
		//yield return new WaitForSeconds (delay);
		FieldOfView.alive = true;
		GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag ("RespawnPoint");
		int random = UnityEngine.Random.Range (0, allMyRespawnPoints.Length);
		gO.gameObject.transform.position = allMyRespawnPoints [random].transform.position;
		gO.gameObject.SetActive (true);
		//this.enabled = true;
		//this.transform.position = pos;
		isDead = false;

	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals("Killzone")) {
			//_waypointsVisited++;
			this.gameObject.SetActive (false);
			Respawn (this.gameObject);

		}
	}

	bool isGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, distToGround + 0.1f);
	}

}
