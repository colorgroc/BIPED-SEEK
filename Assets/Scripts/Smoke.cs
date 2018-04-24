using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    private float cooldown, timeAb;
    private bool hab, used;
    [SerializeField]
    private int coolDown = 10, timeAbility = 10;
    GameObject smoke;
    bool ab1 = false, ab2 = false;
    // Use this for initialization
    void Start()
    {
        smoke = (GameObject)Resources.Load("Prefabs/Smoke");
        used = false;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }

        if (this.ab1 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab1Button) && !used && !hab)
        {
            Quaternion quad = new Quaternion(this.transform.rotation.w, -90, this.transform.rotation.y, this.transform.rotation.z);
            GameObject s = Instantiate(smoke, new Vector3(this.transform.position.x, 13.4f, this.transform.position.z), quad);
            s.GetComponent<ParticleSystem>().Play(false);

            hab = true;
        }
        else if (this.ab2 && Input.GetButtonDown(this.gameObject.GetComponent<PlayerControl>().hab2Button) && !used && !hab)
        {
            Quaternion quad = new Quaternion(this.transform.rotation.w, -90, this.transform.rotation.y, this.transform.rotation.z);
            GameObject s = Instantiate(smoke, new Vector3(this.transform.position.x, 13.4f, this.transform.position.z), quad);
            s.GetComponent<ParticleSystem>().Play(false);

            hab = true;
        }
    }
}
