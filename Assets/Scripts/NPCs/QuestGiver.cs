using UnityEngine;
using System.Collections;

public class QuestGiver : NPC {

	private bool isQuestAvail = true;
	private bool isQuestStarted = false;
	private bool isQuestComplete = false;

	public bool IsQuestAvail() {
		return isQuestAvail;
	}

	public bool IsQuestStarted() {
		return isQuestStarted;
	}

	public bool IsQuestComplete() {
		if (!isQuestComplete) {
			evaluateComplete ();
		}

		return isQuestComplete;
	}

	public void StartQuest() {
		isQuestAvail = false;
		isQuestStarted = true;
	}

	protected virtual void evaluateComplete() {
		isQuestStarted = false;
		isQuestComplete = true;
	}
}
