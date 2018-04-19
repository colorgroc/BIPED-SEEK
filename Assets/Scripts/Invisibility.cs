using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour {

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    // Use this for initialization
    void Start()
    {
        used = false;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //NewContol.guards;
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
                this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }

        if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used)
        {
            this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
            hab = true;
        } 
    }

}
