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

	[SerializeField]
	private Text valueTextScore2;
	[SerializeField]
	private Text valueTextKill2;
	[SerializeField]
	private Text valueTextSurvived2;

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

	public float ValueScore2 {
		set {
			string[] tmp = valueTextScore2.text.Split (':');
			valueTextScore2.text = tmp [0] + ":" + value;
		}
	}
	public float ValueKill2 {
		set {
			string[] tmp = valueTextKill2.text.Split (':');
			valueTextKill2.text = tmp [0] + ":" + value;
		}
	}
	public float ValueSurvived2 {
		set {
			string[] tmp = valueTextSurvived2.text.Split (':');
			valueTextSurvived2.text = tmp [0] + ":" + value;
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
