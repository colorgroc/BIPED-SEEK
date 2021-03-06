﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Invisibility : MonoBehaviour {

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    public bool ab1 = false, ab2 = false;
    public Image iconAb;

    // Use this for initialization
    void Start()
    {
        used = false;
        cooldown = 0;
        timeAb = timeAbility;
        this.ab1 = this.ab2 = false;
        Asignation();
    }
    public void Restart()
    {
        cooldown = 0;
        hab = used = false;
        timeAb = timeAbility;
        this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            cooldown += Time.deltaTime;
            IconRespawn();
            if (cooldown >= coolDown)
            {
                used = false;
                cooldown = 0;
            }
        }

        if (hab)
        {
            timeAb -= Time.deltaTime;
            IconDuration();
            if (timeAb <= 0)
            {
                used = true;
                hab = false;
                timeAb = timeAbility;
                this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }
        if (!Abilities_Tutorial.show)
        {
            if (((this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button)) || (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button))) && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("Invisible");
            }
        }
        else
        {
            if (Input.GetButtonDown("Submit") && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown && Abilities_Tutorial.canAct)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("Invisible");
            }
        }
        if (Time.timeScale == 1)
        {
            if (this.gameObject.GetComponent<PlayerControl>().cooledDown) this.iconAb.GetComponent<Image>().fillAmount = 0;
            else if (!this.gameObject.GetComponent<PlayerControl>().cooledDown && !hab && !used) this.iconAb.GetComponent<Image>().fillAmount = 1;
        }
    }

    void Invisible()
    {
        RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Abilities/Invisible", this.transform.position);
        this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
        hab = true;
    }

    void Asignation()
    {
        if (!Abilities_Tutorial.show)
        {
            if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.INVISIBLITY)
            {
                this.ab1 = true;
                this.iconAb = GameObject.Find("Ability1_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_invisible;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_invisible;
            }
            else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.INVISIBLITY)
            {
                this.ab2 = true;
                this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_invisible;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_invisible;
            }
        }
    }
    void IconRespawn()
    {
        if (!Abilities_Tutorial.show)
        {
            if (this.ab1 || this.ab2)
            {
                this.iconAb.GetComponent<Image>().fillAmount = cooldown / coolDown;
            }
        }
        else
        {
            this.iconAb.GetComponent<Image>().fillAmount = cooldown / coolDown;
        }
    }

    void IconDuration()
    {
        if (!Abilities_Tutorial.show)
        {
            if (this.ab1 || this.ab2)
            {
                this.iconAb.GetComponent<Image>().fillAmount = timeAb / timeAbility;
            }
        }
        else
        {
            this.iconAb.GetComponent<Image>().fillAmount = timeAb / timeAbility;
        }
    }
}
