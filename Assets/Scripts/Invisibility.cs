using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour {

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

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
            hab = true;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
            hab = true;
        }

        //if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        //{
        //    Debug.Log("Invisible");
        //    this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
        //    hab = true;
        //} 
    }

}
