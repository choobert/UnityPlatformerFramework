using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnDialogueCompleteHandler();

public class DialogueManager : MonoBehaviour {

	private static DialogueManager _instance;
	private static GameManager _gm;

	private const string NPCS_XML_PATH = "GameData/NPCs";
	private const string DIALOGUE_XML_PATH = "GameData/Dialogue";

	/*
	private Dictionary<ConditionType, Condition> conditionDictionary;
	private Dictionary<string, NPC_XML> npcDictionary;
	private Dictionary<string, MessageXML> questionsDictionary;
	private Dictionary<string, MessageXML> answersDictionary;

	private List<AnswerXML> currentAnswers;

	private DialoguePanel dialoguePanel;
	*/

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

				// Set up the listeners to the GameManager events
				_gm.OnStateChange += _instance.OnStateChange;
			}

			return _instance;
		}
	}

	private void OnStateChange() {
		if (GameManager.Instance.gameState == GameState.Dialogue) {
			BeginDialogue ();
		}
	}

	private void BeginDialogue() {
	}
}
