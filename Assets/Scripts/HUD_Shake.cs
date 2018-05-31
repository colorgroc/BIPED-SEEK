using UnityEngine;
using System.Collections;
using FMODUnity;

public class HUD_Shake : MonoBehaviour
{
    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private GameObject player;
	public GameObject p;
    //public FMOD.Studio.EventInstance backgroudSound;
    // private float _fakeDelta;
    //private void Awake()
    //{
    //    //backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 1"); --> no va nosepq
    //    //backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 2");
    //    backgroudSound = RuntimeManager.CreateInstance("event:/BipedSeek/Stuff/Vibration 3");
    //}
    private void Start()
    {
        this._originalPos = this.gameObject.transform.localPosition;
		if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show) {
			if (this.gameObject.name.EndsWith ("1"))
				this.player = GameObject.Find ("Player 1");
			else if (this.gameObject.name.EndsWith ("2"))
				this.player = GameObject.Find ("Player 2");
			else if (this.gameObject.name.EndsWith ("3"))
				this.player = GameObject.Find ("Player 3");
			else if (this.gameObject.name.EndsWith ("4"))
				this.player = GameObject.Find ("Player 4");
		} else { 
            if (!Abilities_Tutorial.show)
			    this.player = p;
		}
    }
    private void Update()
    {
        
        if (NewControl.paused || Tutorial_InGame.tutorialPaused) StopAllCoroutines();
        else Shake(this.player.GetComponent<PlayerControl>().detected, 5f);
    }
    public void Shake(bool duration, float amount)
    { 
        StopAllCoroutines();
        StartCoroutine(this.cShake(duration, amount));
    }

    public IEnumerator cShake(bool duration, float amount)
    {

        while (duration)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;
            //RuntimeManager.PlayOneShot("event:/BipedSeek/Stuff/Vibration 1", Vector3.zero);
            //RuntimeManager.PlayOneShot("event:/BipedSeek/Stuff/Vibration 2", Vector3.zero);
            //RuntimeManager.PlayOneShot("event:/BipedSeek/Stuff/Vibration 3",Vector3.zero);
            //backgroudSound.start();
            yield return null;
        }

        transform.localPosition = _originalPos;
        //backgroudSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

