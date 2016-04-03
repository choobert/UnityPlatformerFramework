using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("NPCCollection")]
public class NPC_Collection {

	[XmlArray("NPCs")]
	[XmlArrayItem("NPC")]
	public static List<NPC_XML> npcs = new List<NPC_XML> ();

	public static NPC_Collection Load(string path) {
		TextAsset xml = Resources.Load<TextAsset> (path);
		XmlSerializer serializer = new XmlSerializer (typeof(NPC_Collection));
		StringReader reader = new StringReader (xml.text);
		NPC_Collection items = serializer.Deserialize (reader) as NPC_Collection;
		reader.Close ();
		return items;
	}

	public static Dictionary<string, NPC_XML> GetDictionary(string path) {
		if (npcs.Count < 1) {
			Load (path);
		}

		Dictionary<string, NPC_XML> dict = new Dictionary<string, NPC_XML> ();
		foreach(NPC_XML n in npcs) {
			dict.Add (n.id, n);
		}
		return dict;
	}
}
