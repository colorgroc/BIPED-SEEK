using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	[SerializeField]
	protected float debugDragRadius = 1.0F;

	public virtual void OnDrawGizmos(){
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere (transform.position, debugDragRadius);
	}
}
