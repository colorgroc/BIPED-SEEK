using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Rabbit : MonoBehaviour {

    public GameObject rabbit;
    public float radius;
    public GameObject explosion;

    void Start()
    {

    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.R))
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
