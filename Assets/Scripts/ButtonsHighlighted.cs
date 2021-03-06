﻿using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using FMODUnity;

public class ButtonsHighlighted : MonoBehaviour, ISelectHandler
{
    public bool isVolume, isButton, isDropdown, isToggle;
    private Vector3 initialScale;
    [SerializeField]
    private float duration;// FadeTimer, fadeCoroutine;
    private bool selected = false;
    private float x, y, z;
    float time;
    //private float duration = 1f;
    private bool change = true;
    //[SerializeField]
    //private AudioClip onButtonSound;
    //private AudioSource soundSource;

    private void Start()
    {
        //soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        initialScale = this.transform.localScale;
        time = 0;
        x = initialScale.x;
        y = initialScale.y;
        z = initialScale.z;
    }

    private void Update()
    {
        if (selected)
        {
            time += Time.fixedDeltaTime;

            if (time >= duration)
            {
                change = !change;
                time = 0;
            }

            if (change)
            {
                x += 0.01f*Time.fixedDeltaTime;
                y += 0.01f * Time.fixedDeltaTime;
                z += 0.01f * Time.fixedDeltaTime;
                //if (x >= 0.06f) x = 0.06f;
                //if (y >= 0.14f) y = 0.14f;
                //if (z >= 0.15f) z = 0.15f;
                this.transform.localScale = new Vector3(x, y, z);
                

            }
            else if (!change)
            {
                x -= 0.01f * Time.fixedDeltaTime;
                y -= 0.01f * Time.fixedDeltaTime;
                z -= 0.01f * Time.fixedDeltaTime;
                if (x <= initialScale.x) x = initialScale.x;
                if (y <= initialScale.y) y = initialScale.y;
                if (z <= initialScale.z) z = initialScale.z;
                this.transform.localScale = new Vector3(x, y, z);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
        if (isVolume)
        {
            //Menu.inVolume = true;
            this.GetComponentInChildren<Outline>().enabled = true;
        }
        else if (isDropdown)
            this.GetComponent<Outline>().enabled = true;
        else if (isButton)
        {
            selected = true;
            //this.transform.GetComponent<Image>().CrossFadeAlpha(0.5f, 0, true);
            this.transform.localScale = new Vector3(0.06f, 0.14f, 0.15f);
            //StartCoroutine(FadeINFadeOut(FadeTimer, fadeCoroutine));
        }
       // Debug.Log("Select");
    }
    public void OnDeselect(BaseEventData eventData)
    {
        //if(soundSource != null)
        //soundSource.PlayOneShot(onButtonSound);

        //do your stuff when not selected
        if (isVolume)
        {
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            //Menu.inVolume = false;
            this.GetComponentInChildren<Outline>().enabled = false;
        }
        else if (isDropdown)
        {
            this.GetComponent<Outline>().enabled = false;
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
        }
        else if (isButton)
        {
            selected = false;
            this.transform.localScale = initialScale;
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Navigate", Vector3.zero);
            // this.transform.GetComponent<Image>().CrossFadeAlpha(1, 0, true);
        }
        else if (isToggle)
        {
            RuntimeManager.PlayOneShot("event:/BipedSeek/Menus/Accept", Vector3.zero);
        }

        //  Debug.Log("Deselect");
    }

    IEnumerator FadeINFadeOut(float FadeTime, float fadeCoroutine)
    {
        this.transform.GetComponent<Image>().CrossFadeAlpha(1, FadeTime, true);
        if (selected)
        {
            this.transform.GetComponent<Image>().CrossFadeAlpha(1, 0, true);
            yield break;
        }
        yield return new WaitForSeconds(fadeCoroutine);
        this.transform.GetComponent<Image>().CrossFadeAlpha(0.5f, FadeTime, true);
        if (selected)
        {
            this.transform.GetComponent<Image>().CrossFadeAlpha(1, 0, true);
            yield break;
        }
        yield return new WaitForSeconds(fadeCoroutine);
        StartCoroutine(FadeINFadeOut(FadeTime, fadeCoroutine));
    }

}
