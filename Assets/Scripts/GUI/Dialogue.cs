using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

	public delegate void AnswerSelection (int answerIndex);
	public event AnswerSelection AnswerSelectionEvent;

	public GameObject panel;
	public Text question;
	public Text answer0;
	public Text answer1;
	public Text answer2;

	private Dialogue_Message_XML currentQuestionMessage;
	private List<Dialogue_Message_XML> currentAnswerMessageList;

	private EventSystem eventSystem;

	private void Start() {
		eventSystem = Object.FindObjectOfType<EventSystem> ();
	}

	public void Launch() {
		panel.SetActive (true);
	}

	public void Close() {
		panel.SetActive (false);
	}

	public void UpdateMessages(Dialogue_Message_XML question, List<Dialogue_Message_XML> answers) {
		currentQuestionMessage = question;
		currentAnswerMessageList = answers;

		this.question.text = currentQuestionMessage.text;

		if (currentAnswerMessageList.Count == 0) {
			Debug.LogError ("No answers associated with question: " + question.id);
		} else {
			answer0.text = currentAnswerMessageList [0].text;

			// disable the other message buttons (they will be re-enabled if they are used)
			answer1.text = "";
			answer1.enabled = false;
			answer2.text = "";
			answer2.enabled = false;

			if (currentAnswerMessageList.Count > 1) {
				answer1.text = currentAnswerMessageList [1].text;
				answer1.enabled = true;
			}

			if (currentAnswerMessageList.Count > 2) {
				answer2.text = currentAnswerMessageList [2].text;
				answer2.enabled = true;
			}

			if (currentAnswerMessageList.Count > 3) {
				Debug.LogWarning ("More than 3 answers supplied. Some answers will be lost associated with question: " + question.id);
			}
		}
	}

	public void ClickAnswer(int index) {

		eventSystem.SetSelectedGameObject (null);

		if (AnswerSelectionEvent != null) {
			AnswerSelectionEvent (index);
		}
	}
}
