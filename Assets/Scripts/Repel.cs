using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour {

    [SerializeField]
    private float radius = 10f, power = 5f;

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

       /* if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used)
        {
            Repelation();
            hab = true;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used)
        {
            Repelation();
            hab = true;
        }*/
        if(Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used)
        {
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

            if (rb != null)
                rb.AddExplosionForce(power, this.transform.position, radius, 3.0F);
        }
    }


}
