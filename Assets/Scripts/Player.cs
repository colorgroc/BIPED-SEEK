using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public float speedRotation;
	public GameObject[] players;
	public float jumpSpeed = 100.0F;
	[HideInInspector]
	public bool playerOnFieldView1;
	[HideInInspector]
	public bool playerOnFieldView2;
	[HideInInspector]
	public bool playerOnFieldView3;
	[HideInInspector]
	public bool playerOnFieldView4;

	public GameObject target;
	private float timePast1;
	private float timePast2;
	private float timePast3;
	private float timePast4;



	void Start()
	{
		timePast1 = 0;
		timePast2 = 0;
		timePast3 = 0;
		timePast4 = 0;

		this.gameObject.GetComponent<Light> ().enabled = false;

	}
	// Update is called once per frame
	void Update()
	{
		if (this.gameObject.tag.Equals ("Player 1")) {
			float x = Input.GetAxis ("Horizontal") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical") * Time.deltaTime;
			transform.Translate (0, 0, y * speed);
			transform.Rotate (0, x * speedRotation, 0);

		} else if (this.gameObject.tag.Equals ("Player 2")) {
			float x = Input.GetAxis ("Horizontal 2") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical 2") * Time.deltaTime;
			transform.Translate (0, 0, y * speed);
			transform.Rotate (0, x * speedRotation, 0);
		}

		if (this.gameObject.tag.Equals ("Player 1")) {
			if (playerOnFieldView1) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast1 += Time.deltaTime;
				if (timePast1 > 5) {
					playerOnFieldView1 = false;
				}

			} else if(!playerOnFieldView1) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast1 = 0;
			}
		} else if (this.gameObject.tag.Equals ("Player 2")) {
			if (playerOnFieldView2) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast2 += Time.deltaTime;
				if (timePast2 > 5) {
					playerOnFieldView2 = false;

				}

			} else if(!playerOnFieldView2) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast2 = 0;
			}
		}else if (this.gameObject.tag.Equals ("Player 3")) {
			if (playerOnFieldView3) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast3 += Time.deltaTime;
				if (timePast3 > 5)
					playerOnFieldView3 = false;

			} else if(!playerOnFieldView3){
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast3 = 0;
			}
		}else if (this.gameObject.tag.Equals ("Player 4")) {
			if (playerOnFieldView4) {
				this.gameObject.GetComponent<Light> ().enabled = true;
				timePast4 += Time.deltaTime;
				if (timePast4 > 5)
					playerOnFieldView4 = false;

			} else if(!playerOnFieldView4) {
				this.gameObject.GetComponent<Light> ().enabled = false;
				timePast4 = 0;
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


	void Kill(GameObject gO){
		if (Input.GetKeyDown (KeyCode.E) && this.gameObject.tag.Equals ("Player 2") ) {
			Destroy (gO);
		}
		else if (Input.GetKeyDown (KeyCode.P) && this.gameObject.tag.Equals ("Player 1") ) {
			Destroy (gO);
		}
	}

}
