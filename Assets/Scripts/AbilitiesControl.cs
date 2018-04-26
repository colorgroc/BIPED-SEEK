using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesControl : MonoBehaviour {
    public Teleport teleport;
    public Sprint sprint;
    public ControlAbility control;
    public GuardController_ControlAbility guardController;
    public Immobilizer freeze;
    public Smoke smoke;
    public Invisibility invisibility;

    public PlayerControl playerControl;
    public NPCConnectedPatrol nPCConnectedPatrol;
    public FieldOfView fieldOfView;

    //[SerializeField]
    public Sprite s_teleport, s_sprint, s_control, s_freeze, s_smoke, s_invisible; //poner sprites

}
