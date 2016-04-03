using UnityEngine;
using System.Collections;

public class NPCConditionHasItem : NPC_Condition {

	private string itemId;

	public NPCConditionHasItem(string itemId) {
		type = NPC_Condition_Type.HasItemReq;
	}

	public override bool IsConditionMet() {
		return true;
	}
}
