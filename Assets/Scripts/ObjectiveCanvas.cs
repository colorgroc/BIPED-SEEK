using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCanvas : MonoBehaviour {

    // Use this for initialization
    public static float timeObjective;
    public float time = 3;
    [SerializeField]
    private Text tObjectiu, tRonda;
    public void Start () {
        timeObjective = 0;
        this.gameObject.SetActive(true);

        if (NewControl.objective != null)
        {
            if (NewControl.objective.name.EndsWith("1")) tObjectiu.color = Color.cyan;
            else if (NewControl.objective.name.EndsWith("2")) tObjectiu.color = Color.red;
            else if (NewControl.objective.name.EndsWith("3")) tObjectiu.color = Color.green;
            else if (NewControl.objective.name.EndsWith("4")) tObjectiu.color = Color.yellow;
            tObjectiu.text = NewControl.objective.name;
           
        }
        if (Rondes.timesPlayed + 1 == Rondes.rondas)
            tRonda.text = "Last Round";
        else
            tRonda.text = "Ronda " + (Rondes.timesPlayed + 1).ToString();

    }
    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        timeObjective += Time.fixedDeltaTime;
        //Debug.Log(timeObjective);
        if (timeObjective >= time) this.gameObject.SetActive(false);
    }
}
