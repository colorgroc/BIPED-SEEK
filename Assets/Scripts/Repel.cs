using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour {

    public float viewRadius = 30;
    [Range(0, 360)]
    public float viewAngle = 119;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private float radius = 10f, power = 5f;

    private float cooldown, timeAb, speed;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    // Use this for initialization
    void Start()
    {
        used = false;
        cooldown = 0;
        speed = this.gameObject.GetComponent<PlayerControl>().GetSpeed();
    }

    public void Update()
    {
        if (used)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= timeAbility)
            {
                used = false;
                cooldown = 0;
            }
        }

        if (hab)
        {
            timeAb += Time.deltaTime;
            if (timeAb >= 10)
            {
                used = true;
                hab = false;
                timeAb = 0;
            }
        }

        if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used)
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
