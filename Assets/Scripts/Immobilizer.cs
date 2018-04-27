using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Immobilizer : MonoBehaviour {

    //[SerializeField]
    public float radius = 10f;
    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    Collider[] colliders;
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
            timeAb += Time.deltaTime;
            if (timeAb >= timeAbility)
            {
                used = true;
                hab = false;
                timeAb = 0;
                MoveAgain();
            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            Inmobilitzar();
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            Inmobilitzar();
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }
        //if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab4Button) && !used && !hab)
        //{
        //    Debug.Log("Immobile");
        //    Inmobilitzar();
        //    hab = true;
        //}
    }

    void Inmobilitzar()
    {
        colliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hit in colliders)
        {
         
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && this.gameObject.GetComponent<Rigidbody>() != rb)
            {
                //rb.Sleep();
                if (rb.gameObject.tag.Equals("Guard") || rb.gameObject.tag.Equals("Killer Guards"))
                {
                    //Debug.Log("wasup");
                    hit.GetComponent<NPCConnectedPatrol>().freezed = true;
                }
                else
                {
                    hit.GetComponent<PlayerControl>().canAct = false;
                }
            }
            //en players variable canAct = false --> posarla en el moviment
            //en guards/killers --> travelling = false;
        }
    }

    void MoveAgain()
    {
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {// && rb.IsSleeping())
             //rb.WakeUp();
                if (rb.gameObject.tag.Equals("Guard"))
                {
                    hit.GetComponent<NPCConnectedPatrol>().freezed = false;
                }
                else
                {
                    hit.GetComponent<PlayerControl>().canAct = true;
                }
            }
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
}
