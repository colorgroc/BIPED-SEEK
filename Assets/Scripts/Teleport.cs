using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

    private float cooldown;
    private bool hab;
    [SerializeField]
    private int coolDown = 10;
    [SerializeField]
    private AudioClip abilitySound;
    List<GameObject> guardsList = new List<GameObject>();
    public bool ab1 = false, ab2 = false;
    public Image iconAb;
    private AudioSource soundSource;

    // Use this for initialization
    void Start ()
    {
        soundSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        hab = false;
        cooldown = 0;
        this.ab1 = this.ab2 = false;
        Asignation();

        foreach (GameObject guard in NewControl.guards)
        {
            if(guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
            {
                guardsList.Add(guard);
            }
        }
	}
	
	// Update is called once per frames
	void Update () {
        if (hab)
        {   
            cooldown += Time.deltaTime;
            IconRespawn();
            if (cooldown >= coolDown)
            {
                hab = false;
                cooldown = 0;
            }
        }
        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
        {
            TeleportHability();
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
            soundSource.PlayOneShot(abilitySound);
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !hab && !this.gameObject.GetComponent<PlayerControl>().cooledDown)
        {
            TeleportHability();
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
            soundSource.PlayOneShot(abilitySound);
        }

        if (this.gameObject.GetComponent<PlayerControl>().cooledDown) this.iconAb.GetComponent<Image>().fillAmount = 0;
        else if(!this.gameObject.GetComponent<PlayerControl>().cooledDown && !hab) this.iconAb.GetComponent<Image>().fillAmount = 1;
    }

    void IconRespawn()
    {
        if (this.ab1 || this.ab2)
        {
            this.iconAb.GetComponent<Image>().fillAmount = cooldown / coolDown;
        }
    }
    void TeleportHability()
    {
        int random = Random.Range(0, guardsList.Count);
        Vector3 newGuardPos = this.gameObject.transform.position;
        this.gameObject.transform.position = guardsList[random].transform.position;
        NewControl.guards[random].transform.position = newGuardPos;
    }

    void Asignation()
    {
        if(PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.TELEPORT)
        {
            this.ab1 = true;
            this.iconAb = GameObject.Find("Ability1_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
            this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_teleport;
            this.iconAb.GetComponent<Image>().fillAmount = 1;
            //grisa
            GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_teleport;
        }
        else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.TELEPORT)
        {
            this.ab2 = true;
            this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
            this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_teleport;
            this.iconAb.GetComponent<Image>().fillAmount = 1;
            //grisa
            GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_teleport;

        }


    }
}
