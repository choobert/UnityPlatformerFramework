using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Dialogue")]
public class Dialogue_Collection {

	[XmlArray("Questions")]
	[XmlArrayItem("Question")]
	public static List<Dialogue_Message_XML> questions = new List<Dialogue_Message_XML> ();

	[XmlArray("Answers")]
	[XmlArrayItem("Answer")]
	public static List<Dialogue_Message_XML> answers = new List<Dialogue_Message_XML> ();

	public static Dialogue_Collection Load(string path) {
		TextAsset xml = Resources.Load<TextAsset> (path);
		XmlSerializer serializer = new XmlSerializer (typeof(Dialogue_Collection));
		StringReader reader = new StringReader (xml.text);
		Dialogue_Collection dialogues = serializer.Deserialize (reader) as Dialogue_Collection;
		reader.Close ();
		return dialogues;
	}

	public static Dictionary<string, Dialogue_Message_XML> GetQuestionsDictionary(string path) {
		if (questions.Count < 1) {
			Load (path);
		}

		return ConvertToDictionary (questions);
	}

	public static Dictionary<string, Dialogue_Message_XML> GetAnswersDictionary(string path) {
		if (answers.Count < 1) {
			Load (path);
		}

		return ConvertToDictionary (answers);
	}

	private static Dictionary<string, Dialogue_Message_XML> ConvertToDictionary(List<Dialogue_Message_XML> messages) {
		Dictionary<string, Dialogue_Message_XML> dict = new Dictionary<string, Dialogue_Message_XML> ();
		foreach(Dialogue_Message_XML m in messages) {
			dict.Add (m.id, m);
		}
		return dict;
	}
}
