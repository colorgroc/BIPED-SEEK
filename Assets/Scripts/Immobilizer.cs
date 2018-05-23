using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Immobilizer : MonoBehaviour {

    //[SerializeField]
    public float radius = 30f;
    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    [SerializeField]
    private AudioClip abilitySound;
    Collider[] colliders;
    public bool ab1 = false, ab2 = false;
    public Image iconAb;
    private AudioSource soundSource;

    // Use this for initialization
    void Start()
    {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
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
        MoveAgain();
    }
    public void Update()
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
                MoveAgain();
            }
        }

        if (((this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button)) || (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button))) && !used && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Immobilitzar"); 
            //Congelar();
        }

        if (Time.timeScale == 1)
        {
            if (this.gameObject.GetComponent<PlayerControl>().cooledDown) this.iconAb.GetComponent<Image>().fillAmount = 0;
            else if (!this.gameObject.GetComponent<PlayerControl>().cooledDown && !hab && !used) this.iconAb.GetComponent<Image>().fillAmount = 1;
        }
    }
    void Congelar()
    {
        //soundSource.PlayOneShot(abilitySound);
		RuntimeManager.PlayOneShot("event:/BipedSeek/Player/Abilities/Freeze", this.transform.position);
        Inmobilitzar();
        hab = true;
    }
    void Inmobilitzar()
    {
        colliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.name != "mixamorig:LeftHand")
            {
                //Debug.Log(hit.gameObject.name);
                //GameObject rb = hit.GetComponent<Rigidbody>().gameObject;
                if (hit.gameObject != null && this.gameObject != hit.gameObject)
                {
                    if (hit.gameObject.tag.Equals("Guard") || hit.gameObject.tag.Equals("Killer Guards"))
                    {
                        hit.GetComponent<NPCConnectedPatrol>().freezed = true;
                    }
                    else
                    {
                        hit.GetComponent<PlayerControl>().canAct = false;
                    }
                }
            }
        }
    }

    void MoveAgain()
    {
		if (SceneManager.GetActiveScene ().name != "Tutorial") {
			if (NewControl.guards != null) {
				foreach (GameObject guard in NewControl.guards) {
					guard.GetComponent<NPCConnectedPatrol> ().freezed = false;
				}
			}
			if (NewControl.players.Count > 0) {
				foreach (GameObject player in NewControl.players) {
					player.GetComponent<PlayerControl> ().canAct = true;
				}
			}
			if (NewControl.killers != null) {
				foreach (GameObject killer in NewControl.killers) {
					killer.GetComponent<NPCConnectedPatrol> ().freezed = false;
				}
			}

		} else {
			GameObject[] guards = GameObject.FindGameObjectsWithTag ("Guard");
			foreach (GameObject guard in guards)
				guard.GetComponent<NPCConnectedPatrol> ().freezed = false;
			if (GameObject.Find ("Player 2") != null)
				GameObject.Find ("Player 2").gameObject.GetComponent<PlayerControl> ().canAct = true;
			if (GameObject.Find ("Killer") != null)
				GameObject.Find ("Killer").GetComponent<NPCConnectedPatrol> ().freezed = false; 
		}
    }

    void Asignation()
    {
        if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.IMMOBILIZER)
        {
            this.ab1 = true;
            this.iconAb = GameObject.Find("Ability1_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
            this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_freeze;
            this.iconAb.GetComponent<Image>().fillAmount = 1;
            //grisa
            GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_freeze;
        }
        else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.IMMOBILIZER)
        {
            this.ab2 = true;
            this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
            this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_freeze;
            this.iconAb.GetComponent<Image>().fillAmount = 1;
            //grisa
            GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_freeze;
        }
    }
    void IconRespawn()
    {
        if (this.ab1 || this.ab2)
        {
            this.iconAb.GetComponent<Image>().fillAmount = cooldown / coolDown;
        }
    }

    void IconDuration()
    {
        if (this.ab1 || this.ab2)
        {
            this.iconAb.GetComponent<Image>().fillAmount = timeAb / timeAbility;
        }
    }
}
