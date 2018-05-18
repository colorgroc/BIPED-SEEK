using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.GetComponentInParent<PlayerControl>().wannaKill)
        {
            if (other.gameObject != null && other.gameObject.tag != "Death")// && other.gameObject.name != this.gameObject.transform.parent.name)
            {
                Death.AnimDeath(other.gameObject, other.gameObject.transform.position, other.gameObject.transform.rotation);
                this.gameObject.GetComponentInParent<PlayerControl>().Kill(other.gameObject);     
            }

            this.gameObject.GetComponentInParent<PlayerControl>().wannaKill = false;
        }
    }
}
