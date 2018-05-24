using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Music : MonoBehaviour {
    //[SerializeField]
    //private AudioSource music, sounds;
    // Use this for initialization
    FMOD.Studio.Bus music;
    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Sounds;

    void Awake() {

        music = RuntimeManager.GetBus("bus:/Master/Music");
        Sounds = RuntimeManager.GetBus("bus:/Master/Sounds");
        Master = RuntimeManager.GetBus("bus:/Master");
    }
    private void Start()
    {
        music.setVolume(PlayerPrefs.GetFloat("MusicVolume"));
        Sounds.setVolume(PlayerPrefs.GetFloat("SoundsVolume"));
        if (PlayerPrefs.GetInt("isMute") == 0)
            Master.setMute(false);
        else if (PlayerPrefs.GetInt("isMute") == 1)
            Master.setMute(true);
    }
	
}
