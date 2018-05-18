using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerHUD : MonoBehaviour {

	public Text player;
    public Image cup, animal;
    [SerializeField]
    private Sprite cup1, cup2, cup3, cup4, animal1, animal2, animal3, animal4;
    [SerializeField]
    private AudioClip winnerSound;
    private AudioSource soundSource;

    private void Awake()
    {
    }
    void Start () {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        NewControl.Winner();
        soundSource.PlayOneShot(winnerSound);
        //asignacio copa i animal del guanyador
        if (NewControl.finalWinner != null)
        {
            player.text = NewControl.finalWinner.name;
          
            if(NewControl.finalWinner.name.Substring(NewControl.finalWinner.name.Length - 1).Equals("1"))
            {
                cup.sprite = cup1;

                if(PlayerPrefs.GetInt("characterPlayer_1") == 1)
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
            else if (NewControl.finalWinner.name.Substring(NewControl.finalWinner.name.Length - 1).Equals("2"))
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
            else if (NewControl.finalWinner.name.Substring(NewControl.finalWinner.name.Length - 1).Equals("3"))
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
            else if (NewControl.finalWinner.name.Substring(NewControl.finalWinner.name.Length - 1).Equals("4"))
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
            // cup.sprite = (Sprite)Resources.Load("Winner/WINNER_" + NewControl.finalWinner.name);
            //animal.sprite = (Sprite)Resources.Load("Winner/WINNER_" + PlayerPrefs.GetInt("characterPlayer_" + NewControl.finalWinner.name.Substring(NewControl.finalWinner.name.Length - 1)).ToString());
        }
    }
	void Update()
    {
       

        if (Input.GetButtonDown("Submit")) {
			this.gameObject.SetActive (false);
			Time.timeScale = 1;
 
			SceneManager.LoadScene ("Menu");

		} else Time.timeScale = 0;
        
	}

    void Default()
    {
        NewControl.killers = null;
        NewControl.players = null;
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
  
}
