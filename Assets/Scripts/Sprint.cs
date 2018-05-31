using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Sprint : MonoBehaviour {

    private float cooldown, timeAb, speed;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    //[SerializeField]
    //private AudioClip abilitySound;
    public bool ab1 = false, ab2 = false;
    [SerializeField]
    private float sprint = 1.7f;
    public Image iconAb;
    //private AudioSource soundSource;

    // Use this for initialization
    void Start()
    {
        //soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        used = false;
        cooldown = 0;
        timeAb = timeAbility;
        //speed = this.gameObject.GetComponent<PlayerControl>().GetSpeed();
        speed = PlayerPrefs.GetFloat("Speed");
        this.ab1 = this.ab2 = false;
        Asignation();
    }
    public void Restart()
    {
        cooldown = 0;
        hab = used = false;
        timeAb = timeAbility;
        this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed);
        this.gameObject.GetComponent<PlayerControl>()._sprint = false;
    }
    // Update is called once per frame
    void Update()
    {
        speed = PlayerPrefs.GetFloat("Speed");
        //NewContol.guards;
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
                this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed);
                this.gameObject.GetComponent<PlayerControl>()._sprint = false;

            }
        }
        if (!Abilities_Tutorial.show)
        {
            if (((this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button)) || (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button))) && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                //si hi ha animacio abans d fer l'sprint posar aixo
                //this.gameObject.GetComponent<Animator>().SetTrigger("Sprint");
                //si no, posar aixo
                Speed();
            }
        }
        else
        {
            if (Input.GetButtonDown("Main Menu") && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                Speed();
                //Congelar();
            }
        }
        if (Time.timeScale == 1)
        {
            if (this.gameObject.GetComponent<PlayerControl>().cooledDown) this.iconAb.GetComponent<Image>().fillAmount = 0;
            else if (!this.gameObject.GetComponent<PlayerControl>().cooledDown && !hab && !used) this.iconAb.GetComponent<Image>().fillAmount = 1;
        }
    }

    void Speed()
    {
        //soundSource.PlayOneShot(abilitySound);
		RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Abilities/Sprint", this.transform.position);
        this.gameObject.GetComponent<PlayerControl>()._sprint = true;
        this.gameObject.GetComponent<PlayerControl>().SetSpeed(sprint);
        hab = true;
    }

    void Asignation()
    {
        if (!Abilities_Tutorial.show)
        {
            if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.SPRINT)
            {
                this.ab1 = true;
                this.iconAb = GameObject.Find("Ability1_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_sprint;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_sprint;
            }
            else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SPRINT)
            {
                this.ab2 = true;
                this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_sprint;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_sprint;
            }
        }
    }
    void IconRespawn()
    {
        if (Abilities_Tutorial.show)
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
        if (Abilities_Tutorial.show)
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
