﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    private float cooldown;
    private bool hab;
    [SerializeField]
    private int coolDown = 10;
    List<GameObject> guardsList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        hab = false;
        cooldown = 0;
        foreach(GameObject guard in NewControl.guards)
        {
            if(guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
            {
                guardsList.Add(guard);
            }
        }
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
        int random = Random.Range(0, guardsList.Count);
        Vector3 newGuardPos = this.gameObject.transform.position;
        this.gameObject.transform.position = guardsList[random].transform.position;
        NewControl.guards[random].transform.position = newGuardPos;
    }
}