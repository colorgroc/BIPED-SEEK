using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveCanvas : MonoBehaviour {

    [SerializeField]
    private Text tObjectiu, tRonda;

    public void Start ()
    {
        this.gameObject.SetActive(true);
        if (!Tutorial_InGame.showIt)
        {
            if (NewControl.objective != null)
            {
                if (NewControl.objective.name.EndsWith("1")) tObjectiu.color = Color.cyan;
                else if (NewControl.objective.name.EndsWith("2")) tObjectiu.color = Color.red;
                else if (NewControl.objective.name.EndsWith("3")) tObjectiu.color = Color.green;
                else if (NewControl.objective.name.EndsWith("4")) tObjectiu.color = Color.yellow;
                tObjectiu.text = NewControl.objective.name;

            }
        }
        else tObjectiu.text = "Player 2";

        if (Rondes.timesPlayed + 1 == Rondes.rondas)
            tRonda.text = "Last Round";
        else
            tRonda.text = "Ronda " + (Rondes.timesPlayed + 1).ToString();

    }

    //void FixedUpdate ()
    //{
    //    if (NewControl.startGame)
    //    {
    //        timeObjective += Time.fixedDeltaTime;
    //        if (timeObjective >= time) this.gameObject.SetActive(false);
    //    }
    //}
}
