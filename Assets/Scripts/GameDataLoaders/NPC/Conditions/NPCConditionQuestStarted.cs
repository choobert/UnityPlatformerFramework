using UnityEngine;
using System.Collections;

public class NPCConditionQuestStarted : NPC_Condition {

	private static GameManager _gm;

	private string npcId;

	public NPCConditionQuestStarted(string npcId) {
		this.npcId = npcId;
		type = NPC_Condition_Type.QuestStartedReq;
	}

	public override bool IsConditionMet() {
		NPC npc = NPCManager.GetNPCByID (npcId);

		if (npc is QuestGiver) {
			return ((QuestGiver)npc).IsQuestStarted ();
		} else {
			return false;
		}
	}
}
