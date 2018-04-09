using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immobilizer : MonoBehaviour {

 //   private SphereCollider zone;
 //   private float time = 7.0f;
	
 //   void Start()
 //   {
 //       zone.radius = 10.0f;
 //   }

	//void Update () {

 //       zone.isTrigger = true;
 //       Destroy(gameObject.GetComponent<Immobilizer>(), 10.0f);

 //   }

 //   private void OnTriggerStay(Collider other)
 //   {

 //       if (Input.GetButtonDown(gameObject.GetComponent<PlayerControl>().hab1Button))
 //       {
 //           Timer_Time(other);
 //       }

 //   }

 //   private void Timer_Time(Collider other)
 //   {

 //       if (time > 0)
 //       {
 //           time -= Time.deltaTime;

 //           if (other.gameObject.tag == "Player")
 //           {
 //               gameObject.GetComponent<PlayerControl>().speed = 0;
 //           }

 //           if (other.gameObject.tag == "Npc")
 //           {
 //               gameObject.GetComponent<NPCConnectedPatrol>()._travelling = false;
 //           }

 //       } else
 //       {
 //           gameObject.GetComponent<PlayerControl>().speed = PlayerControl.defaultSpeed;
 //           gameObject.GetComponent<NPCConnectedPatrol>()._travelling = true;
 //       }

 //   }

}
