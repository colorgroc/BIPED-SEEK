using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController_ControlAbility : MonoBehaviour
{

    [SerializeField]
    private float speed = 20, speedRotation = 140;
    [SerializeField]
    public static float defaultSpeed = 20;
    [HideInInspector]
    public string AxisMovement, AxisRotation;

    [SerializeField]
    private Animator anim;
    private bool canAct;

    NavMeshAgent _navMeshAgent;

    // Use this for initialization
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.anim = this.gameObject.GetComponent<Animator>();

        PlayerPrefs.SetFloat("Speed", defaultSpeed);

        if (this.gameObject.name.Equals("IA_Player 1"))
        {
            this.AxisMovement = "V_LPad_1";
            this.AxisRotation = "H_RPad_1";
            this.canAct = true;
        }
        else if (this.gameObject.name.Equals("IA_Player 2"))
        {
            this.AxisMovement = "V_LPad_2";
            this.AxisRotation = "H_RPad_2";
            this.canAct = true;
        }
        else if (this.gameObject.name.Equals("IA_Player 3"))
        {
            this.AxisMovement = "V_LPad_3";
            this.AxisRotation = "H_RPad_3";
            this.canAct = true;
        }
        else if (this.gameObject.name.Equals("IA_Player 4"))
        {
            this.AxisMovement = "V_LPad_4";
            this.AxisRotation = "H_RPad_4";
            this.canAct = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        speed = PlayerPrefs.GetFloat("Speed");
        if (this.canAct)
        {
            float y = Input.GetAxis(this.AxisMovement) * Time.deltaTime;
            float rX = Input.GetAxis(this.AxisRotation) * Time.deltaTime;

            transform.Translate(0, 0, y * speed);
            transform.Rotate(0, rX * speedRotation, 0);

            _navMeshAgent.SetDestination(transform.position);


            if (y > 0) this.anim.SetBool("isWalkingForward", true);
            else if (y < 0) this.anim.SetBool("isWalkingBack", true);
            else
            {
                this.anim.SetBool("isWalkingForward", false);
                this.anim.SetBool("isWalkingBack", false);
            }
        }

    }
}
