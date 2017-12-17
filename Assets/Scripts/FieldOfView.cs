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

	void Start() {
		//targetMask = LayerMask.NameToLayer ("Player");
		//Debug.Log (targetMask.value);
		viewAngle = 119;
		viewRadius = 30;
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}


	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}


	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			GameObject target = targetsInViewRadius [i].transform.gameObject;
			Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.transform.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
					if (this.gameObject.tag.Equals("Player 1")) {
						if (target.gameObject.tag.Equals ("Player 2")) {
							ControlScript.detected2 = true;
							target.gameObject.GetComponent<Player> ().target = target;
						} else if (target.gameObject.tag.Equals ("Player 3")) {
							ControlScript.detected3 = true;
							target.gameObject.GetComponent<Player> ().target = target;
						} else if (target.gameObject.tag.Equals ("Player 4")) {
							ControlScript.detected4 = true;
							target.gameObject.GetComponent<Player> ().target = target;
						} else {
							if (ControlScript.player_1_WannaKill) {
								this.gameObject.GetComponent<Player> ().Kill (target.gameObject);
								ControlScript.player_1_WannaKill = false;
							}
						}

					} else if (this.gameObject.tag.Equals("Player 2")) {
						if(target.gameObject.tag.Equals("Player 1")) ControlScript.detected1 = true;
						if(target.gameObject.tag.Equals("Player 3")) ControlScript.detected3 = true;
						if(target.gameObject.tag.Equals("Player 4")) ControlScript.detected4 = true;

						target.gameObject.GetComponent<Player> ().target = target;

						if (ControlScript.player_2_WannaKill && dstToTarget < 5) {
							this.gameObject.GetComponent<Player> ().Kill (target.gameObject);
							ControlScript.player_2_WannaKill = false;
						}

					} else if (this.gameObject.tag.Equals("Player 3")) {
						if(target.gameObject.tag.Equals("Player 1")) ControlScript.detected1 = true;
						if(target.gameObject.tag.Equals("Player 2")) ControlScript.detected2 = true;
						if(target.gameObject.tag.Equals("Player 4")) ControlScript.detected4 = true;

						target.gameObject.GetComponent<Player> ().target = target;

					}else if (this.gameObject.tag.Equals("Player 4")) {
						if(target.gameObject.tag.Equals("Player 1")) ControlScript.detected1 = true;
						if(target.gameObject.tag.Equals("Player 2")) ControlScript.detected2 = true;
						if(target.gameObject.tag.Equals("Player 3")) ControlScript.detected3 = true;

						target.gameObject.GetComponent<Player> ().target = target;
					}

				} else { 
					if(this.gameObject.tag.Equals("Player 1")){
						if (target.gameObject.tag.Equals ("Player 2")) {
							ControlScript.detected2 = false;
							//target.gameObject.GetComponent<Player> ().timePast2 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 3")) {
							ControlScript.detected3 = false;
							//target.gameObject.GetComponent<Player> ().timePast3 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 4")) {
							ControlScript.detected4 = false;
							//target.gameObject.GetComponent<Player> ().timePast4 = 0;
						}

					}else if(this.gameObject.tag.Equals("Player 2")){

						if (target.gameObject.tag.Equals ("Player 1")) {
							ControlScript.detected1 = false;
							//target.gameObject.GetComponent<Player> ().timePast1 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 3")) {
							ControlScript.detected3 = false;
							//target.gameObject.GetComponent<Player> ().timePast3 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 4")) {
							ControlScript.detected4 = false;
							//target.gameObject.GetComponent<Player> ().timePast4 = 0;
						}

					} else if(this.gameObject.tag.Equals("Player 3")){
						if (target.gameObject.tag.Equals ("Player 1")) {
							ControlScript.detected1 = false;
							//target.gameObject.GetComponent<Player> ().timePast1 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 2")) {
							ControlScript.detected2 = false;
							//target.gameObject.GetComponent<Player> ().timePast2 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 4")) {
							ControlScript.detected4 = false;
							//target.gameObject.GetComponent<Player> ().timePast4 = 0;
						}

					}else if(this.gameObject.tag.Equals("Player 4")){
						if (target.gameObject.tag.Equals ("Player 1")) {
							ControlScript.detected1 = false;
							//target.gameObject.GetComponent<Player> ().timePast1 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 2")) {
							ControlScript.detected2 = false;
							//target.gameObject.GetComponent<Player> ().timePast2 = 0;
						}
						if (target.gameObject.tag.Equals ("Player 3")) {
							ControlScript.detected3 = false;
							//target.gameObject.GetComponent<Player> ().timePast3 = 0;
						}
					}
				}

			} else {
				if(this.gameObject.tag.Equals("Player 1")){
					if (target.gameObject.tag.Equals ("Player 2")) {
						ControlScript.detected2 = false;
						//target.gameObject.GetComponent<Player> ().timePast2 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 3")) {
						ControlScript.detected3 = false;
						//target.gameObject.GetComponent<Player> ().timePast3 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 4")) {
						ControlScript.detected4 = false;
						//target.gameObject.GetComponent<Player> ().timePast4 = 0;
					}

				}else if(this.gameObject.tag.Equals("Player 2")){
					if (target.gameObject.tag.Equals ("Player 1")) {
						ControlScript.detected1 = false;
						//target.gameObject.GetComponent<Player> ().timePast1 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 3")) {
						ControlScript.detected3 = false;
						//target.gameObject.GetComponent<Player> ().timePast3 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 4")) {
						ControlScript.detected4 = false;
						//target.gameObject.GetComponent<Player> ().timePast4 = 0;
					}

				}else if(this.gameObject.tag.Equals("Player 3")){
					if (target.gameObject.tag.Equals ("Player 1")) {
						ControlScript.detected1 = false;
						//target.gameObject.GetComponent<Player> ().timePast1 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 2")) {
						ControlScript.detected2 = false;
						//target.gameObject.GetComponent<Player> ().timePast2 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 4")) {
						ControlScript.detected4 = false;
						//target.gameObject.GetComponent<Player> ().timePast4 = 0;
					}

				}else if(this.gameObject.tag.Equals("Player 4")){
					if (target.gameObject.tag.Equals ("Player 1")) {
						ControlScript.detected1 = false;
						//target.gameObject.GetComponent<Player> ().timePast1 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 2")) {
						ControlScript.detected2 = false;
						//target.gameObject.GetComponent<Player> ().timePast2 = 0;
					}
					if (target.gameObject.tag.Equals ("Player 3")) {
						ControlScript.detected3 = false;
						//target.gameObject.GetComponent<Player> ().timePast3 = 0;
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
}
