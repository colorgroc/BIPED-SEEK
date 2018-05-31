using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public GameObject movements, abilities;//, events;

    // Use this for initialization
    void Awake()
    {
        movements.SetActive(false);
        abilities.SetActive(false);
        //events.SetActive(false);
        if (Tutorial_InGame.showIt)
        {
            movements.SetActive(true);
        }
        else if (Abilities_Tutorial.show)
        {
            abilities.SetActive(true);
        }
        //else if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show && Menu.showIt)
        //{
        //    events.SetActive(true);
        //}
    }
}
	
	
