using UnityEngine;
using System.Collections;

public class NPCConditionQuestAvail : NPC_Condition {

	private static GameManager _gm;

	private string npcId;

	public NPCConditionQuestAvail(string npcId) {
		this.npcId = npcId;
		type = NPC_Condition_Type.QuestAvailReq;
	}

	public override bool IsConditionMet() {
		NPC npc = NPCManager.Instance.GetNPCByID (npcId);

        try
        {
            QuestGiver qg = (QuestGiver)npc;
            return qg.IsQuestAvail();
        }
        catch
        {
            return false;
        }
	}
}
