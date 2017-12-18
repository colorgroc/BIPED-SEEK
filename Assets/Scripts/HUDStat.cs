using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HUDStat {
	[SerializeField]
	private HUDScript score;
	[SerializeField]
	private HUDScript kills;
	[SerializeField]
	private HUDScript survived;

	[SerializeField]
	private float scoreVal;
	[SerializeField]
	private float killVal;
	[SerializeField]
	private float survivedVal;


	public float ScoreVal{
		get{
			return scoreVal;
		}
		set{
			this.scoreVal = value;
			score.ValueScore = scoreVal;
		}
	}
	public float KillVal{
		get{
			return killVal;
		}
		set{
			this.killVal = value;
			score.ValueKill = killVal;
		}
	}
	public float SurvivedVal{
		get{
			return survivedVal;
		}
		set{
			this.survivedVal = value;
			score.ValueSurvived = survivedVal;
		}
	}
}
