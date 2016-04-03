using System.Collections.Generic;
using System.Xml.Serialization;

public class NPC_Answer_XML {

	[XmlAttribute("id")]
	public string id;

	[XmlElement("action")]
	public string action;

	[XmlElement("Condition")]
	public List<NPC_Condition_XML> conditions;
}
