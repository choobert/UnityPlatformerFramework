using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnDialogueCompleteHandler();

public class DialogueManager : MonoBehaviour {

	private static DialogueManager _instance;
	private static GameManager _gm;

	//private Dictionary<ConditionType, Condition> conditionDictionary;
	private List<NPC_Answer_XML> currentAnswers;

	private Dialogue dialoguePanel;
	private string npcId;

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

				// Lets find our canvas that we must add ourselves to
				GameObject guiCanvas = GameObject.Find ("_GUI");				

				// Now that we have created the Base object we need to add our HUD components to it
				GameObject go = (GameObject) Instantiate(Resources.Load ("GUI/DialoguePanel"), new Vector3(), Quaternion.identity);
				go.transform.SetParent(guiCanvas.transform);
				go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

				_instance.dialoguePanel = go.GetComponent<Dialogue> ();
				_instance.dialoguePanel.AnswerSelectionEvent += _instance.AnswerSelection;

			}

			return _instance;
		}
	}

	public void BeginDialogue(string npcId) {
		this.npcId = npcId;

		NPC_XML dialogue = NPC_Collection.Instance.GetNPC(npcId);
		_instance.UpdateDialogue(GetFirstPassingCondition(dialogue.conditions));
		_instance.dialoguePanel.Launch ();
	}

	public void EndDialogue() {

		if (OnDialogueComplete != null) {
			OnDialogueComplete ();
		}

		_gm.SetGameState (GameState.Game);
	}

	private void UpdateDialogue(NPC_Condition_XML trueCondition) {
		currentAnswers = trueCondition.question.answers;

		List<Dialogue_Message_XML> tempAnswers = new List<Dialogue_Message_XML> ();
		foreach (NPC_Answer_XML a in currentAnswers) {
			tempAnswers.Add (Dialogue_Collection.Instance.GetAnswer(a.id));
		}

		dialoguePanel.UpdateMessages (Dialogue_Collection.Instance.GetQuestion(trueCondition.question.id), tempAnswers);
	}

	private NPC_Condition_XML GetFirstPassingCondition(List<NPC_Condition_XML> conditions) {
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

	private void AnswerSelection(int index) {
		Debug.Log ("Received Answer Selection: " + index);
		Debug.Log (currentAnswers.Count);
		NPC_Answer_XML a = currentAnswers [index];

		NPC_Condition_XML c = GetFirstPassingCondition (a.conditions);
		if (c != null) {
			UpdateDialogue (c);
		} else {
			dialoguePanel.Close ();
			EndDialogue ();
		}
	}
}
