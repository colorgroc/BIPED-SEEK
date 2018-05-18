using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCConnectedPatrol : MonoBehaviour {

	[SerializeField]
	float _totalWaitTime = 3f;

	NavMeshAgent _navMeshAgent;
	ConnectedWaypoint _currentWaypoint;
	ConnectedWaypoint _previousWaypoint;
	public GameObject[] allWaypoints;

	public GameObject playerTarget;

	bool _travelling, waiting;
	public bool playerOnFieldView, freezed;

	float _waitTimer;
	int _waypointsVisited;

	public bool isDead;
    [SerializeField]
    private AudioClip killPlayerSound, killObjectiveSound;
    private AudioSource soundSource;
    private Animator anim;

    private void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.anim = this.gameObject.GetComponent<Animator>();
        if (_navMeshAgent == null) Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        else
        {
            if (_currentWaypoint == null)
            {
                allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                            //this.anim.SetBool("isWalkingForward", false);
                        }
                    }
                }
                else Debug.LogError("Failed to find any waypoints for use in the scene");
            }
        }
    }
    public void Start () {
        freezed = false;
        SetDestination();
        this.anim.SetBool("isWalkingForward", _travelling);

    }
	
	public void Update () {

        this.anim.SetBool("isWalkingForward", _travelling);

        _navMeshAgent.acceleration = PlayerPrefs.GetFloat("Speed");
        _navMeshAgent.speed = PlayerPrefs.GetFloat("Speed");

        if(_navMeshAgent.speed > PlayerControl.defaultSpeed && _travelling)
            anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);

        if (_navMeshAgent.speed > PlayerControl.defaultSpeed)
            _navMeshAgent.stoppingDistance = 2;
        else
            _navMeshAgent.stoppingDistance = 0;

        if (this.freezed) _navMeshAgent.isStopped = true;
        else _navMeshAgent.isStopped = false;
       // Debug.Log(_travelling);
        if (_travelling && _navMeshAgent.remainingDistance <= 0.5f) {
			_travelling = false;
			_waypointsVisited++;
            waiting = true;
            _waitTimer = 0f;

		}
        if (this.freezed)
        {
            waiting = true;
            _waitTimer = 0f;
        }
        this.anim.SetBool("isFreezed", this.freezed);
        if (waiting) {
			_waitTimer += Time.deltaTime;
			if (_waitTimer >= _totalWaitTime) {
				waiting = false;
				SetDestination ();
			}
		}
		if (this.gameObject.tag.Equals("Killer Guards") && playerOnFieldView) {
			ChacePlayer (playerTarget.transform.position);
		}
	}

	public void SetDestination(){
       
        if (_waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }
        //Vector3 targetVector = new Vector3();
        if (_currentWaypoint != null)
        {
            Vector3 targetVector = _currentWaypoint.transform.position;

            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;

        }
	}

	public void ChacePlayer(Vector3 targetVector){
		_navMeshAgent.SetDestination (targetVector);
	}
	void OnCollisionEnter(Collision collision){
       // if (NewControl.startGame)
       // {
            if (collision.gameObject.tag.Equals("Guard") || collision.gameObject.tag.Equals("Killer Guards"))
            {
                SetDestination();
            }
            if (this.gameObject.tag.Equals("Guard") && collision.gameObject.layer == 8)
            {
                SetDestination();
            }
      //  }
       if (this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8 && collision.gameObject != NewControl.objective) {
            this.anim.SetBool("wannaKill", true);
            Death.AnimDeath(collision.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            collision.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();
            soundSource.PlayOneShot(killPlayerSound);
            //collision.gameObject.GetComponent<PlayerControl> ().Respawn(collision.gameObject);
            this.anim.SetBool("wannaKill", false);

        }
        else if(this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8 && collision.gameObject == NewControl.objective){
            this.anim.SetBool("wannaKill", true);
            soundSource.PlayOneShot(killObjectiveSound);
            Death.AnimDeath(collision.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            //collision.gameObject.GetComponent<PlayerControl>().badFeedback = true;
            NewControl.objKilledByGuard = true;
            this.anim.SetBool("wannaKill", false);
        }
	}

    void OnTriggerEnter(Collider col)
    {

        //if (col.gameObject.tag.Equals("Guard") || col.gameObject.tag.Equals("Killer Guards"))
        //{
        //    SetDestination();
        //}
        //else if (this.gameObject.tag.Equals("Guard") && col.gameObject.layer == 8)
        //{
        //    SetDestination();
        //}

         if (this.gameObject.tag.Equals("Killer Guards") && col.gameObject.layer == 8 && col.gameObject != NewControl.objective)
        {
            this.anim.SetBool("wannaKill", true);

            col.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();
            soundSource.PlayOneShot(killPlayerSound);
            //col.gameObject.GetComponent<PlayerControl>().Respawn(col.gameObject);
            this.anim.SetBool("wannaKill", false);

        }
        else if (this.gameObject.tag.Equals("Killer Guards") && col.gameObject.layer == 8 && col.gameObject == NewControl.objective)
        {
            this.anim.SetBool("wannaKill", true);
           // col.gameObject.GetComponent<PlayerControl>().badFeedback = true;
            NewControl.objKilledByGuard = true;
            soundSource.PlayOneShot(killObjectiveSound);
            this.anim.SetBool("wannaKill", false);
        }
    }
    public void Respawn(GameObject gO){
		
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
