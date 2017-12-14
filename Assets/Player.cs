using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    public float speed = 6.0F;
    public GameObject[] players;
    public float jumpSpeed = 100.0F;
    // public float gravity = 20.0F;
    // float y = 0;
    void Start()
    {
         players = GameObject.FindGameObjectsWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        /*float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        Jump(jumpSpeed);*/

        if (Input.GetKey(KeyCode.A))
            players[0].transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            players[0].transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            players[0].transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            players[0].transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            players[1].transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            players[1].transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.UpArrow))
            players[1].transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            players[1].transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void Jump(float forceJump)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(0, forceJump, 0, ForceMode.Impulse);

           // Debug.Log("salt");
        }
    }

}
