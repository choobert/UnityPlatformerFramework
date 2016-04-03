
public enum NPC_Condition_Type {Default, HasItemReq, QuestAvailReq, QuestCompletedReq, QuestStartedReq}

public abstract class NPC_Condition {
	protected NPC_Condition_Type type;

	public abstract bool IsConditionMet ();
}
