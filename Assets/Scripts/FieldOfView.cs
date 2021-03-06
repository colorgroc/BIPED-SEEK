﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	[SerializeField]
	public LayerMask targetMask;
	public LayerMask obstacleMask;
    private Animator anim;

    [HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	//public bool alive;
    private float distancia;

	public void Start()
    {
        this.anim = this.gameObject.GetComponent<Animator>();
        viewAngle = 119;
		viewRadius = 30;
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}
	void Update(){
    }
		
	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}


	void FindVisibleTargets() {
       this.visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.transform.position); //canviar dist to target?? --> es podria fer un altre cercle per a l'atac sino
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) 
                {
                    this.visibleTargets.Add(target);
                   
                    if (this.gameObject.layer == 8)
                    {
                        if (target.gameObject.layer == 8)
                        {
                            //Amb llum
                            //target.gameObject.GetComponent<PlayerControl>().detected = true; 
                            //Amb feedback
                            this.gameObject.GetComponent<PlayerControl>().detected = true;
                            //WannaKill(target);
                        }
                        //else
                        //{
                        //    WannaKill(target);
                        //}
                    }
                }

            }
 
        }

    }

    private void WannaKill(Transform target)
    {
        if (this.gameObject.GetComponent<PlayerControl>().wannaKill) //&& distToTarget < distanciaQueVolem 
        {
            this.visibleTargets.Remove(target);
            //if (AnimatorIsPlaying("Punch"))
            this.gameObject.GetComponent<PlayerControl>().Kill(target.gameObject);
            this.gameObject.GetComponent<PlayerControl>().wannaKill = false;
        }
        
    }

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public IEnumerator FadeOut(GameObject gO)
    {
		Color colorStart = gO.gameObject.GetComponent<Renderer> ().material.color;
		Color colorEnd = new Color(0, 1, 1, 1);
		gO.gameObject.GetComponent<Renderer> ().material.color = colorEnd;
        yield return true;
	}
}
