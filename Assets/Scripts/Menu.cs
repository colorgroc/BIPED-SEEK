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
    //private Button[] butons;
    // private int select;

    // Use this for initialization
    void Start () {
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        inMenu = true;
        PlayerPrefs.SetInt("NumPlayers", Input.GetJoystickNames().Length);
        PlayerPrefs.SetInt("NumMapas", numOfMapas);
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
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(GameObject.Find("Full Screen"));
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
    }
    public void WindowScreen()
    {
        Screen.fullScreen = false;
    }
    public void SetVolume()
    {
        //AudioListener.volume = volume.value;
        music.volume = volume.value;
        Debug.Log("volumen");
    }
    public void MuteMusic()
    {
        music.mute = true; ;
    }
    public void SoundMusic()
    {
        music.mute = false; ;
    }
  
}
