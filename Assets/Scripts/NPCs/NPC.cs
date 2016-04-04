using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public string id;

	private bool talkWhenWithinRange = false;

	private void Start() {
        // Inform the NPC Manager of my existence
        NPCManager.Instance.addNPC(id, this);

        TalkWhenWithinRange(); //TODO: Remove - for testing only!
    }

	public void TalkWhenWithinRange() {
		talkWhenWithinRange = true;
	}

	public void CancelTalking() {
		talkWhenWithinRange = false;
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == GameManager.PLAYER_TAG && talkWhenWithinRange) {
			//this.transform.LookAt (collider.transform); //3D only
			GameManager.Instance.SetGameState (GameState.Dialogue);
			DialogueManager.Instance.BeginDialogue (id);
		}
	}
}
