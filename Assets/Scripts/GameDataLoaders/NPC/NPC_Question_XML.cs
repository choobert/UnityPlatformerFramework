using System.Collections.Generic;
using System.Xml.Serialization;

public class NPC_Question_XML {

	[XmlAttribute("id")]
	public string id;

	[XmlElement("Answer")]
	public List<NPC_Answer_XML> answers;
}
