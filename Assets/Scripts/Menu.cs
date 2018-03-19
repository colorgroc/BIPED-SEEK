using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class Menu : MonoBehaviour {

    [SerializeField]
    private Scrollbar volume;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private Canvas mainMenu, options, credits;
    private bool inMenu;
    private Button lastButon;
    [SerializeField]
    private Button opt, cred;
    [SerializeField]
    GameObject fullScreen, windowed, muted, notMuted;
    [SerializeField]
    GameObject mc_p1, mc_p2, mc_p3, mc_p4;
    [SerializeField]
    private int max_players = 4;
    //private Button[] butons;
    // private int select;
    private void Awake()
    {
        //PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);

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

                /*for (int i = 1; i <= max_players; i++)
                {
                    PlayerPrefs.SetString("Movement_P" + i.ToString(), "V_LPad_" + i.ToString());
                    PlayerPrefs.SetString("Rotation_P" + i.ToString(), "H_RPad_" + i.ToString());
                    PlayerPrefs.SetString("Kill_P" + i.ToString(), "X_" + i.ToString());
                    PlayerPrefs.SetString("Hab1_P" + i.ToString(), "B_" + i.ToString());
                    PlayerPrefs.SetString("Hab2_P" + i.ToString(), "Y_" + i.ToString());

    

                    //PlayerPrefs.SetInt("Movement_Value_P" + i.ToString(), 0);
                    //PlayerPrefs.SetInt("Rotation_Value_P" + i.ToString(), 1);
                    //PlayerPrefs.SetInt("Kill_Value_P" + i.ToString(), 3);
                    //PlayerPrefs.SetInt("Hab1_Value_P" + i.ToString(), 0);
                    //PlayerPrefs.SetInt("Hab2_Value_P" + i.ToString(), 1);
                }*/
            }

            /* Dropdown[] dropList1 = mc_p1.GetComponentsInChildren<Dropdown>();
             Dropdown[] dropList2 = mc_p2.GetComponentsInChildren<Dropdown>();
             Dropdown[] dropList3 = mc_p3.GetComponentsInChildren<Dropdown>();
             Dropdown[] dropList4 = mc_p4.GetComponentsInChildren<Dropdown>();

             for (int i = 0; i < 5; i++)
             {
                 if (dropList1[i].transform.parent.name == "Movement")
                 {
                     dropList1[i].value = PlayerPrefs.GetInt("Movement_Value_P1");
                     dropList2[i].value = PlayerPrefs.GetInt("Movement_Value_P2");
                     dropList3[i].value = PlayerPrefs.GetInt("Movement_Value_P3");
                     dropList4[i].value = PlayerPrefs.GetInt("Movement_Value_P4");
                 }
                 else if (dropList1[i].transform.parent.name == "Rotation")
                 {
                     dropList1[i].value = PlayerPrefs.GetInt("Rotation_Value_P1");
                     dropList2[i].value = PlayerPrefs.GetInt("Rotation_Value_P2");
                     dropList3[i].value = PlayerPrefs.GetInt("Rotation_Value_P3");
                     dropList4[i].value = PlayerPrefs.GetInt("Rotation_Value_P4");
                 }
                 else if (dropList1[i].transform.parent.name == "Kill")
                 {
                     dropList1[i].value = PlayerPrefs.GetInt("Kill_Value_P1");
                     dropList2[i].value = PlayerPrefs.GetInt("Kill_Value_P2");
                     dropList3[i].value = PlayerPrefs.GetInt("Kill_Value_P3");
                     dropList4[i].value = PlayerPrefs.GetInt("Kill_Value_P4");
                 }
                 else if (dropList1[i].transform.parent.name == "Hability 1")
                 {
                     dropList1[i].value = PlayerPrefs.GetInt("Hab1_Value_P1");
                     dropList2[i].value = PlayerPrefs.GetInt("Hab1_Value_P2");
                     dropList3[i].value = PlayerPrefs.GetInt("Hab1_Value_P3");
                     dropList4[i].value = PlayerPrefs.GetInt("Hab1_Value_P4");
                 }
                 else if (dropList1[i].transform.parent.name == "Hability 2")
                 {
                     dropList1[i].value = PlayerPrefs.GetInt("Hab2_Value_P1");
                     dropList2[i].value = PlayerPrefs.GetInt("Hab2_Value_P2");
                     dropList3[i].value = PlayerPrefs.GetInt("Hab2_Value_P3");
                     dropList4[i].value = PlayerPrefs.GetInt("Hab2_Value_P4");
                 }
             }*/
        }
        /*else
        {
            for (int i = 1; i <= max_players; i++)
            {
                PlayerPrefs.SetString("Movement_P" + i.ToString(), "V_LPad_" + i.ToString());
                PlayerPrefs.SetString("Rotation_P" + i.ToString(), "H_RPad_" + i.ToString());
                PlayerPrefs.SetString("Kill_P" + i.ToString(), "X_" + i.ToString());
                PlayerPrefs.SetString("Hab1_P" + i.ToString(), "B_" + i.ToString());
                PlayerPrefs.SetString("Hab2_P" + i.ToString(), "Y_" + i.ToString());
            }
        }*/

    }
    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
       
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        inMenu = true;
       
        //music.volume = 1f;
        volume.value = PlayerPrefs.GetFloat("MusicVolume");
        music.volume = volume.value;

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

        NewControl.finalWinner = null;
       // Default();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Back") && !inMenu) BackToMenu();
        
	}

    public void GoToPlay()
    {
        Default();
        SceneManager.LoadScene("Seleccion Personajes");

    }
    public void Exit()
    {
        Application.Quit();
    }

    void Default()
    {
        NewControl.killers = null;
        //NewControl.players = null;
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
        inMenu = false;
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(fullScreen);
        if (PlayerPrefs.GetInt("ScreenMode") == 0)
        {
            //UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(fullScreen);
            fullScreen.GetComponent<Toggle>().isOn = true;
        }
        else if (PlayerPrefs.GetInt("ScreenMode") == 1) { 
            //UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(windowed);
            windowed.GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.GetInt("isMute") == 0)
        {
            //UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(fullScreen);
            notMuted.GetComponent<Toggle>().isOn = true;
        }
        else if (PlayerPrefs.GetInt("isMute") == 1)
        {
            muted.GetComponent<Toggle>().isOn = true;
        }
        lastButon = opt;
    }
    public void ShowCredits()
    {
        inMenu = false;
        mainMenu.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
        options.gameObject.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        lastButon = cred;
    }
    public void BackToMenu()
    {
        inMenu = true;
        mainMenu.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        //lastButon.Select();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastButon.gameObject);
    }
    public void FullScreen()
    {
        Screen.fullScreen = true;
        PlayerPrefs.SetInt("ScreenMode", 0);
    }
    public void WindowScreen()
    {
        Screen.fullScreen = false;
        PlayerPrefs.SetInt("ScreenMode", 1);
    }
    public void SetVolume()
    {
        //AudioListener.volume = volume.value;
        music.volume = volume.value;
        PlayerPrefs.SetFloat("MusicVolume", music.volume);
        //Debug.Log("volumen");
    }
    public void MuteMusic()
    {
        music.mute = true;
        PlayerPrefs.SetInt("isMute", 1);
    }
    public void SoundMusic()
    {
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
