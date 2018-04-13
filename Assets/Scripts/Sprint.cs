using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {

    private float cooldown, timeAb, speed;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    bool ab1 = false, ab2 = false;
    // Use this for initialization
    void Start()
    {
        used = false;
        cooldown = 0;
        speed = this.gameObject.GetComponent<PlayerControl>().GetSpeed();
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
                this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed);
            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used)
        {
            this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed * 1.7f);
            hab = true;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used)
        {
            this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed * 1.7f);
            hab = true;
        }
    }


}
