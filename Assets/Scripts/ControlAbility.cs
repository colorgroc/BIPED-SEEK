using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class ControlAbility : MonoBehaviour {

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    //[SerializeField]
    //private AudioClip abilitySound;
    GameObject[] guards;
    GameObject player, guard;
    private string nameObj;
    private int random;
    List<GameObject> guardsList = new List<GameObject>();
    public bool ab1 = false, ab2 = false;
    public Image iconAb;
    //private AudioSource soundSource;

    // Use this for initialization
    void Start()
    {
        //soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        hab = false;
        cooldown = 0;
        timeAb = timeAbility;
        this.ab1 = this.ab2 = false;
        Asignation();
        if (!Abilities_Tutorial.show)
        {
            foreach (GameObject guard in NewControl.guards)
            {
                if (guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
                {
                    guardsList.Add(guard);
                }
            }
        }
        else
        {
            guards = GameObject.FindGameObjectsWithTag("Guard");
            foreach (GameObject guard in guards)
            {
                if (guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
                {
                    guardsList.Add(guard);
                }
            }
        }
    }
    public void Restart()
    {
        cooldown = 0;
        hab = used = false;
        timeAb = timeAbility;
        if (guard != null && guardsList.Count > 0)
            DefaultControl();
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
           // if (timeAb == Time.deltaTime) soundSource.PlayOneShot(abilitySound);
            if (timeAb <= 0)
            {
                used = true;
                hab = false;
                timeAb = timeAbility;
                DefaultControl();
            }
        }
        if (!Abilities_Tutorial.show)
        {
            if (((this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button)) || (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button))) && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("Control");
            }
        }
        else
        {
            if (Input.GetButtonDown("Cancel") && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("Control");
                //Congelar();
            }
        }

        if (Time.timeScale == 1)
        {
            if (this.gameObject.GetComponent<PlayerControl>().cooledDown) this.iconAb.GetComponent<Image>().fillAmount = 0;
            else if (!this.gameObject.GetComponent<PlayerControl>().cooledDown && !hab && !used) this.iconAb.GetComponent<Image>().fillAmount = 1;
        }
    }
    void Control()
    {
        //soundSource.PlayOneShot(abilitySound);
        RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Abilities/Control", this.transform.position);
        ControlChange();
        hab = true;
    }
    void ControlChange()
    {
        random = Random.Range(0, guardsList.Count);
        player = this.gameObject;
        guard = guardsList[random].gameObject;

        ConvertType(guardsList[random].gameObject, "Guard_Controlled_by_Player");
        ConvertType(this.gameObject, "Player_Controlled_by_IA");
    }
    void DefaultControl()
    {
        if (random != 0 && guardsList[random] != null && guard != null)
        {
            guardsList[random] = guard; //si no va, comentar aixo i descomentar lu d sota
                                        //ConvertType(guardsList[random], "Back_to_Guard");
                                        //player = this.gameObject;
            ConvertType(player, "Back_to_Player");
            ConvertType(guard, "Back_to_Guard");
        }

    }

    private void ConvertType(GameObject gO, string type)
    {
        if (type.Equals("Player_Controlled_by_IA"))
        {
            gO.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = true;
            gO.gameObject.GetComponent<AbilitiesControl>().playerControl.enabled = false;
            if (!Abilities_Tutorial.show)
            {

                if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.IMMOBILIZER || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.IMMOBILIZER)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = false;

                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.INVISIBLITY || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.INVISIBLITY)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = false;
                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.SMOKE || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SMOKE)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().smoke.enabled = false;
                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.SPRINT || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SPRINT)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().sprint.enabled = false;
                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.TELEPORT || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.TELEPORT)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().teleport.enabled = false;
                }
            }
            else
            {
                gO.gameObject.GetComponent<AbilitiesControl>().teleport.enabled = false;
                gO.gameObject.GetComponent<AbilitiesControl>().sprint.enabled = false;
                gO.gameObject.GetComponent<AbilitiesControl>().smoke.enabled = false;
                gO.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = false;
                gO.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = false;
            }
        } 
        else if(type.Equals("Guard_Controlled_by_Player"))
        {
            gO.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = false;
            gO.gameObject.GetComponent<AbilitiesControl>().guardController.enabled = true;
            nameObj = gO.name;
            gO.name = "Player_Guard_" + this.name.Substring(this.name.Length - 1);
        }
        else if (type.Equals("Back_to_Player"))
        {
            gO.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = false;
            gO.gameObject.GetComponent<AbilitiesControl>().playerControl.enabled = true;
            gO.gameObject.name = player.name;
            if (!Abilities_Tutorial.show)
            {
                if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.IMMOBILIZER || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.IMMOBILIZER)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = true;
                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.INVISIBLITY || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.INVISIBLITY)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = true;
                }
                /* else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.REPEL || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.REPEL)
                 {
                     gO.gameObject.GetComponent<Repel>().enabled = true;
                 }*/
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.SMOKE || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SMOKE)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().smoke.enabled = true;
                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.SPRINT || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SPRINT)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().sprint.enabled = true;
                }
                else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.TELEPORT || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.TELEPORT)
                {
                    gO.gameObject.GetComponent<AbilitiesControl>().teleport.enabled = true;
                }
            }
            else
            {
                gO.gameObject.GetComponent<AbilitiesControl>().teleport.enabled = true;
                gO.gameObject.GetComponent<AbilitiesControl>().sprint.enabled = true;
                gO.gameObject.GetComponent<AbilitiesControl>().smoke.enabled = true;
                gO.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = true;
                gO.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = true;
            }
       
        }
        else if (type.Equals("Back_to_Guard"))
        {
            gO.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = true;
            gO.gameObject.GetComponent<AbilitiesControl>().guardController.enabled = false;
            gO.name = nameObj;
        }
    }

    void Asignation()
    {
        if (!Abilities_Tutorial.show)
        {
            if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.CONTROL)
            {
                this.ab1 = true;
                this.iconAb = GameObject.Find("Ability1_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_control;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_control;
            }
            else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.CONTROL)
            {
                this.ab2 = true;
                this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
                this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_control;
                this.iconAb.GetComponent<Image>().fillAmount = 1;
                //grisa
                GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_control;
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
