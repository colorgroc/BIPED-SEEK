using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public float speedRotation;
	public GameObject[] players;
	public float jumpSpeed = 100.0F;
	/*[HideInInspector]
	public bool detected1;
	[HideInInspector]
	public bool detected2;
	[HideInInspector]
	public bool detected3;
	[HideInInspector]
	public bool detected4;

	public bool onFieldView_1;
	public bool onFieldView_2;
	public bool onFieldView_3;
	public bool onFieldView_4;*/

	public GameObject target;
	/*private float timePast1;
	private float timePast2;
	private float timePast3;
	private float timePast4;*/
	public bool isDead;
	private float count;

	//private GameObject[] allWaypoints;
	public int random;
	//private NPCConnectedPatrol npc;



	void Start()
	{


		/*timePast1 = 0;
		timePast2 = 0;
		timePast3 = 0;
		timePast4 = 0;*/
		 
		this.gameObject.GetComponent<Light> ().enabled = false;

	}
	// Update is called once per frame
	void Update()
	{


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

			if (Input.GetKeyDown (KeyCode.P)) {
				Debug.Log ("Kill");
			}

		} else if (this.gameObject.tag.Equals ("Player 2")) {
			float x = Input.GetAxis ("Horizontal 2") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical 2") * Time.deltaTime;
			transform.Translate (0, 0, y * speed);
			transform.Rotate (0, x * speedRotation, 0);

			if (Input.GetKeyDown (KeyCode.E)) {

			}
		}

		/*if (this.gameObject.tag.Equals ("Player 1")) {
			if (detected1) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast1 += Time.deltaTime;
				if (timePast1 > 5) {
					detected1 = false;
				}

			} else if(!detected1) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast1 = 0;
			}
		} else if (this.gameObject.tag.Equals ("Player 2")) {
			if (detected2) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast2 += Time.deltaTime;
				if (timePast2 > 5) {
					detected2 = false;

				}

			} else if(!detected2) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast2 = 0;
			}
		}else if (this.gameObject.tag.Equals ("Player 3")) {
			if (detected3) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast3 += Time.deltaTime;
				if (timePast3 > 5)
					detected3 = false;

			} else if(!detected3){
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast3 = 0;
			}
		}else if (this.gameObject.tag.Equals ("Player 4")) {
			if (detected4) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast4 += Time.deltaTime;
				if (timePast4 > 5)
					detected4 = false;

			} else if(!detected4) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast4 = 0;
			}
		}*/

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



	void Jump(float forceJump)
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Rigidbody>().AddForce(0, forceJump, 0, ForceMode.Impulse);

			// Debug.Log("salt");
		}
	}


	public void Kill(GameObject gO){
		if (gO.gameObject.tag.Equals ("Guard") || gO.gameObject.tag.Equals ("Killler Guards")) {
			Destroy (gO);
			//puntuacio -100;
		}else if(gO.gameObject.layer == 8 && gO != ControlScript.objective){
			//puntuacio -50;
			Respawn(gO);
		}else if(gO.gameObject.layer == 8 && gO == ControlScript.objective){
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
		GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		int random = UnityEngine.Random.Range (0, allWaypoints.Length);
		gO.gameObject.transform.position = allWaypoints [random].transform.position;
		gO.gameObject.SetActive (true);
		//this.enabled = true;
		//this.transform.position = pos;
		isDead = false;

	}
	/*void OnCollisionEnter(Collision collision){
		if (collision.gameObject.layer == 8)) {
			//_waypointsVisited++;
			collision.gameObject.SetActive (false);
			collision.gameObject.GetComponent<Player>().Respawn();

		}
	}*/

}
