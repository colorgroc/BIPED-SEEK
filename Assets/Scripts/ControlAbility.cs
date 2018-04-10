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
    // Use this for initialization
    void Start()
    {
        hab = false;
        cooldown = 0;
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
        if (Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab5Button) && !used)
        {
            ControlChange();
            hab = true;
        }
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
        ConvertType(player, "Back_to_Player");
        
    }

    private void ConvertType(GameObject gO, string type)
    {
        if (type.Equals("Player_Controlled_by_IA"))
        {
            gO.gameObject.GetComponent<NPCConnectedPatrol>().enabled = true;
            gO.gameObject.GetComponent<PlayerControl>().enabled = false;
            //gO.gameObject.GetComponent<Immobilizer>().enabled = false;
            //gO.gameObject.GetComponent<Repel>().enabled = false;
            //gO.gameObject.GetComponent<Sprint>().enabled = false;
            //gO.gameObject.GetComponent<Invisibility>().enabled = false;
            //gO.gameObject.GetComponent<Teleport>().enabled = false;
        } 
        else if(type.Equals("Guard_Controlled_by_Player"))
        {
            gO.gameObject.GetComponent<NPCConnectedPatrol>().enabled = false;
            gO.gameObject.GetComponent<GuardController_ControlAbility>().enabled = true;
            nameObj = gO.name;
            gO.name = "IA_" + gO.name;
        }
        else if (type.Equals("Back_to_Player"))
        {
            gO.gameObject.GetComponent<NPCConnectedPatrol>().enabled = false;
            gO.gameObject.GetComponent<PlayerControl>().enabled = true;
            
            //gO.gameObject.GetComponent<Immobilizer>().enabled = false;
            //gO.gameObject.GetComponent<Repel>().enabled = false;
            //gO.gameObject.GetComponent<Sprint>().enabled = false;
            //gO.gameObject.GetComponent<Invisibility>().enabled = false;
            //gO.gameObject.GetComponent<Teleport>().enabled = false;
        }
        else if (type.Equals("Back_to_Guard"))
        {
            gO.gameObject.GetComponent<NPCConnectedPatrol>().enabled = true;
            gO.gameObject.GetComponent<GuardController_ControlAbility>().enabled = false;
            gO.name = nameObj;
        }
    }
}
