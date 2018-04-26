using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

    private float cooldown;
    private bool hab;
    [SerializeField]
    private int coolDown = 10;
    List<GameObject> guardsList = new List<GameObject>();
    public bool ab1 = false, ab2 = false;
    public Image iconAb;
    // Use this for initialization
    void Start () {
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
        //NewContol.guards;
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
        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !hab)
        {
            TeleportHability();
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !hab)
        {
            TeleportHability();
            hab = true;
            this.iconAb.GetComponent<Image>().fillAmount = 0;
        }

        //if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab5Button) && !hab)
        //{
        //    Debug.Log("Teleport");
        //    TeleportHability();
        //    hab = true;
        //}
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
            GameObject.Find("Ability1_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_smoke;
        }
        else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.TELEPORT)
        {
            this.ab2 = true;
            this.iconAb = GameObject.Find("Ability2_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>();
            this.iconAb.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_teleport;
            this.iconAb.GetComponent<Image>().fillAmount = 1;
            //grisa
            GameObject.Find("Ability2_Grey_" + this.gameObject.name.Substring(this.gameObject.name.Length - 1)).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<AbilitiesControl>().s_smoke;

        }


    }
}
