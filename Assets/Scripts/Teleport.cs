using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    private float cooldown;
    private bool hab;
    [SerializeField]
    private int coolDown = 10;
	// Use this for initialization
	void Start () {
        hab = false;
        cooldown = 0;
	}
	
	// Update is called once per frames
	void Update () {
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
        if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !hab)
        {
            TeleportHability();
            hab = true;   
        }
	}

    void TeleportHability()
    {
        int random = Random.Range(0, NewControl.guards.Length);
        Vector3 newGuardPos = this.gameObject.transform.position;
        this.gameObject.transform.position = NewControl.guards[random].transform.position;
        NewControl.guards[random].transform.position = newGuardPos;
    }
}
