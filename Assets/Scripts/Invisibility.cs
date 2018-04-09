using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour {

    private float cooldown;
    private bool hab;
    [SerializeField]
    private int coolDown = 10;
    // Use this for initialization
    void Start()
    {
        hab = false;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //NewContol.guards;
        if (hab)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= coolDown)
            {
                hab = false;
                cooldown = 0;
            }
        }
        if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !hab)
        {
            this.gameObject.GetComponent<>
        }
    }

}
