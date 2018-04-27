using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : MonoBehaviour {

    private float cooldown, timeAb, speed;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    [SerializeField]
    private AudioClip abilitySound;
    public bool ab1 = false, ab2 = false;
    [SerializeField]
    private float sprint = 1.7f;
    public Image iconAb;
    private AudioSource soundSource;

    // Use this for initialization
    void Start()
    {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        used = false;
        cooldown = 0;
        //speed = this.gameObject.GetComponent<PlayerControl>().GetSpeed();
        speed = PlayerPrefs.GetFloat("Speed");
        this.ab1 = this.ab2 = false;
        Asignation();
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
            timeAb += Time.deltaTime;
            if (timeAb == Time.deltaTime) soundSource.PlayOneShot(abilitySound);
            if (timeAb >= timeAbility)
            {
                used = true;
                hab = false;
                timeAb = 0;
                this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed);
                this.gameObject.GetComponent<PlayerControl>()._sprint = false;
                // PlayerPrefs.SetFloat("Speed", speed);

            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed * 1.7f);
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed * 1.7f);
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }

        //if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab3Button) && !used && !hab)
        //{
        //    Debug.Log("Sprint");
        //    //PlayerPrefs.SetFloat("Speed", speed*sprint);
        //    this.gameObject.GetComponent<PlayerControl>().sprint = true;
        //    this.gameObject.GetComponent<PlayerControl>().SetSpeed(sprint);
        //    hab = true;
        //}
    }

    void Asignation()
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
    void IconRespawn()
    {
        if (this.ab1 || this.ab2)
        {
            this.iconAb.GetComponent<Image>().fillAmount = cooldown / coolDown;
        }
    }
}
