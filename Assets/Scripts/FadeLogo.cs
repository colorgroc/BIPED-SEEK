using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeLogo : MonoBehaviour {

    public Image icon, gameTitle, image;
    public int timeToShowLogo, timeToShowGame, timeBetween;
    private float ticksDone, ticksDoneGame, timePast;
    private float alpha, color, alphaGame;
    private bool finish, finish2;

    // Use this for initialization
    void Start()
    {

        finish = finish2 = false;
        timePast = 0;
        alpha = alphaGame = 0f;
        color = 1f;
        icon.color = new Color(1f, 1f, 1f, alpha);
        gameTitle.color = new Color(1f, 1f, 1f, alphaGame);
        image.color = new Color(1f, 1f, 1f, 1f);
        ticksDone = ticksDoneGame = 0;

    }

    private void FixedUpdate()
    {
        if (!finish)
        {
            if (ticksDone < timeToShowLogo)
            {
                FadeInLogo();
            }
            if (ticksDone >= timeToShowLogo)
            {
                FadeOutLogo();

            }
            else
            {
                ticksDone += Time.fixedDeltaTime;
                Debug.Log(ticksDone);
            }
        }
        else
        {
            timePast += Time.fixedDeltaTime;
            if (timePast >= timeBetween) finish2 = true;
        }

        if (finish2)
        {
            if (ticksDoneGame < timeToShowGame)
            {
                FadeInGame();
            }
            if (ticksDoneGame >= timeToShowGame)
            {
                FadeOutGame();

            }
            else
            {
                ticksDoneGame += Time.fixedDeltaTime;
            }
        }
    }

    void FadeOutLogo()
    {
        icon.color = new Color(1f, 1f, 1f, alpha);
        if (alpha > 0f)
            alpha -= 0.01f;
        if (alpha <= 0f)
        {
            finish = true;
        }   
    }

    void FadeInLogo()
    {
        icon.color = new Color(1f, 1f, 1f, alpha);
        if (alpha < 1f)
            alpha += 0.01f;
    }

    void FadeOutGame()
    {
        gameTitle.color = new Color(1f, 1f, 1f, alphaGame);
        if (alphaGame > 0f)
            alphaGame -= 0.01f;
        if (alphaGame <= 0f)
        {
            finish2 = false;
            FadeScene();
        }
    }

    void FadeInGame()
    {
        gameTitle.color = new Color(1f, 1f, 1f, alphaGame);
        if (alphaGame < 1f)
            alphaGame += 0.01f;
    }

    void FadeScene()
    {
        image.color = new Color(color, color, color, 1f);
        color -= 0.01f;
        if (color <= 0f)
            SceneManager.LoadScene("Menu");
    }

  

}
