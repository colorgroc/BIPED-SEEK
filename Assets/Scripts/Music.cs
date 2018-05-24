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

    void Start () {

        music = RuntimeManager.GetBus("bus:/Master/Music");
        Sounds = RuntimeManager.GetBus("bus:/Master/Sounds");
        Master = RuntimeManager.GetBus("bus:/Master");

        music.setVolume(PlayerPrefs.GetInt("MusicVolume"));
        Sounds.setVolume(PlayerPrefs.GetInt("SoundsVolume"));
        if (PlayerPrefs.GetInt("isMute") == 1)
            Master.setMute(true);
        else if (PlayerPrefs.GetInt("isMute") == 0)
            Master.setMute(false);
    }
	
}
