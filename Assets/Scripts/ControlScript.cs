using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript: MonoBehaviour{
	//[HideInInspector]
	public static bool detected1;
	//[HideInInspector]
	public static bool detected2;
	//[HideInInspector]
	public static bool detected3;
	//[HideInInspector]
	public static bool detected4;

	public static bool player_1_WannaKill;
	public static bool player_2_WannaKill;
	public static bool player_3_WannaKill;
	public static bool player_4_WannaKill;

	public static bool onFieldView_1;
	public static bool onFieldView_2;
	public static bool onFieldView_3;
	public static bool onFieldView_4;

	public static float timePast1;
	public static float timePast2;
	public static float timePast3;
	public static float timePast4;

	public static GameObject[] killers;

	public static GameObject objective;
	// Use this for initialization
	void Start () {
		timePast1 = 0;
		timePast2 = 0;
		timePast3 = 0;
		timePast4 = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
