using System.Xml.Serialization;
public class NPC_Condition_XML {

	public const string HAS_ITEM = "HasItem";
	public const string QUEST_AVAIL = "QuestAvail";
	public const string QUEST_COMPLETED = "QuestCompleted";
	public const string QUEST_STARTED = "QuestStarted";

	[XmlAttribute("type")]
	public string type;

	// Optional Item Id (if required for quest)
	[XmlAttribute("item")]
	public string item;

	[XmlElement("Question")]
	public NPC_Question_XML question;

	public NPC_Condition_Type GetConditionType() {
		switch (type) {
		case HAS_ITEM:
			return NPC_Condition_Type.HasItemReq;
		case QUEST_AVAIL:
			return NPC_Condition_Type.QuestAvailReq;
		case QUEST_COMPLETED:
			return NPC_Condition_Type.QuestCompletedReq;
		case QUEST_STARTED:
			return NPC_Condition_Type.QuestStartedReq;
		default:
			return NPC_Condition_Type.Default;
		}
	}
}
