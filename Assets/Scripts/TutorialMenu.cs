using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour {
    public GameObject firstButton;
    [SerializeField]
    GameObject menu, events, killer;
    [SerializeField]
    private Sprite bg1, bg2, bg3, bg4, bg5;
    [SerializeField]
    private Image menuBg, eventsBg;
    private bool inMenu;
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
        SceneManager.LoadScene("Hab_Tuto");
    }
    public void GoToEventsTutorial()
    {
        inMenu = false;
        events.SetActive(true);
        RandomBackground(eventsBg);
        menu.SetActive(false);
        Abilities_Tutorial.show = false;
        Tutorial_InGame.showIt = false;
        killer.SetActive(true);
    }
    public void BackToTutorialMenu()
    {
        inMenu = true;
        events.SetActive(false);
        RandomBackground(menuBg);
        menu.SetActive(true);
        killer.SetActive(false);
    }

    void RandomBackground(Image canvas)
    {
        int rand = (int)Random.Range(0, 5);
        if (rand == 0) canvas.sprite = bg1;
        else if (rand == 1) canvas.sprite = bg2;
        else if (rand == 2) canvas.sprite = bg3;
        else if (rand == 3) canvas.sprite = bg4;
        else if (rand == 4) canvas.sprite = bg5;
    }
    private void Start()
    {
        inMenu = true;
        killer.SetActive(false);
        menu.SetActive(true);
        events.SetActive(false);
        RandomBackground(menuBg);
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(firstButton);
        if (Input.GetButtonDown("Cancel") && inMenu)
        {
            Abilities_Tutorial.show = false;
            Tutorial_InGame.showIt = false;
            Menu.showIt = false;
            SceneManager.LoadScene("Menu");
        }else if (Input.GetButtonDown("Cancel") && !inMenu)
        {
            BackToTutorialMenu();
        }
    }
}
