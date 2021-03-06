﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.GetComponentInParent<PlayerControl>().wannaKill)
        {
            if (other.gameObject != null && other.gameObject.tag != "Death" && other.gameObject.name != "mixamorig:LeftHand")
            {
                if(other.gameObject != NewControl.objective)
                    Death.AnimDeath(other.gameObject, other.gameObject.transform.position, other.gameObject.transform.rotation);
                this.gameObject.GetComponentInParent<PlayerControl>().Kill(other.gameObject);     
            }

            this.gameObject.GetComponentInParent<PlayerControl>().wannaKill = false;
        }
    }
}
