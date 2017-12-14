using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E01 : MonoBehaviour
{

    private Vector3 target;
    //private GameObject target;
    public int steering_behavior;
    public Rigidbody[] rbPlayers;
    public float dis;
    public float newDis;
    public int j;
    public float player1dis;
    public float player2dis;

    // Use this for initialization
    void Start()
    {
        dis = 15F;
        j = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        rbPlayers = new Rigidbody[players.Length];
        for(int i = 0; i < rbPlayers.Length; i++)
        {
            rbPlayers[i] = players[i].GetComponent<Rigidbody>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        player1dis = Vector3.Distance(rbPlayers[0].transform.position, transform.position);
        player2dis = Vector3.Distance(rbPlayers[1].transform.position, transform.position);
        if(player1dis < player2dis) target = rbPlayers[0].transform.position;
        else if(player2dis < player1dis) target = rbPlayers[1].transform.position;
       

        /* for (int i = 0; i < rbPlayers.Length; i++) {
             newDis = Vector3.Distance(rbPlayers[i].transform.position, transform.position);
             //if (dis == 0) dis = newDis;
             if (newDis <= dis)
             {

                 //dis = newDis;
                 j = i;
                 //newDis = Vector3.Distance(rbPlayers[i].transform.position, transform.position);
                Debug.Log("entro");

             }
            // Debug.Log("i: " + i);
         }
         //Debug.Log("j: " + j);
         target = rbPlayers[j].transform.position;*/
        // target = rbPlayers[j].gameObject;

        switch (steering_behavior)
        {
            case 1:
                //Seek(transform, target.transform.position, Time.deltaTime, true);
                Seek(transform, target, 0.01F, true);
                break;
            case 2:
                Wander(transform, 10.0f, 100.0f, Time.deltaTime);
                break;
        }

    }

    void Seek(Transform agent, Vector3 target, float dt, bool lookAtTarget)
    {
        Vector3 DesiredVelocity = target - agent.position;
        if (lookAtTarget)
            transform.LookAt(target);
        transform.position += DesiredVelocity * dt;
    }

    void Wander(Transform agent, float wanderRadius, float wanderOffset, float dt)
    {
        float maxChange = 120.0f;

        Vector3 circle;
        float wanderAngle = 0.0f;

        wanderAngle += Random.Range(-1, 1) * maxChange;


        circle = agent.position + GetComponent<Rigidbody>().velocity.normalized * wanderOffset;

        Vector3 target_ = new Vector3(0, 0, 0);

        target_.x = circle.x + wanderRadius * Mathf.Cos(wanderAngle * Mathf.Deg2Rad);
        target_.z = circle.z + wanderRadius * Mathf.Sin(wanderAngle * Mathf.Deg2Rad);
        target_.y = agent.position.y;

        Seek(agent, target_, dt, false);

    }




}

