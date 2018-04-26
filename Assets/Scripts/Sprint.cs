using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour {

    private float cooldown, timeAb, speed;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    public bool ab1 = false, ab2 = false;
    [SerializeField]
    private float sprint = 1.7f;
    // Use this for initialization
    void Start()
    {
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
                this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed);
                this.gameObject.GetComponent<PlayerControl>()._sprint = false;
                // PlayerPrefs.SetFloat("Speed", speed);

            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed * 1.7f);
            hab = true;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            this.gameObject.GetComponent<PlayerControl>().SetSpeed(speed * 1.7f);
            hab = true;
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
        }
        else if (PlayerPrefs.GetInt("Ability 2") == (int)NewControl.Abilities.SPRINT)
        {
            this.ab2 = true;
        }
    }
}
