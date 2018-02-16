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
    public static int numOfMapas = 1;
    [SerializeField]
    GameObject fullScreen, windowed, muted, notMuted;
    [SerializeField]
    GameObject mc_p1, mc_p2, mc_p3, mc_p4;
    //private Button[] butons;
    // private int select;

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
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
                else if(!Screen.fullScreen) PlayerPrefs.SetInt("ScreenMode", 1);
                //posar playerprefs controls
            }
        }
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        inMenu = true;
        PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);
        PlayerPrefs.SetInt("NumMapas", numOfMapas);
        //music.volume = 1f;
        volume.value = PlayerPrefs.GetFloat("MusicVolume"); ;

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
        //UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        //butons = this.gameObject.GetComponentsInChildren<Button>();
        //UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(butons[0].gameObject);
        // Debug.Log(butons.Length);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Back") && !inMenu) BackToMenu();
	}

    public void GoToPlay()
    {
        SceneManager.LoadScene(1);

    }
    public void Exit()
    {
        Application.Quit();
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
    public void MostrarControles_p1()
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
    }

}
