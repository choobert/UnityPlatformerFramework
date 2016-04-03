using System.Collections.Generic;
using System.Xml.Serialization;

public class NPC_XML {

	[XmlAttribute("id")]
	public string id;

	[XmlElement("Condition")]
	public List<NPC_Condition_XML> conditions;
}
