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
            if (other.gameObject != null)// && other.gameObject.name != this.gameObject.transform.parent.name)
            {
                Debug.Log("Killed");
                this.gameObject.GetComponentInParent<PlayerControl>().Kill(other.gameObject);
            }

            this.gameObject.GetComponentInParent<PlayerControl>().wannaKill = false;
        }
    }
}
