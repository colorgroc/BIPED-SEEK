using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour {

    [SerializeField]
    private float radius = 30f, power = 20f;

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    bool ab1 = false, ab2 = false;
    // Use this for initialization
    void Start()
    {
        used = false;
        cooldown = 0;
    }

    public void Update()
    {
        if (used)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= coolDown)
            {
                used = false;
                cooldown = 0;
            }
        }

        if (hab)
        {
            timeAb += Time.deltaTime;
            if (timeAb >= timeAbility)
            {
                used = true;
                hab = false;
                timeAb = 0;
            }
        }

        /* if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
         {
             Repelation();
             hab = true;
         }
         else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
         {
             Repelation();
             hab = true;
         }*/
        if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            Debug.Log("Repel");
            Repelation();
            hab = true;
        }
       
    }

    void Repelation()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hit in colliders)
        {
 
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && this.gameObject.GetComponent<Rigidbody>() != rb)
            {
                
                Debug.Log("boom");
               // hit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                Vector3 dirToTarget = -(rb.transform.position - transform.position).normalized;
                float dstToTarget = Vector3.Distance(transform.position, rb.transform.position);

                //if (rb.gameObject.tag.Equals("Guard"))
                    //rb.gameObject.GetComponent<NPCConnectedPatrol>().freezed = true;
                //{
                //    rb.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(dirToTarget*power);
                //}
                // Vector3 impulso = (dirToTarget/dstToTarget)*power;
                // rb.AddForce(impulso);
               // rb.AddForce(dirToTarget * power);
               // rb.AddExplosionForce(power, this.transform.position, radius, 3.0F);
                //hit.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            }
        }
    }


}
