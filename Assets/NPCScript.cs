using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour {

	public Rigidbody[] rbPlayers;
	private float player1dis;
	private float player2dis;
	private bool canNav = false;

	[SerializeField]
	//Transform _destination;
	NavMeshAgent _navMeshAgent;


	// Use this for initialization
	void Start () {

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		rbPlayers = new Rigidbody[players.Length];
		for(int i = 0; i < rbPlayers.Length; i++)
		{
			rbPlayers[i] = players[i].GetComponent<Rigidbody>();
		}

		_navMeshAgent = this.GetComponent<NavMeshAgent> ();

		if (_navMeshAgent == null) {
			Debug.LogError ("The nav mesh agent is not attached to " + gameObject.name);
		} else {
			canNav = true;
			//SetDestination();
		}
	}

	void Update(){
		if (canNav)
			SetDestination ();
	}

	private void SetDestination(){
		Vector3 targetVector;
		player1dis = Vector3.Distance(rbPlayers[0].transform.position, transform.position);
		targetVector = rbPlayers[0].transform.position;
		//player2dis = Vector3.Distance(rbPlayers[1].transform.position, transform.position);
		//if(player1dis < player2dis) targetVector = rbPlayers[0].transform.position;
		//else if(player2dis < player1dis) targetVector = rbPlayers[1].transform.position;

		//if (_destination != null) {
			//Vector3 targetVector = _destination.transform.position;
			_navMeshAgent.SetDestination (targetVector);
		//}
	}

}
