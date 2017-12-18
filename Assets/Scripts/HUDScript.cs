using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	[SerializeField]
	private Text valueTextScore;
	[SerializeField]
	private Text valueTextKill;
	[SerializeField]
	private Text valueTextSurvived;

	private float currentVal;
	public float ValueScore {
		set {
			string[] tmp = valueTextScore.text.Split (':');
			valueTextScore.text = tmp [0] + ":" + value;
		}
	}
	public float ValueKill {
		set {
			string[] tmp = valueTextKill.text.Split (':');
			valueTextKill.text = tmp [0] + ":" + value;
		}
	}
	public float ValueSurvived {
		set {
			string[] tmp = valueTextSurvived.text.Split (':');
			valueTextSurvived.text = tmp [0] + ":" + value;
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
