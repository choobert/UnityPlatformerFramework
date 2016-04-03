using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnDialogueCompleteHandler();

public class DialogueManager : MonoBehaviour {

	private static DialogueManager _instance;
	private static GameManager _gm;

	private const string NPCS_XML_PATH = "GameData/NPCs";
	private const string DIALOGUE_XML_PATH = "GameData/Dialogue";

	//private Dictionary<ConditionType, Condition> conditionDictionary;
	private Dictionary<string, NPC_XML> npcDictionary;
	private Dictionary<string, Dialogue_Message_XML> questionsDictionary;
	private Dictionary<string, Dialogue_Message_XML> answersDictionary;

	private List<NPC_Answer_XML> currentAnswers;

	private Dialogue dialoguePanel;

	public OnDialogueCompleteHandler OnDialogueComplete;

	/*
	* Find/Create/Return our one and only Game Manager object
	* for the game
	**/
	public static DialogueManager Instance {
		get {
			// if we do not have an instance already, lets look to see
			// if one has already been created for us
			if (_instance == null) {
				_instance = Object.FindObjectOfType<DialogueManager> ();
			}

			// If we still dont have an instance, it must not exist,
			// so lets create our own and add it to the GameManager object
			if (_instance == null) {
				
				// Find the GameManager then add the DialogueManager to the same GameObject
				_gm = GameManager.Instance;
				_instance = _gm.gameObject.AddComponent<DialogueManager> ();

				// Create our dictionaries
				_instance.npcDictionary = NPC_Collection.GetDictionary (NPCS_XML_PATH);
			}

			return _instance;
		}
	}

	public void BeginDialogue(string npcId) {
		NPC_XML dialogue = _instance.npcDictionary [npcId];
		_instance.UpdateDialogue(GetFirstPassingCondition(npcId, dialogue.conditions));
		_instance.dialoguePanel.Launch ();
	}

	private void UpdateDialogue(NPC_Condition_XML trueCondition) {
		currentAnswers = trueCondition.question.answers;

		List<Dialogue_Message_XML> tempAnswers = new List<Dialogue_Message_XML> ();
		foreach (NPC_Answer_XML a in currentAnswers) {
			tempAnswers.Add (answersDictionary [a.id]);
		}

		dialoguePanel.UpdateMessages (questionsDictionary [trueCondition.question.id], tempAnswers);
	}

	private NPC_Condition_XML GetFirstPassingCondition(string npcId, List<NPC_Condition_XML> conditions) {
		foreach (NPC_Condition_XML cXML in conditions) {
			NPC_Condition c = ConditionXMLToCondition (npcId, cXML);
			if (c.IsConditionMet ()) {
				return cXML;
			}
		}

		return null;
	}

	private NPC_Condition ConditionXMLToCondition(string npcId, NPC_Condition_XML xml) {
		switch (xml.GetConditionType ()) {
		case NPC_Condition_Type.HasItemReq:
			return new NPCConditionHasItem (xml.item);
		case NPC_Condition_Type.QuestAvailReq:
			return new NPCConditionQuestAvail(npcId);
		case NPC_Condition_Type.QuestStartedReq:
			return new NPCConditionQuestStarted (npcId);
		case NPC_Condition_Type.QuestCompletedReq:
			return new NPCConditionQuestCompleted (npcId);
		case NPC_Condition_Type.Default:
		default:
			return new NPCConditionDefault();
		}
	}
}
