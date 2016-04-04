using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Dialogue")]
public class Dialogue_Collection {

	private static Dialogue_Collection _instance;

	private const string DIALOGUE_XML_PATH = "GameData/Dialogue";

	[XmlArray("Questions")]
	[XmlArrayItem("Question")]
	public List<Dialogue_Message_XML> questions = new List<Dialogue_Message_XML> (); // must be public

	[XmlArray("Answers")]
	[XmlArrayItem("Answer")]
	public List<Dialogue_Message_XML> answers = new List<Dialogue_Message_XML> (); // must be public

	private Dictionary<string, Dialogue_Message_XML> questionDictionary;  
	private Dictionary<string, Dialogue_Message_XML> answerDictionary;


	/*
	* Find/Create/Return our one and only Dialogue Collection
	* for the game
	**/
	public static Dialogue_Collection Instance
	{
		get
		{
			// If not exists, read in the NPC Collection XML
			if (_instance == null)
			{
				_instance = Load();
				_instance.CreateDictionaries ();
			}

			return _instance;
		}
	}

	public Dialogue_Message_XML GetQuestion(string questionId) {
		return questionDictionary [questionId];
	}

	public Dialogue_Message_XML GetAnswer(string answerId) {
		return answerDictionary [answerId];
	}

	private static Dialogue_Collection Load() {
		TextAsset xml = Resources.Load<TextAsset> (DIALOGUE_XML_PATH);
		XmlSerializer serializer = new XmlSerializer (typeof(Dialogue_Collection));
		StringReader reader = new StringReader (xml.text);
		Dialogue_Collection dialogues = serializer.Deserialize (reader) as Dialogue_Collection;
		reader.Close ();
		return dialogues;
	}

	private void CreateDictionaries() {
		questionDictionary = new Dictionary<string, Dialogue_Message_XML> ();
		foreach(Dialogue_Message_XML m in questions) {
			questionDictionary.Add (m.id, m);
		}

		answerDictionary = new Dictionary<string, Dialogue_Message_XML> ();
		foreach(Dialogue_Message_XML m in answers) {
			answerDictionary.Add (m.id, m);
		}
	}
}
