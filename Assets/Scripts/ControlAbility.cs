using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAbility : MonoBehaviour {

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    GameObject player, guard;
    private string nameObj;
    private int random;
    List<GameObject> guardsList = new List<GameObject>();
    public bool ab1 = false, ab2 = false;
    // Use this for initialization
    void Start()
    {
        hab = false;
        cooldown = 0;
        this.ab1 = this.ab2 = false;
        Asignation();

        foreach (GameObject guard in NewControl.guards)
        {
            if (guard.name.EndsWith(this.gameObject.name.Substring(this.name.Length - 1)))
            {
                guardsList.Add(guard);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            cooldown += Time.deltaTime;
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
                DefaultControl();
            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            ControlChange();
            hab = true;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            ControlChange();
            hab = true;
        }
        //if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab6Button) && !used)
        //{
        //    Debug.Log("Control");
        //    ControlChange();
        //    hab = true;
        //}
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
        guardsList[random] = guard; //si no va, comentar aixo i descomentar lu d sota
        //ConvertType(guardsList[random], "Back_to_Guard");
        //player = this.gameObject;
        ConvertType(player, "Back_to_Player");
        ConvertType(guard, "Back_to_Guard");

    }

    private void ConvertType(GameObject gO, string type)
    {
        if (type.Equals("Player_Controlled_by_IA"))
        {
            gO.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = true;
            gO.gameObject.GetComponent<AbilitiesControl>().playerControl.enabled = false;

            if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.IMMOBILIZER || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.IMMOBILIZER)
            {
                gO.gameObject.GetComponent<AbilitiesControl>().freeze.enabled = false;
            }
            else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.INVISIBLITY || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.INVISIBLITY)
            {
                gO.gameObject.GetComponent<AbilitiesControl>().invisibility.enabled = false;
            }
           /* else if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.REPEL || PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.REPEL)
            {
                gO.gameObject.GetComponent<Repel>().enabled = false;
            }*/
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
        else if (type.Equals("Back_to_Guard"))
        {
            gO.gameObject.GetComponent<AbilitiesControl>().nPCConnectedPatrol.enabled = true;
            gO.gameObject.GetComponent<AbilitiesControl>().guardController.enabled = false;
            gO.name = nameObj;
        }
    }

    void Asignation()
    {
        if (PlayerPrefs.GetInt("Ability 1") == (int)NewControl.Abilities.CONTROL)
        {
            this.ab1 = true;
        }
        else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.CONTROL)
        {
            this.ab2 = true;
        }
    }
}
