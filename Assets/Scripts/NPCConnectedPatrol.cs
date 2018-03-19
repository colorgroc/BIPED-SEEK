using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCConnectedPatrol : MonoBehaviour {

	[SerializeField]
	bool _patrolWaiting;

	[SerializeField]
	float _totalWaitTime = 3f;

	NavMeshAgent _navMeshAgent;
	ConnectedWaypoint _currentWaypoint;
	ConnectedWaypoint _previousWaypoint;
	public GameObject[] allWaypoints;

	public GameObject playerTarget;

	bool _travelling;
	bool waiting;
	public bool playerOnFieldView;

	float _waitTimer;
	int _waypointsVisited;
	//Player player;

	public bool isDead;
	public float count;

    private Animator anim;

    // Use this for initialization
    private void Awake()
    {
        
    }
    public void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.anim = this.gameObject.GetComponent<Animator>();
        if (_navMeshAgent == null) Debug.LogError ("The nav mesh agent component is not attached to " + gameObject.name);
		else {
			if (_currentWaypoint == null) {
				allWaypoints = GameObject.FindGameObjectsWithTag ("Waypoint");

				if (allWaypoints.Length > 0) {
					while (_currentWaypoint == null) {
						int random = UnityEngine.Random.Range (0, allWaypoints.Length);
						ConnectedWaypoint startingWaypoint = allWaypoints [random].GetComponent<ConnectedWaypoint> ();

						if (startingWaypoint != null) _currentWaypoint = startingWaypoint;
					}
				} else Debug.LogError ("Failed to find any waypoints for use in the scene");		
			}
		}
		SetDestination ();
	}
	
	// Update is called once per frame
	public void Update () {

        this.anim.SetBool("isWalkingForward", _travelling);
        _navMeshAgent.speed = PlayerPrefs.GetFloat("Speed");
		if (_travelling && _navMeshAgent.remainingDistance <= 1.0f) {
			_travelling = false;
			_waypointsVisited++;

			if (_patrolWaiting) {
				waiting = true;
				_waitTimer = 0f;
			} else
				SetDestination ();
		}
		if (waiting) {
			_waitTimer += Time.deltaTime;
			if (_waitTimer >= _totalWaitTime) {
				waiting = false;
				SetDestination ();
			}
		}
		if (playerOnFieldView) {
			ChacePlayer (playerTarget.transform.position);
			//Debug.Log ("chacing");
		}
	}

	public void SetDestination(){
		if (_waypointsVisited > 0) {
			ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint (_previousWaypoint);
			_previousWaypoint = _currentWaypoint;
			_currentWaypoint = nextWaypoint;
		} 
		Vector3 targetVector = _currentWaypoint.transform.position;
		_navMeshAgent.SetDestination (targetVector);
		_travelling = true;
	}

	public void ChacePlayer(Vector3 targetVector){
		_navMeshAgent.SetDestination (targetVector);
	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals("Guard") || collision.gameObject.tag.Equals("Killer Guards")) {
			SetDestination ();
		}
	    if (this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8 && collision.gameObject != NewControl.objective) {

			collision.gameObject.SetActive (false);
			collision.gameObject.GetComponent<FieldOfView> ().alive = false;
            collision.gameObject.GetComponent<PlayerControl>().badFeedback = true;
            collision.gameObject.GetComponent<PlayerControl> ().Respawn(collision.gameObject);
            //Debug.Log("colliding");

	    }else if(this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8 && collision.gameObject == NewControl.objective){
            collision.gameObject.GetComponent<PlayerControl>().badFeedback = true;
		    NewControl.objKilledByGuard = true;
	    }
	}

	public void Respawn(GameObject gO){
		//yield return new WaitForSeconds (delay);
		
        if (gO.gameObject.tag.Equals("Killer Guards")) {
            GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPointKillers");
            int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            gO.gameObject.transform.position = allMyRespawnPoints[random].transform.position;
            gO.gameObject.transform.parent = GameObject.Find("Killer Guards").transform;
        }
        else if (gO.gameObject.tag.Equals("Guard"))
        {
            GameObject[] allMyRespawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
            int random = UnityEngine.Random.Range(0, allMyRespawnPoints.Length);
            gO.gameObject.transform.position = allMyRespawnPoints[random].transform.position;
            gO.gameObject.transform.parent = GameObject.Find("Guards").transform;
        }
        gO.gameObject.SetActive (true);

	}

}
