using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeLogo : MonoBehaviour {

    public Image icon, gameTitle, image;
    public int ticksToWait, ticksToWaitGame;
    private float ticksDone, ticksDoneGame;
    private float alpha, color, alphaGame;
    private bool finish;
    public float duration = 3f;

    // Use this for initialization
    void Start()
    {

        finish = false;
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
            if (ticksDone < ticksToWait)
            {
                FadeInLogo();
            }
            if (ticksDone >= ticksToWait)
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
            if (ticksDoneGame < ticksToWaitGame)
            {
                FadeInGame();
            }
            if (ticksDoneGame >= ticksToWaitGame)
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
