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
	private HUDScript score2;
	[SerializeField]
	private HUDScript kills2;
	[SerializeField]
	private HUDScript survived2;

	[SerializeField]
	private float scoreVal;
	[SerializeField]
	private float killVal;
	[SerializeField]
	private float survivedVal;

	[SerializeField]
	private float scoreVal2;
	[SerializeField]
	private float killVal2;
	[SerializeField]
	private float survivedVal2;


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

	public float ScoreVal2{
		get{
			return scoreVal2;
		}
		set{
			this.scoreVal2 = value;
			score2.ValueScore2 = scoreVal2;
		}
	}
	public float KillVal2{
		get{
			return killVal2;
		}
		set{
			this.killVal2 = value;
			score2.ValueKill2 = killVal2;
		}
	}
	public float SurvivedVal2{
		get{
			return survivedVal2;
		}
		set{
			this.survivedVal2 = value;
			score2.ValueSurvived2 = survivedVal2;
		}
	}
}
