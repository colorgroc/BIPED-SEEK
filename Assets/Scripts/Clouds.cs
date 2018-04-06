using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField]
    private float velocity = 1f;
    [SerializeField]
    private Transform final, respawn;

   void Start () {

    }


    void Update()
    {
        this.transform.Translate(0, 0, velocity);
        if (this.transform.position.x >= final.position.x)
        {
            this.transform.position = new Vector3(respawn.position.x, this.transform.position.y, this.transform.position.z);
        }
    }
}
