using UnityEngine;
using System.Collections;

public class NPCConditionDefault : NPC_Condition{

	public NPCConditionDefault() {
		type = NPC_Condition_Type.Default;
	}

	public override bool IsConditionMet() {
		return true;
	}
}
