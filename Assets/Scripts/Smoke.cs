using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Smoke : MonoBehaviour {

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    //[SerializeField]
    //private AudioClip abilitySound;
    GameObject smoke;
    public bool ab1 = false, ab2 = false;
    public Image iconAb;
    //private AudioSource soundSource;
    GameObject s;

    // Use this for initialization
    private void Start()
    {
        //soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        smoke = (GameObject)Resources.Load("Prefabs/Smoke");
        hab = used = false;
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
        if (s != null)
        {
            s.GetComponent<ParticleSystem>().Stop(false);
            Destroy(s);
        }
    }
    // Update is called once per frame
    void Update()
    {
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
            }
        }
        if (!Abilities_Tutorial.show)
        {
            if (((this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button)) || (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button))) && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                //TirarSmoke
                this.gameObject.GetComponent<Animator>().SetTrigger("Smoke");
                RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Abilities/Smoke", this.transform.position);

            }
        }
        else
        {
            if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("Smoke");
                RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Abilities/Smoke", this.transform.position);
                //Congelar();
            }
        }
        if (Time.timeScale == 1)
        {
            if (this.gameObject.GetComponent<PlayerControl>().cooledDown) this.iconAb.GetComponent<Image>().fillAmount = 0;
            else if (!this.gameObject.GetComponent<PlayerControl>().cooledDown && !hab && !used) this.iconAb.GetComponent<Image>().fillAmount = 1;
        }
    }
    void TirarSmoke()
    {
        s = Instantiate(smoke, new Vector3(this.transform.position.x, this.transform.position.y + 3.4f, this.transform.position.z), this.transform.rotation);
        s.GetComponent<ParticleSystem>().Play(false);
        hab = true;
        //soundSource.PlayOneShot(abilitySound);
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

    void Asignation()
    {
        if (!Abilities_Tutorial.show)
        {
            if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.SMOKE)
            {
                this.ab1 = true;
                this.iconAb = GameObject.Find("Ability1_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_smoke;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_smoke;
            }
            else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SMOKE)
            {
                this.ab2 = true;
                this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_smoke;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_smoke;
            }
        }
    }
}
