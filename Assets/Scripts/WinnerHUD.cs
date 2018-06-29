using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;

public class WinnerHUD : MonoBehaviour {

	public Text player;
    public Image cup, animal;
    private bool once = false;
    [SerializeField]
    private Sprite cup1, cup2, cup3, cup4, animal1, animal2, animal3, animal4;

    private void Awake()
    {
    }
    void Start () {

        //asignacio copa i animal del guanyador
        if (NewControl.finalWinner != null)
        {
            GameObject.Find("Control").GetComponent<NewControl>().rankingCanvas.SetActive(false);
            RuntimeManager.PlayOneShot("event:/BipedSeek/Stuff/Winner", Vector3.zero);
            //player.text = NewControl.finalWinner.name;
            player.text = Ranking.orderedRank[0].gameObject.name;
            if (NewControl.finalWinner.name.EndsWith("1"))
            {
                cup.sprite = cup1;

                if (PlayerPrefs.GetInt("characterPlayer_1") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_1") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_1") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_1") == 4)
                {
                    animal.sprite = animal4;
                }
            }
            else if (NewControl.finalWinner.name.EndsWith("2"))
            {
                cup.sprite = cup2;

                if (PlayerPrefs.GetInt("characterPlayer_2") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_2") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_2") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_2") == 4)
                {
                    animal.sprite = animal4;
                }
            }
            else if (NewControl.finalWinner.name.EndsWith("3"))
            {
                cup.sprite = cup3;

                if (PlayerPrefs.GetInt("characterPlayer_3") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_3") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_3") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_3") == 4)
                {
                    animal.sprite = animal4;
                }
            }
            else if (NewControl.finalWinner.name.EndsWith("4"))
            {
                cup.sprite = cup4;

                if (PlayerPrefs.GetInt("characterPlayer_4") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_4") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_4") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_4") == 4)
                {
                    animal.sprite = animal4;
                }
            }

        }

    }
	void Update()
    {
        if (NewControl.finalWinner != null)
        {
            GameObject.Find("Control").GetComponent<NewControl>().rankingCanvas.SetActive(false);
            if (!once)
            {
                RuntimeManager.PlayOneShot("event:/BipedSeek/Stuff/Winner", Vector3.zero);
                once = true;
            }
            //player.text = NewControl.finalWinner.name;
            player.text = Ranking.orderedRank[0].gameObject.name;
            if (NewControl.finalWinner.name.EndsWith("1"))
            {
                cup.sprite = cup1;

                if (PlayerPrefs.GetInt("characterPlayer_1") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_1") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_1") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_1") == 4)
                {
                    animal.sprite = animal4;
                }
            }
            else if (NewControl.finalWinner.name.EndsWith("2"))
            {
                cup.sprite = cup2;

                if (PlayerPrefs.GetInt("characterPlayer_2") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_2") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_2") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_2") == 4)
                {
                    animal.sprite = animal4;
                }
            }
            else if (NewControl.finalWinner.name.EndsWith("3"))
            {
                cup.sprite = cup3;

                if (PlayerPrefs.GetInt("characterPlayer_3") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_3") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_3") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_3") == 4)
                {
                    animal.sprite = animal4;
                }
            }
            else if (NewControl.finalWinner.name.EndsWith("4"))
            {
                cup.sprite = cup4;

                if (PlayerPrefs.GetInt("characterPlayer_4") == 1)
                {
                    animal.sprite = animal1;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_4") == 2)
                {
                    animal.sprite = animal2;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_4") == 3)
                {
                    animal.sprite = animal3;
                }
                else if (PlayerPrefs.GetInt("characterPlayer_4") == 4)
                {
                    animal.sprite = animal4;
                }
            }

        }
        if (Input.GetButtonDown("Main Menu")) {
			this.gameObject.SetActive (false);
			Time.timeScale = 1;
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            GameObject.Find("Control").GetComponent<NewControl>().backgroudMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Default();
            SceneManager.LoadScene ("Menu");

		} else Time.timeScale = 0;
        
	}

    void Default()
    {
        NewControl.killers = null;
        NewControl.players = new List<GameObject>();
        NewControl.guards = null;
        NewControl.numOfPlayers = 0;
        NewControl.objComplete = false;
        NewControl.objKilledByGuard = false;
        NewControl.timeLeft = 0;
        NewControl.objective = null;
        NewControl.finalWinner = null;
        NewControl.parcialWinner = null;
        Ranking.orderedRank = new List<GameObject>(); 
        Ranking.rankList = new List<GameObject>();
        NumCanvasSeleccionJugadores.ready_P1 = false;
        NumCanvasSeleccionJugadores.ready_P2 = false;
        NumCanvasSeleccionJugadores.ready_P3 = false;
        NumCanvasSeleccionJugadores.ready_P4 = false;
    }
  
}
