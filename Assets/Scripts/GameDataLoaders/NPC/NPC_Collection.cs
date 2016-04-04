using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("NPCCollection")]
public class NPC_Collection {

	private static NPC_Collection _instance;

	private const string NPCS_XML_PATH = "GameData/NPC";

	[XmlArray("NPCs")]
	[XmlArrayItem("NPC")]
	public List<NPC_XML> npcs = new List<NPC_XML>(); // must be public

	private Dictionary<string, NPC_XML> npcDictionary;

	public static NPC_Collection Instance {
		get {
			if (_instance == null) {
				_instance = Load ();
				_instance.CreateDictionary ();
			}

			return _instance;
		}
	}

	public NPC_XML GetNPC(string npcId) {
		if(npcDictionary.ContainsKey(npcId)) {
			return npcDictionary[npcId];
		}
		return null;
	}

	private static NPC_Collection Load() {
		TextAsset xml = Resources.Load<TextAsset> (NPCS_XML_PATH);
		XmlSerializer serializer = new XmlSerializer (typeof(NPC_Collection));
		StringReader reader = new StringReader (xml.text);
		NPC_Collection items = serializer.Deserialize (reader) as NPC_Collection;
		reader.Close ();
		return items;
	}

	private void CreateDictionary() {
		npcDictionary = new Dictionary<string, NPC_XML> ();
		foreach(NPC_XML n in npcs) {
			npcDictionary.Add (n.id, n);
		}
	}
}