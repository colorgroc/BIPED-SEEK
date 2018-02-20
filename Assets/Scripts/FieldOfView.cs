using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {
	//public Player player;
	//public NPCConnectedPatrol npc;
	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	[SerializeField]
	public LayerMask targetMask;
	public LayerMask obstacleMask;

    [HideInInspector]
	//public List<Transform> visibleTargets = new List<Transform>();
	public List<GameObject> visibleTargets = new List<GameObject>();
	private Collider[] targetsInViewRadius;
	public bool alive;

	void Start() {
		//targetMask = LayerMask.NameToLayer ("Player");
		//Debug.Log (targetMask.value);
		this.visibleTargets.Clear ();
		viewAngle = 119;
		viewRadius = 30;
		//StartCoroutine ("FindTargetsWithDelay", .2f);
	}
	void Update(){
		FindVisibleTargets ();
		//Debug.Log (alive);
		if (this.alive) {
			this.visibleTargets.Clear ();
			this.alive = false;
			//StartCoroutine ("FindTargetsWithDelay", .2f); //no funciona, preguntar a algu o intentar fer que quan et moris crear un nou gameobject player
		}
	}
		
	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}


	void FindVisibleTargets() {
		//this.visibleTargets.Clear ();
		//if(alive) {this.visibleTargets.Clear (); alive = false;}
		targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			GameObject target = targetsInViewRadius [i].transform.gameObject;
			Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    this.visibleTargets.Add(target);
                   
                    if (this.gameObject.layer == 8)
                    {
                        if (target.gameObject.layer == 8)
                        {
                            //ControlScript.detected2 = true;
                            target.gameObject.GetComponent<PlayerControl>().detected = true;
                            if (this.gameObject.GetComponent<PlayerControl>().wannaKill)
                            {
								//Debug.Log("Killing");
                                this.visibleTargets.Remove(target);
                                this.gameObject.GetComponent<PlayerControl>().Kill(target.gameObject);
                                this.gameObject.GetComponent<PlayerControl>().wannaKill = false;
                            }
                        }
                        else
                        {
							//if (!target.gameObject.layer == 8) {
								if (this.gameObject.GetComponent<PlayerControl> ().wannaKill) {
									//Debug.Log("Killing");
									this.visibleTargets.Remove (target);
									this.gameObject.GetComponent<PlayerControl> ().Kill (target.gameObject);
									this.gameObject.GetComponent<PlayerControl> ().wannaKill = false;
								}
							//}
                        }
                    }
                }
                else
                {

                    if (this.gameObject.layer == 8 && target.gameObject.layer == 8)
                    {

                        foreach (GameObject player in NewControl.players)
                        {
                            if(player.gameObject != this.gameObject)
                                player.GetComponent<PlayerControl>().detected = false;
                        }

                    }
                }

            }
            else
            {
                if (this.gameObject.layer == 8 && target.gameObject.layer == 8)
                {
                    foreach (GameObject player in NewControl.players)
                    {
                        if (player.gameObject != this.gameObject)
                            player.GetComponent<PlayerControl>().detected = false;
                    }

                }
            }

                   
		}

	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public IEnumerator FadeOut(GameObject gO){
		Color colorStart = gO.gameObject.GetComponent<Renderer> ().material.color;
		Color colorEnd = new Color(0, 1, 1, 1);
		//for (float t = 0f; t < 1.0f; t += Time.deltaTime) {
		gO.gameObject.GetComponent<Renderer> ().material.color = colorEnd;
		yield return true;
		//}
	}
	/*private void FadeOut(GameObject gO){
		//Color colorStart = gO.gameObject.GetComponent<Renderer> ().material.color;
		Color colorEnd = new Color(0, 1, 1, 1);
		//gO.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (colorStart, colorEnd, Mathf.PingPong(Time.deltaTime, 1));
		gO.gameObject.GetComponent<Renderer> ().material.color = colorEnd;
	}*/
}
