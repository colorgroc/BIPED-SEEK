using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfViewKillers : MonoBehaviour
{
	//public float dstToTarget;
	//public Player player;
	//public NPCConnectedPatrol npc;
	public float viewRadius;
	[Range (0, 360)]
	public float viewAngle;

	[SerializeField]
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<GameObject> visibleTargets = new List<GameObject> ();

	void Start ()
	{
		viewAngle = 119;
		viewRadius = 30;
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}

	void Update(){
		if (!this.gameObject.GetComponent<KillersProperties> ().follow) 
			this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
	}

	IEnumerator FindTargetsWithDelay (float delay)
	{
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}


	void FindVisibleTargets ()
	{
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			GameObject target = targetsInViewRadius [i].transform.gameObject;
			Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.transform.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
					this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = true;
					this.gameObject.GetComponent<NPCConnectedPatrol> ().playerTarget = target;
					//this.gameObject.GetComponent<KillersProperties>().follow = true;
				} else { 
					Debug.Log ("Woha");
					//this.gameObject.GetComponent<KillersProperties>().follow = false;
					this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
					//this.gameObject.GetComponent<NPCConnectedPatrol> ().playerTarget = null;
				}
				if (dstToTarget >= 30) {
					this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
				}
			} /*else if (Vector3.Angle (transform.forward, dirToTarget) >= viewAngle / 2 && this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView) {
				Debug.Log ("work bitch");
				this.gameObject.GetComponent<KillersProperties>().follow = false;
				this.gameObject.GetComponent<NPCConnectedPatrol>().playerOnFieldView = false;
			}*/
			else //this.gameObject.GetComponent<KillersProperties>().follow = false;
				this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
		}

		//this.gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
	}

	/*void FindVisibleTargets ()
	{
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			GameObject target = targetsInViewRadius [i].transform.gameObject;
			Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.transform.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
					for(int j = 0; j < ControlScript.killers.Length; j++){
						
						if(this.gameObject.Equals(ControlScript.killers[j].gameObject)){
							ControlScript.killers[j].gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = true;
							ControlScript.killers[j].gameObject.GetComponent<NPCConnectedPatrol> ().playerTarget = target;
						}
					}

				} else { 
					for(int j = 0; i < ControlScript.killers.Length; j++){
						if(this.gameObject.Equals(ControlScript.killers[j].gameObject)){
							ControlScript.killers[j].gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
						}
					}
				}

			} else
				for(int j = 0; i < ControlScript.killers.Length; j++){
					if(this.gameObject.Equals(ControlScript.killers[j].gameObject)){
						ControlScript.killers[j].gameObject.GetComponent<NPCConnectedPatrol> ().playerOnFieldView = false;
					}
				}
		}
	}*/

	public Vector3 DirFromAngle (float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3 (Mathf.Sin (angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));
	}
}
