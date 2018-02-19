using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCanvas : MonoBehaviour {

    // Use this for initialization
    private float timeObjective;
    public float time = 10;
    [SerializeField]
    private Text t;
    void Start () {
        timeObjective = 0;

        t.text = NewControl.objective.name;
    }
	
	// Update is called once per frame
	void Update () {
        timeObjective += 1;
        if (timeObjective >= time) this.gameObject.SetActive(false);
    }
}
