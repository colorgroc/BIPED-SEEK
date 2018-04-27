using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        this.ab1 = this.ab2 = false;
        Asignation();
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
            timeAb += Time.deltaTime;
            if (timeAb >= timeAbility)
            {
                used = true;
                hab = false;
                timeAb = 0;
                this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }

        //if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        //{
        //    Debug.Log("Invisible");
        //    this.gameObject.GetComponentInChildren<Renderer>().GetComponent<SkinnedMeshRenderer>().enabled = false;
        //    hab = true;
        //} 
    }
    void Asignation()
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
    void IconRespawn()
    {
        if (this.ab1 || this.ab2)
        {
            this.iconAb.GetComponent<Image>().fillAmount = cooldown / coolDown;
        }
    }
}
