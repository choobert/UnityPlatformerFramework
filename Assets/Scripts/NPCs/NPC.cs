using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public string id;

	private bool talkWhenWithinRange = false;

	//TODO: Remove - for testing only!
	private void Start() {
		TalkWhenWithinRange ();
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
