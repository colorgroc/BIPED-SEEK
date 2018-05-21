﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Menu : MonoBehaviour
{

    [SerializeField]
    private Scrollbar volume;
    [SerializeField]
    private AudioSource music, sounds;
    [SerializeField]
    public AudioClip onButton, clickButton, backButton;
    [SerializeField]
    private Canvas mainMenu, options, credits;
    private bool inMenu;
    private Button lastButon;
    [SerializeField]
    private Button opt, cred;
    [SerializeField]
    GameObject fullScreen, muted, tutorialOptions, tutorialMenu;
    [SerializeField]
    GameObject mc_p1, mc_p2, mc_p3, mc_p4;
    [SerializeField]
    private int max_players = 4;
    public static bool inDropdown;//, inVolume;
    [SerializeField]
    private Image menuBg, optionsBg, creditsBg;
    [SerializeField]
    private Sprite bg1, bg2, bg3, bg4, bg5;
    GameObject lastSelect;
    Resolution res;

    private void Awake()
    {

        if (Application.isEditor == false)
        {
            if (PlayerPrefs.GetInt("FirstGame") == 0)
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("FirstGame", 1);
                PlayerPrefs.SetFloat("MusicVolume", 1f);
                PlayerPrefs.SetInt("isMute", 0); //sona musica
                if (Screen.fullScreen)
                    PlayerPrefs.SetInt("ScreenMode", 0); //full screen
                else if (!Screen.fullScreen) PlayerPrefs.SetInt("ScreenMode", 1);
                PlayerPrefs.SetInt("Tutorial", 0); //1 = si
                tutorialMenu.SetActive(true);
                Tutorial_InGame.showIt = true;
            }
            //else Tutorial.showIt = false;
        }
        //else Tutorial.showIt = true;
        sounds.GetComponent<AudioSource>().enabled = false;
        sounds.volume = 1;
    }

    void Start()
    {
        //control HZ monitors
        res = Screen.currentResolution;
        if (res.refreshRate == 60)
            QualitySettings.vSyncCount = 1;
        if (res.refreshRate == 120)
            QualitySettings.vSyncCount = 2;
        //print(QualitySettings.vSyncCount);

        lastSelect = new GameObject();
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        inMenu = true;
        RandomBackground(menuBg);

        volume.value = PlayerPrefs.GetFloat("MusicVolume");
        music.volume = volume.value;

        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            Tutorial_InGame.showIt = true;
            tutorialMenu.SetActive(true);
        }
        else
        {
            Tutorial_InGame.showIt = false;
            tutorialMenu.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ScreenMode") == 0)
        {
            FullScreen();
        }
        else WindowScreen();

        if (PlayerPrefs.GetInt("isMute") == 0)
        {
            SoundMusic();
        }
        else MuteMusic();
        sounds.mute = false;

        NewControl.finalWinner = null;
        sounds.GetComponent<AudioSource>().enabled = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !inMenu && !inDropdown)
        {
            BackToMenu();
        }
        // Count is 4 when its open, and 3 when closed.
        if (fullScreen.GetComponent<Dropdown>().transform.childCount == 3 && muted.GetComponent<Dropdown>().transform.childCount == 3 && tutorialOptions.GetComponent<Dropdown>().transform.childCount == 3)
        {
            inDropdown = false;
        }
        else inDropdown = true;

        if(inMenu && EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(lastSelect);
        else if(inMenu && EventSystem.current.currentSelectedGameObject != null)
            lastSelect = EventSystem.current.currentSelectedGameObject;
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
    public void GoToPlay()
    {
        Tutorial_InGame.showIt = false;
        sounds.PlayOneShot(clickButton);
        Default();
        SceneManager.LoadScene("Seleccion Personajes");

    }
    public void Exit()
    {
        sounds.PlayOneShot(clickButton);
        Application.Quit();
    }

    void Default()
    {
        NewControl.killers = null;
        Rondes.timesPlayed = 0;
       // ObjectiveCanvas.timeObjective = 0;
        NewControl.guards = null;
        NewControl.numOfPlayers = 0;
        NewControl.objComplete = false;
        NewControl.objKilledByGuard = false;
        NewControl.timeLeft = 0;
        NewControl.objective = null;
        NewControl.finalWinner = null;
        NewControl.parcialWinner = null;
        NumCanvasSeleccionJugadores.ready_P1 = false;
        NumCanvasSeleccionJugadores.ready_P2 = false;
        NumCanvasSeleccionJugadores.ready_P3 = false;
        NumCanvasSeleccionJugadores.ready_P4 = false;
    }

    public void ShowOptions()
    {
        RandomBackground(optionsBg);
        sounds.PlayOneShot(clickButton);
        inMenu = false;
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(fullScreen);

        if (PlayerPrefs.GetInt("ScreenMode") == 0) //On
        {
            fullScreen.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("ScreenMode") == 1) //windowed (off)
        {
            fullScreen.GetComponent<Dropdown>().value = 1;
        }
        if (PlayerPrefs.GetInt("isMute") == 0) //MusicOn
        {
            muted.GetComponent<Dropdown>().value = 0;
        }
        else if (PlayerPrefs.GetInt("isMute") == 1)//MusicOff
        {
            muted.GetComponent<Dropdown>().value = 1;
        }
        else if (PlayerPrefs.GetInt("Tutorial") == 0)//TutorialOn
        {
            tutorialOptions.GetComponent<Dropdown>().value = 0;
            tutorialMenu.SetActive(true);
            Tutorial_InGame.showIt = true;
        }
        else if (PlayerPrefs.GetInt("Tutorial") == 1)//TutorialOff
        {
            tutorialOptions.GetComponent<Dropdown>().value = 1;
            tutorialMenu.SetActive(false);
            Tutorial_InGame.showIt = false;
        }
        lastButon = opt;
    }
    public void ShowCredits()
    {
        RandomBackground(creditsBg);
        sounds.PlayOneShot(clickButton);
        inMenu = false;
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
        options.gameObject.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        lastButon = cred;
    }
    public void GoToTutorial()
    {
        Tutorial_InGame.showIt = true;
        SceneManager.LoadScene("Tutorial");
    }
    public void BackToMenu()
    {
        RandomBackground(menuBg);
        sounds.PlayOneShot(backButton, 4.0F);
        inMenu = true;
        mainMenu.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastButon.gameObject);
    }

   public void TutorialValue()
    {
        if (tutorialOptions.GetComponent<Dropdown>().value == 0)
        {
            tutorialMenu.SetActive(true);
            sounds.PlayOneShot(onButton);
            PlayerPrefs.SetInt("Tutorial", 0);
            Tutorial_InGame.showIt = true;
        }
        else if (tutorialOptions.GetComponent<Dropdown>().value == 1)
        {
            tutorialMenu.SetActive(false);
            sounds.PlayOneShot(onButton);
            PlayerPrefs.SetInt("Tutorial", 1);
            Tutorial_InGame.showIt = false;
        }
    }
    public void ScreenValue()
    {
        if (fullScreen.GetComponent<Dropdown>().value == 0)
            FullScreen();
        else if (fullScreen.GetComponent<Dropdown>().value == 1)
            WindowScreen();
    }
    public void MusicValue()
    {
        if (muted.GetComponent<Dropdown>().value == 0)
            SoundMusic();
        else if (muted.GetComponent<Dropdown>().value == 1)
            MuteMusic();
    }
    public void FullScreen()
    {
        sounds.PlayOneShot(onButton);
        Screen.fullScreen = true;
        PlayerPrefs.SetInt("ScreenMode", 0);
    }
    public void WindowScreen()
    {
        sounds.PlayOneShot(onButton);
        Screen.fullScreen = false;
        PlayerPrefs.SetInt("ScreenMode", 1);
    }
    public void SetVolume()
    {
        music.volume = volume.value;
        PlayerPrefs.SetFloat("MusicVolume", music.volume);
    }
    public void MuteMusic()
    {
        sounds.PlayOneShot(onButton);
        sounds.mute = true;
        music.mute = true;
        PlayerPrefs.SetInt("isMute", 1);
    }
    public void SoundMusic()
    {
        sounds.PlayOneShot(onButton);
        sounds.mute = false;
        music.mute = false;
        PlayerPrefs.SetInt("isMute", 0);
    }

    /*public void MostrarControles_p1()
    {
        GameObject.Find("NamePlayer").GetComponent<Text>().text = "Player 1";
        mc_p1.SetActive(true);
        mc_p2.SetActive(false);
        mc_p3.SetActive(false);
        mc_p4.SetActive(false);
    }
    public void MostrarControles_p2()
    {
        GameObject.Find("NamePlayer").GetComponent<Text>().text = "Player 2";
        mc_p1.SetActive(false);
        mc_p2.SetActive(true);
        mc_p3.SetActive(false);
        mc_p4.SetActive(false);
    }
    public void MostrarControles_p3()
    {
        GameObject.Find("NamePlayer").GetComponent<Text>().text = "Player 3";
        mc_p1.SetActive(false);
        mc_p2.SetActive(false);
        mc_p3.SetActive(true);
        mc_p4.SetActive(false);
    }
    public void MostrarControles_p4()
    {
        GameObject.Find("NamePlayer").GetComponent<Text>().text = "Player 4";
        mc_p1.SetActive(false);
        mc_p2.SetActive(false);
        mc_p3.SetActive(false);
        mc_p4.SetActive(true);
    }*/
    /*
    public void ChangeControl_Movement()
    {
        int num = 0;
        if (mc_p1.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p1.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Movement_P1", button + "1");
        }
        else if (mc_p2.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p2.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Movement_P2", button + "2");
        }
        else if (mc_p3.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p3.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Movement_P3", button + "3");
        }
        else if (mc_p4.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p4.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Movement_P4", button + "4");
        }
    }

    public void ChangeControl_Rotation()
    {
        int num = 1;
        if (mc_p1.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p1.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Rotation_P1", button + "1");
        }
        else if (mc_p2.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p2.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Rotation_P2", button + "2");
        }
        else if (mc_p3.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p3.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Rotation_P3", button + "3");
        }
        else if (mc_p4.activeInHierarchy)
        {
            string button = "";
            button = Axis(mc_p4.GetComponentsInChildren<Dropdown>(), num);
            PlayerPrefs.SetString("Rotation_P4", button + "4");
        }
    }
    private string Axis(Dropdown[] dropList, int num)
    {
        string button = "";
        if (dropList[num].value == 0) button = "H_LPad_";
        else if (dropList[num].value == 1) button = "H_RPad_";
        else if (dropList[num].value == 2) button = "H_Arrows_";
        return button;
    }
    private string Button(Dropdown[] dropList, int num)
    {
        string button = "";
        if (dropList[num].value == 0) button = "A_";
        else if (dropList[num].value == 1) button = "B_";
        else if (dropList[num].value == 2) button = "Y_";
        else if (dropList[num].value == 3) button = "X_";
        else if (dropList[num].value == 4) button = "R_Bumper_";
        else if (dropList[num].value == 5) button = "R_Trigger_";
        else if (dropList[num].value == 6) button = "L_Bumper_";
        else if (dropList[num].value == 7) button = "L_Trigger_";
        return button;
    }
   
      public void ChangeControl_Kill()
       {
           int num = 2;
           if (mc_p1.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p1.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Kill_P1", button + "1");

           }
           else if (mc_p2.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p2.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Kill_P2", button + "2");
           }
           else if (mc_p3.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p3.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Kill_P3", button + "3");
           }
           else if (mc_p4.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p4.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Kill_P4", button + "4");
           }
       }

       public void ChangeControl_Hab1()
       {
           int num = 3;
           if (mc_p1.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p1.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab1_P1", button + "1");
           }
           else if (mc_p2.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p2.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab1_P2", button + "2");
           }
           else if (mc_p3.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p3.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab1_P3", button + "3");
           }
           else if (mc_p4.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p4.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab1_P4", button + "4");
           }
       }

       public void ChangeControl_Hab2()
       {
           int num = 4;
           if (mc_p1.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p1.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab2_P1", button + "1");
           }
           else if (mc_p2.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p2.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab2_P2", button + "2");
           }
           else if (mc_p3.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p3.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab2_P3", button + "3");
           }
           else if (mc_p4.activeInHierarchy)
           {
               string button = "";
               button = Button(mc_p4.GetComponentsInChildren<Dropdown>(), num);
               PlayerPrefs.SetString("Hab2_P4", button + "4");
           }
       }*/



}
