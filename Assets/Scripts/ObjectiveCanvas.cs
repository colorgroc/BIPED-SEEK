using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCanvas : MonoBehaviour {

    // Use this for initialization
    public float timeObjective;
    public float time = 3;
    [SerializeField]
    private Text t;
    public void Start () {
        timeObjective = 0;

        if(NewControl.objective != null)
        {
            /*if(NewControl.objective.name.EndsWith("1")) t.text = "Player 1";
            else if (NewControl.objective.name.EndsWith("2")) t.text = "Player 2";
            else if (NewControl.objective.name.EndsWith("3")) t.text = "Player 3";
            else if (NewControl.objective.name.EndsWith("4")) t.text = "Player 4";*/
            t.text = NewControl.objective.name;
        }
            
         
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeObjective += Time.fixedDeltaTime;
        //Debug.Log(timeObjective);
        if (timeObjective >= time) this.gameObject.SetActive(false);
    }
}
