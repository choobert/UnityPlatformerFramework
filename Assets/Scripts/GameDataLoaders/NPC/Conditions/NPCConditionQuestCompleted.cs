using UnityEngine;
using System.Collections;

public class NPCConditionQuestCompleted : NPC_Condition {

	private static GameManager _gm;

	private string npcId;

	public NPCConditionQuestCompleted(string npcId) {
		this.npcId = npcId;
		type = NPC_Condition_Type.QuestCompletedReq;
	}

	public override bool IsConditionMet() {
		NPC npc = NPCManager.GetNPCByID (npcId);

		if (npc is QuestGiver) {
			return ((QuestGiver)npc).IsQuestComplete ();
		} else {
			return false;
		}
	}
}
