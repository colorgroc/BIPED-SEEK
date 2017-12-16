using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfGuards : MonoBehaviour {
	
	public GameObject guard;
	private GameObject[] guardList;
	public int numOfGuards;

	void Awake(){
		guardList = new GameObject[numOfGuards];
		for (int i = 0; i < guardList.Length; i++) {
			GameObject clone = (GameObject)Instantiate (guard);
			clone.transform.position = new Vector3 (guard.transform.position.x, guard.transform.position.y , guard.transform.position.z);
			//clone.GetComponent<NPCConnectedPatrol> ().Start();
			//guardList [i] = clone;
		}
	}
	//}
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
