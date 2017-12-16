using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    public float speed;
	public float speedRotation;
    public GameObject[] players;
    public float jumpSpeed = 100.0F;

	//Vector3 velocity;
	//Rigidbody myRigidbody;
    // public float gravity = 20.0F;
    // float y = 0;
    void Start()
    {
		 //myRigidbody = GetComponent<Rigidbody> ();
         players = GameObject.FindGameObjectsWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
		if (this.gameObject.tag.Equals ("Player 1")) {
			float x = Input.GetAxis ("Horizontal") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical") * Time.deltaTime;
			transform.Translate(0, 0, y * speed);
			transform.Rotate(0, x * speedRotation, 0);

		} else if (this.gameObject.tag.Equals ("Player 2")) {
			float x = Input.GetAxis ("Horizontal 2") * Time.deltaTime;
			float y = Input.GetAxis ("Vertical 2") * Time.deltaTime;
			transform.Translate(0, 0, y * speed);
			transform.Rotate(0, x * speedRotation, 0);
		}

        //transform.position += new Vector3(0, 0, y * speed);
       

		
		//velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * speed;
       /* float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);*/

        //Jump(jumpSpeed);

		/*if (Input.GetKey (KeyCode.A)) {
			players [0].transform.Translate (Vector3.left * speed * Time.deltaTime);
			//players[0].transform.Rotate(Vector3.left * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			players [0].transform.Translate (Vector3.right * speed * Time.deltaTime);
			//players [0].transform.Rotate (Vector3.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.W)) {
			players [0].transform.Translate (Vector3.forward * speed * Time.deltaTime);
			//players [0].transform.Rotate (Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			players [0].transform.Translate (Vector3.back * speed * Time.deltaTime);
			//players [0].transform.Rotate (Vector3.back * speed * Time.deltaTime);
		}*/

       /* if (Input.GetKey(KeyCode.LeftArrow))
            players[1].transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            players[1].transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.UpArrow))
            players[1].transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            players[1].transform.Translate(Vector3.back * speed * Time.deltaTime);*/
    }

	/*void FixedUpdate() {
		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
	}*/

    void Jump(float forceJump)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(0, forceJump, 0, ForceMode.Impulse);

           // Debug.Log("salt");
        }
    }

}
