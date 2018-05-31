using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour {
    public GameObject firstButton;
	public void GoToMovementTutorial()
    {
        Tutorial_InGame.showIt = true;
        Abilities_Tutorial.show = false;
        Menu.musicStarted = false;
        Menu.backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Tutorial");
    }
    public void GoToAbilitiesTutorial()
    {
        Abilities_Tutorial.show = true;
        Tutorial_InGame.showIt = false;
        Menu.musicStarted = false;
        Menu.backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("Tutorial");
    }
    public void GoToEventsTutorial()
    {
        Abilities_Tutorial.show = false;
        Tutorial_InGame.showIt = false;
        //Menu.musicStarted = false;
        //Menu.backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //SceneManager.LoadScene("Tutorial");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Back"))
        {
            Abilities_Tutorial.show = false;
            Tutorial_InGame.showIt = false;
            Menu.showIt = false;
            SceneManager.LoadScene("Menu");
        }
    }
}
