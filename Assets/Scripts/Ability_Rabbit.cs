using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Rabbit : MonoBehaviour {

    public GameObject rabbit;
    public float radius;
    public GameObject explosion;
    [HideInInspector]
    public string hab1Button;

    void Start()
    {
        if (this.gameObject.name.Equals("Player_1"))
        {
            this.hab1Button = PlayerPrefs.GetString("Hab1_P1");
        }
        else if (this.gameObject.name.Equals("Player_2"))
        {
            this.hab1Button = PlayerPrefs.GetString("Hab1_P2");

        }
        else if (this.gameObject.name.Equals("Player_3"))
        {
            this.hab1Button = PlayerPrefs.GetString("Hab1_P3");
        }
        else if (this.gameObject.name.Equals("Player_4"))
        {
            this.hab1Button = PlayerPrefs.GetString("Hab1_P4");
        }
    }

    void Update () {

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            Multiply();        
        }*/

        if (Input.GetButtonDown(this.hab1Button))
        {
            Multiply();
        }

	}

    void Multiply()
    {
        int randomRabbits = Random.Range(2, 8);

        for (int i = 0; i < randomRabbits; i++)
        {
            Vector3 rabbitPos = randomCircle();       
            GameObject copia = Instantiate(rabbit, rabbitPos, transform.rotation) as GameObject;
        }

        Vector3 ranPlayer = randomCircle();
        Instantiate(explosion, transform.position + new Vector3(0,1,0), transform.rotation);
        transform.position = ranPlayer;

    }
        
    Vector3 randomCircle()
    {
        float randomAngle = Random.Range(0, 360);

        Vector3 rabbitPos = new Vector3();

        rabbitPos.x = transform.position.x + Mathf.Cos(randomAngle) * radius;
        rabbitPos.y = transform.position.y;
        rabbitPos.z = transform.position.z + Mathf.Sin(randomAngle) * radius;

        return rabbitPos;
    }

}
