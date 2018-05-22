using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    [SerializeField]
    private AudioSource music, sounds;
    // Use this for initialization
    void Start () {
        music.volume = PlayerPrefs.GetInt("MusicVolume");
        sounds.volume = PlayerPrefs.GetInt("SoundsVolume");
        if (PlayerPrefs.GetInt("isMute") == 1)
            music.mute = true;
        else if (PlayerPrefs.GetInt("isMute") == 0)
            music.mute = false;
    }
	
}
