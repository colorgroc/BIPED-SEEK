using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWaypoint : Waypoint {

	[SerializeField]
	protected float _connectivityRadius = 50f;

	List<ConnectedWaypoint> _connections;

	// Use this for initialization
	public void Start () {
		GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		_connections = new List<ConnectedWaypoint> ();

		for (int i = 0; i < allWaypoints.Length; i++) {
			ConnectedWaypoint nextWaypoint = allWaypoints [i].GetComponent<ConnectedWaypoint> ();
			if (nextWaypoint != null) {
				if (Vector3.Distance (this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this) {
					_connections.Add (nextWaypoint);
				}
			}
		}
	}

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, debugDragRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _connectivityRadius);
    }

    public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint){
		if (_connections.Count == 0) {
			//no hi ha waypoints, retorna null
			Debug.LogError ("Insuficient waypoint count.");
			return null;
		} else if (_connections.Count == 1 && _connections.Contains (previousWaypoint)) {
			//nomes un waypoint, usa aquest
			return previousWaypoint;
		} else {
			ConnectedWaypoint nextWaypoint;
			int nextIndex = 0;
			do {
				nextIndex = UnityEngine.Random.Range (0, _connections.Count);
				nextWaypoint = _connections [nextIndex];
			} while(nextWaypoint == previousWaypoint);

			return nextWaypoint;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
