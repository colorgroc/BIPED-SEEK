using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
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
	int _waypointsVisited, rand;

	public bool isDead;

    private Animator anim;

    private void Awake()
    {
        _waitTimer = 0;
        _waypointsVisited = 0;

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
                    //while (_currentWaypoint == null)
                   // {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                            //_travelling = true;
                        }
                   // }
                }
                else Debug.LogError("Failed to find any waypoints for use in the scene");
            }
            rand = Random.Range(0, 2);
            
        }
    }
    public void Start () {
        //this.anim = this.gameObject.GetComponent<Animator>();
        if (rand == 0)
        {
            _travelling = true;
            this.anim.SetBool("isWalkingForward", true);
            SetDestination();
        }
        else
        {
            _travelling = false;
            this.anim.SetBool("isWalkingForward", false);
            _waitTimer = 0;
        }
        this.freezed = false;

        //waiting = true;
        //_waitTimer = 0f;
    }
	
	public void Update () {

        
        if(_navMeshAgent.speed > PlayerControl.defaultSpeed && _travelling)
            anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);

        if (_navMeshAgent.speed > PlayerControl.defaultSpeed)
            _navMeshAgent.stoppingDistance = 2;
        else
            _navMeshAgent.stoppingDistance = 0;

        if (this.freezed)
        {
            _navMeshAgent.isStopped = true;
            _travelling = false;
            _waitTimer = 0f;
        }
        else _navMeshAgent.isStopped = false;
       // Debug.Log(_travelling);
        if (_travelling && _navMeshAgent.remainingDistance <= 5f) {
			_travelling = false;
			_waypointsVisited++;
           // waiting = true;
            _waitTimer = 0f;

		}

        this.anim.SetBool("isFreezed", this.freezed);
        if (!_travelling) {
			_waitTimer += Time.deltaTime;
			if (_waitTimer >= _totalWaitTime) {
				_travelling = true;
				SetDestination ();
			}
		}
		if (this.gameObject.tag.Equals("Killer Guards") && playerOnFieldView) {
            // Debug.Log(Vector3.Distance(this.gameObject.transform.position, playerTarget.transform.position));
            if (Vector3.Distance(this.gameObject.transform.position, playerTarget.transform.position) <= 10)
            {
                _navMeshAgent.isStopped = true;
                this.anim.SetTrigger("kill");
                //this.anim.SetBool("wannaKill", true);
            }
            else
            {
                _navMeshAgent.isStopped = false;
                //else if (Vector3.Distance(this.gameObject.transform.position, playerTarget.transform.position) >= 25) this.anim.SetBool("wannaKill", false);
                ChacePlayer(playerTarget.transform.position);
            }
		}
        this.anim.SetBool("isWalkingForward", _travelling);
        _navMeshAgent.acceleration = PlayerPrefs.GetFloat("Speed");
        _navMeshAgent.speed = PlayerPrefs.GetFloat("Speed");
    }

	public void SetDestination(){
       
        if (_waypointsVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
           // _travelling = true;
        }

        if (_currentWaypoint != null)
        {
            Vector3 targetVector = _currentWaypoint.transform.position;
            _travelling = true;
            _navMeshAgent.SetDestination(targetVector);
        }
	}

	public void ChacePlayer(Vector3 targetVector){
		_navMeshAgent.SetDestination (targetVector);
       
    }
	void OnCollisionEnter(Collision collision){
        if (NewControl.startGame)
        {
            if (collision.gameObject.tag.Equals("Guard") || collision.gameObject.tag.Equals("Killer Guards"))
            {
                SetDestination();
            }
            if (this.gameObject.tag.Equals("Guard") && collision.gameObject.layer == 8)
            {
                SetDestination();
            }
        }
        if (this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8 && collision.gameObject != NewControl.objective) {
            //this.anim.SetBool("wannaKill", true);
           // this.anim.SetTrigger("WannaKill");
            Death.AnimDeath(collision.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            collision.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();
            RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Death", this.transform.position);
            // this.anim.SetBool("wannaKill", false);

        }
        else if(this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8 && collision.gameObject == NewControl.objective){
            //this.anim.SetBool("wannaKill", true);
            //this.anim.SetTrigger("WannaKill");

           // Death.AnimDeath(collision.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            NewControl.objKilledByGuard = true;
            //this.anim.SetBool("wannaKill", false);
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
            // this.anim.SetTrigger("WannaKill");
            //this.anim.SetBool("wannaKill", true);
            Death.AnimDeath(col.gameObject, col.gameObject.transform.position, col.gameObject.transform.rotation);
            RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Death/Death", this.transform.position);
            col.gameObject.GetComponent<PlayerControl>().RespawnCoolDown();
          //  this.anim.SetBool("wannaKill", false);

        }
        else if (this.gameObject.tag.Equals("Killer Guards") && col.gameObject.layer == 8 && col.gameObject == NewControl.objective)
        {
           // this.anim.SetTrigger("WannaKill");
            //this.anim.SetBool("wannaKill", true);
            NewControl.objKilledByGuard = true;
           // Death.AnimDeath(col.gameObject, col.gameObject.transform.position, col.gameObject.transform.rotation);
            //this.anim.SetBool("wannaKill", false);
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
            if (allMyRespawnPoints[random].transform.position != GameObject.Find("Player " + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).transform.position)
            {
                gO.gameObject.transform.position = allMyRespawnPoints[random].transform.position;
                gO.gameObject.transform.parent = GameObject.Find("Guards").transform;
            }
            
        }
        gO.gameObject.SetActive (true);

	}

}
